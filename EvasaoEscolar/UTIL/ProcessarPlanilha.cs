using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;




namespace EvasaoEscolar.UTIL
{
    public class ProcessarPlanilha
    {
        private IBaseRepository<AlunoDomain> _alunoRepository;
        private IBaseRepository<PlanilhaDadosDomain> _planilhaDadosRepository;
        private IBaseRepository<UploadPlanilhaDomain> _uploadplanilhaRepository;
        private IBaseRepository<DisciplinaTurmaDomain> _disciplinaturmaRepository;


        public ProcessarPlanilha()
        {

        }

        public ProcessarPlanilha(IBaseRepository<AlunoDomain> alunoRepository, IBaseRepository<PlanilhaDadosDomain> planilhaDadosRepository,
        IBaseRepository<DisciplinaTurmaDomain> disciplinaturmaRepository, IBaseRepository<UploadPlanilhaDomain> uploadplanilhaRepository)
        {
            _alunoRepository = alunoRepository;
            _planilhaDadosRepository = planilhaDadosRepository;
            _disciplinaturmaRepository = disciplinaturmaRepository;
            _uploadplanilhaRepository = uploadplanilhaRepository;
        }


        public string ProcessandoPlanilha(IFormFile arquivo, int turmaDoFront, int disciplinaDoFront, DateTime dataCorrespondente)
        {

            var data = new MemoryStream();

            arquivo.CopyTo(data);

            data.Seek(0, SeekOrigin.Begin);

            var buf = new byte[data.Length];
            data.Read(buf, 0, buf.Length);

            IExcelDataReader reader = null;

            if (arquivo.FileName.EndsWith(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(data);
            }
            else if (arquivo.FileName.EndsWith(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(data);
            }

            reader.IsFirstRowAsColumnNames = false;

            DataSet result = reader.AsDataSet();
            reader.Close();

            //Valores inseridos para teste
            turmaDoFront = 2;
            disciplinaDoFront = 2;
            dataCorrespondente = DateTime.Today;

            for (int i = 2; i < result.Tables[0].Rows.Count; i++)
            {
                //instanciando objetos
                AlunoDomain aluno = new AlunoDomain();
                PlanilhaDadosDomain planilhaDados = new PlanilhaDadosDomain();
                DisciplinaTurmaDomain disciplinaTurmaObj = new DisciplinaTurmaDomain();
                UploadPlanilhaDomain uploadPlanilhaObj = new UploadPlanilhaDomain();


                aluno.NomeAluno = result.Tables[0].Rows[i][1].ToString();
                aluno.Matricula = result.Tables[0].Rows[i][5].ToString();
                var status = result.Tables[0].Rows[i][6].ToString();

                planilhaDados.DadosProcessados = true;

                planilhaDados.Aula1Planilha = result.Tables[0].Rows[i][7].ToString();
                planilhaDados.Aula2Planilha = result.Tables[0].Rows[i][8].ToString();
                planilhaDados.Aula3Planilha = result.Tables[0].Rows[i][9].ToString();
                planilhaDados.Aula4Planilha = result.Tables[0].Rows[i][10].ToString();
                planilhaDados.Aula5Planilha = result.Tables[0].Rows[i][11].ToString();



                //teste para popular tabela DisciplinaTurma
                disciplinaTurmaObj.DisciplinaId = disciplinaDoFront;
                disciplinaTurmaObj.TurmaId = turmaDoFront;

                //teste para popular tabela UploadPlanilha                   
                //_disciplinaturmaRepository.Listar().Where(c=> c.TurmaId == turmaDoFront && c.DisciplinaId == disciplinaDoFront);  

                int DisciplinaTurmaIdOBTIDA = 1;
                uploadPlanilhaObj.DataReferenciaPlanilha = dataCorrespondente;
                uploadPlanilhaObj.DataUploadPlanilha = DateTime.Now;
                uploadPlanilhaObj.DisciplinaTurmaId = DisciplinaTurmaIdOBTIDA;



                if (status == "ativo")
                {
                    aluno.StatusAlunoEvadiu = true;
                }
                else
                {
                    aluno.StatusAlunoEvadiu = false;
                }
                _alunoRepository.Inserir(aluno);
                _planilhaDadosRepository.Inserir(planilhaDados);
                _disciplinaturmaRepository.Inserir(disciplinaTurmaObj);
                _uploadplanilhaRepository.Inserir(uploadPlanilhaObj);
            }


            string retorno = "Upload Concluido";

            return retorno;

        }



    }
}
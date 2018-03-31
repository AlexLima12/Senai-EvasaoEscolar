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
using System.Linq;

namespace EvasaoEscolar.UTIL
{
    public class ProcessarPlanilha
    {
        // private IBaseRepository<AlunoDomain> _alunoRepository;
        // private IBaseRepository<PlanilhaDadosDomain> _planilhaDadosRepository;
        // private IBaseRepository<UploadPlanilhaDomain> _uploadplanilhaRepository;
        // private IBaseRepository<DisciplinaTurmaDomain> _disciplinaturmaRepository;

        public ProcessarPlanilha()
        {

        }

        // public ProcessarPlanilha(IBaseRepository<AlunoDomain> alunoRepository, IBaseRepository<PlanilhaDadosDomain> planilhaDadosRepository,
        // IBaseRepository<DisciplinaTurmaDomain> disciplinaturmaRepository, IBaseRepository<UploadPlanilhaDomain> uploadplanilhaRepository)
        // {
        //     _alunoRepository = alunoRepository;
        //     _planilhaDadosRepository = planilhaDadosRepository;
        //     _disciplinaturmaRepository = disciplinaturmaRepository;
        //     _uploadplanilhaRepository = uploadplanilhaRepository;
        // }

        public string ProcessandoPlanilha(IFormFile arquivo, int turmaDoFront, int disciplinaDoFront, DateTime dataCorrespondente,
         IBaseRepository<AlunoDomain> _alunoRepository, IBaseRepository<PlanilhaDadosDomain> _planilhaDadosRepository,
          IBaseRepository<DisciplinaTurmaDomain> _disciplinaturmaRepository, IBaseRepository<UploadPlanilhaDomain> _uploadplanilhaRepository,
          IBaseRepository<AlunoDisciplinaTurmaDomain> _alunoDisciplinaTurmaRepository)
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



            //Valores inseridos para teste, pois esses valores devem ser recebidos do front
            turmaDoFront = 1;
            disciplinaDoFront = 3;
            dataCorrespondente = DateTime.Today;

            DisciplinaTurmaDomain disciplinaTurmaObj = new DisciplinaTurmaDomain();
            UploadPlanilhaDomain uploadPlanilhaObj = new UploadPlanilhaDomain();

            //popular tabela DisciplinaTurma
            disciplinaTurmaObj.DisciplinaId = disciplinaDoFront;
            disciplinaTurmaObj.TurmaId = turmaDoFront;
            var disciplinasEturmas = _disciplinaturmaRepository.Listar();
           // .Where(x => x.DisciplinaId == disciplinaDoFront && x.TurmaId == turmaDoFront);
            

            foreach (var item in disciplinasEturmas)
            {
                
            string condicao;

                if (item.DisciplinaId == disciplinaDoFront && item.TurmaId == turmaDoFront){
                
                    condicao = "true";
                    }
                    else {
                        condicao = "false";
                    }
                    

               switch (condicao)
               {
                   case "true":
                   break;

                   case "false":
                       _disciplinaturmaRepository.Inserir(disciplinaTurmaObj);
                       break;                   
               }

            //     if (condicao != false)
            //     {
            //         _disciplinaturmaRepository.Inserir(disciplinaTurmaObj);
            //     }
               
             }

           

            //popular tabela UploadPlanilha 
            uploadPlanilhaObj.DataReferenciaPlanilha = dataCorrespondente;
            uploadPlanilhaObj.DataUploadPlanilha = DateTime.Now;
            uploadPlanilhaObj.DisciplinaTurmaId = disciplinaTurmaObj.Id;
            //_disciplinaturmaRepository.Listar().Where(c=> c.TurmaId == turmaDoFront && c.DisciplinaId == disciplinaDoFront); 

            _uploadplanilhaRepository.Inserir(uploadPlanilhaObj);

            //For para percorrer as linhas da planilha
            for (int i = 2; i < result.Tables[0].Rows.Count; i++)
            {
                //instanciando objetos
                AlunoDomain alunoObj = new AlunoDomain();
                PlanilhaDadosDomain planilhaDadosObj = new PlanilhaDadosDomain();
                AlunoDisciplinaTurmaDomain alunoDisciplinaTurmaObj = new AlunoDisciplinaTurmaDomain();

                alunoObj.NomeAluno = result.Tables[0].Rows[i][1].ToString();
                alunoObj.Matricula = result.Tables[0].Rows[i][5].ToString();
                var status = result.Tables[0].Rows[i][6].ToString();

                planilhaDadosObj.DadosProcessados = true;

                planilhaDadosObj.Aula1Planilha = result.Tables[0].Rows[i][7].ToString();
                planilhaDadosObj.Aula2Planilha = result.Tables[0].Rows[i][8].ToString();
                planilhaDadosObj.Aula3Planilha = result.Tables[0].Rows[i][9].ToString();
                planilhaDadosObj.Aula4Planilha = result.Tables[0].Rows[i][10].ToString();
                planilhaDadosObj.Aula5Planilha = result.Tables[0].Rows[i][11].ToString();

                if (status == "ativo")
                {
                    alunoObj.StatusAlunoEvadiu = false;
                }
                else
                {
                    alunoObj.StatusAlunoEvadiu = true;
                }


                //IF para pular vazios
                if (alunoObj.Matricula != "")
                {
                    //inserir aluno vindo da planilha            
                    _alunoRepository.Inserir(alunoObj);

                    //Pega o Id e nome do aluno para inserir no objeto planilhaDados
                    planilhaDadosObj.AlunoId = alunoObj.Id;
                    planilhaDadosObj.NomeAluno = alunoObj.NomeAluno;
                    planilhaDadosObj.UploadPlanilhaId = uploadPlanilhaObj.Id;

                    //Insere os dados na tabela PlanilhaDados
                    _planilhaDadosRepository.Inserir(planilhaDadosObj);

                    //AlunoDisciplinaTurma vai dentro do FOR
                    alunoDisciplinaTurmaObj.AlunoId = alunoObj.Id;
                    alunoDisciplinaTurmaObj.DisciplinaTurmaId = disciplinaTurmaObj.Id;
                    _alunoDisciplinaTurmaRepository.Inserir(alunoDisciplinaTurmaObj);
                }

                else
                {
                }
            }



            string retorno = "Upload Concluido";
            return retorno;
        }
    }
}
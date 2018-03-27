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
        
        
        public ProcessarPlanilha()
        {

        }

        //descomnetar 
        // public ProcessarPlanilha(IFormFile file, IBaseRepository<AlunoDomain> alunoRepository)
        // {
        // manter comentario  //   _alunoRepository = alunoRepository;
        // }



        public string ProcessandoPlanilha(IFormFile arquivo, IBaseRepository<AlunoDomain> alunoRepository, IBaseRepository<PlanilhaDadosDomain> planilhaDadosRepository)
        {
            _alunoRepository = alunoRepository;
            _planilhaDadosRepository = planilhaDadosRepository;

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



            for (int i = 2; i < result.Tables[0].Rows.Count; i++)
            {

                AlunoDomain aluno = new AlunoDomain();
                PlanilhaDadosDomain planilhaDados = new PlanilhaDadosDomain();
                UploadPlanilhaDomain uploadPlanilha = new UploadPlanilhaDomain();


                aluno.NomeAluno = result.Tables[0].Rows[i][1].ToString();
                aluno.Matricula = result.Tables[0].Rows[i][5].ToString();
                var status = result.Tables[0].Rows[i][6].ToString();

                planilhaDados.DadosProcessados = true;
              
                planilhaDados.Aula1Planilha = result.Tables[0].Rows[i][7].ToString();
                planilhaDados.Aula2Planilha = result.Tables[0].Rows[i][8].ToString();
                planilhaDados.Aula3Planilha = result.Tables[0].Rows[i][9].ToString();
                planilhaDados.Aula4Planilha = result.Tables[0].Rows[i][10].ToString();
                planilhaDados.Aula5Planilha = result.Tables[0].Rows[i][11].ToString();



                if (status == "ativo")
                {
                    aluno.StatusAlunoEvadiu = true;
                }
                else
                {
                    aluno.StatusAlunoEvadiu = false;
                }


                if (aluno == null)
                {

                }
                else
                {
                    _alunoRepository.Inserir(aluno);

                    //var ultimoAluno = _alunoRepository.Listar().FirstOrDefault(c => c.Matricula == aluno.Matricula);

                    planilhaDados.UploadPlanilhaId = 1;
                  // Usar este quando o front estiver me enviando os dados do UPLOAD  planilhaDados.UploadPlanilhaId = uploadPlanilha.Id;
                    planilhaDados.AlunoId = aluno.Id;
                    _planilhaDadosRepository.Inserir(planilhaDados);


                }


            }


            //   aluno = contexto.Aluno.FirstOrDefault(x => x.matricula);


            string retorno = arquivo.FileName;

            return retorno;

        }



    }
}
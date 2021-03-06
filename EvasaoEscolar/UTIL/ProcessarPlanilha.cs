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
        
        public ProcessarPlanilha()
        {

        }
       

        public string ProcessandoPlanilha(IFormFile arquivo, int turmaDoFront, int disciplinaDoFront, DateTime dataCorrespondente,
         IBaseRepository<AlunoDomain> _alunoRepository, IBaseRepository<PlanilhaDadosDomain> _planilhaDadosRepository,
          IBaseRepository<DisciplinaTurmaDomain> _disciplinaturmaRepository, IBaseRepository<UploadPlanilhaDomain> _uploadplanilhaRepository,
          IBaseRepository<AlunoDisciplinaTurmaDomain> _alunoDisciplinaTurmaRepository, IBaseRepository<FrequenciaDomain> _frequenciaRepository, IBaseRepository<AlertasDomain> alertasRepository)
        {
            //Receber arquivodo excel via Controller UPLOAD
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
            turmaDoFront = 3;
            disciplinaDoFront = 1;
            dataCorrespondente = DateTime.Today;

            DisciplinaTurmaDomain disciplinaTurmaObj = new DisciplinaTurmaDomain();
            UploadPlanilhaDomain uploadPlanilhaObj = new UploadPlanilhaDomain();

            bool condicao;

            //popular tabela DisciplinaTurma
            disciplinaTurmaObj.DisciplinaId = disciplinaDoFront;
            disciplinaTurmaObj.TurmaId = turmaDoFront;
            var disciplinasEturmas = _disciplinaturmaRepository.Listar().Where(x => x.DisciplinaId == disciplinaDoFront && x.TurmaId == turmaDoFront);

            if (disciplinasEturmas.Count() == 0)
            {
                //Quando o objeto a seguir é inserido, eu posso pegar o ID para o último insert
                _disciplinaturmaRepository.Inserir(disciplinaTurmaObj);

                //popular tabela UploadPlanilha caso ainda NÃO exista Disciplia e Turma gravadas no banco
                uploadPlanilhaObj.DataReferenciaPlanilha = dataCorrespondente;
                uploadPlanilhaObj.DataUploadPlanilha = DateTime.Now;
                uploadPlanilhaObj.DisciplinaTurmaId = disciplinaTurmaObj.Id;
                _uploadplanilhaRepository.Inserir(uploadPlanilhaObj);
                condicao = true;
            }


            else
            {
                //popular tabela UploadPlanilha caso já exista Disciplia e turma gravadas no banco
                uploadPlanilhaObj.DataReferenciaPlanilha = dataCorrespondente;
                uploadPlanilhaObj.DataUploadPlanilha = DateTime.Now;

                foreach (var item in disciplinasEturmas)
                {
                    int idObtida;
                    idObtida = item.Id;
                    uploadPlanilhaObj.DisciplinaTurmaId = idObtida;
                    _uploadplanilhaRepository.Inserir(uploadPlanilhaObj);

                }
                condicao = false;
            }


            //For para percorrer as linhas da planilha
            for (int i = 2; i < result.Tables[0].Rows.Count; i++)
            {
                //instanciando objetos
                AlunoDomain alunoObj = new AlunoDomain();
                PlanilhaDadosDomain planilhaDadosObj = new PlanilhaDadosDomain();
                AlunoDisciplinaTurmaDomain alunoDisciplinaTurmaObj = new AlunoDisciplinaTurmaDomain();
                FrequenciaDomain frequenciaObj = new FrequenciaDomain();

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

                //Consulta para VERIFICAR SE JÁ EXISTE ALUNO CADASTRADO POR MATRÍCULA                        
                var alunosCadastrados = _alunoRepository.Listar().Where(x => x.Matricula == alunoObj.Matricula);

                int alunodisciplinaturmaIDConsulta = 0;
                int alunoIDConsulta = 0;

                foreach (var item in alunosCadastrados)
                {
                    alunoIDConsulta = item.Id;
                }

                var alunoDisciplinaTurmaCadastrados = _alunoDisciplinaTurmaRepository.Listar().Where(x => x.AlunoId == alunoIDConsulta);
                foreach (var item in alunoDisciplinaTurmaCadastrados)
                {
                    alunodisciplinaturmaIDConsulta = item.Id;
                }

                if (alunosCadastrados.Count() == 0)
                {

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

                        // só inserir o objeto a seguir quando aluno não tiver cadastrado ainda                            
                        // inserir alunoDisciplinaTurma no banco
                        if (condicao == true)
                        {
                            alunoDisciplinaTurmaObj.AlunoId = alunoObj.Id;
                            alunoDisciplinaTurmaObj.DisciplinaTurmaId = disciplinaTurmaObj.Id;
                            _alunoDisciplinaTurmaRepository.Inserir(alunoDisciplinaTurmaObj);
                        }

                        else
                        {
                            foreach (var item1 in disciplinasEturmas)
                            {
                                int idObtida;
                                idObtida = item1.Id;
                                alunoDisciplinaTurmaObj.AlunoId = alunoObj.Id;
                                alunoDisciplinaTurmaObj.DisciplinaTurmaId = idObtida;
                                _alunoDisciplinaTurmaRepository.Inserir(alunoDisciplinaTurmaObj);

                            }

                        }
                        //Popular tabela frequência:                       

                        //Foreach para criar array com dados de falta e frequêmcia de cada aluno
                        int faltas = 0;
                        var faltasPorAluno = _planilhaDadosRepository.Listar().Where(x => x.AlunoId == alunoObj.Id);

                        foreach (var item in faltasPorAluno)
                        {
                            string[] faltasArray = new string[5] { item.Aula1Planilha, item.Aula2Planilha, item.Aula3Planilha, item.Aula4Planilha, item.Aula5Planilha };
                            faltas = faltasArray.Where(e => e.ToLower() == "f").Count();
                        }

                        frequenciaObj.AlunoDisciplinaTurmaId = alunoDisciplinaTurmaObj.Id;
                        frequenciaObj.Falta = faltas;
                        frequenciaObj.Dias = 0;
                        frequenciaObj.Atraso = 0;
                        frequenciaObj.Presenca = 5 - faltas;
                        frequenciaObj.NumeroDeAulas = 5;

                        // // if para verificar se aluno já esta cadastrado na TABELA FREQUENCIA
                        // var consultaFrequencia = _frequenciaRepository.Listar().Where(x => x.AlunoDisciplinaTurmaId == alunoDisciplinaTurmaObj.Id);
                        // if (alunosCadastrados.Count() == 0)
                        // {
                        //     //insert tbl_frequencia
                        _frequenciaRepository.Inserir(frequenciaObj);
                        // }

                        //     else
                        //     {
                        //         //update tbl_frequencia

                        //     }
                    }

                    else
                    {
                    }


                }
                else
                {

                    //Else quando o aluno já está cadastrado

                    //Popular tabela planilha dados
                    planilhaDadosObj.AlunoId = alunoIDConsulta;
                    planilhaDadosObj.NomeAluno = alunoObj.NomeAluno;
                    planilhaDadosObj.UploadPlanilhaId = uploadPlanilhaObj.Id;
                    _planilhaDadosRepository.Inserir(planilhaDadosObj);


                    //Popular tabela frequência:                       

                    #region Foreach criar array
                    //Foreach para criar array com dados de falta e frequêmcia de cada aluno
                    int faltass = 0;
                    var faltasPorAlunoss = _planilhaDadosRepository.Listar().Where(x => x.Id == planilhaDadosObj.Id);

                    foreach (var item in faltasPorAlunoss)
                    {
                        string[] faltasArray = new string[5] { item.Aula1Planilha, item.Aula2Planilha, item.Aula3Planilha, item.Aula4Planilha, item.Aula5Planilha };
                        faltass = faltasArray.Where(e => e.ToLower() == "f").Count();
                    }

                    // frequenciaObj.AlunoDisciplinaTurmaId = alunodisciplinaturmaIDConsulta;
                    // frequenciaObj.Falta = faltass;
                    // frequenciaObj.Dias = 0;
                    // frequenciaObj.Atraso = 0;
                    frequenciaObj.Presenca = 5 - faltass;
                    // frequenciaObj.NumeroDeAulas = faltass + frequenciaObj.Presenca;

                    #endregion


                    //consulta se já existem frequencia cadastradas para o alunodisciplinaturmaId
                    var frequenciasCadastradas = _frequenciaRepository.Listar().Where(x => x.AlunoDisciplinaTurmaId == alunodisciplinaturmaIDConsulta).FirstOrDefault();

                    frequenciasCadastradas.Falta = frequenciasCadastradas.Falta + faltass;
                    frequenciasCadastradas.NumeroDeAulas = frequenciasCadastradas.NumeroDeAulas + 5;
                    frequenciasCadastradas.Presenca = frequenciasCadastradas.Presenca + frequenciaObj.Presenca;
                    _frequenciaRepository.Atualizar(frequenciasCadastradas);

                    // //popular tabela alertas
                    // AlertasDomain alertasObj = new AlertasDomain();

                    // int qualquer = uploadPlanilhaObj.Id;

                    // //selecionar ultimo upload
                    // var ultima = _uploadplanilhaRepository.Listar().Where(a => a.DisciplinaTurmaId == alunodisciplinaturmaIDConsulta).OrderBy(a => a.DataReferenciaPlanilha).Last();

                    // var faltaAnterior = _planilhaDadosRepository.Listar().Where(x => x.AlunoId == alunoIDConsulta && x.UploadPlanilhaId == ultima.Id);


                    // //CONTINUAR DAQUI
                    // bool houveFalta = false;
                    // foreach (var item in faltaAnterior)
                    // {
                    //     if(item.Aula1Planilha == "f"|| item.Aula2Planilha == "f"){
                    //         houveFalta = true;
                    //     }
                    // }

                    // alertasObj.AlertaAntigo = false;
                    // alertasObj.AlunoId = alunoIDConsulta;
                    // alertasObj.DataAlerta = DateTime.Today;
                    // alertasObj.MensagemAlerta = "Alerta teste";
                    // alertasObj.NivelPrioridade = 1;
                    // alertasObj.OrigemAlerta = 1;

                    // DateTime dia;
                    // dia = DateTime.Today;
                    // dia = dia.AddDays(-1);


                }
            }

            string retorno = "Upload Concluido";
            return retorno;
        }
    }
}
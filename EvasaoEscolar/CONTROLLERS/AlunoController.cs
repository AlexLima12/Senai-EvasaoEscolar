using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using EvasaoEscolar.Enum;
using System.Collections.Generic;

namespace EvasaoEscolar.CONTROLLERS
{

    [Route("api/[controller]")]
    public class AlunoController : Controller
    {
        private IBaseRepository<AlunoDomain> _alunoRepository;
        private IBaseRepository<EscolaDomain> _escolaRepository;
        private IBaseRepository<AlertasDomain> _alertasRepository;


        public AlunoController(IBaseRepository<AlunoDomain> alunoRepository, IBaseRepository<AlertasDomain> alertasRepository, IBaseRepository<EscolaDomain> escolaRepository)
        {

            _alunoRepository = alunoRepository;
            _escolaRepository = escolaRepository;
            _alertasRepository = alertasRepository;
        }

        [HttpGet]
        [Route("todos")]
        public IActionResult Buscar()
        {
            try
            {
                var alunos = _alunoRepository.Listar(new string[]{"clAnotacoes", "clAlunoDisciplinaTurma", "clAlunoDisciplinaTurma.DisciplinaTurma"
                ,"clAlunoDisciplinaTurma.DisciplinaTurma.Turma", "clAlertas"});

          //     alunos.OrderByDescending( c => c.clAlertas.OrderBy(e => e.NivelPrioridade));


                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados. " + ex.Message);
            }
        }



        [HttpGet]
        [Route("todos/mobile")]
        public IActionResult BuscarTodosRetornoMobile()
        {
            try
            {
                IEnumerable<AlunoDomain> alunos = _alunoRepository.Listar(new string[] { "clAnotacoes", "clAlunoDisciplinaTurma.DisciplinaTurma.Turma", "clAlertas" });

                //var retorno = alunos.Where(e => e.StatusAlunoEvadiu == false && (!(e.clAlertas.Select(h => h.AlertaAntigo == false).Any())));

                var retorno = alunos.Where(e => e.StatusAlunoEvadiu == false && (e.clAlertas.Any(r => r.AlertaAntigo == false)))

                .Select(x => new
                {
                    idAluno = x.Id
                    ,
                    matricula = x.Matricula
                    ,
                    nomeAluno = x.NomeAluno
                    ,
                    clAnotacoes = x.clAnotacoes.Select(d => new { idAnotacao = d.Id, mensagemAnotacao = d.Mensagem, idAluno = d.AlunoId }).ToList()
                    ,
                    clAlertas = x.clAlertas
                        .Where(d => d.AlertaAntigo == false)
                        .Select(c => new { nivelPrioridade = c.NivelPrioridade, mensagemAlerta = c.MensagemAlerta, origemAlerta = c.OrigemAlerta, dataAlerta = c.DataAlerta }).ToList()
                    ,
                    turmas = x.clAlunoDisciplinaTurma.Select(a => new { turma = a.DisciplinaTurma.Turma.NomeTurma, statusTurma = a.DisciplinaTurma.Turma.StatusTurma }).ToList()
                });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados. " + ex.Message);
            }
        }


        [HttpGet]
        [Route("buscarid/{Id}")]
        public IActionResult BuscarPorId(int Id)
        {
            try
            {
                //var aluno = _alunoRepository.BuscarPorId(Id);

                var alunos = _alunoRepository.BuscarPorId(Id, new string[]{"clAnotacoes", "clAlunoDisciplinaTurma", "clAlunoDisciplinaTurma.DisciplinaTurma"
                ,"clAlunoDisciplinaTurma.DisciplinaTurma.Turma", "clAlertas"});





                //   var alunos = _alunoRepository.Listar(new string[]{"clAnotacoes", "clAlunoDisciplinaTurma", "clAlunoDisciplinaTurma.DisciplinaTurma"
                //     ,"clAlunoDisciplinaTurma.DisciplinaTurma.Turma", "clAlertas"}).Where(x => x.Id == Id);


                //com relacionamento


                //   var relacionamentos = _alunoRepository.Listar(new string[]{"Permissao"}).Where( c=> c.UsuarioId == Id);

                //  foreach (var item in relacionamentos)
                //  {
                //     aluno.ClPermissoesUsuarios.Add(item);
                // }


                //

                if (alunos != null)
                    return Ok(alunos);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar os dados solictidados. " + ex.Message);
            }
        }


    }
}
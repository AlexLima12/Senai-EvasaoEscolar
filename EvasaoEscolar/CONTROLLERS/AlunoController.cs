using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using EvasaoEscolar.Enum;

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

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados. " + ex.Message);
            }
        }



        [HttpGet]
        [Route("todosOrdenado")]
        public IActionResult BuscarOrdenado()
        {
            try
            {
                var alunos = _alunoRepository.Listar(new string[]{"clAnotacoes", "clAlunoDisciplinaTurma", "clAlunoDisciplinaTurma.DisciplinaTurma"
                ,"clAlunoDisciplinaTurma.DisciplinaTurma.Turma", "clAlertas"});
                
                var alertasOrdenadas = alunos.OrderBy(x => x.clAlertas.Last().DataAlerta);

                return Ok(alertasOrdenadas);
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
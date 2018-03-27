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
    public class AlertasController : Controller
    {
        private IBaseRepository<AlunoDomain> _alunoRepository;
        private IBaseRepository<AlertasDomain> _alertaRepository;

        public AlertasController(IBaseRepository<AlunoDomain> alunoRepository, IBaseRepository<AlertasDomain> alertaRepository)
        {
            _alunoRepository = alunoRepository;
            _alertaRepository = alertaRepository;
        }

        [HttpGet]
        [Route("todos")]
        public IActionResult Buscar()
        {
            try
            {
                var alertas = _alertaRepository.Listar(new string[]{"Aluno"}).OrderBy( x=> x.DataAlerta);

                    // "clAlunoDisciplinaTurma", "clAlunoDisciplinaTurma.DisciplinaTurma" ,"clAlunoDisciplinaTurma.DisciplinaTurma.Turma", "clAlertas"
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados. " + ex.Message);
            }
        }
        
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using EvasaoEscolar.Enum;
//using System.Web.Http.Cors;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace EvasaoEscolar.CONTROLLERS
{
    [Route("api/[controller]")]
     [EnableCors("AllowAnyOrigin")]

    public class TurmaController : Controller

    {


        private IBaseRepository<TurmaDomain> _turmaRepository;


        public TurmaController(IBaseRepository<TurmaDomain> turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        [HttpGet]
        [Route("todos")]
       
        public IActionResult Buscar()
        {
            try
            {
                var turmas = _turmaRepository.Listar();

                return Ok(turmas);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados. " + ex.Message);
            }
        }


        		 public IActionResult Cadastrar([FromBody] TurmaDomain turmas){
				 
			if (!ModelState.IsValid)
            return BadRequest(ModelState);

            try
            {
                _turmaRepository.Inserir(turmas);
				return Ok($"id:{turmas.Id}");
          
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar os dados... " + ex.Message);
            }
		 }


    }
}
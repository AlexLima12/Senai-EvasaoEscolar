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
using EvasaoEscolar.CONTEXTO;

namespace EvasaoEscolar.CONTROLLERS
{
    
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    
        public class DisciplinaController : Controller
    {
        private IBaseRepository<DisciplinasDomain> _disciplinaRepository;
        readonly EvasaEscolarContext contexto;

        public DisciplinaController(IBaseRepository<DisciplinasDomain> disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        // [HttpGet]
        // [Route("todos")]
        // public IActionResult Buscar()
        // {
        //     try
        //     {
        //         var disciplinas = _disciplinaRepository.Listar();
        //         return Ok(disciplinas);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest("Erro ao buscar dados. " + ex.Message);
        //     }
        // }

        [HttpGet]
        [Route("todos")]
        public IActionResult Buscar()
        {
            try
            {
                var disciplinas = _disciplinaRepository.Listar();

                return Ok(disciplinas);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados. " + ex.Message);
            }
        }




        public IActionResult Cadastrar([FromBody] DisciplinasDomain disciplinas)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _disciplinaRepository.Inserir(disciplinas);
                return Ok($"id:{disciplinas.Id} cadastrada com sucesso");

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar os dados... " + ex.Message);
            }
        }


         [Route("deletarid/{Id}")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var disciplinas = contexto.DbDisciplinas.Where(i => i.Id == Id).FirstOrDefault();
            if (disciplinas == null)
                return NotFound();

            contexto.DbDisciplinas.Remove(disciplinas);
            int x = contexto.SaveChanges();
            if (x > 0)
            {
                return Ok($"id:{disciplinas.Id}");
            }
            else
            {
                return BadRequest();
            }

        }



    }
}
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

    public class TurmaController : Controller

    {
        private IBaseRepository<TurmaDomain> _turmaRepository;
        readonly EvasaEscolarContext contexto;
        public TurmaController(IBaseRepository<TurmaDomain> turmaRepository, EvasaEscolarContext contexto)
        {
            _turmaRepository = turmaRepository;
            this.contexto = contexto;
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

        public IActionResult Cadastrar([FromBody] TurmaDomain turmas)
        {
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


        [HttpPut("atualizarPorId/{id}/{status}")]
        [Route("atualizarPorId")]
        public IActionResult AtualizarId(int id, bool status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var turma = _turmaRepository.BuscarPorId(id);
                turma.StatusTurma = status;
                _turmaRepository.Atualizar(turma);
                return Ok($"Turma atualizada");

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar os dados... " + ex.Message);
            }
        }

        [Route("deletarid/{Id}")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var turmas = contexto.DbTurmas.Where(i => i.Id == Id).FirstOrDefault();
            if (turmas == null)
                return NotFound();

            contexto.DbTurmas.Remove(turmas);
            int x = contexto.SaveChanges();
            if (x > 0)
            {
                return Ok($"id:{turmas.Id}");
            }
            else
            {
                return BadRequest();
            }

        }


    }
}
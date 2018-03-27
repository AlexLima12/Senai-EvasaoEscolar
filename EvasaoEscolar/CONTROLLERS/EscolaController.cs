using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;

namespace EvasaoEscolar.CONTROLLERS
{
    [Route("api/[controller]")]
    public class EscolaController : Controller
    {
        private IBaseRepository<EscolaDomain> _escolRepository;

        public EscolaController(IBaseRepository<EscolaDomain> escolaRepository)
        {
            _escolRepository = escolaRepository;
        }

        //Cadastrar
        public IActionResult Cadastrar([FromBody] EscolaDomain escola)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _escolRepository.Inserir(escola);
                return Ok($"Escola {escola.NomeEscola} Cadastrado Com Sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar dados. " + ex.Message);
            }
        }

        //listar Todos
        [HttpGet]
        [Route("todos")]
        public IActionResult Buscar()
        {
            try
            {
                return Ok(_escolRepository.Listar());
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
                var escola = _escolRepository.BuscarPorId(Id);
                if (escola != null)
                    return Ok(escola);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar os dados solictidados. " + ex.Message);
            }
        }



        [HttpPut]
        [Route("atualizar")]
        public IActionResult Atualizar([FromBody] EscolaDomain escola)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _escolRepository.Atualizar(escola);
                return Ok($"Escola {escola.NomeEscola} Atualizado Com Sucesso.");

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar os dados... " + ex.Message);
            }
        }


        [Route("deletar")]
        [HttpDelete]
        public IActionResult Deletar([FromBody] EscolaDomain escola)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _escolRepository.Deletar(escola);
                return Ok($"Usuário {escola.NomeEscola} Deletado Com Sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao deletar dados. " + ex.Message);
            }
        }


    }
}
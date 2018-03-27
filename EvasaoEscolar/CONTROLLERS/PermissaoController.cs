using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;

namespace EvasaoEscolar.CONTROLLERS
{
     [Route("api/[controller]")]
    public class PermissaoController : Controller
    {
         private IBaseRepository<PermissaoDomain> _permissaoRepository;

           public PermissaoController(IBaseRepository<PermissaoDomain> permissaoRepository)
        {
            _permissaoRepository = permissaoRepository;
        }

        
        //Cadastrar
        public IActionResult Cadastrar([FromBody] PermissaoDomain permissao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _permissaoRepository.Inserir(permissao);
                return Ok($"Permissao {permissao.NomePermissao} Cadastrado Com Sucesso.");
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
                return Ok(_permissaoRepository.Listar());
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
                var escola = _permissaoRepository.BuscarPorId(Id);
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
        public IActionResult Atualizar([FromBody] PermissaoDomain permissao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _permissaoRepository.Atualizar(permissao);
                return Ok($"Escola {permissao.NomePermissao} Atualizado Com Sucesso.");

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar os dados... " + ex.Message);
            }
        }


         [Route("deletar")]
        [HttpDelete]
        public IActionResult Deletar([FromBody] PermissaoDomain permissao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _permissaoRepository.Deletar(permissao);
                return Ok($"Usuário {permissao.NomePermissao} Deletado Com Sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao deletar dados. " + ex.Message);
            }
        }



    }
}
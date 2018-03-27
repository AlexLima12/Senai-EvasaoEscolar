using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace EvasaoEscolar.CONTROLLERS
{
    [Route("api/[controller]")]
//    [Authorize("Bearer", Roles="Master")]
    public class UsuarioController : Controller
    {
        private IBaseRepository<UsuarioDomain> _usuarioRepository;
        private IBaseRepository<EscolaDomain> _escolaRepository;

        private IBaseRepository<UsuarioPermissaoDomain> _usuariopermissaoRepository;

        public UsuarioController(IBaseRepository<UsuarioDomain> usuarioRepository, IBaseRepository<EscolaDomain> escolaRepository, IBaseRepository<UsuarioPermissaoDomain> usuariopermissaoRepository )
        {
            _usuarioRepository = usuarioRepository;
            _escolaRepository = escolaRepository;
            _usuariopermissaoRepository = usuariopermissaoRepository;

        }

    
        public IActionResult Cadastrar([FromBody] UsuarioDomain usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _usuarioRepository.Inserir(usuario);
              //  _usuariopermissaoRepository.Inserir(Upermissao);
                return Ok($"Usuario {usuario.NomeUsuario} Cadastrado Com Sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar dados. " + ex.Message);
            }
        }


        /// <summary>
        /// Api para listar todos usuários
        /// </summary>
        /// <returns></returns>
        //listar Todos
        [HttpGet]
        [Route("todos")]
        public IActionResult Buscar()
        {
            try
            {
                var usuarios = _usuarioRepository.Listar(new string[]{"Escolas","ClPermissoesUsuarios","ClPermissoesUsuarios.Permissao"});
                return Ok(usuarios); 
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados. " + ex.Message);
            }
        }


        //buscar por ID
        [HttpGet]
        [Route("buscarid/{Id}")]
        public IActionResult BuscarPorId(int Id)
        {
            try
            {
                var usuario = _usuarioRepository.BuscarPorId(Id);
                //com relacionamento
                var escola = _escolaRepository.BuscarPorId(Id);
                usuario.Escolas = escola;

                var permissoes = _usuariopermissaoRepository.Listar(new string[]{"Permissao"}).Where( c=> c.UsuarioId == Id);

                foreach (var item in permissoes)
                {
                    usuario.ClPermissoesUsuarios.Add(item);
                }


                if (usuario != null)
                    return Ok(usuario);
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
        public IActionResult Atualizar([FromBody] UsuarioDomain usuario)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                 _usuarioRepository.Atualizar(usuario);
                 return Ok($"Usuario {usuario.NomeUsuario} Atualizado Com Sucesso.");

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar os dados... " + ex.Message);
            }
        }


        [Route("deletar")]
        [HttpDelete]
        public IActionResult Deletar([FromBody] UsuarioDomain usuario)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                 _usuarioRepository.Deletar(usuario);
                 return Ok($"Usuário {usuario.NomeUsuario} Deletado Com Sucesso.");
            }
            catch(Exception ex)
            {
                return BadRequest("Erro ao deletar dados. " + ex.Message);
            }
        }

    }
}
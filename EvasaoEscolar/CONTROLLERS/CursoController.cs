using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace EvasaoEscolar.CONTROLLERS
{

    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    public class CursoController : Controller
    {
          private IBaseRepository<CursoDomain> _cursoRepository;

          public CursoController(IBaseRepository<CursoDomain> cursoRepository)
          {
              _cursoRepository = cursoRepository;
          }
        
        
        [HttpGet]
        [Route("todos")]      
        public IActionResult Buscar()
        {
            try
            {
                var cursos = _cursoRepository.Listar();
                return Ok(cursos); 
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar dados... " + ex.Message);
            }
        }

        [HttpGet]
        [Route("buscarid/{Id}")]
        public IActionResult BuscarPorId(int Id)
        {
            try
            {
                var cursos = _cursoRepository.BuscarPorId(Id);
                if (cursos != null)
                    return Ok(cursos);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao buscar os dados solictidados. " + ex.Message);
            }
        }


            
         public IActionResult Cadastrar([FromBody] CursoDomain curso)
         {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try 
            {
                _cursoRepository.Inserir(curso);
                return Ok($"Curso: {curso.NomeCurso} cadastrado com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest("Erro ao cadastrar dados. " + ex.Message);
            }
         }



         
    }
}
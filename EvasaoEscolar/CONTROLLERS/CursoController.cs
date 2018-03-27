using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;

namespace EvasaoEscolar.CONTROLLERS
{

    [Route("api/[controller]")]
    public class CursoController : Controller
    {
          private IBaseRepository<CursoDomain> _cursoRepository;

          public CursoController(IBaseRepository<CursoDomain> cursoRepository)
          {
              _cursoRepository = cursoRepository;
          }
        

         public IActionResult Cadastrar([FromBody] CursoDomain curso)
         {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try 
            {
                _cursoRepository.Inserir(curso);
                return Ok($"Curso {curso.NomeCurso} Cadastrado Com Sucesso.");
            }
            catch(Exception ex)
            {
                return BadRequest("Erro ao cadastrar dados. " + ex.Message);
            }
         }

         
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.REPOSITORIES;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using EvasaoEscolar.CONTEXTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace EvasaoEscolar.CONTROLLERS {
	
	[Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
	
	public class AnotacoesController : Controller{
			
            AnotacoesDomain anotacoes = new AnotacoesDomain();
			private IBaseRepository<AnotacoesDomain> _anatacoesRepository;
             readonly EvasaEscolarContext contexto;
			
			public AnotacoesController(IBaseRepository<AnotacoesDomain> anotacoesRepository,  EvasaEscolarContext contexto){
				_anatacoesRepository = anotacoesRepository;

                this.contexto = contexto;
			}
						
			
			 public IActionResult Cadastrar([FromBody] AnotacoesDomain anotacoes){
				 
			if (!ModelState.IsValid)
            return BadRequest(ModelState);

            try
            {
                _anatacoesRepository.Inserir(anotacoes);
				return Ok($"id:{anotacoes.Id}");
             //   return Ok($"A anotaçao de Id: {anotacoes.Id} foi cadastrada com sucesso...");
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
            var anotacoess = contexto.DbAnotacoes.Where(i => i.Id == Id).FirstOrDefault();
            if (anotacoess == null)
               return NotFound();

            contexto.DbAnotacoes.Remove(anotacoess);
            int x = contexto.SaveChanges();
            if (x > 0)
            {
                return Ok($"id:{anotacoes.Id}");
            }
            else
            {
                return BadRequest();
            }

        }


		
		[HttpPut]
        [Route("atualizar")]
        public IActionResult Atualizar([FromBody] AnotacoesDomain anotacoes)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                 _anatacoesRepository.Atualizar(anotacoes);
                 return Ok($"id:{anotacoes.Id}");

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao atualizar os dados... " + ex.Message);
            }
        }



		
	}
}

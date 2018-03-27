
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using EvasaoEscolar.CONTEXTO;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.MODELS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace EvasaoEscolar.CONTROLLERS
{
    
    [Route("api/login")]
     [EnableCors("AllowAnyOrigin")]
  
    public class LoginController : Controller
    {
        readonly EvasaEscolarContext _loginContexto;

        public LoginController(EvasaEscolarContext loginContexto)
        {
            _loginContexto = loginContexto;   
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(
            [FromBody]UsuarioDomain usuario,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            
            UsuarioDomain user = _loginContexto.DbUsuarios.Include("ClPermissoesUsuarios")
                                                .Include("ClPermissoesUsuarios.Permissao")
                                                .FirstOrDefault(c => c.Email == usuario.Email && 
                                                                    c.Senha == usuario.Senha );

            
            
            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Id.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                        new Claim("NomeUsuario", user.NomeUsuario),
                        new Claim(ClaimTypes.Email, user.Email)
                    }
                );

                foreach (var item in user.ClPermissoesUsuarios)
                {
                        identity.AddClaim(new Claim(ClaimTypes.Role, item.Permissao.NomePermissao));
                }

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                var retorno = new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };

                return Ok(retorno);
            }
            else
            {
                var retorno = new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };

                return BadRequest(retorno);
            }
        }

    }
}
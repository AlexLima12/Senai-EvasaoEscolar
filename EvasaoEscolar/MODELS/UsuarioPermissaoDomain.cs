using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class UsuarioPermissaoDomain : BaseDomain 


    {
         [ForeignKey("UsuarioId")]
        public UsuarioDomain  Usuario { get; set; }
        public int UsuarioId { get; set; }

        

        [ForeignKey("PermissaoId")]
        public PermissaoDomain Permissao { get; set; }
        public int PermissaoId { get; set; }

        
    }
}
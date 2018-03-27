using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvasaoEscolar.MODELS
{
    public class PermissaoDomain : BaseDomain
    {
       
        [Required]
        [StringLength(50)]
        public string NomePermissao { get; set; }
        

    //    public ICollection<UsuarioPermissaoDomain> ClUsuariosPermissoes { get; set; }
    }
}
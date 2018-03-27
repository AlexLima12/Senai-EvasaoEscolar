using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class UsuarioDomain : BaseDomain
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Senha { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Celular { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }

        

        [ForeignKey("EscolaId")]
        public EscolaDomain  Escolas { get; set; }
        public int EscolaId { get; set; }


           
        public ICollection<UsuarioPermissaoDomain> ClPermissoesUsuarios { get; set; }
        

    }
}
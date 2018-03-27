using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class EscolaDomain : BaseDomain
    {
         [Required]
        [StringLength(100)]
        public string NomeEscola { get; set; }

        
        [StringLength(100)]
        public string NumeroUnidade { get; set; }

        //public ICollection<UsuarioDomain> clUsuarios { get; set; }

        public ICollection<TurmaDomain> clTurmas { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvasaoEscolar.MODELS
{
    public class TermoDomain : BaseDomain
    {   
        [Required]
        [StringLength(100)]
        public string NomeTermo { get; set; }

         public ICollection<DisciplinasDomain> Disciplinas { get; set; }
    }
}
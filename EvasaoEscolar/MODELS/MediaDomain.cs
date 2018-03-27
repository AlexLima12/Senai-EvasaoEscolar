using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class MediaDomain : BaseDomain
    {   
        [Required]
        [StringLength(50)]
        public string CalculoMedia { get; set; }

           [ForeignKey ("AlunoDisciplinaTurmaId")]
        public AlunoDisciplinaTurmaDomain AlunoDisciplinaTurma { get; set; }
        public int AlunoDisciplinaTurmaId { get; set; }
    }
}
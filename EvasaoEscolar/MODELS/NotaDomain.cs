using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class NotaDomain : BaseDomain
    {
        [Required]
        [StringLength(50)]
        public string DescricaoNota { get; set; }
        
        [Required]
        public double ValorNota { get; set; }




        [ForeignKey ("AlunoDisciplinaTurmaId")]
        public AlunoDisciplinaTurmaDomain AlunoDisciplinaTurma { get; set; }
        public int AlunoDisciplinaTurmaId { get; set; }
    }
}
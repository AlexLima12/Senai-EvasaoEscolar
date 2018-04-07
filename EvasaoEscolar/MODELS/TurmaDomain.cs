using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EvasaoEscolar.Enum;

namespace EvasaoEscolar.MODELS
{
    public class TurmaDomain : BaseDomain
    {
        [Required]
        [StringLength(100)]
        public string NomeTurma { get; set; }

        [Required]      
        public int Periodo { get; set; }
      
        

        [ForeignKey("EscolaId")]
        public EscolaDomain Escola { get; set; }
        public int EscolaId { get; set; }


        public bool StatusTurma { get; set; }

        public ICollection<DisciplinaTurmaDomain> DisciplinaTurma { get; set; }

        public ICollection<IotDomain> Iot { get; set; }

    }
}
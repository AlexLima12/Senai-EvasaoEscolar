using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EvasaoEscolar.MODELS
{
    public class IotDomain : BaseDomain
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataDiaIot { get; set; }


         [ForeignKey("TurmaId")]
        public TurmaDomain  Turma { get; set; }
        public int TurmaId { get; set; }

        public ICollection<IotDadosDomain> IotDados { get; set; }
    }
}
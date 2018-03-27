using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class UploadPlanilhaDomain : BaseDomain
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataUploadPlanilha { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataReferenciaPlanilha { get; set; }


        [ForeignKey("DisciplinaTurmaId")]
        public DisciplinaTurmaDomain DisciplinaTurma { get; set; }
        public int DisciplinaTurmaId { get; set; }


        public ICollection<PlanilhaDadosDomain> clPlanilhaDados { get; set; }
    }
}
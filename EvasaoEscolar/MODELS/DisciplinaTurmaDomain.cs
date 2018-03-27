using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class DisciplinaTurmaDomain : BaseDomain
    {
        [ForeignKey("TurmaId")]
        public TurmaDomain Turma { get; set; }
        public int TurmaId { get; set; }



        [ForeignKey("DisciplinaId")]
        public DisciplinasDomain Disciplinas { get; set; }
        public int DisciplinaId { get; set; }



        public ICollection<AlunoDisciplinaTurmaDomain> clAlunoDisciplinaTurma { get; set; }
        public ICollection<UploadPlanilhaDomain> clUploadPlanilha { get; set; }

    }
}
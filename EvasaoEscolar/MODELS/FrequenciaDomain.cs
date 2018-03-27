using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class FrequenciaDomain : BaseDomain
    {

        public int Dias{ get; set; }

        public int Falta { get; set; }

        public int NumeroDeAulas { get; set; }

        public int Atraso { get; set; }

        public int Presenca { get; set; }

        public int FaltaComp { get; set; }


        [ForeignKey ("AlunoDisciplinaTurmaId")]
        public AlunoDisciplinaTurmaDomain AlunoDisciplinaTurma { get; set; }
        public int AlunoDisciplinaTurmaId { get; set; }
    }
}
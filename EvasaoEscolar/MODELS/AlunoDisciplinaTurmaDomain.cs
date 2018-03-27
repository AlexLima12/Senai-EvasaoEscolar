using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class AlunoDisciplinaTurmaDomain : BaseDomain
    {
        [ForeignKey("AlunoId")]
        public AlunoDomain Aluno { get; set; }
        public int AlunoId { get; set; }


        [ForeignKey("DisciplinaTurmaId")]
        public DisciplinaTurmaDomain DisciplinaTurma { get; set; }
        public int DisciplinaTurmaId { get; set; }



        public ICollection<FrequenciaDomain> Frequencia { get; set; }
        public ICollection<NotaDomain> Nota { get; set; }

        public ICollection<MediaDomain> Media { get; set; }
    }
}
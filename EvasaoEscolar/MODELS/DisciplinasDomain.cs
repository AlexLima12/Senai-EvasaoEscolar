using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class DisciplinasDomain : BaseDomain
    {
        [Required]
        [StringLength(100)]
        public string NomeDisciplina { get; set; }

        [ForeignKey("CursoId")]
        public CursoDomain Curso { get; set; }
        public int CursoId { get; set; }


        [ForeignKey("TermoId")]
        public TermoDomain Termo { get; set; }
        public int TermoId { get; set; }


        //referência para tabela Disciplina Turma Domain
        public ICollection<DisciplinaTurmaDomain> clDisciplinaTurma { get; set; }
    }
}
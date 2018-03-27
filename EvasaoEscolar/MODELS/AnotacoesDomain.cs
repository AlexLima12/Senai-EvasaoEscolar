using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class AnotacoesDomain : BaseDomain
    {

        [Required]
        [StringLength(255)]
        public string Mensagem { get; set; }


               [ForeignKey("AlunoId")]
        public AlunoDomain Aluno { get; set; }
        public int AlunoId { get; set; }

    }
}
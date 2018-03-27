using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvasaoEscolar.MODELS
{
    public class AlunoDomain : BaseDomain
    {
        [Required]
        [StringLength(15)]
        public string Matricula { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeAluno { get; set; }

        [Required]        
        public bool StatusAlunoEvadiu { get; set; }      
      

        public ICollection<AlunoDisciplinaTurmaDomain> clAlunoDisciplinaTurma { get; set; }
        public ICollection<PlanilhaDadosDomain> clPlanilhaDados { get; set; }
        public ICollection<AlertasDomain> clAlertas{ get; set; }

          public ICollection<IotDadosDomain> IotDados { get; set; }

          public ICollection<AnotacoesDomain> clAnotacoes { get; set; }



    }
}
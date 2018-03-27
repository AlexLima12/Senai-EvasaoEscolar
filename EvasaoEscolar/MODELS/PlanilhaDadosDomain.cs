using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class PlanilhaDadosDomain : BaseDomain
    {
                
        public bool DadosProcessados { get; set; }
       
       
        [StringLength(50)]
        public string NomeAluno{ get; set; }

        [StringLength(50)]
        public string JustFaltaPlanilha { get; set; }

        [StringLength(50)]
        public string Aula1Planilha { get; set; }

        [StringLength(50)]
        public string Aula2Planilha { get; set; }

        [StringLength(50)]
        public string Aula3Planilha { get; set; }

        [StringLength(50)]
        public string Aula4Planilha { get; set; }

        [StringLength(50)]
        public string Aula5Planilha { get; set; }

        [ForeignKey("AlunoId")]
        public AlunoDomain Aluno { get; set; }
        public int AlunoId { get; set; }

        [ForeignKey("UploadPlanilhaId")]
        public UploadPlanilhaDomain UploadPlanilha { get; set; }
        public int UploadPlanilhaId { get; set; }

        public PlanilhaDadosDomain()
        {
            
        }

    }
}
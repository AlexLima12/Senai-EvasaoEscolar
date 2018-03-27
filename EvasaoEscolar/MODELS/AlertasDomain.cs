using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EvasaoEscolar.Enum;

namespace EvasaoEscolar.MODELS
{
    public class AlertasDomain : BaseDomain
    {
        // 0 = ok, 1 = Atenção, 2 = Grave
        [Required]        
        public int NivelPrioridade { get; set; }


        [ForeignKey("AlunoId")]
        public AlunoDomain Aluno { get; set; }
        public int AlunoId { get; set; }


       [Required]
        [StringLength(255)]
        public string MensagemAlerta { get; set; }

        public bool AlertaAntigo { get; set; }

        

        [DataType (DataType.DateTime)]
        public DateTime DataAlerta { get; set; }


        // 0 = "IOT" ou 1 ="EXCEL"
      
        [StringLength(1)]
        public int OrigemAlerta { get; set; }




       // public ICollection<AlertaItemDomain> clAlertaItem { get; set; }
    }
}
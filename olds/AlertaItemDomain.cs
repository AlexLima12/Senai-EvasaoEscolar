using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EvasaoEscolar.Enum;

namespace EvasaoEscolar.MODELS
{
    public class AlertaItemDomain : BaseDomain
    {
        [ForeignKey("AlertaId")]
        public AlertasDomain Alertas { get; set; }
        public int AlertaId { get; set; }


        [Required]
        [StringLength(255)]
        public string MensagemAlerta { get; set; }

        public bool AlertaAntigo { get; set; }


        //Enum para escolher "IOT" ou "EXCEL"
        [Required]
        [StringLength(1)]
        public EnOrigemAlerta OrigemAlerta { get; set; }


    }
}
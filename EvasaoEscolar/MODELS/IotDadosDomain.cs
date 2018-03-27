using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvasaoEscolar.MODELS
{
    public class IotDadosDomain : BaseDomain
    {
           [ForeignKey("AlunoId")]
        public AlunoDomain  Aluno { get; set; }
        public int AlunoId { get; set; }


              [ForeignKey("IotId")]
        public IotDomain  Iot { get; set; }
        public int IotId { get; set; }

        

        public bool StatusProcessadoIotDados { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataIotDados { get; set; }

        public decimal HeadPose { get; set; }
        public decimal EsposureValue { get; set; }
        public decimal EsposureLevel { get; set; }
        public decimal Smile { get; set; }
        public decimal Contempt { get; set; }
        public decimal Surprise { get; set; }
        public decimal Happiness { get; set; }
        public decimal Neutral { get; set; }
        public decimal Sadness { get; set; }
        public decimal Disgust { get; set; }
        public decimal Anger { get; set; }
        public decimal Fear { get; set; }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool EyeOccluded { get; set; }
        public bool MouthOccluded { get; set; }
        public bool ForeheadOccluded { get; set; }
        public int AulaIot { get; set; }


        [StringLength(100)]
        public string FaceId { get; set; }





    }
}
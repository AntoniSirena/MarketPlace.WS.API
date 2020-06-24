using JS.Base.WS.API.Base;
using JS.Base.WS.API.Models.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class IdentificationData : Audit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long RequestId { get; set; }

        [Required]
        public int RegionalId { get; set; }

        [Required]
        public int DistritId { get; set; }

        [Required]
        public int CenterId { get; set; }

        [Required]
        public int TandaId { get; set; }

        [Required]
        public int GradeId { get; set; }

        [Required]
        public long DocentId { get; set; }

        [Required]
        public long CompanionId { get; set; }


        public int? VisitIdA { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string VisitDateA { get; set; }

        public int? QuantityChildrenA { get; set; }
        public int? QuantityGirlsA { get; set; }
        public int? ExpectedTimeA { get; set; }
        public int? RealTimeA { get; set; }

        public int? VisitIdB { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string VisitDateB { get; set; }

        public int? QuantityChildrenB { get; set; }
        public int? QuantityGirlsB { get; set; }
        public int? ExpectedTimeB { get; set; }
        public int? RealTimeB { get; set; }

        public int? VisitIdC { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string VisitDateC { get; set; }

        public int? QuantityChildrenC { get; set; }
        public int? QuantityGirlsC { get; set; }
        public int? ExpectedTimeC { get; set; }
        public int? RealTimeC { get; set; }


        [ForeignKey("RequestId")]
        public virtual AccompanyingInstrumentRequest Request { get; set; }

        [ForeignKey("RegionalId")]
        public virtual Regional Regional { get; set; }

        [ForeignKey("DistritId")]
        public virtual District District { get; set; }

        [ForeignKey("CenterId")]
        public virtual EducativeCenter EducativeCenter { get; set; }

        [ForeignKey("TandaId")]
        public virtual Tanda Tanda { get; set; }

        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        [ForeignKey("DocentId")]
        public virtual Docent Docent { get; set; }

        [ForeignKey("CompanionId")]
        public virtual User Companion { get; set; }


        [ForeignKey("VisitIdA")]
        public virtual Visit VisitA { get; set; }

        [ForeignKey("VisitIdB")]
        public virtual Visit VisitB { get; set; }

        [ForeignKey("VisitIdC")]
        public virtual Visit VisitC { get; set; }

    }
}
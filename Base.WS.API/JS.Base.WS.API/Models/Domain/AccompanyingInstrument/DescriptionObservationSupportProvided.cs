using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain.AccompanyingInstrument
{
    public class DescriptionObservationSupportProvided : Audit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long RequestId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int AreaIdA { get; set; }

        public string DateA { get; set; }

        public string CommentA { get; set; }

        [Required]
        public int AreaIdB { get; set; }

        public string DateB { get; set; }

        public string CommentB { get; set; }

        [Required]
        public int AreaIdC { get; set; }

        public string DateC { get; set; }

        public string CommentC { get; set; }


        [ForeignKey("RequestId")]
        public virtual AccompanyingInstrumentRequest Request { get; set; }

        [ForeignKey("StatusId")]
        public virtual RequestStatus Status { get; set; }

        [ForeignKey("AreaIdA")]
        public virtual Area AreaA { get; set; }

        [ForeignKey("AreaIdB")]
        public virtual Area AreaB { get; set; }

        [ForeignKey("AreaIdC")]
        public virtual Area AreaC { get; set; }

    }
}
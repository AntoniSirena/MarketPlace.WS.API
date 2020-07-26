using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain.AccompanyingInstrument
{
    public class ReflectionPracticeDetail : Audit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long ReflectionPracticeId { get; set; }

        [Required]
        public long VariableDetailId { get; set; }

        [Required]
        public int AreaIdA { get; set; }

        [Required]
        public int IndicadorIdA { get; set; }

        [Required]
        public int AreaIdB { get; set; }

        [Required]
        public int IndicadorIdB { get; set; }

        [Required]
        public int AreaIdC { get; set; }

        [Required]
        public int IndicadorIdC { get; set; }



        [ForeignKey("ReflectionPracticeId")]
        public virtual ReflectionPractice ReflectionPractice { get; set; }

        [ForeignKey("VariableDetailId")]
        public virtual VariableDetail VariableDetail { get; set; }

        [ForeignKey("AreaIdA")]
        public virtual Area AreaA { get; set; }

        [ForeignKey("IndicadorIdA")]
        public virtual Indicator IndicatorA { get; set; }

        [ForeignKey("AreaIdB")]
        public virtual Area AreaB { get; set; }

        [ForeignKey("IndicadorIdB")]
        public virtual Indicator IndicatorB { get; set; }

        [ForeignKey("AreaIdC")]
        public virtual Area AreaC { get; set; }

        [ForeignKey("IndicadorIdC")]
        public virtual Indicator IndicatorC { get; set; }
    }
}
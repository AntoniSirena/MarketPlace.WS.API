using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class VariableDetail
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int VariableID { get; set; }


        [ForeignKey("VariableID")]
        public virtual Variable Variable { get; set; }
    }
}
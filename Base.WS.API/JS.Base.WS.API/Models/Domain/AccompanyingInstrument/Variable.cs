using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class Variable
    {
        public Variable()
        {
            variableDetails = new HashSet<VariableDetail>();
        }

        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public virtual ICollection<VariableDetail> variableDetails { get; set; }
    }
}
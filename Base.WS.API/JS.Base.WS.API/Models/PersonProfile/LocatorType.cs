using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.PersonProfile
{
    public class LocatorType
    {
        public LocatorType()
        {
            Locators = new HashSet<Locator>();
        }

        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Locator> Locators { get; set; }
    }
}
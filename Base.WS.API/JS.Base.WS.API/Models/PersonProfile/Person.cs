using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.PersonProfile
{
    public class Person: Audit
    {
        public Person()
        {
            Locators = new HashSet<Locator>();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string secondSurname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }

        public string FullName { get { return string.Format("{0} {1} {2} {3}", FirstName, SecondName, Surname, secondSurname); } }


        public virtual ICollection<Locator> Locators { get; set; }

    }
}
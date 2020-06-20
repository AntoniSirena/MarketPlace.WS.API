using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Common
{
    public class CurrentUserInfoDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
    }
}
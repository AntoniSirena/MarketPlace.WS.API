using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain
{
    public class DocentDetailsDto
    {
        public DocentPersonInfo DocentPersonInfo { get; set; }
    }


    public class DocentPersonInfo
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string EducativeCenter { get; set; }
    }
}
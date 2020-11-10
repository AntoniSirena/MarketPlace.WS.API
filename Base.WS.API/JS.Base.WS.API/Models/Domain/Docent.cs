using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.Domain
{
    public class Docent: Audit
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [Required]
        public string Surname { get; set; }

        public string SecondSurname { get; set; }

        public string FullName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int DocumentTypeID { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int AreaId { get; set; }

        [Required]
        public int EducativeCenterId { get; set; }


        [ForeignKey("DocumentTypeID")]
        public virtual DocumentType DocumentType { get; set; }

        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }

        [ForeignKey("EducativeCenterId")]
        public virtual EducativeCenter EducativeCenter { get; set; }
    }
}
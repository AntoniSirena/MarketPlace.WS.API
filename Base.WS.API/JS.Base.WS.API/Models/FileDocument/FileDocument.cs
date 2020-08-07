using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Models.FileDocument
{
    public class FileDocument: Audit
    {

        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
        public string Description { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain
{
    public class SuggestionsAgreementDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public int StausId { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColour { get; set; }

        public int AreaIdA { get; set; }
        public string DateA { get; set; }
        public string CommentA { get; set; }
        public string TeacherSignatureA { get; set; }
        public string CompanionSignatureA { get; set; }
        public string DistrictTechnicianSignatureA { get; set; }

        public int AreaIdB { get; set; }
        public string DateB { get; set; }
        public string CommentB { get; set; }
        public string TeacherSignatureB { get; set; }
        public string CompanionSignatureB { get; set; }
        public string DistrictTechnicianSignatureB { get; set; }

        public int AreaIdC { get; set; }
        public string DateC { get; set; }
        public string CommentC { get; set; }
        public string TeacherSignatureC { get; set; }
        public string CompanionSignatureC { get; set; }
        public string DistrictTechnicianSignatureC { get; set; }

    }
}
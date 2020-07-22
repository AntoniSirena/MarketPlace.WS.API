using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.Domain
{
    public class DescriptionObservationSupportProvidedDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public int StausId { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColour { get; set; }

        public int AreaIdA { get; set; }
        public string DateA { get; set; }
        public string CommentA { get; set; }

        public int AreaIdB { get; set; }
        public string DateB { get; set; }
        public string CommentB { get; set; }

        public int AreaIdC { get; set; }
        public string DateC { get; set; }
        public string CommentC { get; set; }
    }
}
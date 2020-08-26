using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DTO.Response.AccompInstDetail.Domain
{
    public class AccompanyInstrumentDetailsDto
    {
        public _IdentificationData IdentificationData { get; set; }

        public _VariableDetailsDto VariableA { get; set; }
        public _VariableDetailsDto VariableB { get; set; }
        public _VariableDetailsDto VariableC { get; set; }
        public _VariableDetailsDto VariableD { get; set; }
        public _VariableDetailsDto VariableE { get; set; }
        public _VariableDetailsDto VariableF { get; set; }
        public _VariableDetailsDto VariableG { get; set; }
        public _VariableDetailsDto VariableH { get; set; }

        public _CommentsRevisedDocumentDto CommentsRevisedDocument { get; set; }
        public _DescriptionObservationSupportProvidedDto DescriptionObservationSupportProvided { get; set; }
        public _SuggestionsAgreementDto SuggestionsAgreement { get; set; }

    }


    public class _IdentificationData
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string Regional { get; set; }
        public string Distrit { get; set; }
        public string Center { get; set; }
        public string Tanda { get; set; }
        public string Grade { get; set; }
        public string Docent { get; set; }
        public string DocentDocument { get; set; }
        public string Companion { get; set; }
        public string CompanionDocument { get; set; }


        public string VisitA { get; set; }
        public string VisitDateA { get; set; }
        public int? QuantityChildrenA { get; set; }
        public int? QuantityGirlsA { get; set; }
        public int? ExpectedTimeA { get; set; }
        public int? RealTimeA { get; set; }

        public string VisitB { get; set; }
        public string VisitDateB { get; set; }
        public int? QuantityChildrenB { get; set; }
        public int? QuantityGirlsB { get; set; }
        public int? ExpectedTimeB { get; set; }
        public int? RealTimeB { get; set; }

        public string VisitC { get; set; }
        public string VisitDateC { get; set; }
        public int? QuantityChildrenC { get; set; }
        public int? QuantityGirlsC { get; set; }
        public int? ExpectedTimeC { get; set; }
        public int? RealTimeC { get; set; }

        public string PrintDate { get; set; }

    }

    public class _CommentsRevisedDocumentDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public int StausId { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColour { get; set; }

        public string AreaA { get; set; }
        public string DateA { get; set; }

        public string AreaB { get; set; }
        public string DateB { get; set; }

        public string AreaC { get; set; }
        public string DateC { get; set; }

        public List<_CommentsRevisedDocumentDetailDto> CommentsRevisedDocumenDetails { get; set; }
    }

    public class _CommentsRevisedDocumentDetailDto
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public string AreaA { get; set; }
        public string DateA { get; set; }
        public string CommentA { get; set; }

        public string AreaB { get; set; }
        public string DateB { get; set; }
        public string CommentB { get; set; }

        public string AreaC { get; set; }
        public string DateC { get; set; }
        public string CommentC { get; set; }
    }

    public class _DescriptionObservationSupportProvidedDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public int StausId { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColour { get; set; }

        public string AreaA { get; set; }
        public string DateA { get; set; }
        public string CommentA { get; set; }

        public string AreaB { get; set; }
        public string DateB { get; set; }
        public string CommentB { get; set; }

        public string AreaC { get; set; }
        public string DateC { get; set; }
        public string CommentC { get; set; }
    }

    public class _SuggestionsAgreementDto
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public int StausId { get; set; }
        public string StatusDescription { get; set; }
        public string StatusColour { get; set; }

        public string AreaA { get; set; }
        public string DateA { get; set; }
        public string CommentA { get; set; }
        public string TeacherSignatureA { get; set; }
        public string CompanionSignatureA { get; set; }
        public string DistrictTechnicianSignatureA { get; set; }

        public string AreaB { get; set; }
        public string DateB { get; set; }
        public string CommentB { get; set; }
        public string TeacherSignatureB { get; set; }
        public string CompanionSignatureB { get; set; }
        public string DistrictTechnicianSignatureB { get; set; }

        public string AreaC { get; set; }
        public string DateC { get; set; }
        public string CommentC { get; set; }
        public string TeacherSignatureC { get; set; }
        public string CompanionSignatureC { get; set; }
        public string DistrictTechnicianSignatureC { get; set; }

    }
}
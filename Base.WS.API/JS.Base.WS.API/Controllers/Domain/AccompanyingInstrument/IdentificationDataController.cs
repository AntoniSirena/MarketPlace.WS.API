using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Request.Domain.RequestFlowAI;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Domain
{
    [RoutePrefix("api/identificationData")]
    [Authorize]
    public class IdentificationDataController : GenericApiController<IdentificationData>
    {

        private MyDBcontext db;
        private Response response;
        private AccompanyingInstrumentService accompanyingInstrumentService;

        public IdentificationDataController()
        {
            db = new MyDBcontext();
            response = new Response();
            accompanyingInstrumentService = new AccompanyingInstrumentService();
        }

        private long currentUserId = CurrentUser.GetId();


        public override IHttpActionResult Create(dynamic entity)
        {
            long docentId = Convert.ToInt64(entity["DocentId"]);

            var docentRequest = db.AccompanyingInstrumentRequests.Where(x => x.DocentId == docentId && 
                                                                       x.RequestStatu.ShortName == Constants.RequestStatus.Approved ||
                                                                       x.RequestStatu.ShortName == Constants.RequestStatus.Cancelad).ToList().LastOrDefault();

            if (docentRequest == null)
            {
                response.Code = InternalResponseCodeError.Code317;
                response.Message = "No puede crear un nuevo formulario, porque tiene uno en un estado diferente de aprovado o cancelado";

                return Ok(response);
            }

            //Creating Accompanying Instrument Request
            var inProcess = db.RequestStatus.Where(x => x.ShortName == Constants.RequestStatus.InProcess).FirstOrDefault();
            var request = new AccompanyingInstrumentRequest()
            {
                StatusId = inProcess.Id,
                DocentId = docentId,
                OpeningDate = DateTime.Now,
                ClosingDate = null,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,
            };

            var requestResult = db.AccompanyingInstrumentRequests.Add(request);
            db.SaveChanges();

            //Update RequestFlowAIInProcess
            accompanyingInstrumentService.Process(requestResult.Id);

            entity["RequestId"] = requestResult.Id;

            object input = JsonConvert.DeserializeObject<object>(entity.ToString());
            return base.Create(input);
        }


        [HttpGet]
        [Route("GetAccompInstRequest")]
        public IHttpActionResult GetAccompInstRequest()
        {
            var result = accompanyingInstrumentService.GetAccompInstRequest();

            if (result.Count() == 0)
            {
                response.Code = InternalResponseCodeError.Code312;
                response.Message = InternalResponseCodeError.Message312;

                return Ok(response);
            }
            {
                response.Data = result;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("GetAccompanyInstrumentDetails")]
        public IHttpActionResult GetAccompanyInstrumentDetails(long requestId)
        {
            var result = accompanyingInstrumentService.GetAccompanyInstrumentDetails(requestId);

            if (result == null)
            {
                response.Code = InternalResponseCodeError.Code321;
                response.Message = InternalResponseCodeError.Message321;

                return Ok(response);
            }
            {
                response.Data = result;
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("CreateVariable")]
        public IHttpActionResult CreateVariable([FromBody] long identificationDataId)
        {
            bool result = false;

            long requestId = db.IdentificationDatas.Where(x => x.Id == identificationDataId).Select(y => y.RequestId).FirstOrDefault();

            result = accompanyingInstrumentService.CreateVariables(requestId);

            if (result)
            {
                result = true;
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("GetVariableByRequestId")]
        public IHttpActionResult GetVariableByRequestId(long requestId, string variable)
        {
            var result = new VariableDto();

            result = accompanyingInstrumentService.GetVariableByRequestId(requestId, variable);

            if (result == null)
            {
                response.Code = InternalResponseCodeError.Code313;
                response.Message = InternalResponseCodeError.Message313;
            }
            if (result.Error)
            {
                response.Data = result;
                response.Code = "999";
                response.Message = result.ErrorMessage;
            }
            else
            {
                response.Data = result;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("GetCommentsRevisedDocumentByRequestId")]
        public IHttpActionResult GetCommentsRevisedDocumentByRequestId(long requestId)
        {
            var result = new CommentsRevisedDocumentDto();

            result = accompanyingInstrumentService.GetCommentsRevisedDocument(requestId);

            if (result == null)
            {
                response.Code = InternalResponseCodeError.Code314;
                response.Message = InternalResponseCodeError.Message314;
            }
            else
            {
                response.Data = result;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("GetDescriptionObservationSupportProvidedByRequestId")]
        public IHttpActionResult GetDescriptionObservationSupportProvidedByRequestId(long requestId)
        {
            var result = new DescriptionObservationSupportProvidedDto();

            result = accompanyingInstrumentService.GetDescriptionObservationSupportProvided(requestId);

            if (result == null)
            {
                response.Code = InternalResponseCodeError.Code315;
                response.Message = InternalResponseCodeError.Message315;
            }
            else
            {
                response.Data = result;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("GetSuggestionsAgreementByRequestId")]
        public IHttpActionResult GetSuggestionsAgreementByRequestId(long requestId)
        {
            var result = new SuggestionsAgreementDto();

            result = accompanyingInstrumentService.GetSuggestionsAgreement(requestId);

            if (result == null)
            {
                response.Code = InternalResponseCodeError.Code316;
                response.Message = InternalResponseCodeError.Message316;
            }
            else
            {
                response.Data = result;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateVariable")]
        public IHttpActionResult UpdateVariable(VariableDto request)
        {
            var result = accompanyingInstrumentService.UpdateVariable(request);

            if (!result.Error)
            {
                response.Data = result;
                response.Message = InternalResponseMessageGood.Message203;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateCommentsRevisedDocument")]
        public IHttpActionResult UpdateCommentsRevisedDocument(CommentsRevisedDocumentDto request)
        {
            bool result = accompanyingInstrumentService.UpdateCommentsRevisedDocument(request);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message203;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateDescriptionObservationSupportProvided")]
        public IHttpActionResult UpdateDescriptionObservationSupportProvided(DescriptionObservationSupportProvidedDto request)
        {
            bool result = accompanyingInstrumentService.UpdateDescriptionObservationSupportProvided(request);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message203;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("UpdateSuggestionsAgreement")]
        public IHttpActionResult UpdateSuggestionsAgreement(SuggestionsAgreementDto request)
        {
            bool result = accompanyingInstrumentService.UpdateSuggestionsAgreement(request);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message203;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("CompleteRequest")]
        public IHttpActionResult CompleteRequest(long requestId)
        {
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId && x.RequestStatu.ShortName == Constants.RequestStatus.InProcess).FirstOrDefault();

            if (request == null)
            {
                response.Code = InternalResponseCodeError.Message320;
                response.Message = "El formulario debe estar en proceso, para ser completado";

                return Ok(response);
            }

            bool result = accompanyingInstrumentService.CompleteRequest(requestId);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message205;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("ApproveRequest")]
        public IHttpActionResult ApproveRequest(long requestId)
        {
            var request0 = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId && x.RequestStatu.ShortName == Constants.RequestStatus.Approved).FirstOrDefault();

            if (request0 != null)
            {
                response.Code = InternalResponseCodeError.Message320;
                response.Message = "El formulario ya se encuentra aprobado, el mismo no puede ser modificado";

                return Ok(response);
            }

            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId && x.RequestStatu.ShortName == Constants.RequestStatus.PendingToApprove).FirstOrDefault();

            if (request == null)
            {
                response.Code = InternalResponseCodeError.Message320;
                response.Message = "El formulario debe estar pendiendete de aprobar, para ser aprobado";

                return Ok(response);
            }

            bool result = accompanyingInstrumentService.ApproveRequest(requestId);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message206;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("SendToObservationRequest")]
        public IHttpActionResult SendToObservationRequest(long requestId)
        {
            var request0 = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId && x.RequestStatu.ShortName == Constants.RequestStatus.InObservation).FirstOrDefault();

            if (request0 != null)
            {
                response.Code = InternalResponseCodeError.Message320;
                response.Message = "El formulario ya se encuentra en observación, favor proceda con la investigación";

                return Ok(response);
            }


            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId && x.RequestStatu.ShortName == Constants.RequestStatus.PendingToApprove).FirstOrDefault();

            if (request == null)
            {
                response.Code = InternalResponseCodeError.Message320;
                response.Message = "El formulario debe estar pendiendete de aprobar, para ser enviado a observación";

                return Ok(response);
            }

            bool result = accompanyingInstrumentService.SendToObservationRequest(requestId);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message207;

                request.QuantityVecesSendedObservation += 1;
                db.SaveChanges();
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("CreateResearchSummary")]
        public IHttpActionResult CreateResearchSummary(ResearchSummaryDto request)
        {

            bool result = accompanyingInstrumentService.CreateResearchSummary(request);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message210;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("ProcessRequest")]
        public IHttpActionResult ProcessRequest(long requestId)
        {
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId && x.RequestStatu.ShortName == Constants.RequestStatus.InObservation).FirstOrDefault();

            if (request == null)
            {
                response.Code = InternalResponseCodeError.Message320;
                response.Message = "El formulario debe estar en observación, para volver a ser procesado";

                return Ok(response);
            }

            var requestFlow = db.ResearchSummaries.Where(x => x.RequestId == requestId).ToList();

            if (requestFlow.Count() < request.QuantityVecesSendedObservation)
            {
                response.Code = InternalResponseCodeError.Code322;
                response.Message = InternalResponseCodeError.Message322;

                return Ok(response);
            }

            bool result = accompanyingInstrumentService.Process(requestId);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message209;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("CancelRequest")]
        public IHttpActionResult CancelRequest(long requestId)
        {
            var request = db.AccompanyingInstrumentRequests.Where(x => x.Id == requestId && x.RequestStatu.ShortName == Constants.RequestStatus.PendingToApprove).FirstOrDefault();

            if (request == null)
            {
                response.Code = InternalResponseCodeError.Message320;
                response.Message = "El formulario debe estar pendiendete de aprobar, para ser cancelado";

                return Ok(response);
            }

            bool result = accompanyingInstrumentService.CancelRequest(requestId);

            if (result)
            {
                response.Message = InternalResponseMessageGood.Message208;
            }
            else
            {
                response.Code = InternalResponseCodeError.Code301;
                response.Message = InternalResponseCodeError.Message301;
            }

            return Ok(response);
        }



        [HttpGet]
        [Route("CreateAccompanyInstrumentPDF")]
        [AllowAnonymous]
        public IHttpActionResult CreateAccompanyInstrumentPDF(long requestId)
        {

            var result = accompanyingInstrumentService.CreateAccompanyInstrumentPDF(requestId);

            response.Data = result;

            return Ok(response);
        }


    }
}
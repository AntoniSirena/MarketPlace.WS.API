using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Domain;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Domain
{

    [RoutePrefix("api/appointment")]
    [Authorize]
    public class AppointmentController : ApiController
    {

        private MyDBcontext db;
        private Response response;

        private long currentUserId = CurrentUser.GetId();

        public AppointmentController()
        {
            db = new MyDBcontext();
            response = new Response();
        }


        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] Appointment request)
        {

            if (!request.ScheduledAppointment)
            {
                request.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            }
            if (request.ScheduledAppointment)
            {
                request.StartDate = Convert.ToDateTime(request.StartDate).ToString("dd/MM/yyyy");
            }

            request.StatusId = db.AppointmentStatuses.Where(x => x.ShortName == Global.Constants.AppointmentStatus.InProcess).Select(y => y.Id).FirstOrDefault();
            request.CreationTime = DateTime.Now;
            request.CreatorUserId = currentUserId;
            request.IsActive = true;

            var result = db.Appointments.Add(request);
            db.SaveChanges();

            response.Code = "000";

            if (!request.ScheduledAppointment)
            {
                response.Message = string.Concat("Estimado usuario su turno #", result.Id.ToString(), " se ha creado correctamente");
            }
            if (request.ScheduledAppointment)
            {
                response.Message = string.Concat("Estimado usuario su cita #", result.Id.ToString(), " se ha creado correctamente");
            }

            response.Data = new { Id = result.Id };

            return Ok(response);
        }


        [HttpGet]
        [Route("GetEnterprises")]
        public IEnumerable<EnterpriseDTO> GetEnterprises()
        {
            var result = db.Enterprises.Where(x => x.AvailableOnlineAppointment == true && x.User.UserStatus.ShortName == Global.Constants.UserStatuses.Active)
                .Select(y => new EnterpriseDTO()
                {
                    Id = y.Id,
                    UserId = y.UserId,
                    Name = y.Name,
                }).OrderBy(z => z.Name).ToList();

            return result;
        }


        [HttpPost]
        [Route("GetAppointmentDetails")]
        public AppointmentDetailDTO GetAppointmentDetails([FromBody] Appointment request)
        {
            var result = new AppointmentDetailDTO();

            var enterprise = db.Enterprises.Where(x => x.Id == request.EnterpriseId).FirstOrDefault();

            result.EnterpriseImage = string.Concat(enterprise.ImageContenTypeLong, ',', Utilities.JS_File.GetStrigBase64(enterprise.ImagePath));
            result.EnterpriseName = enterprise.Name;
            result.EnterpriseAddress = enterprise.Address;
            result.EnterprisePhoneNumber = enterprise.PhoneNumber;
            result.EnterpriseServiceTime = enterprise.ServiceTime;
            result.UserName = request.Name;
            result.DocumentNomber = request.DocumentNomber;
            result.PhoneNomber = request.PhoneNomber;
            result.Comment = request.Comment;
            result.StartDate = Convert.ToDateTime(request.StartDate).ToString("dd/MM/yyyy");
            result.ScheduledAppointment = request.ScheduledAppointment;

            return result;
        }



    }
}

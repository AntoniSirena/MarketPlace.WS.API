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
using static JS.Base.WS.API.Global.Constants;

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
                string _startDate = DateTime.Now.ToString("yyyy-MM-dd");
                request.StartDate = Convert.ToDateTime(_startDate);
            }
            if (request.ScheduledAppointment)
            {
                request.StartDate = request.StartDate;
            }

            if (request.StartDate.Date < DateTime.Now.Date)
            {
                response.Code = "400";
                response.Message = "Estimado usuario la fecha para ser atendido debe ser mayor ó igual a la fecha actual";
                return Ok(response);
            }

            request.StatusId = db.AppointmentStatuses.Where(x => x.ShowToCustomer == true & x.ShortName == Global.Constants.AppointmentStatus.Pending).Select(y => y.Id).FirstOrDefault();
            request.ShortStartDate = request.StartDate.ToShortDateString();
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
            var result = db.Enterprises.Where(x => x.IsActive == true & x.AvailableOnlineAppointment == true && x.User.UserStatus.ShortName == Global.Constants.UserStatuses.Active)
                .Select(y => new EnterpriseDTO()
                {
                    Id = y.Id,
                    UserId = y.UserId,
                    Name = y.Name,
                }).OrderBy(z => z.Name).ToList();

            return result;
        }


        [HttpGet]
        [Route("GetAppointmentStatuses")]
        public IEnumerable<AppointmentStatusDTO> GetAppointmentStatuses()
        {
            var result = db.AppointmentStatuses.Where(x => x.ShowToCustomer == true)
                .Select(y => new AppointmentStatusDTO()
                {
                    Id = y.Id,
                    ShortName = y.ShortName,
                    Description = y.Description,
                }).OrderBy(z => z.Id).ToList();

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
            result.PhoneNumber = request.PhoneNumber;
            result.Comment = request.Comment;
            result.StartDate = Convert.ToDateTime(request.StartDate).ToString("dd/MM/yyyy");
            result.ScheduledAppointment = request.ScheduledAppointment;

            return result;
        }


        [HttpGet]
        [Route("CheckAppointment")]
        public CheckAppointmentDTO CheckAppointment(long number, string name, long phoneNumber)
        {
            var result = new CheckAppointmentDTO();

            var appointments = db.Appointments.Where(x => x.AppointmentStatus.ShortName == Global.Constants.AppointmentStatus.Pending).ToList();

            if (number > 0)
            {
                var _number = appointments.Where(x => x.Id == number).Select(y => new AppointmentId()
                {
                    Id = y.Id
                }).ToList();

                result.AppointmentId = _number;
            }

            if (!string.IsNullOrEmpty(name))
            {
                var _name = appointments.Where(x => x.Name == name).Select(y => new AppointmentId()
                {
                    Id = y.Id
                }).ToList();

                if (result.AppointmentId == null)
                {
                    result.AppointmentId = _name;
                }
                else
                {
                    result.AppointmentId.AddRange(_name);
                }
            }

            if (phoneNumber > 0)
            {
                var _phoneNumber = appointments.Where(x => x.PhoneNumber == phoneNumber).Select(y => new AppointmentId()
                {
                    Id = y.Id
                }).ToList();

                if (result.AppointmentId == null)
                {
                    result.AppointmentId = _phoneNumber;
                }
                else
                {
                    result.AppointmentId.AddRange(_phoneNumber);
                }
            }

            result.AppointmentId = RemoveDuplicateElement(result.AppointmentId);

            int quantity = 0;
            if (result.AppointmentId != null)
            {
                quantity = result.AppointmentId.Count();
                result.AppointmentId = result.AppointmentId.OrderBy(x => x.Id).ToList();
            }
            result.Message = string.Concat("Cantidad de registro encontrado ", quantity.ToString());

            return result;
        }


        [HttpGet]
        [Route("GetCheckAppointmentDetail")]
        public CheckAppointmentDTO GetCheckAppointmentDetail(long number)
        {
            var result = new CheckAppointmentDTO();

            var appointment = db.Appointments.Where(x => x.Id == number).FirstOrDefault();

            var appointments = db.Appointments.Where(x => x.EnterpriseId == appointment.EnterpriseId
                                                    & x.AppointmentStatus.ShortName == Global.Constants.AppointmentStatus.Pending
                                                    & x.StartDate.Day == appointment.StartDate.Day).ToList();

            int quantityAppoint = appointments.Count() - 1;

            result.Id = number;
            result.EnterpriseImage = string.Concat(appointment.Enterprise.ImageContenTypeLong, ',', Utilities.JS_File.GetStrigBase64(appointment.Enterprise.ImagePath));
            result.EnterpriseName = appointment.Enterprise.Name;
            result.EnterpriseAddress = appointment.Enterprise.Address;
            result.EnterprisePhoneNumber = appointment.Enterprise.PhoneNumber;
            result.EnterpriseServiceTime = appointment.Enterprise.ServiceTime;
            result.InFrontMe = quantityAppoint;
            result.Message = string.Concat("Estimado cliente tiene ", quantityAppoint.ToString(), " turno ó cita pendiente delante de usted ");
            result.UserName = appointment.Name;
            result.DocumentNomber = appointment.DocumentNomber;
            result.PhoneNumber = appointment.PhoneNumber;
            result.Comment = appointment.Comment;
            result.StartDate = Convert.ToDateTime(appointment.StartDate).ToString("dd/MM/yyyy");

            return result;
        }


        [HttpGet]
        [Route("GetAppointments")]
        public IHttpActionResult GetAppointments(string startDate, string endDate, int statusId)
        {
            var result = new List<AppointmentDTO>();

            DateTime _startDate = Convert.ToDateTime(startDate);
            DateTime _endDate = Convert.ToDateTime(endDate);

            var enterprise = db.Enterprises.Where(x => x.UserId == currentUserId).FirstOrDefault();

            var userRole = db.UserRoles.Where(x => x.UserId == currentUserId).FirstOrDefault();

            string[] allowViewAllAppointmentByRoles = ConfigurationParameter.AllowViewAllAppointmentByRoles.Split(',');


            string currentDate = DateTime.Now.ToShortDateString();

            string status = "Pendiente";
            if (statusId > 0)
            {
                status = db.AppointmentStatuses.Where(x => x.Id == statusId).Select(y => y.Description).FirstOrDefault();
            }


            if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                if (allowViewAllAppointmentByRoles.Contains(userRole.Role.ShortName))
                {
                    if (statusId > 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true & x.StatusId == statusId & x.ShortStartDate == currentDate).Select(y => new AppointmentDTO()
                        {
                            Id = y.Id,
                            EnterpriseName = y.Enterprise.Name,
                            UserName = y.Name,
                            PhoneNumber = y.PhoneNumber.ToString(),
                            StartDate = y.ShortStartDate,
                            Status = y.AppointmentStatus.Description,
                            StatusColour = y.AppointmentStatus.Colour,

                        }).OrderBy(x => x.Id).ToList();
                    }

                    if (statusId == 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true & x.ShortStartDate == currentDate).Select(y => new AppointmentDTO()
                        {
                            Id = y.Id,
                            EnterpriseName = y.Enterprise.Name,
                            UserName = y.Name,
                            PhoneNumber = y.PhoneNumber.ToString(),
                            StartDate = y.ShortStartDate,
                            Status = y.AppointmentStatus.Description,
                            StatusColour = y.AppointmentStatus.Colour,

                        }).OrderBy(x => x.Id).ToList();
                    }
                }
                else
                {
                    if (statusId > 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true & x.StatusId == statusId & x.ShortStartDate == currentDate & x.EnterpriseId == enterprise.Id).Select(y => new AppointmentDTO()
                        {
                            Id = y.Id,
                            EnterpriseName = y.Enterprise.Name,
                            UserName = y.Name,
                            PhoneNumber = y.PhoneNumber.ToString(),
                            StartDate = y.ShortStartDate,
                            Status = y.AppointmentStatus.Description,
                            StatusColour = y.AppointmentStatus.Colour,

                        }).OrderBy(x => x.Id).ToList();
                    }

                    if (statusId == 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true & x.ShortStartDate == currentDate & x.EnterpriseId == enterprise.Id).Select(y => new AppointmentDTO()
                        {
                            Id = y.Id,
                            EnterpriseName = y.Enterprise.Name,
                            UserName = y.Name,
                            PhoneNumber = y.PhoneNumber.ToString(),
                            StartDate = y.ShortStartDate,
                            Status = y.AppointmentStatus.Description,
                            StatusColour = y.AppointmentStatus.Colour,

                        }).OrderBy(x => x.Id).ToList();
                    }
                }
            }


            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (allowViewAllAppointmentByRoles.Contains(userRole.Role.ShortName))
                {

                    if (statusId > 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true & x.StatusId == statusId
                                                  & x.StartDate >= _startDate & x.StartDate <= _endDate).Select(y => new AppointmentDTO()
                                                  {
                                                      Id = y.Id,
                                                      EnterpriseName = y.Enterprise.Name,
                                                      UserName = y.Name,
                                                      PhoneNumber = y.PhoneNumber.ToString(),
                                                      StartDate = y.ShortStartDate,
                                                      Status = y.AppointmentStatus.Description,
                                                      StatusColour = y.AppointmentStatus.Colour,

                                                  }).OrderBy(x => x.Id).ToList();
                    }

                    if(statusId == 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true
                                                  & x.StartDate >= _startDate & x.StartDate <= _endDate).Select(y => new AppointmentDTO()
                                                  {
                                                      Id = y.Id,
                                                      EnterpriseName = y.Enterprise.Name,
                                                      UserName = y.Name,
                                                      PhoneNumber = y.PhoneNumber.ToString(),
                                                      StartDate = y.ShortStartDate,
                                                      Status = y.AppointmentStatus.Description,
                                                      StatusColour = y.AppointmentStatus.Colour,

                                                  }).OrderBy(x => x.Id).ToList();
                    }
                }
                else
                {
                    if(statusId > 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true & x.StatusId == statusId & x.EnterpriseId == enterprise.Id
                                                    & x.StartDate >= _startDate & x.StartDate <= _endDate).Select(y => new AppointmentDTO()
                                                    {
                                                        Id = y.Id,
                                                        EnterpriseName = y.Enterprise.Name,
                                                        UserName = y.Name,
                                                        PhoneNumber = y.PhoneNumber.ToString(),
                                                        StartDate = y.ShortStartDate,
                                                        Status = y.AppointmentStatus.Description,
                                                        StatusColour = y.AppointmentStatus.Colour,

                                                    }).OrderBy(x => x.Id).ToList();
                    }

                    if (statusId == 0)
                    {
                        result = db.Appointments.Where(x => x.IsActive == true & x.EnterpriseId == enterprise.Id
                                                    & x.StartDate >= _startDate & x.StartDate <= _endDate).Select(y => new AppointmentDTO()
                                                    {
                                                        Id = y.Id,
                                                        EnterpriseName = y.Enterprise.Name,
                                                        UserName = y.Name,
                                                        PhoneNumber = y.PhoneNumber.ToString(),
                                                        StartDate = y.ShortStartDate,
                                                        Status = y.AppointmentStatus.Description,
                                                        StatusColour = y.AppointmentStatus.Colour,

                                                    }).OrderBy(x => x.Id).ToList();
                    }
                }
            }


            if (result.Count() == 0)
            {
                response.Code = "404";
                response.Message = string.Concat("Estimado usuario en estos momentos no tiene turnos ó citas ", status, "s");

                return Ok(response);
            }

            response.Data = result;
            response.Message = string.Concat("Cantidad de registros encontrados ", result.Count().ToString() );

            return Ok(response);
        }


        [HttpGet]
        [Route("UpdateStatus")]
        public IHttpActionResult UpdateStatus(string status, long id)
        {
            int statusId = db.AppointmentStatuses.Where(x => x.ShortName == status).Select(y => y.Id).FirstOrDefault();

            var appointment = db.Appointments.Where(x => x.Id == id).FirstOrDefault();

            appointment.StatusId = statusId;
            appointment.LastModificationTime = DateTime.Now;
            appointment.LastModifierUserId = currentUserId;
            db.SaveChanges();

            response.Message = InternalResponseMessageGood.Message201;
            response.Data = new { Id = appointment.Id };

            return Ok(response);
        }



        private List<AppointmentId> RemoveDuplicateElement(List<AppointmentId> list)
        {
            if (list != null)
            {
                list = list.OrderBy(x => x.Id).ToList();
                int index = 0;
                while (index < list.Count - 1)
                {
                    if (list[index].Id == list[index + 1].Id)
                    {
                        list.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }
            }

            return list;
        }

    }
}

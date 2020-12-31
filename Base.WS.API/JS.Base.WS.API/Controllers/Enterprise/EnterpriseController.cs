using JS.AlertService.Global;
using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Enterprise;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.EnterpriseConf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers
{

    [Authorize]
    [RoutePrefix("api/enterprise")]
    public class EnterpriseController : ApiController
    {

        private MyDBcontext db;
        private Response response;

        private long currentUserId = CurrentUser.GetId();

        public EnterpriseController()
        {
            db = new MyDBcontext();
            response = new Response();
        }


        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<EnterpriseDto> GetAll()
        {
            var result = new List<EnterpriseDto>();

            var userRole = db.UserRoles.Where(x => x.UserId == currentUserId).FirstOrDefault();

            string[] AllowViewAllEnterprisesByRoles = ConfigurationParameter.AllowViewAllEnterprisesByRoles.Split(',');

            if (AllowViewAllEnterprisesByRoles.Contains(userRole.Role.ShortName))
            {
                result = db.Enterprises.Where(y => y.IsActive == true).Select(x => new EnterpriseDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    PropetaryName = x.PropetaryName,
                    Name = x.Name,
                    RNC = x.RNC,
                    CommercialRegister = x.CommercialRegister,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Address = x.Address,
                    Sigla = x.Sigla,
                    Slogan = x.Slogan,
                    WorkSchedule = x.WorkSchedule,
                    AvailableOnlineAppointment = x.AvailableOnlineAppointment,
                    Image = x.Image,
                    ImagePath = x.ImagePath,
                    ImageContenTypeShort = x.ImageContenTypeShort,
                    ImageContenTypeLong = x.ImageContenTypeLong,
                    ServiceTime = x.ServiceTime,
                    CreationTime = x.CreationTime,
                    CreatorUserId = x.CreatorUserId,
                    LastModificationTime = x.LastModificationTime,
                    LastModifierUserId = x.LastModifierUserId,
                    DeletionTime = x.DeletionTime,
                    DeleterUserId = x.DeleterUserId,

                }).OrderByDescending(y => y.Id).ToList();

            }
            else
            {
                result = db.Enterprises.Where(c => c.UserId == currentUserId && c.IsActive == true).Select(x => new EnterpriseDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    PropetaryName = x.PropetaryName,
                    Name = x.Name,
                    RNC = x.RNC,
                    CommercialRegister = x.CommercialRegister,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Address = x.Address,
                    Sigla = x.Sigla,
                    Slogan = x.Slogan,
                    WorkSchedule = x.WorkSchedule,
                    AvailableOnlineAppointment = x.AvailableOnlineAppointment,
                    Image = x.Image,
                    ImagePath = x.ImagePath,
                    ImageContenTypeShort = x.ImageContenTypeShort,
                    ImageContenTypeLong = x.ImageContenTypeLong,
                    CreationTime = x.CreationTime,
                    ServiceTime = x.ServiceTime,
                    CreatorUserId = x.CreatorUserId,
                    LastModificationTime = x.LastModificationTime,
                    LastModifierUserId = x.LastModifierUserId,
                    DeletionTime = x.DeletionTime,
                    DeleterUserId = x.DeleterUserId,

                }).OrderByDescending(y => y.Id).ToList();

            }

            return result;
        }


        [HttpGet]
        [Route("GetById")]
        public EnterpriseDto GetById(long id)
        {
            var result = new EnterpriseDto();

            result = db.Enterprises.Where(y => y.Id == id && y.IsActive == true).Select(x => new EnterpriseDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                PropetaryName = x.PropetaryName,
                Name = x.Name,
                RNC = x.RNC,
                CommercialRegister = x.CommercialRegister,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Address = x.Address,
                Sigla = x.Sigla,
                Slogan = x.Slogan,
                WorkSchedule = x.WorkSchedule,
                AvailableOnlineAppointment = x.AvailableOnlineAppointment,
                Image = x.Image,
                ImagePath = x.ImagePath,
                ImageContenTypeShort = x.ImageContenTypeShort,
                ImageContenTypeLong = x.ImageContenTypeLong,
                ServiceTime = x.ServiceTime,
                CreationTime = x.CreationTime,
                CreatorUserId = x.CreatorUserId,
                LastModificationTime = x.LastModificationTime,
                LastModifierUserId = x.LastModifierUserId,
                DeletionTime = x.DeletionTime,
                DeleterUserId = x.DeleterUserId,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,

            }).FirstOrDefault();

            result.Image = string.Concat(result.ImageContenTypeLong, ',', Utilities.JS_File.GetStrigBase64(result.ImagePath));

            return result;
        }


        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] Enterprise request)
        {
            //Validate enterprise
            long enterpriseId = db.Enterprises.Where(x => x.UserId == currentUserId && x.IsActive == true).Select(y => y.Id).FirstOrDefault();
            if (enterpriseId > 0)
            {
                response.Code = InternalResponseCodeError.Code325;
                response.Message = InternalResponseCodeError.Message325;

                return Ok(response);
            }

            var fileTypeAlloweds = ConfigurationParameter.ImgTypeAllowed.Split(',');

            string root = ConfigurationParameter.EnterpriseImgDirectory;
            string[] arrayImgBase64 = request.Image.Split(',');
            string imgBase64 = arrayImgBase64[arrayImgBase64.Length - 1];

            string[] splitName1 = arrayImgBase64[0].Split('/');
            string[] splitName2 = splitName1[1].Split(';');
            string contentType = splitName2[0];

            //Validate contentType
            if (!fileTypeAlloweds.Contains(contentType))
            {
                response.Code = InternalResponseCodeError.Code324;
                response.Message = InternalResponseCodeError.Message324;

                return Ok(response);
            }

            request.Image = string.Empty;
            request.ImagePath = string.Empty;
            request.ImageContenTypeShort = contentType;
            request.ImageContenTypeLong = arrayImgBase64[0];
            request.UserId = currentUserId;
            request.CreationTime = DateTime.Now;
            request.CreatorUserId = currentUserId;
            request.IsActive = true;

            var resp = db.Enterprises.Add(request);
            db.SaveChanges();

            //Save img
            var fileName = string.Concat("Enterprise_img_", resp.Id.ToString());
            var filePath = Path.Combine(root, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllBytes(filePath, Convert.FromBase64String(imgBase64));

            //update Novelty
            resp.ImagePath = filePath;
            db.SaveChanges();

            response.Data = new { Id = resp.Id };
            response.Message = InternalResponseMessageGood.Message200;

            return Ok(response);
        }


        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] Enterprise request)
        {
            var fileTypeAlloweds = ConfigurationParameter.ImgTypeAllowed.Split(',');

            string[] arrayImgBase64 = request.Image.Split(',');
            string imgBase64 = arrayImgBase64[arrayImgBase64.Length - 1];

            string[] splitName1 = arrayImgBase64[0].Split('/');
            string[] splitName2 = splitName1[1].Split(';');
            string contentType = splitName2[0];

            string root = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;

            //Validate contentType
            if (!fileTypeAlloweds.Contains(contentType) & arrayImgBase64.Count() > 1)
            {
                response.Code = InternalResponseCodeError.Code324;
                response.Message = InternalResponseCodeError.Message324;

                return Ok(response);
            }

            request.Image = string.Empty;
            request.ImageContenTypeShort = contentType;
            request.ImageContenTypeLong = arrayImgBase64[0];
            request.LastModificationTime = DateTime.Now;
            request.LastModifierUserId = currentUserId;

            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            //Validate path
            if (string.IsNullOrEmpty(request.ImagePath))
            {
                root = ConfigurationParameter.EnterpriseImgDirectory;
                fileName = string.Concat("Enterprise_img_", request.Id.ToString());
                filePath = Path.Combine(root, fileName);
                request.ImagePath = filePath;

                var enterprise = db.Enterprises.Where(x => x.Id == request.Id).FirstOrDefault();
                enterprise.ImagePath = filePath;
                db.SaveChanges();
            }

            //Save img
            if (File.Exists(request.ImagePath))
            {
                File.Delete(request.ImagePath);
            }

            File.WriteAllBytes(request.ImagePath, Convert.FromBase64String(imgBase64));

            response.Message = InternalResponseMessageGood.Message201;

            return Ok(response);
        }


        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(long Id)
        {
            var result = db.Enterprises.Where(x => x.Id == Id & x.IsActive == true).FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            result.IsActive = false;
            result.IsDeleted = true;
            result.DeletionTime = DateTime.Now;
            result.DeleterUserId = currentUserId;
            db.SaveChanges();

            response.Message = InternalResponseMessageGood.Message202;

            return Ok(response);
        }

    }
}

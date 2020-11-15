using JS.Base.WS.API.Base;
using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.Publicity;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.Publicity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using JS.Utilities;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Publicity
{
    [RoutePrefix("api/novelty")]
    [Authorize]
    public class NoveltyController : ApiController
    {
        private MyDBcontext db;
        private Response response;


        public NoveltyController()
        {
            db = new MyDBcontext();
            response = new Response();
        }

        private long currentUserId = CurrentUser.GetId();

        [HttpGet]
        [Route("GetNovelties")]
        public IEnumerable<NoveltyDto> GetNovelties()
        {
            var result = db.Novelties.Where(x => x.IsActive == true).Select(y => new NoveltyDto()
            {
                Id = y.Id,
                Title = y.Title,
                Description = y.Description,
                IsEnabled = y.IsEnabled,
                NoveltyTypeId = y.NoveltyTypeId,
                NoveltyType = y.NoveltyType.Description,
                Img = y.Img,
                ImgPath = y.ImgPath,
                StartDate = y.StartDate,
                EndDate = y.EndDate,
                IsPublic = y.IsPublic,
                CreationTime = y.CreationTime,
                CreatorUserId = y.CreatorUserId,
                LastModificationTime = y.LastModificationTime,
                LastModifierUserId = y.LastModifierUserId,
                DeletionTime = y.DeletionTime,
                DeleterUserId = y.DeleterUserId,
            }).OrderByDescending(x => x.Id).ToList();

            return result;
        }


        [HttpGet]
        [Route("GetNoveltyById")]
        public NoveltyDto GetNoveltyById(long Id)
        {
            var result = db.Novelties.Where(x => x.IsActive == true & x.Id == Id).Select(y => new NoveltyDto() {
                Id = y.Id,
                Title = y.Title,
                Description = y.Description,
                IsEnabled = y.IsEnabled,
                NoveltyTypeId = y.NoveltyTypeId,
                NoveltyType = y.NoveltyType.Description,
                Img = y.Img,
                ImgPath = y.ImgPath,
                ContenTypeShort = y.ContenTypeShort,
                ContenTypeLong = y.ContenTypeLong,
                StartDate = y.StartDate,
                EndDate = y.EndDate,
                IsPublic = y.IsPublic,
                IsActive = y.IsActive,
                IsDeleted = y.IsDeleted,
                CreationTime = y.CreationTime,
                CreatorUserId = y.CreatorUserId,
                LastModificationTime = y.LastModificationTime,
                LastModifierUserId = y.LastModifierUserId,
                DeletionTime = y.DeletionTime,
                DeleterUserId = y.DeleterUserId,
            }).FirstOrDefault();
            result.Img = string.Concat(result.ContenTypeLong, ',', JS_File.GetStrigBase64(result.ImgPath));

            return result;
        }


        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create([FromBody] Novelty request)
        {
            var fileTypeAlloweds = ConfigurationParameter.ImgTypeAllowed.Split(',');

            string root = ConfigurationParameter.PublicityFileDirectory;
            string[] arrayImgBase64 = request.Img.Split(',');
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

            request.Img = string.Empty;
            request.ImgPath = string.Empty;
            request.ContenTypeShort = contentType;
            request.ContenTypeLong = arrayImgBase64[0];
            request.CreationTime = DateTime.Now;
            request.CreatorUserId = currentUserId;
            request.IsActive = true;

            var resp = db.Novelties.Add(request);
            db.SaveChanges();

            //Save img
            var fileName = string.Concat("Novelty_img_", resp.Id.ToString());
            var filePath = Path.Combine(root, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllBytes(filePath, Convert.FromBase64String(imgBase64));

            //update Novelty
            resp.ImgPath = filePath;
            db.SaveChanges();

            response.Data = new { Id = resp.Id };
            response.Message = InternalResponseMessageGood.Message200;

            return Ok(response);
        }


        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] Novelty request)
        {
            var fileTypeAlloweds = ConfigurationParameter.ImgTypeAllowed.Split(',');

            string[] arrayImgBase64 = request.Img.Split(',');
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

            request.Img = string.Empty;
            request.ContenTypeShort = contentType;
            request.ContenTypeLong = arrayImgBase64[0];
            request.LastModificationTime = DateTime.Now;
            request.LastModifierUserId = currentUserId;

            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();

            //Validate path
            if (string.IsNullOrEmpty(request.ImgPath))
            {
                root = ConfigurationParameter.PublicityFileDirectory;
                fileName = string.Concat("Novelty_img_", request.Id.ToString());
                filePath = Path.Combine(root, fileName);
                request.ImgPath = filePath;

                var novelty = db.Novelties.Where(x => x.Id == request.Id).FirstOrDefault();
                novelty.ImgPath = filePath;
                db.SaveChanges();
            }

            //Save img
            if (File.Exists(request.ImgPath))
            {
                File.Delete(request.ImgPath);
            }

            File.WriteAllBytes(request.ImgPath, Convert.FromBase64String(imgBase64));

            response.Message = InternalResponseMessageGood.Message201;

            return Ok(response);
        }


        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(long Id)
        {
            var result = db.Novelties.Where(x => x.Id == Id & x.IsActive == true).FirstOrDefault();

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


        [HttpGet]
        [Route("GetNoveltyTypes")]
        public IEnumerable<NoveltyTypeDto> GetNoveltyTypes()
        {
            var result = db.NoveltyTypes.Select(y => new NoveltyTypeDto(){
                Id = y.Id,
                ShortName = y.ShortName,
                Description = y.Description,
            }).ToList();

            return result;
        }

    }
}

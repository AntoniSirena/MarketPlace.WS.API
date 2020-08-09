using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.FileDocument;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers
{

    [RoutePrefix("api/file")]
    [Authorize]
    public class FileUploadController : ApiController
    {
        private Response response;
        private FileDocument fileDocumentService;
        private MyDBcontext db;
        private long currentUserId;

        public FileUploadController()
        {
            response = new Response();
            fileDocumentService = new FileDocument();
            db = new MyDBcontext();
            currentUserId = CurrentUser.GetId();
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IHttpActionResult> UploadFile()
        {
            var ctx = HttpContext.Current;
            var root = ConfigurationParameter.FileDirectory;
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
              await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');
                    string[] splitName = name.Split('.');
                    string contentType = splitName[splitName.Length - 1];

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.Move(localFileName, filePath);

                    //Save file info in Data Base
                    fileDocumentService.SaveFile(name, filePath, true, contentType);

                }
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }

            response.Message = "Archivo guardado exitosamente";

            return Ok(response);
        }


        [HttpGet]
        [Route("GetFileById")]
        public IHttpActionResult GetFileById(long id)
        {
            string result = string.Empty;

            string path = db.FileDocuments.Where(x => x.Id == id).Select(y => y.Path).FirstOrDefault();

            byte[] file = File.ReadAllBytes(path);

            result = Convert.ToBase64String(file);

            return Ok(result);
        }


        [HttpPost]
        [Route("SaveFile")]
        public async Task<IHttpActionResult> SaveFile(object image)
        {
            var filePath = Path.GetTempFileName();

            

            using (var stream = new FileStream(filePath, FileMode.Create))
            {

            }

                return Ok();
        }


        [HttpGet]
        [Route("GetFiles")]
        public IHttpActionResult GetFiles()
        {
            var response = new List<FileDocumentDto>();

            long userVisitor = db.Users.Where(x => x.Id == currentUserId && x.IsVisitorUser == true).Select(y => y.Id).FirstOrDefault();

            if (userVisitor > 0)
            {
                response = db.FileDocuments.Where(x => x.IsActive == true && x.IsPublic == true).Select(y => new FileDocumentDto()
                {
                    Id = y.Id,
                    Name = y.Name,
                    Description = y.Description,
                    ContentType = y.ContentType,
                    Path = y.Path,
                    IsPublic = y.IsPublic,
                }).OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                response = db.FileDocuments.Where(x => x.IsActive == true).Select(y => new FileDocumentDto()
                {
                    Id = y.Id,
                    Name = y.Name,
                    Description = y.Description,
                    ContentType = y.ContentType,
                    Path = y.Path,
                    IsPublic = y.IsPublic,
                }).OrderByDescending(x => x.Id).ToList();
            }

            return Ok(response);
        }

    }
}

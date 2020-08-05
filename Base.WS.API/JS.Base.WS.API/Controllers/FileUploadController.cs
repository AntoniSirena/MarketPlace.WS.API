using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
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

        public FileUploadController()
        {
            response = new Response();
            fileDocumentService = new FileDocument();
            db = new MyDBcontext();
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IHttpActionResult> UploadFile()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/APP_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
              await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.Move(localFileName, filePath);

                    //Save file info in Data Base
                    fileDocumentService.SaveFile(name, filePath);

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
        [Route("GetFile")]
        public IHttpActionResult GetFile(string name)
        {
            string result = string.Empty;

            string path = db.FileDocuments.Where(x => x.Name == name).Select(y => y.Path).FirstOrDefault();

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


    }
}

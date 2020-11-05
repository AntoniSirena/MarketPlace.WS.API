using JS.Alert.WS.API.Base;
using JS.Alert.WS.API.DBContext;
using System.Web.Http;
using JS.Alert.WS.API.DTO.Request.Alert;
using JS.Alert.WS.API.Services;
using System.Linq;

namespace JS.Alert.WS.API.Controllers.Alerts
{
    [RoutePrefix("api/alert")]
    [AllowAnonymous]
    public class AlertController : BaseController
    {
        private MyDBcontext db;
        private AlertService alertService;

        public AlertController()
        {
            db = new MyDBcontext();
            alertService = new AlertService();
        }


        [HttpPost]
        [Route("SendMail")]
        public ClientResponse<bool> SendMail(Mail request)
        {
            var response = new ClientResponse<bool>();

            var result = alertService.SendMail(request);

            response.Data = result;

            return response;
        }


        [HttpGet]
        [Route("GetOperation")]
        public string GetOperation(string shortName)
        {
            string response = db.Operations.Where(x => x.ShortName == shortName & x.IsEnabled == true & x.IsActive == true).Select(y => y.Body).FirstOrDefault();

            return response;
        }

    }
}


using System.Web.Mvc;

namespace JS.Base.WS.API.Services.IServices.IDomain
{
    interface IOrderService
    {
        string GeneratePDF(long orderId);
    }
}

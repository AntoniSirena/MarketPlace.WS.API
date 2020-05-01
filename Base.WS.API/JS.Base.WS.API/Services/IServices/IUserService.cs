using JS.Base.WS.API.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Base.WS.API.Services.IServices
{
    public interface IUserService
    {
        List<UserStatus> GetUserStatuses();

    }
}

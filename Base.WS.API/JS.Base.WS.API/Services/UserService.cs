using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Services
{
    public class UserService
    {
        MyDBcontext db = new MyDBcontext();

        public List<UserStatus> GetUserStatuses()
        {
            var result = db.UserStatus.Select(x => new UserStatus
            {
                Id = x.Id,
                ShortName = x.ShortName,
                Description = x.Description,
                Colour = x.Colour,
            }).ToList();

            return result;
        }
    }
}
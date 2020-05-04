using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Services
{
    public class CommonService: ICommonService
    {
        MyDBcontext db = new MyDBcontext();

        public List<RoleDto> GetRoles()
        {
            var result = db.Roles
                         .Where(x => x.IsActive == true)
                         .Select(x => new RoleDto
                         {
                             Id = x.Id,
                             Description = x.Description,
                             ShortName = x.ShortName,
                             Parent = x.Parent,
                         }).ToList();

            return result;
        }


        public List<UserDto> GetUsers()
        {
            var result = db.Users
                        .Where(x => x.IsActive == true)
                        .Select(x => new UserDto
                        {
                            Id = x.Id,
                            UserName = x.UserName,
                            FullName = x.Person == null ? null : x.Person.FullName,
                        }).ToList();

            return result;
        }
    }
}
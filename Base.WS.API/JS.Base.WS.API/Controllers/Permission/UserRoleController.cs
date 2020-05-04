using JS.Base.WS.API.Controllers.Generic;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.Models.Permission;
using JS.Base.WS.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Permission
{
    [Authorize]
    [RoutePrefix("api/userRole")]
    public class UserRoleController : GenericApiController<UserRole>
    {
        private MyDBcontext db;
        private UserRoleService UserRoleService;
        private CommonService CommonService;

        public UserRoleController()
        {
            db = new MyDBcontext();
            UserRoleService = new UserRoleService();
            CommonService = new CommonService();
        }

        public override IHttpActionResult GetAll()
        {
            var result = UserRoleService.GetAll();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        public override IHttpActionResult GetById(int id)
        {
            var result = UserRoleService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [Route("GetRoles")]
        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            var result = CommonService.GetRoles();

            return Ok(result);
        }

        [Route("GetUsers")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var result = CommonService.GetUsers();

            var UserRoles = db.UserRoles
                            .Select(x => new UserDto {
                                Id = x.UserId,
                                UserName = x.User.UserName,
                                FullName = x.User.Person == null ? null : x.User.Person.FullName,
                            })
                            .ToList();

            result = (from Us in result
                         where !(from UsR in UserRoles
                                 select UsR.Id)
                                 .Contains(Us.Id)
                         select Us).OrderByDescending(x => x.Id).ToList();

            return Ok(result);
        }
    }
}

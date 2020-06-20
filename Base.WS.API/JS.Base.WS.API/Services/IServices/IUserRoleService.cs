using JS.Base.WS.API.DTO.Response.User;
using JS.Base.WS.API.Models.Permission;
using System.Collections.Generic;


namespace JS.Base.WS.API.Services.IServices
{
    public interface IUserRoleService
    {
        List<UserRoleDto> GetAll();
        UserRoleDto GetById(long Id);
        bool CreateUserRol(long UserId, string code);
        bool ValidateSecurityCode(string code);
    }
}

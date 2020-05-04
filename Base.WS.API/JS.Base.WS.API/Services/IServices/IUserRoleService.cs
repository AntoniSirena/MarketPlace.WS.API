using JS.Base.WS.API.DTO.Response.User;
using System.Collections.Generic;


namespace JS.Base.WS.API.Services.IServices
{
    public interface IUserRoleService
    {
        List<UserRoleDto> GetAll();
        UserRoleDto GetById(long Id);
    }
}

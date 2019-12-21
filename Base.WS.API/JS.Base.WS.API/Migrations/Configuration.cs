namespace JS.Base.WS.API.Migrations
{
    using JS.Base.WS.API.Models.Authorization;
    using JS.Base.WS.API.Models.PersonProfile;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JS.Base.WS.API.DBContext.MyDBcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JS.Base.WS.API.DBContext.MyDBcontext context)
        {
            //Locator Types
            context.LocatorTypes.AddOrUpdate(
              p => p.Description,
              new LocatorType { Code = "01", Description = "Direccion" },
              new LocatorType { Code = "02", Description = "Telefono Resid" },
              new LocatorType { Code = "03", Description = "Cellular" },
              new LocatorType { Code = "04", Description = "Correo" },
              new LocatorType { Code = "05", Description = "Persona" }
              );

            //System user
            context.Users.AddOrUpdate(
              p => p.UserName,
              new User { UserName = "system", Password = "system*123", Name = "System", Surname = "System", EmailAddress = "system@hotmail.com", StatusId = 1, CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false },
              new User { UserName = "admin", Password = "admin*123", Name = "Admin", Surname = "Admin", EmailAddress = "admin@hotmail.com", StatusId = 1, CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false }
            );

            context.UserStatus.AddOrUpdate(
                x => x.ShortName,
                new UserStatus { Description = "Active", ShortName = "Active", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now},
                new UserStatus { Description = "Inactive", ShortName = "Inactive", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now}
                );

        }
    }
}

namespace JS.Base.WS.API.Migrations
{
    using JS.Base.WS.API.Models;
    using JS.Base.WS.API.Models.Authorization;
    using JS.Base.WS.API.Models.Domain;
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
            //context.Users.AddOrUpdate(
            //  p => p.UserName,
            //  new User { UserName = "system", Password = "system*123", Name = "System", Surname = "System", PersonId = 6, EmailAddress = "system@hotmail.com", StatusId = 1, CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false },
            //  new User { UserName = "admin", Password = "admin*123", Name = "Admin", Surname = "Admin", PersonId = 6, EmailAddress = "admin@hotmail.com", StatusId = 1, CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false }
            //);

            context.UserStatus.AddOrUpdate(
                x => x.ShortName,
                new UserStatus { ShortName = "Active", Description = "Activo", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now, Colour = "rgb(52, 212, 146)" },
                new UserStatus { ShortName = "Inactive", Description = "Inactivo", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now, Colour = "rgb(241, 81, 81)" },
                new UserStatus { ShortName = "PendingToActive", Description = "Pendiente de activar", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now, Colour = "rgb(243, 183, 71)" }
                );

            context.Genders.AddOrUpdate(
                x => x.Description,
                new Gender { ShortName = "M", Description = "Maculino", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now },
                new Gender { ShortName = "F", Description = "Femenino", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now }
                );

            //Document Types
            context.DocumentTypes.AddOrUpdate(
                x => x.Description,
                new DocumentType { ShortName = "Cédula", Description = "Cédula", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now },
                new DocumentType { ShortName = "Pasaporte", Description = "Pasaporte", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now },
                new DocumentType { ShortName = "RNC", Description = "RNC", IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now }
                );

            //RequestStatuses
            context.RequestStatus.AddOrUpdate(
                x => x.ShortName,
                new RequestStatus { ShortName = "InProcess", Name = "En proceso", Colour = "rgb(52, 212, 146)", AllowEdit = true, IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now },
                new RequestStatus { ShortName = "Completed", Name = "Completada", Colour = "rgb(59, 159, 199)", AllowEdit = false, IsActive = true, CreatorUserId = 1, CreationTime = DateTime.Now }
                );

        }
    }
}

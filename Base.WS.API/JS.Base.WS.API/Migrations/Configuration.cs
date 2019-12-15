namespace JS.Base.WS.API.Migrations
{
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

            //System person
            context.People.AddOrUpdate(
              p => p.FirstName,
              new Person { FirstName = "System", SecondName = "System", Surname = "System", secondSurname = "System", BirthDate = "2019-11-16", CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false}
            );

        }
    }
}

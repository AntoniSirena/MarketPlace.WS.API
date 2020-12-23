namespace JS.Base.WS.API.Migrations
{
    using JS.Base.WS.API.Models;
    using JS.Base.WS.API.Models.Authorization;
    using JS.Base.WS.API.Models.Domain;
    using JS.Base.WS.API.Models.PersonProfile;
    using JS.Base.WS.API.Models.Publicity;
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

            context.UserStatus.AddOrUpdate(
                x => x.ShortName,
                new UserStatus { ShortName = "Active", Description = "Activo", IsActive = true, ShowToCustomer = true, CreatorUserId = 1, CreationTime = DateTime.Now, Colour = "btn btn-success" },
                new UserStatus { ShortName = "Inactive", Description = "Inactivo", IsActive = true, ShowToCustomer = true, CreatorUserId = 1, CreationTime = DateTime.Now, Colour = "btn btn-danger" },
                new UserStatus { ShortName = "PendingToActive", Description = "Pendiente de activar", IsActive = true, ShowToCustomer = true, CreatorUserId = 1, CreationTime = DateTime.Now, Colour = "btn btn-warning" },
                new UserStatus { ShortName = "PendingToChangePassword", Description = "Pendiente de cambiar contraseña", IsActive = true, ShowToCustomer = false, CreatorUserId = 1, CreationTime = DateTime.Now, Colour = "btn btn-primary" }
                );

            int userStatusId = context.UserStatus.Where(x => x.ShortName == "Active").Select(x => x.Id).FirstOrDefault();

            //System users
            //context.Users.AddOrUpdate(
            //  p => p.UserName,
            //  new User { UserName = "system", Password = "1tH03LsSOvhmKWdrAIHhCPDFBwMPEkmzzS+ePUfK74g=", Name = "System", Surname = "System", PersonId = null, EmailAddress = "antoni.sirena@gmail.com", PhoneNumber = "8299093042", StatusId = userStatusId, CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false },
            //  new User { UserName = "admin", Password = "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", Name = "Admin", Surname = "Admin", PersonId = null, EmailAddress = "antoni.sirena@gmail.com", PhoneNumber = "8299093042", StatusId = userStatusId, CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false },
            //  new User { UserName = "visitador", Password = "Yo5Nrsy7ye8BfPEmd/i5Pk65+VW1g7ud9FE+WBqoZ4c=", Name = "Visitador", Surname = "Visitador", IsVisitorUser = true, PersonId = null, EmailAddress = "antoni.sirena@gmail.com", PhoneNumber = "8299093042", StatusId = userStatusId, CreationTime = DateTime.Now, CreatorUserId = 1, IsActive = true, IsDeleted = false }
            //);

            long userId = context.Users.Where(x => x.UserName == "system").Select(x => x.Id).FirstOrDefault();

            context.UserTypes.AddOrUpdate(
                x => x.ShortName,
                new UserType { ShortName = "Person", Description = "Persona", ShowToCustomer = true, IsActive = true, CreatorUserId = userId, CreationTime = DateTime.Now },
                new UserType { ShortName = "Enterprise", Description = "Empresa", ShowToCustomer = true, IsActive = true, CreatorUserId = userId, CreationTime = DateTime.Now }
                );

            context.Genders.AddOrUpdate(
                x => x.ShortName,
                new Gender { ShortName = "M", Description = "Maculino", IsActive = true, CreatorUserId = userId, CreationTime = DateTime.Now },
                new Gender { ShortName = "F", Description = "Femenino", IsActive = true, CreatorUserId = userId, CreationTime = DateTime.Now }
                );

            //Document Types
            context.DocumentTypes.AddOrUpdate(
                x => x.ShortName,
                new DocumentType { ShortName = "Cédula", Description = "Cédula", ShowToCustomer = true, IsActive = true, CreatorUserId = userId, CreationTime = DateTime.Now },
                new DocumentType { ShortName = "Pasaporte", Description = "Pasaporte", ShowToCustomer = false, IsActive = true, CreatorUserId = userId, CreationTime = DateTime.Now },
                new DocumentType { ShortName = "RNC", Description = "RNC", ShowToCustomer = false, IsActive = true, CreatorUserId = userId, CreationTime = DateTime.Now }
                );


            //Appointment Statuses
            context.AppointmentStatuses.AddOrUpdate(
                x => x.ShortName,
                new AppointmentStatus { ShortName = "OnHold", Description = "En espera", ShowToCustomer = true, Colour = "" },
                new AppointmentStatus { ShortName = "InProcess", Description = "En proceso", ShowToCustomer = true, Colour = "" },
                new AppointmentStatus { ShortName = "Finished", Description = "Finalizada", ShowToCustomer = true, Colour = "" },
                new AppointmentStatus { ShortName = "Cancelled", Description = "Cancelada", ShowToCustomer = true, Colour = "" }
                );


            //NoveltyTypes
            context.NoveltyTypes.AddOrUpdate(
                x => x.ShortName,
                new NoveltyType { ShortName = "Sporty", Description = "Deporte" },
                new NoveltyType { ShortName = "Politics", Description = "Política" },
                new NoveltyType { ShortName = "Show", Description = "Espectáculo" },
                new NoveltyType { ShortName = "Social", Description = "Social" },
                new NoveltyType { ShortName = "Economy", Description = "Economía" },
                new NoveltyType { ShortName = "Art", Description = "Arte" },
                new NoveltyType { ShortName = "Police", Description = "Policiale" },
                new NoveltyType { ShortName = "Science", Description = "Ciencia" },
                new NoveltyType { ShortName = "Education", Description = "Educación" }
                );

            //Comapy Categories
            context.CompanyCategories.AddOrUpdate(
                x => x.ShortName,
                new CompanyCategory { ShortName = "Food", Description = "Comida", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "ProvisionsMerchant", Description = "Comerciante y Detallista de Provisione", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Technology", Description = "Tecnología: (Computadora, Sistema, Seguridad y Celular)", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Pharmacy", Description = "Farmacia", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "HardwareStoreReplacement", Description = "Ferretería y Repuesto", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "BeautyStyle", Description = "Salon, Uña y Barberí", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Workshops", Description = "Taller: (Mecánica, Soldadura & Herrería, Puerta & Ventana y Tapicería)", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Driver", Description = "Taxi y Delivery", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Auto", Description = "RentCar y Dealer", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "CivilEngineer", Description = "Ingeniería Civil: (Agrimensura, Construcción, Electricidad y Plomería)", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "HotelCottage", Description = "Hotel y Casa de campo", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Entertainment", Description = "Entretenimiento: (Club Night, Billar, Drink y Piscina)", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Lawyer", Description = "Abogado", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Store", Description = "Tienda", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Accounting", Description = "Contabilidad", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "GraphicDesign", Description = "Diseño: (Gráfico, de Moda y más)", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Nursery", Description = "Vivero", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId },
                new CompanyCategory { ShortName = "Health", Description = "Salud: (Clinicas y Hospitales)", IsActive = true, CreationTime = DateTime.Now, CreatorUserId = userId }

                );
        }
    }
}

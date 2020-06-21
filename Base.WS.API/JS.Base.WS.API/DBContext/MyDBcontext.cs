using JS.Base.WS.API.Models;
using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Models.Configuration;
using JS.Base.WS.API.Models.Domain;
using JS.Base.WS.API.Models.Permission;
using JS.Base.WS.API.Models.PersonProfile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.DBContext
{
    public class MyDBcontext: DbContext
    {

        public MyDBcontext() : base("name=Defult")
        {

        }

        //metodo para eliminar la plurarizacion de las entidades
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<LocatorType> LocatorTypes { get; set; }
        public virtual DbSet<Locator> Locators { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityAction> EntityActions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public virtual DbSet<ConfigurationParameter> ConfigurationParameters { get; set; }
        public virtual DbSet<PersonType> PersonTypes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }



        //Domin
        public virtual DbSet<Regional> Regionals { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<EducativeCenter> EducativeCenters { get; set; }
        public virtual DbSet<Tanda> Tandas { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Docent> Docents { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Indicator> Indicators { get; set; }


    }
}
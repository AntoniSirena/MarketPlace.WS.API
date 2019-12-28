using JS.Base.WS.API.Models.Authorization;
using JS.Base.WS.API.Models.Permission;
using JS.Base.WS.API.Models.PersonProfile;
using System;
using System.Collections.Generic;
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
        public virtual DbSet<Role> Rols { get; set; }
        public virtual DbSet<UserRol> UserRols { get; set; }
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityAction> EntityActions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }

    }
}
using JS.Alert.WS.API.Models;
using System.Data.Entity;

namespace JS.Alert.WS.API.DBContext
{
    public class MyDBcontext : DbContext
    {
        public MyDBcontext() : base("name=JS.Alert")
        {

        }


        public virtual DbSet<Operation> Operations { get; set; }

    }
}
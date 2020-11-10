
using Hangfire;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(JS.Base.WS.API.Startup))]

namespace JS.Base.WS.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(Global.Constants.ConnectionStrings.JSBase);

            app.UseHangfireDashboard();
            app.UseHangfireServer();


            //RecurringJob.AddOrUpdate(() => SendEmail(), Cron.Minutely());

        }

        public void SendEmail()
        {
            Console.WriteLine("Correo enviado de forma exitosa.");
        }

        public void SendSMS()
        {
            Console.WriteLine("Correo enviado de forma exitosa.");
        }
    }
}

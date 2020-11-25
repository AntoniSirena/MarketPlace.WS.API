
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
            

        }
    }
}

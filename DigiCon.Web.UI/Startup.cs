
using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigiCon.Startup))]
namespace DigiCon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
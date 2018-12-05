using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ASP_Chat.Startup))]

namespace ASP_Chat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}

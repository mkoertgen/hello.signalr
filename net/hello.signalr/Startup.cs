using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace hello.signalr
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Startup
    {
        // ReSharper disable once UnusedMember.Global
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.Configuration.DefaultMessageBufferSize = 100;
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(6);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(2);

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
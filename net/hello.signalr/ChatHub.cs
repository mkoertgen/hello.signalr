using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace hello.signalr
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            LogInfo($"{nameof(Send)}({nameof(name)}:'{name}', {nameof(message)}:'{message}')");
            Clients.All.broadcastMessage(name, message);
        }

        public override Task OnConnected()
        {
            LogInfo();
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            LogInfo($"{nameof(OnDisconnected)}(stopCalled:{stopCalled})");
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            LogInfo();
            return base.OnReconnected();
        }

        private static void LogInfo([CallerMemberName] string message = null)
        {
            if (!string.IsNullOrWhiteSpace(message)) Trace.TraceInformation(message);
        }
    }

}
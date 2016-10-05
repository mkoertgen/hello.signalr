using System;
using Microsoft.AspNet.SignalR.Client;

namespace ChatClient
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var url = args.Length > 1 ? args[1] : "http://localhost:8080";
            var conn = new HubConnection(url);


            var hubProxy = conn.CreateHubProxy("ChatHub");
            hubProxy.On<string, string>("BroadcastMessage", (name, message) =>
            {
                Console.WriteLine($"[{name}]: '{message}')");
            });

            conn.Start().Wait();
            Console.WriteLine($"Connected to '{url}' (using transport '{conn.Transport.Name}').");
            Console.WriteLine("Start typing to chat. 'exit' to quit.");
            var line = Console.ReadLine();
            while (line != "exit")
            {
                hubProxy.Invoke<string, string>("Send", null, "Console:C#", line);
                line = Console.ReadLine();
            }

            conn.Stop();
        }
    }
}

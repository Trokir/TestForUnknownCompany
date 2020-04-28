using System;
using System.Net;

namespace ClientApp.Data.Interfaces
{
    public interface IAsynchronousClient
    {
        event EventHandler<string> SendContent; 
        void StartClient(IPHostEntry ipHostInfo ,IPAddress ipAddress,IPEndPoint localEndPoint);
        string GetText{ get; set; } 
    }
}
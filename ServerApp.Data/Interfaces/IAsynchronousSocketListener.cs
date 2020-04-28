using System;
using System.Net;
using System.Text;

namespace ServerApp.Data.Interfaces
{
    public interface IAsynchronousSocketListener
    {
        event EventHandler<string> SendContent; 
        void StartListening(IPHostEntry ipHostInfo ,IPAddress ipAddress,IPEndPoint localEndPoint);
       
    }
}
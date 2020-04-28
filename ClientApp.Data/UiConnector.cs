using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Data.Interfaces;

namespace ClientApp.Data
{
   public class UiConnector
   {
       private readonly IAsynchronousClient _client;
       private  readonly IPHostEntry _ipHostInfo;
       private  readonly IPAddress _ipAddress;
       private readonly IPEndPoint _localEndPoint;
       private const int Port = 8888;
       public event EventHandler<string> RefreshData;
       public UiConnector()
       {
           _ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
           _ipAddress = _ipHostInfo.AddressList[0];
           _localEndPoint = new IPEndPoint(_ipAddress, Port);
           _client = new AsynchronousClient();
            _client.SendContent += _client_SendContent;
       }

        private void _client_SendContent(object sender, string e)
        {
            RefreshData?.Invoke(this,e);
        }

        public void ConnectToServer(string text)
       {
           _client.GetText=text;
           Task.Factory.StartNew(() => _client.StartClient(_ipHostInfo, _ipAddress, _localEndPoint));
       }
   }
}

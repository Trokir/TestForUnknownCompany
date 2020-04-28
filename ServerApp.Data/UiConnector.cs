using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServerApp.Data.Interfaces;

namespace ServerApp.Data
{
	public class UiConnector
	{
		public string Content;
		private readonly IAsynchronousSocketListener _listener;
		private  readonly IPHostEntry _ipHostInfo;
		private  readonly IPAddress _ipAddress;
		private readonly IPEndPoint _localEndPoint;
		private const int _port = 8888;

		public event EventHandler<string> RefreshData;
		public UiConnector()
		{
			_ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			_ipAddress = _ipHostInfo.AddressList[0];
			_localEndPoint = new IPEndPoint(_ipAddress, _port);
			_listener = new AsynchronousSocketListener();
			_listener.SendContent += _listener_SendContent;
		}

		private void _listener_SendContent(object sender, string e)
		{
			RefreshData?.Invoke(this,e);
		}
		
		public string GetCurrentIpAddress =>$" IP address : {_ipHostInfo.AddressList[0].MapToIPv4()}";

		public string GetCurrentPort =>$" Port : {_port}";
		public  void StartListener()
		{
		
			Task.Factory.StartNew(() => _listener.StartListening(_ipHostInfo, _ipAddress, _localEndPoint));

		}
	}
}

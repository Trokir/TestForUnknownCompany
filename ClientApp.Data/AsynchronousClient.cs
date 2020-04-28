using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ClientApp.Data.Interfaces;

namespace ClientApp.Data
{
    public class AsynchronousClient : IAsynchronousClient
    {

        // ManualResetEvent instances signal completion.  
        private  readonly ManualResetEvent ConnectDone =
            new ManualResetEvent(false);

        private  static ManualResetEvent _sendDone =
            new ManualResetEvent(false);

        private  readonly ManualResetEvent ReceiveDone =
            new ManualResetEvent(false);

        // The Response from the remote device.  
        public event EventHandler<string> SendContent;


        public string Response { get; set; } 
        public string GetText{ get; set; } 
        private void  GetContent(string text)
        {
            SendContent?.Invoke(new{},text);
        }

        /// <summary>
        /// Starts the client.
        /// </summary>
        /// <param name="ipHostInfo">The ip host information.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="localEndPoint">The local end point.</param>
        public void StartClient(IPHostEntry ipHostInfo ,IPAddress ipAddress,IPEndPoint localEndPoint)
        {
            // Connect to a remote device.  
            try
            {
                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(localEndPoint,
                    new AsyncCallback(ConnectCallback), client);
                ConnectDone.WaitOne();

                // Send test data to the remote device.
                Send(client,  $"<STX>{GetText}<ETX>");
                _sendDone.WaitOne();
                GetContent($"<STX>{GetText}<ETX>");
                // Receive the Response from the remote device.  
                Receive(client);
                ReceiveDone.WaitOne();

                // Write the Response to the Debug.  
                Debug.WriteLine("Response received : {0}", Response);
                GetContent(Response);
                // Release the socket.  
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }


        /// <summary>
        /// Connects the callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket) ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Debug.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                ConnectDone.Set();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Receives the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;
                Response = state.sb.ToString();
                GetContent(Response);
                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private  void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject) ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in Response.  
                    if (state.sb.Length > 1)
                    {
                        Response = state.sb.ToString();
                    }

                    // Signal that all bytes have been received.  
                    ReceiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private  void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private   void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket) ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                GetContent($"Sent {bytesSent} bytes to server.");
                // Signal that all bytes have been sent.  
                _sendDone.Set();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

    }
}

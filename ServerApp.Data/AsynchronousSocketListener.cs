﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ServerApp.Data.Interfaces;
using static System.String;

namespace ServerApp.Data
{
    public class AsynchronousSocketListener : IAsynchronousSocketListener
    {
        // Thread signal.  
        public static ManualResetEvent AllDone = new ManualResetEvent(false);


        private string _content;
        public event EventHandler<string> SendContent;


        private void GetContent(string text)
        {
            SendContent?.Invoke(new { }, text);
        }



        int _counter = 0;
        int _incounter = 0;
        /// <summary>
        /// Starts the listening.
        /// </summary>
        /// <param name="ipHostInfo">The ip host information.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="localEndPoint">The local end point.</param>
        public void StartListening(IPHostEntry ipHostInfo, IPAddress ipAddress, IPEndPoint localEndPoint)
        {

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    AllDone.Reset();
                    GetContent($"Waiting message....");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    AllDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }



        }
        /// <summary>
        /// Accepts the callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            AllDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject { workSocket = handler };
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }
        /// <summary>
        /// Reads the callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        public void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read
                // more data.  
                _content = state.sb.ToString();

                if (_content.Contains("<ETX>") && _content.Contains("<STX>") && _content.Length - 10 > 0)
                {
                    _incounter = 0;
                    GetContent($"Correct client request #{++_counter}  {_content}");
                    _content = $"Server response <ACK>";
                    GetContent($"Correct client request #{_counter}  {_content}");
                    // Echo the data back to the client.  
                    Send(handler, _content);
                }
                else
                {
                    if (_incounter == 3)
                    {
                        SendError(handler, _content);
                        return;
                    }
                    else
                    {

                        GetContent($"Incorrect client request #{++_incounter}  Server response <NACK>");
                        byte[] byteData = Encoding.ASCII.GetBytes("Server response <NACK>");

                        // Begin sending the data to the remote device.  
                        handler.BeginSend(byteData, 0, byteData.Length, 0,
                            new AsyncCallback(SendErrorCallback), handler);
                    }

                }
            }
        }
        /// <summary>
        /// Sends the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="data">The data.</param>
        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
        /// <summary>
        /// Sends the callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Debug.WriteLine("Sent {0} bytes to client.", bytesSent);
                _content = $"Sent  {bytesSent}  bytes to client.";
                GetContent(_content);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        private void SendError(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendErrorCallback), handler);
        }
        /// <summary>
        /// Sends the error callback.
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void SendErrorCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  


                _content = "Connection closed";
                GetContent(_content);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }

}

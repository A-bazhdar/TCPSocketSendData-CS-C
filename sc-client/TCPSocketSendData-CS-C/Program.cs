using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketListener
{
    // Main Method
    static void Main(string[] args)
    {
        ExecuteClient();
    }

    // ExecuteClient() Method
    static void ExecuteClient()
    {

        byte[] bytes = new byte[1024];

        try
        {
            // Connect to a Remote server  
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry("127.0.0.1");

            //IPAddress ipAddress = host.AddressList[0];
            IPAddress ipAddress = IPAddress.Parse("192.168.1.11");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 3000);

            // Create a TCP/IP  socket.    
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.    
            try
            {
                // Connect to Remote EndPoint  
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                while (true)
                {

                    // Encode the data string into a byte array.    
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.    
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.    
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
                }

                // Release the socket.    
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        //const int bytesize = 1024 * 1024;
        //try // Try connecting and send the message bytes  
        //{
        //    System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient("192.168.1.11", 3000); // Create a new connection  
        //    NetworkStream stream = client.GetStream();

        //    //stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes  
        //    //Console.WriteLine("================================");
        //    //Console.WriteLine("=   Connected to the server    =");
        //    //Console.WriteLine("================================");
        //    //Console.WriteLine("Waiting for response...");

        //    //messageBytes = new byte[bytesize]; // Clear the message   

        //    //// Receive the stream of bytes  
        //    //stream.Read(messageBytes, 0, messageBytes.Length);

        //    // Clean up  
        //    stream.Dispose();
        //    client.Close();
        //}
        //catch (Exception e) // Catch exceptions  
        //{
        //    Console.WriteLine(e.Message);
        //}

        //try
        //{

        //    // Establish the remote endpoint 
        //    // for the socket. This example 
        //    // uses port 11111 on the local 
        //    // computer.
        //    //IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        //    TcpClient client = new TcpClient();

        //    IPHostEntry ipHost = Dns.GetHostEntry("192.168.1.11");
        //    IPAddress ipAddr = ipHost.AddressList[0];
        //    IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 3000);

        //    // Creation TCP/IP Socket using 
        //    // Socket Class Costructor
        //    Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        //    try
        //    {

        //        // Connect Socket to the remote 
        //        // endpoint using method Connect()
        //        sender.Connect(localEndPoint);

        //        // We print EndPoint information 
        //        // that we are connected
        //        Console.WriteLine("Socket connected to -> {0} ",
        //                      sender.RemoteEndPoint.ToString());

        //        // Creation of messagge that
        //        // we will send to Server
        //        byte[] messageSent = Encoding.ASCII.GetBytes("Test Client<EOF>");
        //        int byteSent = sender.Send(messageSent);

        //        // Data buffer
        //        byte[] messageReceived = new byte[1024];

        //        // We receive the messagge using 
        //        // the method Receive(). This 
        //        // method returns number of bytes
        //        // received, that we'll use to 
        //        // convert them to string
        //        int byteRecv = sender.Receive(messageReceived);
        //        Console.WriteLine("Message from Server -> {0}",
        //              Encoding.ASCII.GetString(messageReceived,
        //                                         0, byteRecv));

        //        // Close Socket using 
        //        // the method Close()
        //        sender.Shutdown(SocketShutdown.Both);
        //        sender.Close();
        //    }

        //    // Manage of Socket's Exceptions
        //    catch (ArgumentNullException ane)
        //    {

        //        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        //    }

        //    catch (SocketException se)
        //    {

        //        Console.WriteLine("SocketException : {0}", se.ToString());
        //    }

        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Unexpected exception : {0}", e.ToString());
        //    }
        //}

        //catch (Exception e)
        //{

        //    Console.WriteLine(e.ToString());
        //}
    }
    //private const int Port = 2500;
    //private static Socket serverSocket;

    //public static int Main(String[] args)
    //{
    //    StartServer();
    //    return 0;
    //}


    //public static void StartServer()
    //{
    //    Console.WriteLine("Setting up server...");
    //    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //    serverSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
    //    serverSocket.Listen(5);
    //    serverSocket.BeginAccept(AcceptCallback, null);
    //    Console.WriteLine("Server setup complete");
    //    Console.WriteLine("Listening on port: " + Port);
    //    Console.ReadKey();

    //}
    //private static void AcceptCallback(IAsyncResult AR)
    //{
    //    Socket socket;

    //    //try
    //    //{
    //    //    socket = serverSocket.EndAccept(AR);
    //    //}
    //    //catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
    //    //{
    //    //    return;
    //    //}

    //    //clientSockets.Add(socket);
    //    //socket.BeginReceive(buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, socket);
    //    //Console.WriteLine("Client connected: " + socket.RemoteEndPoint);
    //    //serverSocket.BeginAccept(AcceptCallback, null);
    //}
}
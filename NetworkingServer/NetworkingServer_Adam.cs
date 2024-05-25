using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class NetworkingServer_Adam
{
    public static void Main(string[] args)
    {
        TcpListener server = null;
        TcpClient client = null;

        // Default port number we are going to use
        int portnumber = 5000;
        if (args.Length >= 1)
        {
            portnumber = int.Parse(args[0]);
        }

        try
        {
            server = new TcpListener(IPAddress.Any, portnumber);
            server.Start();
            Console.WriteLine("Server is started");

            while (true)
            {
                Console.WriteLine("Waiting for connect request...");
                client = server.AcceptTcpClient();
                Console.WriteLine("Connect request is accepted...");

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

                string msgFromClient = reader.ReadLine();
                Console.WriteLine("Message received from client = " + msgFromClient);

                if (msgFromClient != null && !msgFromClient.Equals("bye", StringComparison.OrdinalIgnoreCase))
                {
                    string ansMsg = "Hello, " + msgFromClient;
                    writer.WriteLine(ansMsg);
                }

                if (msgFromClient != null && msgFromClient.Equals("bye", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                client.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e);
        }
        finally
        {
            server.Stop();
        }
    }

}
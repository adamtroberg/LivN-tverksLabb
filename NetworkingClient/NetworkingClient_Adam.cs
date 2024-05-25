using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class NetworkingClient_Adam
{
    public static void Main(string[] args)
    {
        TcpClient client = null;

        // Default port number we are going to use
        int portnumber = 5000;
        if (args.Length >= 1)
        {
            portnumber = int.Parse(args[0]);
        }

        for (int i = 0; i < 10; i++)
        {
            try
            {
                client = new TcpClient(Dns.GetHostName(), portnumber);
                Console.WriteLine("Client socket is created");

                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                StreamReader reader = new StreamReader(stream);

                Console.WriteLine("Enter your name. Type Bye to exit.");
                string msg = Console.ReadLine().Trim();
                writer.WriteLine(msg);

                Console.WriteLine("Message returned from the server: " + reader.ReadLine());

                if (msg.Equals("bye", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class MulticastChatServer_Adam
{
    public static void Main(string[] args)
    {
        int portnumber = 5001;
        UdpClient server = new UdpClient(portnumber);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("225.4.5.6"), portnumber);

        server.JoinMulticastGroup(groupEP.Address);
        Console.WriteLine("Multicast server is created at port " + portnumber);

        while (true)
        {
            byte[] buffer = server.Receive(ref groupEP);
            string msg = Encoding.ASCII.GetString(buffer, 0, buffer.Length).Trim();
            Console.WriteLine("Message received from client: " + msg);
        }
    }
}
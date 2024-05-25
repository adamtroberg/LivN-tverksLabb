using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class MulticastChatClient_Adam
{
    public static void Main(string[] args)
    {
        int portnumber = 5001;
        UdpClient client = new UdpClient();
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("225.4.5.6"), portnumber);

        client.JoinMulticastGroup(groupEP.Address);
        Console.WriteLine("Type a message for the server:");

        string msg = Console.ReadLine();
        byte[] buffer = Encoding.ASCII.GetBytes(msg);
        client.Send(buffer, buffer.Length, groupEP);

        client.Close();
    }
}
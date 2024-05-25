using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

class Adam_Klient
{
    public static void Main(string[] args)
    {
        TcpListener server = null;
        TcpClient client = null;

        // Default port number we are going to use
        int portnumber = 5002;
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
                    string answer = Calculate(msgFromClient);
                    writer.WriteLine(answer);
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

    private static string Calculate(string message)
    {
        try
        {
            message = message.Replace(" ", ""); // Remove any whitespace
            string pattern = @"(\d+)([+\-*/x])(\d+)";
            Match match = Regex.Match(message, pattern);

            if (match.Success)
            {
                int operand1 = int.Parse(match.Groups[1].Value);
                string op = match.Groups[2].Value;
                int operand2 = int.Parse(match.Groups[3].Value);

                double result = 0;
                switch (op)
                {
                    case "+":
                        result = operand1 + operand2;
                        break;
                    case "-":
                        result = operand1 - operand2;
                        break;
                    case "*":
                        result = operand1 * operand2;
                        break;
                    case "/":
                        result = (double)operand1 / operand2;
                        break;
                    case "x":
                        result = operand1 * operand2;
                        break;
                    default:
                        return "Fel operator";
                }
                return $"The answer is " + result;
            }
            else
            {
                return "Felaktigt uttryck (endast plus minus gånger delat)";
            }
        }
        catch (Exception e)
        {
            return "Error: " + e.Message;
        }
    }
}
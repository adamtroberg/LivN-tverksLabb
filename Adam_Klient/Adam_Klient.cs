using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Adam_Klient
{
    public static void Main(string[] args)
    {
        TcpClient client = null;

        // Default port number we are going to use
        int portnumber = 5002;
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
                string response = 
                writer.WriteLine(response);

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

    // Beräkna talet
    private static string CalculateMath(string message)
    {
        // Ta bort alla mellanslag
        message = message.Remove(" ", ""); 
        // OBS den här koden har jag kopierat för att ta ut operanden
        string pattern = @"(\d+)([+\-*x/])(\d+)";

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
                }
                return $"The answer of {msg} is " + result;
            }
            else
            {
                return "Invalid expression";
            }
        }
        catch (Exception e)
        {
            return "Error: " + e.Message;
        }
    }
    }
}

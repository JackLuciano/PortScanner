using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace PortScanner
{
    internal class ClassHandler
    {
        private List<DefaultPort> DefaultPorts = new List<DefaultPort>
        {
            new DefaultPort(80, "HTTP"),
            new DefaultPort(443, "HTTPS"),
            new DefaultPort(21, "FTP"),
            new DefaultPort(22, "FTPS / SSH"),
            new DefaultPort(110, "POP3"),
            new DefaultPort(995, "POP3 SSL"),
            new DefaultPort(143, "IMAP"),
            new DefaultPort(993, "IMAP SSL"),
        };

        public bool IsPortAvailable(int port)
        {
            bool isAvailable = true;
            foreach (var _ in from IPEndPoint endpoint in IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners()
                              where endpoint.Port == port
                              select new { })
            {
                isAvailable = false;
                break;
            }

            return isAvailable;
        }

        public static IPAddress GetIPAddress()
        {
            IPAddress Address = Dns.GetHostAddresses(Dns.GetHostName()).Where(address =>
            address.AddressFamily == AddressFamily.InterNetwork).First();

            return Address;
        }

        public ClassHandler()
        {
            Initialize();
        }

        private void OutputLine(int Port)
        {
            Console.Write("\n" + GetIPAddress() + ":" + Port + " -> ");

            if (!IsPortAvailable(Port))
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.Write(IsPortAvailable(Port) ? "available" : "not available");

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void Initialize()
        {
            Console.Clear();

            Console.WriteLine("Type 'default' for default port list, or type ports like 80, 111, 10");

            string? Input = Console.ReadLine();

            if (Input != null && Input != string.Empty)
            {
                if (Input == "default")
                {
                    foreach (DefaultPort? Port in DefaultPorts)
                    {
                        OutputLine(Port.Port);
                    }
                }
                else
                {
                    string[] Inputs = Input.Split(' ');

                    bool IsParsed = false;

                    foreach (string? Port in Inputs)
                    {
                        if (int.TryParse(Port, out int OutPort))
                        {
                            IsParsed = true;

                            OutputLine(OutPort);
                        }
                    }

                    if (!IsParsed)
                        Initialize();
                }
            }
            else
            {
                Initialize();
            }
        }
    }
}
namespace PortScanner
{
    internal class DefaultPort
    {
        public int Port { get; set; }
        public string Name { get; set; }

        public DefaultPort(int port, string name)
        {
            Port = port;
            Name = name;
        }
    }
}
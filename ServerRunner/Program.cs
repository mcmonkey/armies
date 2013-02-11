using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server;

namespace ServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Server server = new Server.Server();
            server.Start();
        }
    }
}

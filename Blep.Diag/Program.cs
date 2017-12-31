using Blep.Diag.Commands;
using Blep.Framework.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blep.Diag
{
    class Program
    {
        static void Main(string[] args)
        {
            var argument = args.Length > 0 ? args[0] : string.Empty;
            var value = args.Length > 1 ? args[1] : string.Empty;

            switch (argument)
            {
                case "info":
                    {
                        var command = new ListDeviceResourcesCommand(value);
                        command.Execute();
                    }
                    break;

                case "discovery":
                    {
                        var command = new DiscoveryCommand();
                        command.Execute();
                    }
                    break;

                case "poll":
                    {
                        var command = new PollDeviceCommand(value);
                        command.Execute();
                    }
                    break;

            }



        }
    }
}

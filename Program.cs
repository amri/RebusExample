using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rebus.Activation;
using Rebus.Config;

namespace RebusExample
{
    class Program
    {
        private static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                var connectionString = ConfigurationManager.AppSettings["serviceBus"];
                Configure.With(activator)
                    .Transport(t => t.UseAzureServiceBus(connectionString, "rebusQueue")).Start();
            }
            Console.WriteLine("q");
            Console.ReadLine();


        }
    }
}

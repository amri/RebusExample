using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;

namespace RebusExample
{
    class Program
    {
        private static void Main(string[] args)
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                activator.Register(() => new PrintDateTime());

                var connectionString = ConfigurationManager.AppSettings["serviceBus"];
                Configure.With(activator)
                    .Transport(t => t.UseAzureServiceBus(connectionString, "rebusQueue")).Start();
                var timer = new Timer();
                timer.Elapsed += delegate
                {
                    activator.Bus.SendLocal(DateTime.Now).Wait();

                };
                timer.Interval = 1000;
                timer.Start();

                Console.ReadLine();
            }
            


        }
    }

    internal class PrintDateTime : IHandleMessages<DateTime>
    {
        public async Task Handle(DateTime message)
        {
            Console.WriteLine("The time is {0}",message);
        }
    }
}

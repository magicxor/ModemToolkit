using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ModemToolkit.Common.Core;
using ModemToolkit.Common.Models;
using NLog;

namespace ModemToolkit.Console
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                logger.Info("{0} has been started at {1}", Assembly.GetExecutingAssembly().GetName().Name, DateTime.Now);

                var container = ContainerConfig.Configure(new Settings());
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IModemInfoService>();
                    app.Run();
                }

                System.Console.WriteLine("Press any key to exit...");
                System.Console.ReadLine();
                logger.Info("{0} has been stopped at {1}", Assembly.GetExecutingAssembly().GetName().Name, DateTime.Now);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }
    }
}

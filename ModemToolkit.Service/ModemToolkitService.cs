using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ModemToolkit.Common.Core;
using ModemToolkit.Common.Models;
using NLog;

namespace ModemToolkit.Service
{
    public partial class ModemToolkitService : ServiceBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ModemToolkitService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("{0} has been started at {1}", Assembly.GetExecutingAssembly().GetName().Name, DateTime.Now);

            var container = ContainerConfig.Configure(new Settings());
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IModemInfoService>();
                app.Run();
            }
        }

        protected override void OnStop()
        {
            logger.Info("{0} has been stopped at {1}", Assembly.GetExecutingAssembly().GetName().Name, DateTime.Now);
        }
    }
}

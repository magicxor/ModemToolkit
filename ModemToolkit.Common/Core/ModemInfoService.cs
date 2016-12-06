using System;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Hosting;
using ModemToolkit.Common.Models;
using NLog;
using Owin;

namespace ModemToolkit.Common.Core
{
    public interface IModemInfoService
    {
        void Run();
    }

    public class ModemInfoService : IModemInfoService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ISettings Settings { get; set; }
        private IDisposable Server { get; set; }

        public ModemInfoService(ISettings settings)
        {
            Settings = settings;
        }

        public void Run()
        {
            try
            {
                logger.Info("Starting the server on {0}", Settings.ServerUri);
                Server = WebApp.Start(Settings.ServerUri, (app) =>
                {
                    var container = ContainerConfig.Configure(Settings);

                    // Register the Autofac middleware FIRST. This also adds Autofac-injected middleware registered with the container.
                    app.UseAutofacMiddleware(container);

                    HttpConfiguration config = new HttpConfiguration
                    {
                        DependencyResolver = new AutofacWebApiDependencyResolver(container)
                    };
                    config.Routes.MapHttpRoute(name: "Api", routeTemplate: "Api/{controller}/{action}");
                    app.UseAutofacWebApi(config);
                    app.UseWebApi(config);
                });
                logger.Info("The server is started on {0}", Settings.ServerUri);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }
    }
}
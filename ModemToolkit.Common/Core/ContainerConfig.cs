using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using ModemToolkit.Common.Models;

namespace ModemToolkit.Common.Core
{
    public static class ContainerConfig
    {
        public static IContainer Configure(ISettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(settings).As<ISettings>().ExternallyOwned();
            builder.RegisterType<ModemInfoService>().As<IModemInfoService>();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register dependencies, then...
            var container = builder.Build();
            return container;
        }
    }
}

using System;
using ModemToolkit.Common.Enums;
using NLog;

namespace ModemToolkit.Common.Models
{
    public interface ISettings : IBaseSettings
    {
        string ServerUri { get; set; }
        string ModemGetInfoCommand { get; set; }
        string ModemGetInfoArguments { get; set; }
    }

    public class Settings : BaseSettings, ISettings
    {
        public string ServerUri { get; set; }
        public string ModemGetInfoCommand { get; set; }
        public string ModemGetInfoArguments { get; set; }

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Settings()
        {
            try
            {
                ServerUri = LoadItem(SettingSources.ConfigurationManager, "ServerUri", @"http://127.0.0.1:50248/", ToStringConverter);
                ModemGetInfoCommand = LoadItem(SettingSources.ConfigurationManager, "ModemGetInfoCommand", @"gnokii", ToStringConverter);
                ModemGetInfoArguments = LoadItem(SettingSources.ConfigurationManager, "ModemGetInfoArguments", @"--monitor once", ToStringConverter);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }
    }
}
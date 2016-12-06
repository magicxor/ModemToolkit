using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ModemToolkit.Common.Enums;
using NLog;

namespace ModemToolkit.Common.Models
{
    public interface IBaseSettings
    {
    }

    public abstract class BaseSettings : IBaseSettings
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected Func<string, string> ToStringConverter { get; set; } = s => s;
        protected Func<string, int> ToIntConverter { get; set; } = s => int.Parse(s);
        protected Func<string, bool> ToBoolConverter { get; set; } = s => bool.Parse(s);
        protected Func<string, List<string>> ToListOfStringConverter { get; set; } = s => s.Split(',').ToList();

        protected T LoadItem<T>(SettingSources source, string key, T defaultValue, Func<string, T> converterFunc)
        {
            try
            {
                logger.Trace($"Running {GetType().Name}.{nameof(LoadItem)}<{typeof(T).Name}>({source}, {key}, {defaultValue}, {converterFunc})");
                string value;
                switch (source)
                {
                    case SettingSources.ConnectionStrings:
                        value = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                        break;
                    case SettingSources.ConfigurationManager:
                        value = ConfigurationManager.AppSettings[key];
                        break;
                    default:
                        throw new Exception($"Unknown value of {typeof(SettingSources).Name} parameter");
                }
                logger.Trace($"Successfully loaded value {value}. Start converting...");
                var resultValue = converterFunc(value);
                logger.Trace($"Successfully converted.");
                return resultValue;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Can't read {key} from {source}. Return default value {defaultValue}.");
                return defaultValue;
            }
        }
    }
}

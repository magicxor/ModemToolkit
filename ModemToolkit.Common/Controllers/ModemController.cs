using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModemToolkit.Common.Models;

namespace ModemToolkit.Common.Controllers
{
    public class ModemController : BaseApiController
    {
        private ISettings Settings { get; set; }

        public ModemController(ISettings settings)
        {
            Settings = settings;
        }

        [HttpGet]
        public HttpResponseMessage GetInfo()
        {
            try
            {
                LogRequest();

                System.Diagnostics.Process proc = new System.Diagnostics.Process
                {
                    EnableRaisingEvents = false,
                    StartInfo =
                    {
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        FileName = Settings.ModemGetInfoCommand,
                        Arguments = Settings.ModemGetInfoArguments,
                    }
                };

                proc.Start();
                var data = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                logger.Info(data);
                return CreateHttpResponseMessage(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return CreateHttpResponseMessage(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}

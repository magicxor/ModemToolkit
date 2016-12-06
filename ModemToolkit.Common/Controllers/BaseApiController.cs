using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;

namespace ModemToolkit.Common.Controllers
{
    public abstract class BaseApiController: ApiController
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();

        protected HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode httpStatusCode, string message)
        {
            return new HttpResponseMessage(httpStatusCode)
            {
                Content = new StringContent(message)
            };
        }

        protected void LogRequest()
        {
            var httpMethod = this.Request.GetOwinContext().Request.Method;
            var userIpAddress = this.Request.GetOwinContext().Request.RemoteIpAddress;
            var uri = this.Request.GetOwinContext().Request.Uri.OriginalString;
            logger.Info("{0} request from IP {1} with uri {2}", httpMethod, userIpAddress, uri);
        }
    }
}

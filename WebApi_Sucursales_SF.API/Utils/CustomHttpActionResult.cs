using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace WebApi_Sucursales_SF.API.Utils
{
    public class CustomHttpActionResult<T> : IHttpActionResult
    {
        HttpConfiguration _configuration;
        T _content;
        HttpStatusCode _statusCode;
        HttpRequestMessage _request;

        public CustomHttpActionResult(T content, HttpStatusCode statusCode, HttpRequestMessage request, HttpConfiguration configuration)
        {
            _content = content;
            _request = request;
            _configuration = configuration;
            _statusCode = statusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new ObjectContent<dynamic>(_content, _configuration.Formatters.JsonFormatter),
                RequestMessage = _request
            };

            return Task.FromResult(response);
        }
    }
}
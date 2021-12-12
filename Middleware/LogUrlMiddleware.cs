using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;

namespace kinder_app.Middleware
{
    // REQUIREMENT: middleware 2
    public class LogUrlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogUrlMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // REQUIREMENT: logging 2
            _logger.Information($"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");

            await this._next(context);
        }

    }
}

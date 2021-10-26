using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace WebAPI
{
    public class YamlOutputFormatter : TextOutputFormatter
    {
        private readonly ISerializer _serializer;

        public YamlOutputFormatter(ISerializer serializer)
        {
            _serializer = serializer;

            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/yml"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding encoding)
        {
            HttpContext httpContext = context.HttpContext;

            using (var writer = context.WriterFactory(httpContext.Response.Body, encoding))
            {
                _serializer.Serialize(writer, context.Object);

                await writer.FlushAsync();
            }
        }
    }
}

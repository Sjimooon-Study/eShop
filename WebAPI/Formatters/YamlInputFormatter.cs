using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace WebAPI.Formatters
{
    public class YamlInputFormatter : TextInputFormatter
    {
        private readonly IDeserializer _deserializer;

        public YamlInputFormatter(IDeserializer deserializer)
        {
            _deserializer = deserializer;

            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/yml"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            HttpContext httpContext = context.HttpContext;

            using (var reader = context.ReaderFactory(httpContext.Request.Body, encoding))
            {
                try
                {
                    var model = _deserializer.Deserialize(reader, context.ModelType);
                    return InputFormatterResult.SuccessAsync(model);
                }
                catch (Exception e)
                {
                    // TODO: Log exception.

                    return InputFormatterResult.FailureAsync();
                }
            }
        }
    }
}

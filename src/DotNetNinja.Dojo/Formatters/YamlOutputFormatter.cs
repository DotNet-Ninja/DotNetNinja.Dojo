using System.Text;

using DotNetNinja.Dojo.Constants;

using Microsoft.AspNetCore.Mvc.Formatters;

using YamlDotNet.Serialization;

namespace DotNetNinja.Dojo.Formatters;

public class YamlOutputFormatter : TextOutputFormatter
{
    private readonly ISerializer _serializer;

    public YamlOutputFormatter(ISerializer serializer)
    {
        _serializer = serializer;

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
        SupportedMediaTypes.Add(MimeType.ApplicationYaml);
        SupportedMediaTypes.Add(MimeType.TextYaml);
        SupportedMediaTypes.Add(MimeType.ApplicationXYaml);
        SupportedMediaTypes.Add(MimeType.TextXYaml);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        await using var writer = context.WriterFactory(response.Body, selectedEncoding);
        _serializer.Serialize(writer, context.Object ?? string.Empty);
        await writer.FlushAsync();
    }
}
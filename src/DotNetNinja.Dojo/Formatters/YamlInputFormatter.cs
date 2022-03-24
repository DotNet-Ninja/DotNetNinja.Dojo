using System.Text;

using DotNetNinja.Dojo.Constants;

using Microsoft.AspNetCore.Mvc.Formatters;

using YamlDotNet.Serialization;

namespace DotNetNinja.Dojo.Formatters;

public class YamlInputFormatter : TextInputFormatter
{
    private readonly IDeserializer _deserializer;

    public YamlInputFormatter(IDeserializer deserializer)
    {
        _deserializer = deserializer;
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
        SupportedMediaTypes.Add(MimeType.ApplicationYaml);
        SupportedMediaTypes.Add(MimeType.TextYaml);
        SupportedMediaTypes.Add(MimeType.ApplicationXYaml);
        SupportedMediaTypes.Add(MimeType.TextXYaml);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var request = context.HttpContext.Request;
        using var reader = context.ReaderFactory(request.Body, encoding);
        try
        {
            var data = await reader.ReadToEndAsync();
            var model = _deserializer.Deserialize(data, context.ModelType);
            return await InputFormatterResult.SuccessAsync(model);
        }
        catch
        {
            return await InputFormatterResult.FailureAsync();
        }
    }
}
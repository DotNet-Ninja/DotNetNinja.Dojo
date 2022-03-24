using System.Text.Json.Serialization;

using DotNetNinja.Dojo.Constants;
using DotNetNinja.Dojo.Formatters;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DotNetNinja.Dojo.Extensions;

public static class ApiContentTypeExtensions
{
    public static IMvcBuilder AddYamlApiSupport(this IMvcBuilder mvc)
    {
        return mvc.AddMvcOptions(options =>
        {
            options.InputFormatters.Add(new YamlInputFormatter(new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance).Build()));
            options.OutputFormatters.Add(new YamlOutputFormatter(new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance).Build()));
            options.FormatterMappings.SetMediaTypeMappingForFormat("yaml", MimeType.ApplicationXYaml);
        });
    }

    public static IMvcBuilder AddXmlApiSupport(this IMvcBuilder mvc)
    {
        return mvc.AddXmlDataContractSerializerFormatters();
    }

    public static IMvcBuilder AddJsonFormatOptions(this IMvcBuilder mvc)
    {
        return mvc.AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    public static IServiceCollection AddApiContentFormatters(this IMvcBuilder mvc)
    {
        return mvc.AddJsonFormatOptions()
            .AddYamlApiSupport()
            .AddXmlApiSupport().Services;
    }
}
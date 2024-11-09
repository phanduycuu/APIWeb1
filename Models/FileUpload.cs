using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class FileUpload : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParams = context.MethodInfo
            .GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile))
            .ToArray();

        if (fileParams.Length > 0)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = fileParams.ToDictionary(
                                p => p.Name,
                                p => new OpenApiSchema { Type = "string", Format = "binary" }
                            )
                        }
                    }
                }
            };
        }
    }
}

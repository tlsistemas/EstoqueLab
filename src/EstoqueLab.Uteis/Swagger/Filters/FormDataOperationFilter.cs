using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EstoqueLab.Uteis.Swagger.Filters
{
    public class FormDataOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Identify relevant operations by convention - e.g. action name
            if (context.MethodInfo.Name != "PostObject") return;

            //operation.RequestBody.Content.Add("multipart/form-data", new OpenApiMediaType
            operation.RequestBody.Content.Add("application/x-www-form-urlencoded", new OpenApiMediaType

            {
                Schema = new OpenApiSchema
                {
                    Type = "object",
                    Properties = new Dictionary<String, OpenApiSchema>
                    {
                        ["file"] = new OpenApiSchema { Type = "String", Format = "binary" }
                    }
                }
            });
        }
    }

}

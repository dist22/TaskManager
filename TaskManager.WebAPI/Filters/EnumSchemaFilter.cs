using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManager.WebAPI.Filters;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumNames = Enum.GetNames(context.Type);
            schema.Enum.Clear();
            foreach (var name in enumNames)
            {
                schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiString(name));
            }

            schema.Type = "string";
            schema.Format = null;
        }
    }
}
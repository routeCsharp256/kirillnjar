using System;
using System.Linq;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OzonEdu.MerchandiseService.Infrastructure.SchemaFilters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum) return;
            var description = new StringBuilder();
            Enum.GetNames(context.Type)
                .ToList()
                .ForEach(name => description.Append($"{Convert.ToInt64(Enum.Parse(context.Type, name))} - {name} "));
            model.Description = description.ToString();
        }
    }
}
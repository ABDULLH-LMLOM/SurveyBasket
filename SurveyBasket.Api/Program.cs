
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer((schema, context, ct) =>
    {
        if (schema.Type.HasValue &&
            schema.Type.Value.HasFlag(JsonSchemaType.String) &&
            schema.Type.Value.HasFlag(JsonSchemaType.Integer))
        {
            schema.Type = JsonSchemaType.Integer;
            schema.Pattern = null;
        }
        return Task.CompletedTask;
    });
});

// Add Mapster configuration
var mappingConfig = TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IMapper>( new Mapper(mappingConfig));


builder.Services.AddScoped<IPollService, PollService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

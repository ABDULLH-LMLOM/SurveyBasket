namespace SurveyBasket.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection Services)
    {
        Services.AddControllers();

        Services.AddOpenApiService();

        Services.AddScoped<IPollService, PollService>();

        Services.AddFluentValidationService();

        Services.AddMapsterService();

        return Services;
    }

    public static IServiceCollection AddOpenApiService(this IServiceCollection Services)
    {
        Services.AddOpenApi(options =>
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
        return Services;
    }

    public static IServiceCollection AddFluentValidationService(this IServiceCollection Services)
    {
        Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return Services;
    }

    public static IServiceCollection AddMapsterService(this IServiceCollection Services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        Services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return Services;
    }
}

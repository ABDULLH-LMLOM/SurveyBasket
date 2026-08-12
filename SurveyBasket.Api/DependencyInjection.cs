namespace SurveyBasket.Api;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddControllers();

        Services.AddOpenApi();


        Services.AddScoped<IPollService, PollService>();

        Services.AddFluentValidation();

        Services.AddMapster();

        Services.AddDbContext(Configuration);

        //return Services;
    }

    public static void AddOpenApi(this IServiceCollection Services)
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
        //return Services;
    }

    public static void AddFluentValidation(this IServiceCollection Services)
    {
        Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //return Services;
    }

  
    public static void AddMapster(this IServiceCollection Services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        Services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        //return Services;
    }


    public static void AddDbContext(this IServiceCollection Services, IConfiguration Configuration)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        //return Services;
    }
}

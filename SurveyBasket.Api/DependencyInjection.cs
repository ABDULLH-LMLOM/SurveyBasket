using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace SurveyBasket.Api;

public static class DependencyInjection
{
    //public static void AddDependencies(this IServiceCollection Services, IConfiguration Configuration)
    //{
    //    Services.AddControllers();

    //    Services.AddOpenApi();


    //    Services.AddScoped<IPollService, PollService>();

    //    Services.AddFluentValidation();

    //    Services.AddMapster();

    //    Services.AddDbContext(Configuration);

    //}

    public static void AddOpenApiService(this IServiceCollection Services)
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
    }

    public static void AddFluentValidationService(this IServiceCollection Services)
    {
        Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }


    public static void AddMapsterService(this IServiceCollection Services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        Services.AddSingleton<IMapper>(new Mapper(mappingConfig));
    }


    public static void AddDbContextService(this IServiceCollection Services, IConfiguration Configuration)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    public static void AddAuthService(this IServiceCollection Services , IConfiguration configuration)
    {
        Services.AddScoped<IAuthService, AuthService>();

        Services.AddSingleton<JwtProvider>();

        Services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>();

        

        var options = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        Services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();


        Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key!))
                };
            });


    }
}

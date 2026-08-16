using SurveyBasket.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApiService();

builder.Services.AddFluentValidationService();

builder.Services.AddMapsterService();

builder.Services.AddAuthService();

builder.Services.AddDbContextService(builder.Configuration);

//builder.Services.AddIdentityApiEndpoints<User>()
//    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddScoped<IPollService, PollService>();
builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapIdentityApi<User>();

app.MapControllers();

app.Run();

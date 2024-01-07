using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:4200",
                    "http://localhost:4300",
                    "http://localhost:8000",
                    "https://user.webhotel.click",
                    "https://admin.webhotel.click"
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    );
});
builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
   
    //.AddJsonFile("ocelotWard.json", optional: false, reloadOnChange: true)
    //.AddJsonFile("ocelotAuthentication.json", optional: false, reloadOnChange: true).AddJsonFile("ocelotUserMap.json", optional: false, reloadOnChange: true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "hello api gateway");

//app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
OcelotPipelineConfiguration configuration = new OcelotPipelineConfiguration();
await app.UseOcelot();
app.Run();
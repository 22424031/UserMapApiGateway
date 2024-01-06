using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
   
    //.AddJsonFile("ocelotWard.json", optional: false, reloadOnChange: true)
    //.AddJsonFile("ocelotAuthentication.json", optional: false, reloadOnChange: true).AddJsonFile("ocelotUserMap.json", optional: false, reloadOnChange: true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "hello api gateway");

//app.UseAuthorization();

app.MapControllers();
OcelotPipelineConfiguration configuration = new OcelotPipelineConfiguration();
await app.UseOcelot();
app.Run();
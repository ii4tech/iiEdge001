using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json.Linq;
using RavenTestApi;
using RavenTestApi.DbClients;
using RavenTestApi.Entities;
using RavenTestApi.Services;
using Serilog;
using RavenTestApi.Entities.Queries;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

IDatabaseBootstrap dbs = new DatabaseBootstrap();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.WebHost.UseKestrel(options =>
//    {
//        options.ListenAnyIP(5051, listenOptions => {
//               listenOptions.UseHttps(Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path"), "Visvis10");
//         });
//    });

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

var app = builder.Build();

ConfigurationBuilder config = new ConfigurationBuilder();
config.AddEnvironmentVariables();
if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    config.AddUserSecrets<Program>();
}



IConfigurationRoot Configuration = config.Build();

DatabaseConfig.Name = Configuration["dbname"];
DatabaseConfig.Env = Configuration["env"];
DatabaseConfig.DbReset = Convert.ToBoolean(Configuration["dbreset"]);

dbs.Setup();

IGenericRepository gr = new GenericRepository();
TblRawAccel accel = new TblRawAccel();

accel.Time = Util.getEpoch(DateTime.UtcNow);
accel.X = 453;
accel.Y = 556;
accel.Z = 489;

QryTblRawAccel qryAccel = new QryTblRawAccel(accel);
JObject json = JObject.FromObject(accel);

var addrecord = gr.AddAsync(qryAccel.Insert());
Console.WriteLine($"Add initial record result  = {addrecord.Result}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseForwardedHeaders(new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//});

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

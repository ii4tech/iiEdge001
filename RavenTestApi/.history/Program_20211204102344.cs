using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json.Linq;
using RavenTestApi;
using RavenTestApi.DbClients;
using RavenTestApi.Entities;
using RavenTestApi.Services;
using Serilog;
using RavenTestApi.Entities.Queries;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

X509Certificate2 certificate = null;


ConfigurationBuilder config = new ConfigurationBuilder();
config.AddEnvironmentVariables();
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    config.AddUserSecrets<Program>();

    //Needs /app/secure/ for Linux Docker
    string[] info = Directory.GetFiles("secure/");

    Log.Information("file - {}" + info.Length);

    if (info.Length > 0)
    {
        Log.Information($"Dev env and file - {info[0]}");

        certificate = new X509Certificate2(info[0], "Visvis10");
    }
} 
else 
{
    //Needs /app/secure/ for Linux Docker
    string[] info = Directory.GetFiles("/app/secure/");
    Log.Information("file - {}" + info.Length);

    if (info.Length > 0)
    {
        Log.Information($"Docker env and file - {info[0]}");

        certificate = new X509Certificate2(info[0], "Visvis10");
    }
}


IConfigurationRoot Configuration = config.Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.WebHost.UseUrls("https://*:443,http://localhost:5000");
builder.WebHost.UseKestrel(options =>
   {
       options.ListenAnyIP(443, listenOptions => {
              listenOptions.UseHttps(certificate);
        });
        //options.ListenLocalhost(80);
   });

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

var app = builder.Build();

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

IDatabaseBootstrap dbs = new DatabaseBootstrap();

DatabaseConfig.dbname = Configuration["dbname"];
DatabaseConfig.rdbname = Configuration["rdbname"];
DatabaseConfig.Env = Configuration["env"];
DatabaseConfig.DbReset = Convert.ToBoolean(Configuration["dbreset"]);

dbs.Setup();
Log.Information($"sqlite is setup, now running app");

app.Run();

Log.Information($"App is running, creating dbs");

RavenDb db = new RavenDb();

IGenericRepository gr = new SqliteRepository();
TblRawAccel accel = new TblRawAccel();

accel.Time = Util.getEpoch(DateTime.UtcNow);
accel.X = 453;
accel.Y = 556;
accel.Z = 489;

QryTblRawAccel qryAccel = new QryTblRawAccel(accel);
JObject json = JObject.FromObject(accel);

var addrecord = gr.AddAsync(qryAccel.Insert());
Log.Information($"Add initial record result  = {addrecord.Result}");


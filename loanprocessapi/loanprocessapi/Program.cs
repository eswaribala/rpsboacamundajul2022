
using Camunda.Worker;
using Camunda.Worker.Client;
using loanprocessapi.BPMNDeployment;
using loanprocessapi.Contexts;
using loanprocessapi.Handlers;
using loanprocessapi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

var connectionString=configuration.GetConnectionString("SWRequestDBConnString");

builder.Services.AddDbContext<SoftwareRequestContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//DI--Singleton,Scoped,Transient
builder.Services.AddScoped<ISoftwareRequestRepo, SoftwareRequestRepo>();
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Camunda worker startUp
builder.Services.AddSingleton(_ => new BpmnService(configuration["RestApiUri"]));
builder.Services.AddHostedService<BpmnDeployService>();
builder.Services.AddExternalTaskClient()
    .ConfigureHttpClient((provider, client) =>
    {
        client.BaseAddress = new Uri(configuration["RestApiUri"]);
    });
builder.Services.AddCamundaWorker("Loan Worker Service", 1)
    .AddHandler<LoginValidationHandler>()
    .AddHandler<LogHandler>()
    .AddHandler<SWRequestHandler>()
    .AddHandler<SWRequestSaveHandler>()
    .AddHandler<KafkaProcessHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Forms")),
    RequestPath = "/Forms",
    EnableDefaultFiles = true
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Produto.Infrastructure.IOC;
using Produto.Application.Exceptions;
using Produto.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<Settings>()
    ?? throw ProgramException.AppsettingNotSetException();

builder.Services.AddSingleton(configuration);

var app = await builder.ConfigureServices(configuration).ConfigurePipeline(configuration);

 app.Logger.LogInformation("Aplicação iniciada com sucesso!");

await app.RunAsync();

public partial class Program { }


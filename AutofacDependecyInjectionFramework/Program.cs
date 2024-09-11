using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacDependecyInjectionFramework.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configurando o Autofac como o Container de Injeção de Dependência
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Registrando serviços no container Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Singleton - Mesmo objeto usado durante toda a aplicação
    containerBuilder.RegisterType<SingletonService>().As<ISingletonService>().SingleInstance();

    // Transient - Nova instância para cada requisição
    containerBuilder.RegisterType<TransientService>().As<ITransientService>().InstancePerDependency();

    // Scoped - Instância por requisição HTTP (ou por escopo)
    containerBuilder.RegisterType<ScopedService>().As<IScopedService>().InstancePerLifetimeScope();

    // ExternallyOwned - Gerenciado externamente, Autofac não irá descartar
    containerBuilder.RegisterType<ExternallyOwnedService>().As<IExternallyOwnedService>().ExternallyOwned();

    // Matching Scope - Compartilhado em escopos específicos
    containerBuilder.RegisterType<MatchingScopeService>().As<IMatchingScopeService>()
        .InstancePerMatchingLifetimeScope("custom-scope");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using EncurtadorURL.API.Backend.Business;
using EncurtadorURL.API.Backend.DataAccess;
using EncurtadorURL.API.Backend.DataAccess.Context;
using EncurtadorURL.API.Backend.Interfaces.IBusiness;
using EncurtadorURL.API.Backend.Interfaces.IDataAccess;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["dbContextSettings:ConnectionString"];
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<UrlEncurtadasContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUrlEncurtadasContext, UrlEncurtadasContext>();
builder.Services.AddScoped<IUrlEncurtadaDataAccess, UrlEncurtadaDataAccess>();
builder.Services.AddScoped<IUrlEncurtadaBusiness, UrlEncurtadaBusiness>();

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

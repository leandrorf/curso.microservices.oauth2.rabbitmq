using AutoMapper;
using GeekShopping.ProductApi.Config;
using GeekShopping.ProductApi.Models.Context;
using GeekShopping.ProductApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers( );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

var connection = builder.Configuration[ "MySqlConnection:MySqlConnectionString" ];

builder.Services.AddDbContext<MySqlContext>( options =>
{
    options.UseMySql( connection, ServerVersion.AutoDetect( connection ) );
} );

IMapper mapper = MappingConfig.RegisterMaps( ).CreateMapper( );

builder.Services.AddSingleton( mapper );
builder.Services.AddAutoMapper( AppDomain.CurrentDomain.GetAssemblies( ) );

builder.Services.AddScoped<IProductRepository, ProductRepository>( );

var app = builder.Build( );

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment( ) )
{
    app.UseSwagger( );
    app.UseSwaggerUI( );
}

app.UseHttpsRedirection( );

app.UseAuthorization( );

app.MapControllers( );

app.Run( );

using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Business.Mapper;
using Web.Data.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("MsSqlConnection");
builder.Services.AddDbContext<VbDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateApplicationUserCommand).GetTypeInfo().Assembly));
var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));

builder.Services.AddSingleton(mapperConfig.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(x => { x.MapControllers(); });


app.Run();

using AutoMapper;
using CapgeminiCard.API.Context;
using CapgeminiCard.API.Mapper;
using CapgeminiCard.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CardContext>(opt => opt.UseInMemoryDatabase("CapgeminiCardTestDb"));
builder.Services.AddScoped<ICardRepository, CardRepository>();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(x =>
{
    x.AddProfile(new CardMapping());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
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

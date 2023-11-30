using CryptoScrapper.DAL;
using CryptoScrapper.DAL.Interfaces;
using CryptoScrapper.DAL.Models;
using CryptoScrapper.DAL.Repositories;
using CryptoScrapper.DAL.Settings;
using CryptoScrapperAPI_PAD.Controllers;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Configure Mongo DB DI

builder.Services.Configure<UserDatabaseSettings>(
    builder.Configuration.GetSection("UserStoreDatabase"));
builder.Services.Configure<ListItemDatabaseSettings>(
    builder.Configuration.GetSection("ListItemStoreDatabase"));
builder.Services.Configure<ListDatabaseSettings>(
    builder.Configuration.GetSection("ListStoreDatabase"));

builder.Services.AddSingleton(typeof(IMongoRepository<User>), typeof(MongoUserRepository<User>));
builder.Services.AddSingleton(typeof(IMongoRepository<CustomList>), typeof(MongoListRepository<CustomList>));
builder.Services.AddSingleton(typeof(IMongoRepository<CustomListItem>), typeof(MongoListItemRepository<CustomListItem>));



//Configure MediatR DI
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserController).Assembly));
//builder.Services.AddMediatR(cfg => cfg.AsScoped(), Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

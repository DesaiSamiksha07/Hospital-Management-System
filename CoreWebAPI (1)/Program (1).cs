using DataAccess;
using DataModel.IRepository;
using DataModel.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Get Database Connection
var _setting = new DataSettingManager().LoadSettings();
var _connection = _setting.DBConnection;

//Set connection in Context file
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(_connection));

builder.Services.AddScoped<IToken,Token>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

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

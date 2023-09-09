using BugTrackerCore.Interfaces;
using BugTrackerCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BugTrackerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BugTrackerDB"));
});

//builder.Services.AddSingleton<IList<string>, List<string>>();
builder.Services.AddScoped<IDbRepository, BugTrackerDbRepository>();
builder.Services.AddSingleton<ILocalRepository, LocalRepository>();

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

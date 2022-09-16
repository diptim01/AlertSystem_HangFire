using AlertSystem.Interface;
using AlertSystem.Persistence;
using AlertSystem.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuartion = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(c => c.UseSqlServerStorage(configuartion.GetConnectionString("HangFireConnection")));
builder.Services.AddScoped<IJobInterface, JobServices>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHangfireServer();

builder.Services.AddDbContext<AlertContext>(c => c.UseSqlServer(configuartion.GetConnectionString("AlertDb")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire");
app.UseAuthorization();

app.MapControllers();

app.Run();

using API_Hospitalar.HospitalContextDb;
using API_Hospitalar.HospitalServices;
using API_Hospitalar.IHospital;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHospitalService, HospitalService>();
var connectionString= "Server=DESKTOP-BG5E4QK\\SQLEXPRESS;Database=HOSPITAL;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<HospitalContext>(o=>o.UseSqlServer(connectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

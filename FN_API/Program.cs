using FN_API.Entities;
using FN_API.Payloads.Responses;
using FN_API.Services.Implements;
using FN_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKhoaHocService, KhoaHocService>();
builder.Services.AddSingleton<ResponseObject<KhoaHoc>>();
builder.Services.AddSingleton<ResponseObject<List<KhoaHoc>>>();
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

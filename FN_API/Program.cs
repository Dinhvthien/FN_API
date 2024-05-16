using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.Responses;
using FN_API.Services;
using FN_API.Services.Implements;
using FN_API.Services.Interfaces;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKhoaHocService, KhoaHocService>();
builder.Services.AddScoped<IChuDeService, ChuDeService>();
builder.Services.AddScoped<IDangKyHocService, DangKyHocService>();
builder.Services.AddScoped<ILoaiKhoaHocService, LoaiKhoaHocService>();
builder.Services.AddScoped<IHocVienService, HocVienService>();
builder.Services.AddScoped<ITinhTrangHocService, TinhTrangHocService>();
builder.Services.AddScoped<IQuyenHanService, QuyenHanService>();
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
builder.Services.AddScoped<IBaiVietService, BaiVietService>();
builder.Services.AddScoped<ILoaiBaiVietServices, LoaiBaiVietService>();

builder.Services.AddScoped<IFileService, FileService>();




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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"
});
app.Run();

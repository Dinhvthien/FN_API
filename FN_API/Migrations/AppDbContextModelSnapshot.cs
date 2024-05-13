﻿// <auto-generated />
using System;
using FN_API.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FN_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FN_API.Entities.BaiViet", b =>
                {
                    b.Property<int>("BaiVietId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BaiVietId"), 1L, 1);

                    b.Property<int?>("ChuDeId")
                        .HasColumnType("int");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDungNgan")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<string>("TenTacGia")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenbaiViet")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.HasKey("BaiVietId");

                    b.HasIndex("ChuDeId");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("BaiViet");
                });

            modelBuilder.Entity("FN_API.Entities.ChuDe", b =>
                {
                    b.Property<int>("ChuDeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChuDeId"), 1L, 1);

                    b.Property<int?>("LoaiBaiVietId")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChuDe")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ChuDeId");

                    b.HasIndex("LoaiBaiVietId");

                    b.ToTable("ChuDe");
                });

            modelBuilder.Entity("FN_API.Entities.DangKyHoc", b =>
                {
                    b.Property<int>("DangKyHocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DangKyHocId"), 1L, 1);

                    b.Property<int?>("HocVienId")
                        .HasColumnType("int");

                    b.Property<int?>("KhoaHocId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayDangKy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TaiKhoanId")
                        .HasColumnType("int");

                    b.Property<int?>("TinhTrangHocId")
                        .HasColumnType("int");

                    b.HasKey("DangKyHocId");

                    b.HasIndex("HocVienId");

                    b.HasIndex("KhoaHocId");

                    b.HasIndex("TaiKhoanId");

                    b.HasIndex("TinhTrangHocId");

                    b.ToTable("DangKyHoc");
                });

            modelBuilder.Entity("FN_API.Entities.HocVien", b =>
                {
                    b.Property<int>("HocVienId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HocVienId"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hoten")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhuongXa")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("QuanHuyen")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SoDienThoai")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("SoNha")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TinhThanh")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("HocVienId");

                    b.ToTable("HocVien");
                });

            modelBuilder.Entity("FN_API.Entities.KhoaHoc", b =>
                {
                    b.Property<int>("KhoaHocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KhoaHocId"), 1L, 1);

                    b.Property<string>("GioiThieu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("HocPhi")
                        .HasColumnType("real");

                    b.Property<int?>("LoaiKhoaHocId")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoHocVien")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuongMon")
                        .HasColumnType("int");

                    b.Property<string>("TenKhoaHoc")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ThoiGianHoc")
                        .HasColumnType("int");

                    b.HasKey("KhoaHocId");

                    b.HasIndex("LoaiKhoaHocId");

                    b.ToTable("KhoaHoc");
                });

            modelBuilder.Entity("FN_API.Entities.LoaiBaiViet", b =>
                {
                    b.Property<int>("LoaiBaiVietId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoaiBaiVietId"), 1L, 1);

                    b.Property<string>("TenLoai")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LoaiBaiVietId");

                    b.ToTable("LoaiBaiViet");
                });

            modelBuilder.Entity("FN_API.Entities.LoaiKhoaHoc", b =>
                {
                    b.Property<int>("LoaiKhoaHocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoaiKhoaHocId"), 1L, 1);

                    b.Property<string>("TenLoai")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("LoaiKhoaHocId");

                    b.ToTable("LoaiKhoaHoc");
                });

            modelBuilder.Entity("FN_API.Entities.QuyenHan", b =>
                {
                    b.Property<int>("QuyenHanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuyenHanId"), 1L, 1);

                    b.Property<string>("TenQuyenHan")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("QuyenHanId");

                    b.ToTable("QuyenHan");
                });

            modelBuilder.Entity("FN_API.Entities.TaiKhoan", b =>
                {
                    b.Property<int>("TaiKhoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaiKhoanId"), 1L, 1);

                    b.Property<string>("MatKhau")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("QuyenHanId")
                        .HasColumnType("int");

                    b.Property<string>("TenDangNhap")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TenNguoiDung")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("TaiKhoanId");

                    b.HasIndex("QuyenHanId");

                    b.ToTable("TaiKhoan");
                });

            modelBuilder.Entity("FN_API.Entities.TinhTrangHoc", b =>
                {
                    b.Property<int>("TinhTrangHocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TinhTrangHocId"), 1L, 1);

                    b.Property<string>("TenTinhTrang")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("TinhTrangHocId");

                    b.ToTable("TinhTrangHoc");
                });

            modelBuilder.Entity("FN_API.Entities.BaiViet", b =>
                {
                    b.HasOne("FN_API.Entities.ChuDe", "ChuDe")
                        .WithMany("BaiViets")
                        .HasForeignKey("ChuDeId");

                    b.HasOne("FN_API.Entities.TaiKhoan", "TaiKhoan")
                        .WithMany()
                        .HasForeignKey("TaiKhoanId");

                    b.Navigation("ChuDe");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("FN_API.Entities.ChuDe", b =>
                {
                    b.HasOne("FN_API.Entities.LoaiBaiViet", "LoaiBaiViet")
                        .WithMany("ChuDe")
                        .HasForeignKey("LoaiBaiVietId");

                    b.Navigation("LoaiBaiViet");
                });

            modelBuilder.Entity("FN_API.Entities.DangKyHoc", b =>
                {
                    b.HasOne("FN_API.Entities.HocVien", "HocVien")
                        .WithMany("DangKyHocs")
                        .HasForeignKey("HocVienId");

                    b.HasOne("FN_API.Entities.KhoaHoc", "KhoaHoc")
                        .WithMany()
                        .HasForeignKey("KhoaHocId");

                    b.HasOne("FN_API.Entities.TaiKhoan", "TaiKhoan")
                        .WithMany()
                        .HasForeignKey("TaiKhoanId");

                    b.HasOne("FN_API.Entities.TinhTrangHoc", "TinhTrangHoc")
                        .WithMany("DangKyHocs")
                        .HasForeignKey("TinhTrangHocId");

                    b.Navigation("HocVien");

                    b.Navigation("KhoaHoc");

                    b.Navigation("TaiKhoan");

                    b.Navigation("TinhTrangHoc");
                });

            modelBuilder.Entity("FN_API.Entities.KhoaHoc", b =>
                {
                    b.HasOne("FN_API.Entities.LoaiKhoaHoc", "LoaiKhoaHoc")
                        .WithMany("KhoaHocs")
                        .HasForeignKey("LoaiKhoaHocId");

                    b.Navigation("LoaiKhoaHoc");
                });

            modelBuilder.Entity("FN_API.Entities.TaiKhoan", b =>
                {
                    b.HasOne("FN_API.Entities.QuyenHan", "QuyenHan")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("QuyenHanId");

                    b.Navigation("QuyenHan");
                });

            modelBuilder.Entity("FN_API.Entities.ChuDe", b =>
                {
                    b.Navigation("BaiViets");
                });

            modelBuilder.Entity("FN_API.Entities.HocVien", b =>
                {
                    b.Navigation("DangKyHocs");
                });

            modelBuilder.Entity("FN_API.Entities.LoaiBaiViet", b =>
                {
                    b.Navigation("ChuDe");
                });

            modelBuilder.Entity("FN_API.Entities.LoaiKhoaHoc", b =>
                {
                    b.Navigation("KhoaHocs");
                });

            modelBuilder.Entity("FN_API.Entities.QuyenHan", b =>
                {
                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("FN_API.Entities.TinhTrangHoc", b =>
                {
                    b.Navigation("DangKyHocs");
                });
#pragma warning restore 612, 618
        }
    }
}

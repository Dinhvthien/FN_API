using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataResponses;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Payloads.Converters
{
    public class DangKyHocConverter
    {
        private readonly AppDbContext _context;
        public DangKyHocConverter()
        {
            _context = new AppDbContext();
        }
        public async Task<List<DataResponseDangKyHoc>> DataRespomseListHocVien(List<DangKyHoc> data)
        {
            List<DataResponseDangKyHoc> danhsachDky = new List<DataResponseDangKyHoc>();
            foreach (var item in data)
            {
                DataResponseDangKyHoc dangkyhocRes = new DataResponseDangKyHoc();
                dangkyhocRes.DangKyHocId = item.DangKyHocId;
                dangkyhocRes.tenkhoahoc = (await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == item.KhoaHocId)).TenKhoaHoc;
                dangkyhocRes.tenhocvien = (await _context.HocVien.SingleOrDefaultAsync(c => c.HocVienId == item.HocVienId)).Hoten;
                dangkyhocRes.tentinhtrang = (await _context.TinhTrangHoc.SingleOrDefaultAsync(c => c.TinhTrangHocId == item.TinhTrangHocId)).TenTinhTrang;
                dangkyhocRes.tentaikhoan = (await _context.TaiKhoan.SingleOrDefaultAsync(c => c.TaiKhoanId == item.TaiKhoanId)).TenNguoiDung;
                dangkyhocRes.ngayketthuc = item.NgayKetThuc;
                dangkyhocRes.ngaybatdau = item.NgayBatDau;
                dangkyhocRes.ngaydangky = item.NgayDangKy;
                danhsachDky.Add(dangkyhocRes);
            }
            return danhsachDky;
        }
        public async Task<DataResponseDangKyHoc> DataRespomsehocvien(DangKyHoc item)
        {
            DataResponseDangKyHoc dangkyhocRes = new DataResponseDangKyHoc();
            dangkyhocRes.DangKyHocId = item.DangKyHocId;
            dangkyhocRes.tenkhoahoc = (await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == item.KhoaHocId)).TenKhoaHoc;
            dangkyhocRes.tenhocvien = (await _context.HocVien.SingleOrDefaultAsync(c => c.HocVienId == item.HocVienId)).Hoten;
            dangkyhocRes.tentinhtrang = (await _context.TinhTrangHoc.SingleOrDefaultAsync(c => c.TinhTrangHocId == item.TinhTrangHocId)).TenTinhTrang;
            dangkyhocRes.tentaikhoan = (await _context.TaiKhoan.SingleOrDefaultAsync(c => c.TaiKhoanId == item.TaiKhoanId)).TenNguoiDung;
            dangkyhocRes.ngayketthuc = item.NgayKetThuc;
            dangkyhocRes.ngaybatdau = item.NgayBatDau;
            dangkyhocRes.ngaydangky = item.NgayDangKy;
            return dangkyhocRes;
        }
    }
}

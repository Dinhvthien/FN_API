using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Payloads.Converters
{
    public class TaiKhoanConverter
    {
        private readonly AppDbContext _context;
        public TaiKhoanConverter()
        {
            _context = new AppDbContext();
        }

        public async Task<List<DataResponseTaiKhoan>> DataRespomseListChiTietHoaDon(IEnumerable<Data_RequestTaiKhoan> data)
        {
            List<DataResponseTaiKhoan> danhSachTaiKhoan = new List<DataResponseTaiKhoan>();
            foreach (var item in data)
            {
                DataResponseTaiKhoan taiKhoanResponse = new DataResponseTaiKhoan();
               taiKhoanResponse.TenQuyenHan = (await _context.QuyenHan.SingleOrDefaultAsync(c => c.QuyenHanId == item.QuyenHanId)).TenQuyenHan;
                taiKhoanResponse.TaiKhoanId = item.TaiKhoanId;
                taiKhoanResponse.MatKhau = item.MatKhau;
                taiKhoanResponse.TenNguoiDung = item.TenNguoiDung;
                taiKhoanResponse.TenDangNhap = item.TenDangNhap;
                danhSachTaiKhoan.Add(taiKhoanResponse);
            }
            return danhSachTaiKhoan;
        }
        public async Task<List<DataResponseTaiKhoan>> DataRespomseenumChiTietHoaDon(List<Data_RequestTaiKhoan> data)
        {
            List<DataResponseTaiKhoan> danhSachTaiKhoan = new List<DataResponseTaiKhoan>();
            foreach (var item in data)
            {
                DataResponseTaiKhoan taiKhoanResponse = new DataResponseTaiKhoan();
                taiKhoanResponse.TenQuyenHan = (await _context.QuyenHan.SingleOrDefaultAsync(c => c.QuyenHanId == item.QuyenHanId)).TenQuyenHan;
                taiKhoanResponse.TaiKhoanId = item.TaiKhoanId;
                taiKhoanResponse.MatKhau = item.MatKhau;
                taiKhoanResponse.TenNguoiDung = item.TenNguoiDung;
                taiKhoanResponse.TenDangNhap = item.TenDangNhap;
                danhSachTaiKhoan.Add(taiKhoanResponse);
            }
            return danhSachTaiKhoan;
        }
        public async Task<DataResponseTaiKhoan> DataRespomseChiTietHoaDon(Data_RequestTaiKhoan item)
        { 
            DataResponseTaiKhoan taiKhoanResponse = new DataResponseTaiKhoan();
            taiKhoanResponse.TenQuyenHan = (await _context.QuyenHan.SingleOrDefaultAsync(c => c.QuyenHanId == item.QuyenHanId)).TenQuyenHan;
            taiKhoanResponse.TaiKhoanId = item.TaiKhoanId;
            taiKhoanResponse.MatKhau = item.MatKhau;
            taiKhoanResponse.TenNguoiDung = item.TenNguoiDung;
            taiKhoanResponse.TenDangNhap = item.TenDangNhap;
            return taiKhoanResponse;
        }
    }
}

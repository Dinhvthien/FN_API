using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Payloads.Converters
{
    public class BaiVietConverter
    {
        private readonly AppDbContext _context;
        public BaiVietConverter()
        {
            _context = new AppDbContext();
        }
        public async Task<List<DataResponseBaiViet>> DataRespomseeListBaiViet(List<Data_RequestListBaiViet> data)
        {
            List<DataResponseBaiViet> danhSachBaiViet = new List<DataResponseBaiViet>();
            foreach (var item in data)
            {
                DataResponseBaiViet baiviet = new DataResponseBaiViet();
                baiviet.tenchude = (await _context.ChuDe.SingleOrDefaultAsync(c => c.ChuDeId == item.ChuDeId)).TenChuDe;
                baiviet.tentaikhoan = (await _context.TaiKhoan.SingleOrDefaultAsync(c => c.TaiKhoanId == item.TaiKhoanId)).TenNguoiDung;
                baiviet.BaiVietId = item.BaiVietId;
                baiviet.NoiDung = item.NoiDung;
                baiviet.TenTacGia = item.TenTacGia;
                baiviet.NoiDungNgan = item.NoiDungNgan;
                baiviet.Hinhanh = item.Hinhanh;
                baiviet.TenbaiViet = item.TenbaiViet;
                baiviet.thoigiantao = item.thoigiantao;
                danhSachBaiViet.Add(baiviet);
            }
            return danhSachBaiViet;
        }
        public async Task<DataResponseBaiViet> DataRespomseBaiViet(BaiViet item)
        {
            DataResponseBaiViet baiviet = new DataResponseBaiViet();
            baiviet.tenchude = (await _context.ChuDe.SingleOrDefaultAsync(c => c.ChuDeId == item.ChuDeId)).TenChuDe;
            baiviet.tentaikhoan = (await _context.TaiKhoan.SingleOrDefaultAsync(c => c.TaiKhoanId == item.TaiKhoanId)).TenNguoiDung;
            baiviet.BaiVietId = item.BaiVietId;
            baiviet.NoiDung = item.NoiDung;
            baiviet.TenTacGia = item.TenTacGia;
            baiviet.NoiDungNgan = item.NoiDungNgan;
            baiviet.Hinhanh = item.HinhAnh;
            baiviet.TenbaiViet = item.TenbaiViet;
            baiviet.thoigiantao = (DateTime)item.ThoiGianTao;
            return baiviet;
        }
    }
}

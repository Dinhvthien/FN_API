using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Payloads.Converters
{
    public class KhoaHocConverter
    {
        private readonly AppDbContext _context;
        public KhoaHocConverter()
        {
            _context = new AppDbContext();
        }
        public async Task<List<DataResponseKhoaHoc>> DataRespomseListChiTietHoaDon(IEnumerable<KhoaHoc> data)
        {
            List<DataResponseKhoaHoc> danhSachKhoaHoc = new List<DataResponseKhoaHoc>();
            foreach (var item in data)
            {
                DataResponseKhoaHoc khoaHocResponse = new DataResponseKhoaHoc();
                khoaHocResponse.KhoaHocId = item.KhoaHocId;
                khoaHocResponse.TenKhoaHoc = item.TenKhoaHoc;
                khoaHocResponse.ThoiGianHoc = item.ThoiGianHoc;
                khoaHocResponse.GioiThieu = item.GioiThieu;
                khoaHocResponse.HocPhi = item.HocPhi;
                khoaHocResponse.NoiDung = item.NoiDung;
                khoaHocResponse.SoHocVien = item.SoHocVien;
                khoaHocResponse.SoLuongMon = item.SoLuongMon;
                khoaHocResponse.HinhAnh = item.HinhAnh;
                khoaHocResponse.tenloaikhoahoc = (await _context.LoaiKhoaHoc.SingleOrDefaultAsync(c => c.LoaiKhoaHocId == item.LoaiKhoaHocId)).TenLoai;
                danhSachKhoaHoc.Add(khoaHocResponse);
            }
            return danhSachKhoaHoc;
        }
        public async Task<DataResponseKhoaHoc> DataRespomseChiTietHoaDon(Data_RequetKhoaHoc data)
        {
            DataResponseKhoaHoc khoaHocResponse = new DataResponseKhoaHoc();
            khoaHocResponse.KhoaHocId = data.KhoaHocId;
            khoaHocResponse.TenKhoaHoc = data.TenKhoaHoc;
            khoaHocResponse.ThoiGianHoc = data.ThoiGianHoc;
            khoaHocResponse.GioiThieu = data.GioiThieu;
            khoaHocResponse.HocPhi = data.HocPhi;
            khoaHocResponse.NoiDung = data.NoiDung;
            khoaHocResponse.SoHocVien = data.SoHocVien;
            khoaHocResponse.SoLuongMon = data.SoLuongMon;
            khoaHocResponse.HinhAnh = data.HinhAnh;
            khoaHocResponse.tenloaikhoahoc = (await _context.LoaiKhoaHoc.SingleOrDefaultAsync(c => c.LoaiKhoaHocId == data.LoaiKhoaHocId)).TenLoai;
            return khoaHocResponse;
        }
    }
}

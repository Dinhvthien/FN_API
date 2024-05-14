using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.Converters;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FN_API.Services.Implements
{
    public class KhoaHocService : IKhoaHocService
    {
        private readonly AppDbContext _context;
        private readonly KhoaHocConverter _khConverter;
        private readonly ResponseObject<DataResponseKhoaHoc> _responseObject;
        private readonly ResponseObject<List<DataResponseKhoaHoc>> _responselistObject;
        public int page_size { get; set; } = 5;
        public KhoaHocService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseKhoaHoc>();
            _responselistObject = new ResponseObject<List<DataResponseKhoaHoc>>();
            _khConverter = new KhoaHocConverter();
        }

        public async Task<ResponseObject<List<DataResponseKhoaHoc>>> DanhSachKh(int page)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                var list = (await _context.KhoaHoc.ToListAsync()).Skip((page - 1) * page_size).Take(page_size);
                return _responselistObject.ResponseSuccses("Danh sách khóa học", await _khConverter.DataRespomseListChiTietHoaDon(list));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responselistObject.ResponseError(400, "", null);
            }
        }

        public async Task<ResponseObject<DataResponseKhoaHoc>> SuaKhoaHoc(Data_RequetKhoaHoc khoaHoc)
        {
            var finkhoahoc = await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == khoaHoc.KhoaHocId);
            finkhoahoc.TenKhoaHoc = khoaHoc.TenKhoaHoc;
            finkhoahoc.ThoiGianHoc = khoaHoc.ThoiGianHoc;
            finkhoahoc.GioiThieu = khoaHoc.GioiThieu;
            finkhoahoc.HocPhi = khoaHoc.HocPhi;
            finkhoahoc.NoiDung = khoaHoc.NoiDung;
            finkhoahoc.SoHocVien = khoaHoc.SoHocVien;
            finkhoahoc.SoLuongMon = khoaHoc.SoLuongMon;
            finkhoahoc.HinhAnh = khoaHoc.HinhAnh;
            finkhoahoc.LoaiKhoaHocId = khoaHoc.LoaiKhoaHocId;
            _context.KhoaHoc.Update(finkhoahoc);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccses("Sửa khóa học thành công", await _khConverter.DataRespomseChiTietHoaDon(khoaHoc));
        }

        public async Task<ResponseObject<DataResponseKhoaHoc>> ThemKhoaHoc(Data_RequetKhoaHoc khoaHoc)
        {
            var finkhoahoc = new KhoaHoc();
            finkhoahoc.KhoaHocId = khoaHoc.KhoaHocId;
            finkhoahoc.TenKhoaHoc = khoaHoc.TenKhoaHoc;
            finkhoahoc.ThoiGianHoc = khoaHoc.ThoiGianHoc;
            finkhoahoc.GioiThieu = khoaHoc.GioiThieu;
            finkhoahoc.HocPhi = khoaHoc.HocPhi;
            finkhoahoc.NoiDung = khoaHoc.NoiDung;
            finkhoahoc.SoHocVien = khoaHoc.SoHocVien;
            finkhoahoc.SoLuongMon = khoaHoc.SoLuongMon;
            finkhoahoc.HinhAnh = khoaHoc.HinhAnh;
            finkhoahoc.LoaiKhoaHocId = khoaHoc.LoaiKhoaHocId;
            await _context.KhoaHoc.AddAsync(finkhoahoc);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccses("Them khóa học thành công", await _khConverter.DataRespomseChiTietHoaDon(khoaHoc));
        }

        public async Task<ResponseObject<List<DataResponseKhoaHoc>>> TimKiemKhoaHocTheoTen(string name, int page)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                var FindKhoaHoc = (await _context.KhoaHoc.Where(c => c.TenKhoaHoc.Trim().ToLower().Contains(name.Trim().ToLower())).ToListAsync()).Skip(((page - 1) * page_size)).Take(page_size); ;
                if (FindKhoaHoc == null)
                {
                    return _responselistObject.ResponseError(400, "Tìm khóa học thành công", null);
                }
                return _responselistObject.ResponseSuccses("thành công", await _khConverter.DataRespomseListChiTietHoaDon(FindKhoaHoc));
            }
            catch (Exception e)
            {
                return _responselistObject.ResponseError(400, "Tìm khóa học thành công", null);
            }
        }

        public async Task<bool> XoaKhoaHoc(int khoaHocId)
        {
            try
            {
                var obj = await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == khoaHocId);
                if (obj != null)
                {
                    _context.KhoaHoc.Remove(obj);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

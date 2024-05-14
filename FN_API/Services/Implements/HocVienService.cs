using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.Converters;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FN_API.Services.Implements
{
    public class HocVienService : IHocVienService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseHocVien> _responseObject;
        private readonly ResponseObject<List<DataResponseHocVien>> _responselistObject;
        private readonly HocVienConverte _hvConverter;

        public int page_size { get; set; } = 5;
        public HocVienService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseHocVien>();
            _responselistObject = new ResponseObject<List<DataResponseHocVien>>();
            _hvConverter = new HocVienConverte();
        }


        public async Task<ResponseObject<List<DataResponseHocVien>>> DanhSachHV(int page = 1)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                var list = (await _context.HocVien.ToListAsync()).Skip((page - 1) * page_size).Take(page_size);
                return _responselistObject.ResponseSuccses("Danh sách khóa học", await _hvConverter.DataRespomseListHocVien(list));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responselistObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public static string FormatName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(name.ToLower());
        }
        public async Task<ResponseObject<DataResponseHocVien>> Suahocvien(Data_RequestHocVien item)
        {
            try
            {
                var HocVienResponse = await _context.HocVien.SingleOrDefaultAsync(c => c.HocVienId == item.HocVienId);
                if (_context.HocVien.Any(c => c.Email == item.Email))
                {
                    return _responseObject.ResponseError(400, "Email của bạn đã có trong hệ thông ", null);
                }
                if (_context.HocVien.Any(c => c.SoDienThoai.Trim() == item.SoDienThoai.Trim()))
                {
                    return _responseObject.ResponseError(400, "Số điện thoạ của bạn đã có trong hệ thông ", null);
                }
                HocVienResponse.Hoten = FormatName(item.Hoten);
                HocVienResponse.NgaySinh = item.NgaySinh;
                HocVienResponse.SoDienThoai = item.SoDienThoai;
                HocVienResponse.Email = item.Email;
                HocVienResponse.TinhThanh = item.TinhThanh;
                HocVienResponse.QuanHuyen = item.QuanHuyen;
                HocVienResponse.PhuongXa = item.PhuongXa;
                //HocVienResponse.HinhAnh = item.HinhAnh;
                HocVienResponse.SoNha = item.SoNha;
                _context.HocVien.Update(HocVienResponse);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa học viên thành công", await _hvConverter.DataRespomsehocvien(HocVienResponse));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public async Task<ResponseObject<DataResponseHocVien>> ThemHocVien(string url ,Data_RequestHocVien item)
        {
            try
            {
                HocVien HocVienResponse = new HocVien();
                HocVienResponse.Hoten = item.Hoten;
                HocVienResponse.NgaySinh = item.NgaySinh;
                HocVienResponse.SoDienThoai = item.SoDienThoai;
                HocVienResponse.Email = item.Email;
                HocVienResponse.TinhThanh = item.TinhThanh;
                HocVienResponse.QuanHuyen = item.QuanHuyen;
                HocVienResponse.PhuongXa = item.PhuongXa;
                HocVienResponse.HinhAnh = url;
                HocVienResponse.SoNha = item.SoNha;
                await _context.HocVien.AddAsync(HocVienResponse);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Thêm học viên thành công", await _hvConverter.DataRespomsehocvien(HocVienResponse));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public async Task<ResponseObject<List<DataResponseHocVien>>> TimKiemHVTheoTenvaEmail(string name, int page)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                var FindHocVien = (await _context.HocVien.Where(c => c.Hoten.Trim().ToLower().Contains(name.Trim().ToLower()) || c.Email.Trim().Contains(name.Trim())).ToListAsync()).Skip(((page - 1) * page_size)).Take(page_size); ;
                if (FindHocVien == null)
                {
                    return _responselistObject.ResponseError(400, "Tìm khóa học thành công", null);
                }
                return _responselistObject.ResponseSuccses("thành công", await _hvConverter.DataRespomseListHocVien(FindHocVien));
            }
            catch (Exception e)
            {
                return _responselistObject.ResponseError(400, "Tìm khóa học thành công", null);
            }
        }

        public async Task<bool> XoaHocvien(int hocvienID)
        {
            try
            {
                var obj = await _context.HocVien.SingleOrDefaultAsync(c => c.HocVienId == hocvienID);
                if (obj != null)
                {
                    _context.HocVien.Remove(obj);
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

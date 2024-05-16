using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.Converters;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Services.Implements
{
    public class LoaiKhoaHocService : ILoaiKhoaHocService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<LoaiKhoaHoc> _responseObject;
        private readonly ResponseObject<List<LoaiKhoaHoc>> _responseListObject;

        public LoaiKhoaHocService()
        {
            _context = new AppDbContext();
            _responseObject =  new ResponseObject<LoaiKhoaHoc>();
            _responseListObject = new ResponseObject<List<LoaiKhoaHoc>>();
        }
        public async Task<ResponseObject<List<LoaiKhoaHoc>>> DanhSachLKh()
        {
            try
            {
                var listLKH = await _context.LoaiKhoaHoc.ToListAsync();
                return  _responseListObject.ResponseSuccses("Danh sach", listLKH);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseListObject.ResponseError(400,"không thành công", null);
            }
        }

        public async Task<ResponseObject<LoaiKhoaHoc>> SuaLoaiKhoaHoc(int khoahocid, string TenLoaiKhoaHoc)
        {
            try
            {
                var obj = await _context.LoaiKhoaHoc.SingleOrDefaultAsync(c => c.LoaiKhoaHocId == khoahocid);
                if (obj == null)
                {
                    return _responseObject.ResponseError(400,"Sửa không thành công", null);
                }
                obj.TenLoai = TenLoaiKhoaHoc.Trim();
                 _context.LoaiKhoaHoc.Update(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa thành công", obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400,"Sửa không thành công", null);
            }
        }

        public async Task<ResponseObject<LoaiKhoaHoc>> ThemLoaiKhoaHoc(string TenLoaiKhoaHoc)
        {
            try
            {
                var obj = new LoaiKhoaHoc();
                obj.TenLoai = TenLoaiKhoaHoc.Trim();
                await _context.LoaiKhoaHoc.AddAsync(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Thêm thành công", obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400,"Thêm thành công", null);
            }
        }

        public async Task<bool> XoaLoaiKhoaHoc(int khoaHocId)
        {
            try
            {
                var obj = await _context.LoaiKhoaHoc.SingleOrDefaultAsync(c => c.LoaiKhoaHocId == khoaHocId);
                if (obj == null)
                {
                    return false;
                }
                _context.LoaiKhoaHoc.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

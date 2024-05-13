using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Services.Implements
{
    public class KhoaHocService : IKhoaHocService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<KhoaHoc> _responseObject;
        private readonly ResponseObject<List<KhoaHoc>> _responselistObject;
        public KhoaHocService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<KhoaHoc>();
            _responselistObject = new ResponseObject<List<KhoaHoc>>();

        }

        public async Task<ResponseObject<List<KhoaHoc>>> DanhSachKh()
        {
            var obj = await _context.KhoaHoc.ToListAsync();
            try
            {
                return _responselistObject.ResponseSuccses("Danh sách khóa học", obj);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responselistObject.ResponseError(400, "", obj);
            }
        }

        public async Task<ResponseObject<KhoaHoc>> SuaKhoaHoc(KhoaHoc khoaHoc)
        {

                _context.KhoaHoc.Update(khoaHoc);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa khóa học thành công",khoaHoc);
        }

        public async Task<ResponseObject<KhoaHoc>> ThemKhoaHoc(KhoaHoc khoaHoc)
        {
                await _context.KhoaHoc.AddAsync(khoaHoc);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Thêm khóa học thành công", khoaHoc);
        }

        public async Task<ResponseObject<KhoaHoc>> XoaKhoaHoc(int khoaHocId)
        {
            var obj = await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == khoaHocId);
                if (obj != null)
                {
                    _context.KhoaHoc.Remove(obj);
                    await _context.SaveChangesAsync();
                    return _responseObject.ResponseSuccses("Xóa khóa học thành công", obj);

                }
                return _responseObject.ResponseError(400, "Xóa khóa học không thành công", obj);
        }
    }
}

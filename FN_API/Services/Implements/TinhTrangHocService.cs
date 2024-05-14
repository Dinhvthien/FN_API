using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Services.Implements
{
    public class TinhTrangHocService : ITinhTrangHocService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseTinhTrangHoc> _responseObject;
        private readonly ResponseObject<List<DataResponseTinhTrangHoc>> _responseListObject;

        public TinhTrangHocService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseTinhTrangHoc>();
            _responseListObject = new ResponseObject<List<DataResponseTinhTrangHoc>>();
        }
        public async Task<ResponseObject<List<DataResponseTinhTrangHoc>>> DanhSachTTH()
        {
            try
            {
                var listTTh = await _context.TinhTrangHoc.ToListAsync();
                List<DataResponseTinhTrangHoc> data = new List<DataResponseTinhTrangHoc>();
                foreach (var item in listTTh)
                {
                    DataResponseTinhTrangHoc dataResponseTinhTrangHoc = new DataResponseTinhTrangHoc();
                    dataResponseTinhTrangHoc.TenTinhTrang = item.TenTinhTrang;
                    dataResponseTinhTrangHoc.TinhTrangHocId = item.TinhTrangHocId;
                    data.Add(dataResponseTinhTrangHoc);
                }
                return _responseListObject.ResponseSuccses("Danh sách", data);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseListObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public async Task<ResponseObject<DataResponseTinhTrangHoc>> SuaTinhTrangHoc(int TinhTrangId, string TenTinhTrang)
        {
            try
            {
                var obj = await _context.TinhTrangHoc.SingleOrDefaultAsync(c => c.TinhTrangHocId == TinhTrangId);
                obj.TenTinhTrang = TenTinhTrang;
                DataResponseTinhTrangHoc dataResponseTinhTrangHoc = new DataResponseTinhTrangHoc();
                dataResponseTinhTrangHoc.TenTinhTrang = TenTinhTrang;
                dataResponseTinhTrangHoc.TinhTrangHocId = obj.TinhTrangHocId;
                if (obj == null)
                {
                    return _responseObject.ResponseError(400, "Sửa không thành công", dataResponseTinhTrangHoc);
                }
                _context.TinhTrangHoc.Update(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa thành công", dataResponseTinhTrangHoc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Sửa không thành công", null);
            }
        }

        public async Task<ResponseObject<DataResponseTinhTrangHoc>> ThemTinhTrangHoc(string TenTinhTrang)
        {
            try
            {
                TinhTrangHoc tinhTrangHoc = new TinhTrangHoc();
                tinhTrangHoc.TenTinhTrang = TenTinhTrang;
                _context.TinhTrangHoc.Add(tinhTrangHoc);
                await _context.SaveChangesAsync();
                DataResponseTinhTrangHoc dataResponseTinhTrangHoc = new DataResponseTinhTrangHoc();
                dataResponseTinhTrangHoc.TenTinhTrang = TenTinhTrang;
                dataResponseTinhTrangHoc.TinhTrangHocId = tinhTrangHoc.TinhTrangHocId;
                return _responseObject.ResponseSuccses("Thêm thành công", dataResponseTinhTrangHoc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Thêm không thành công", null);
            }
        }

        public async Task<bool> XoaTinhTrangHoc(int khoaHocId)
        {
            try
            {
                var obj = await _context.TinhTrangHoc.SingleOrDefaultAsync(c => c.TinhTrangHocId == khoaHocId);
                if (obj == null)
                {
                    return false;
                }
                _context.TinhTrangHoc.Remove(obj);
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

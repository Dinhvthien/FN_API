using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Services.Implements
{
    public class QuyenHanService : IQuyenHanService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseQuyenHan> _responseObject;
        private readonly ResponseObject<List<DataResponseQuyenHan>> _responseListObject;

        public QuyenHanService()
        {
            _context = new AppDbContext();
            _responseListObject = new ResponseObject<List<DataResponseQuyenHan>>();
            _responseObject = new ResponseObject<DataResponseQuyenHan>();
        }
        public async Task<ResponseObject<List<DataResponseQuyenHan>>> DanhSachQH(int page, int pageSize)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                pageSize = (pageSize <= 0) ? 10 : pageSize;
                var totalItems = await _context.QuyenHan.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var listTTh = await _context.QuyenHan
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                List<DataResponseQuyenHan> data = new List<DataResponseQuyenHan>();
                foreach (var item in listTTh)
                {
                    DataResponseQuyenHan dataResponseQH = new DataResponseQuyenHan();
                    dataResponseQH.QuyenHanId = item.QuyenHanId;
                    dataResponseQH.TenQuyenHan = item.TenQuyenHan;
                    data.Add(dataResponseQH);
                }

                return _responseListObject.ResponseSuccses($"Trang {page}/{totalPages}", data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseListObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }


        public async Task<ResponseObject<DataResponseQuyenHan>> SuaQuyenhan(int quyenhanid, string tenquyenhan)
        {
            try
            {
                var obj = await _context.QuyenHan.SingleOrDefaultAsync(c => c.QuyenHanId == quyenhanid);
                obj.TenQuyenHan = tenquyenhan;
                DataResponseQuyenHan dataResponseQH = new DataResponseQuyenHan();
                dataResponseQH.TenQuyenHan = tenquyenhan;
                dataResponseQH.QuyenHanId = obj.QuyenHanId;
                if (obj == null)
                {
                    return _responseObject.ResponseError(400, "Sửa không thành công", dataResponseQH);
                }
                _context.QuyenHan.Update(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa thành công", dataResponseQH);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Sửa không thành công", null);
            }
        }

        public async Task<ResponseObject<DataResponseQuyenHan>> ThemQuyenHan(string tenquyenhan)
        {
            try
            {
                QuyenHan quyenHan = new QuyenHan();
                quyenHan.TenQuyenHan = tenquyenhan;
                _context.QuyenHan.Add(quyenHan);
                await _context.SaveChangesAsync();
                DataResponseQuyenHan dataResponseQH = new DataResponseQuyenHan();
                dataResponseQH.QuyenHanId = quyenHan.QuyenHanId;
                dataResponseQH.TenQuyenHan = tenquyenhan;
                return _responseObject.ResponseSuccses("Thêm thành công", dataResponseQH);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Thêm không thành công", null);
            }
        }

        public async Task<bool> XoaQuyenhan(int quyenHanId)
        {
            try
            {
                var obj = await _context.QuyenHan.SingleOrDefaultAsync(c => c.QuyenHanId == quyenHanId);
                if (obj == null)
                {
                    return false;
                }
                _context.QuyenHan.Remove(obj);
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

using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Services.Implements
{
    public class LoaiBaiVietService : ILoaiBaiVietServices
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseLoaiBaiViet> _responseObject;
        private readonly ResponseObject<List<DataResponseLoaiBaiViet>> _responseListObject;

        public LoaiBaiVietService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseLoaiBaiViet>();
            _responseListObject = new ResponseObject<List<DataResponseLoaiBaiViet>>();
        }
        public async Task<ResponseObject<List<DataResponseLoaiBaiViet>>> DanhSachLbv(int page, int pageSize)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                pageSize = (pageSize <= 0) ? 10 : pageSize;
                var totalItems = await _context.LoaiBaiViet.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                totalPages = (totalPages <= 0) ? 1 : totalPages;

                var listTTh = await _context.LoaiBaiViet
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                List<DataResponseLoaiBaiViet> data = new List<DataResponseLoaiBaiViet>();
                foreach (var item in listTTh)
                {
                    DataResponseLoaiBaiViet dataResponseQH = new DataResponseLoaiBaiViet();
                    dataResponseQH.LoaiBaiVietId = item.LoaiBaiVietId;
                    dataResponseQH.TenLoai = item.TenLoai;
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

        public async Task<ResponseObject<DataResponseLoaiBaiViet>> SuaLoaiBaiViet(int loaibaivietid, string tenloaibaiviet)
        {
            try
            {
                var obj = await _context.LoaiBaiViet.SingleOrDefaultAsync(c => c.LoaiBaiVietId == loaibaivietid);
                obj.TenLoai = tenloaibaiviet;
                DataResponseLoaiBaiViet dataResponseQH = new DataResponseLoaiBaiViet();
                dataResponseQH.TenLoai = tenloaibaiviet;
                dataResponseQH.LoaiBaiVietId = obj.LoaiBaiVietId;
                if (obj == null)
                {
                    return _responseObject.ResponseError(400, "Sửa không thành công", dataResponseQH);
                }
                _context.LoaiBaiViet.Update(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa thành công", dataResponseQH);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Sửa không thành công", null);
            }
        }

        public async Task<ResponseObject<DataResponseLoaiBaiViet>> ThemLoaiBaiViet(string tenloaibaiviet)
        {
            try
            {
                LoaiBaiViet quyenHan = new LoaiBaiViet();
                quyenHan.TenLoai = tenloaibaiviet;
                await _context.LoaiBaiViet.AddAsync(quyenHan);
                await _context.SaveChangesAsync();
                DataResponseLoaiBaiViet dataResponseQH = new DataResponseLoaiBaiViet();
                dataResponseQH.LoaiBaiVietId = quyenHan.LoaiBaiVietId;
                dataResponseQH.TenLoai = tenloaibaiviet;
                return _responseObject.ResponseSuccses("Thêm thành công", dataResponseQH);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Thêm không thành công", null);
            }
        }

        public async Task<bool> XoaLoaiBaiViet(int loaibaivietid)
        {
            try
            {
                var obj = await  _context.LoaiBaiViet.SingleOrDefaultAsync(c => c.LoaiBaiVietId == loaibaivietid);
                if (obj == null)
                {
                    return false;
                }
                _context.LoaiBaiViet.Remove(obj);
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

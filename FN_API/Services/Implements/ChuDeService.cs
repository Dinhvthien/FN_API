using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.Converters;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Services.Implements
{
    public class ChuDeService:IChuDeService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseChuDe> _responseObject;
        private readonly ResponseObject<List<DataResponseChuDe>> _responseListObject; 
        private readonly ChuDeConverter _cdConverter;


        public ChuDeService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseChuDe>();
            _responseListObject = new ResponseObject<List<DataResponseChuDe>>();
            _cdConverter = new ChuDeConverter();
        }

        public async Task<ResponseObject<List<DataResponseChuDe>>> DanhSachCD(int page, int pageSize)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                pageSize = (pageSize <= 0) ? 10 : pageSize;
                var totalItems = await _context.ChuDe.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                totalPages = (totalPages <= 0) ? 1 : totalPages;

                var listTTh = await _context.ChuDe
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                List<Data_RequestChuDe> data = new List<Data_RequestChuDe>();
                foreach (var item in listTTh)
                {
                    Data_RequestChuDe dataResponseQH = new Data_RequestChuDe();
                    dataResponseQH.ChuDeId = item.ChuDeId;
                    dataResponseQH.TenChuDe = item.TenChuDe;
                    dataResponseQH.NoiDung = item.NoiDung;
                    dataResponseQH.LoaiBaiVietId = (int)item.LoaiBaiVietId;

                    data.Add(dataResponseQH);
                }

                return _responseListObject.ResponseSuccses($"Trang {page}/{totalPages}", await _cdConverter.DataRespomseenumChiTietHoaDon(data));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseListObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public async Task<ResponseObject<DataResponseChuDe>> Suachude(Data_RequestChuDe data)
        {
            try
            {
                if (data.TenChuDe == "" && data.NoiDung == "" && data.LoaiBaiVietId == 0)
                {
                    return _responseObject.ResponseError(400, "Không được để trống", null);
                }
                var obj = await _context.ChuDe.SingleOrDefaultAsync(c => c.ChuDeId == data.ChuDeId);
                obj.ChuDeId = data.ChuDeId;
                obj.NoiDung = data.NoiDung;
                obj.LoaiBaiVietId = data.LoaiBaiVietId;
                if (obj == null)
                {
                    return _responseObject.ResponseError(400, "Sửa không thành công",null);
                }
                _context.ChuDe.Update(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa thành công", await _cdConverter.DataRespomseChiTietHoaDon(data));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Sửa không thành công", null);
            }
        }

        public async Task<ResponseObject<DataResponseChuDe>> Themchude(Data_RequestChuDe data)
        {
            try
            {
                if (data.TenChuDe == null && data.NoiDung == null && data.LoaiBaiVietId == null)
                {
                    return _responseObject.ResponseError(400, "Không được để trống", null);
                }
                ChuDe cd = new ChuDe();
                cd.ChuDeId = data.ChuDeId;
                cd.TenChuDe = data.TenChuDe;
                cd.NoiDung = data.NoiDung;
                cd.LoaiBaiVietId = data.LoaiBaiVietId;
                await _context.ChuDe.AddAsync(cd);
                await _context.SaveChangesAsync();
                data.ChuDeId = cd.ChuDeId;
                return _responseObject.ResponseSuccses("Thêm thành công", await _cdConverter.DataRespomseChiTietHoaDon(data));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Thêm không thành công", null);
            }
        }

        public async Task<bool> Xoachude(int chudeid)
        {
            try
            {
                var obj = await _context.ChuDe.SingleOrDefaultAsync(c => c.ChuDeId == chudeid);
                if (obj == null)
                {
                    return false;
                }
                _context.ChuDe.Remove(obj);
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

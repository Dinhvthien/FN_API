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
    public class BaiVietService : IBaiVietService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseBaiViet> _responseObject;
        private readonly ResponseObject<List<DataResponseBaiViet>> _responselistObject;
        private readonly BaiVietConverter _bvConverter;
        public BaiVietService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseBaiViet>();
            _responselistObject = new ResponseObject<List<DataResponseBaiViet>>();
            _bvConverter = new BaiVietConverter();
        }
        public async Task<ResponseObject<List<DataResponseBaiViet>>> DanhSachBV(int page, int pageSize)
        {
            List<Data_RequestListBaiViet> data = new List<Data_RequestListBaiViet>();

            try
            {
                page = (page <= 0) ? 1 : page;
                pageSize = (pageSize <= 0) ? 10 : pageSize;
                var totalItems = await _context.BaiViet.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                totalPages = (totalPages <= 0) ? 1 : totalPages;

                var listTTh = await _context.BaiViet
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                foreach (var item in listTTh)
                {
                    Data_RequestListBaiViet dataResponseQH = new Data_RequestListBaiViet();
                    dataResponseQH.TaiKhoanId = (int)item.TaiKhoanId;
                    dataResponseQH.ChuDeId = (int)item.ChuDeId;
                    dataResponseQH.BaiVietId = item.BaiVietId;
                    dataResponseQH.TenbaiViet = item.TenbaiViet;

                    dataResponseQH.NoiDung = item.NoiDung;
                    dataResponseQH.TenTacGia = item.TenTacGia;
                    dataResponseQH.NoiDungNgan = item.NoiDungNgan;
                    dataResponseQH.Hinhanh = item.HinhAnh;
                    dataResponseQH.thoigiantao = (DateTime)item.ThoiGianTao;
                    data.Add(dataResponseQH);
                }

                return _responselistObject.ResponseSuccses($"Trang {page}/{totalPages}", await _bvConverter.DataRespomseeListBaiViet(data));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responselistObject.ResponseError(400, "Có lỗi xẩy ra", await _bvConverter.DataRespomseeListBaiViet(data));
            }
        }

        public async Task<ResponseObject<DataResponseBaiViet>> SuaBaiViet(BaiViet baiViet)
        {
            try
            {
                var findBaiViet = await _context.BaiViet.SingleOrDefaultAsync(c => c.BaiVietId == baiViet.BaiVietId);
                findBaiViet.TaiKhoanId = baiViet.TaiKhoanId;
                findBaiViet.ChuDeId = baiViet.ChuDeId;
                findBaiViet.TenbaiViet = baiViet.TenbaiViet;
                findBaiViet.BaiVietId = baiViet.BaiVietId;
                findBaiViet.NoiDung = baiViet.NoiDung;
                findBaiViet.TenTacGia = baiViet.TenTacGia;
                findBaiViet.NoiDungNgan = baiViet.NoiDungNgan;
                findBaiViet.HinhAnh = baiViet.HinhAnh;
                findBaiViet.ThoiGianTao = baiViet.ThoiGianTao;
                _context.BaiViet.Update(findBaiViet);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa thành công", await _bvConverter.DataRespomseBaiViet(baiViet));
            }
            catch (Exception)
            {
                return _responseObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public async Task<ResponseObject<DataResponseBaiViet>> ThemBaiViet(BaiViet baiViet)
        {
            try
            {
                baiViet.ThoiGianTao = DateTime.Now;
                await _context.BaiViet.AddAsync(baiViet);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("thêm thành công", await _bvConverter.DataRespomseBaiViet(baiViet));
            }
            catch (Exception)
            {
                return _responseObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public async Task<ResponseObject<List<DataResponseBaiViet>>> TimKiemBaiVietTheoTen(string name, int page, int pageSize)
        {
            try
            {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            page = (page <= 0) ? 1 : page;

            // Tìm kiếm tất cả các tài khoản có tên chứa từ khóa name
            var query = _context.BaiViet.Where(x => x.TenbaiViet.Contains(name));

            // Tính toán tổng số lượng tài khoản
            var totalItems = await query.CountAsync();

            // Tính toán tổng số trang
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                totalPages = (totalPages <= 0) ? 1 : totalPages;

                // Lấy các tài khoản cho trang cụ thể
                var taiKhoanList = await query
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

                // Chuyển đổi danh sách các tài khoản sang định dạng DataResponseTaiKhoan
                var responseDataList = taiKhoanList.Select(baiviet => new Data_RequestListBaiViet
                {
                    TaiKhoanId = (int)baiviet.TaiKhoanId,
                    ChuDeId = (int)baiviet.ChuDeId,
                    NoiDung = baiviet.NoiDung,
                    NoiDungNgan = baiviet.NoiDungNgan,
                    TenbaiViet = baiviet.TenbaiViet,
                    TenTacGia = baiviet.TenTacGia,
                    Hinhanh = baiviet.HinhAnh,
                    BaiVietId = baiviet.BaiVietId,
                    thoigiantao = (DateTime)baiviet.ThoiGianTao
                }).ToList();

                return _responselistObject.ResponseSuccses($"Trang {page}/{totalPages}", await _bvConverter.DataRespomseeListBaiViet(responseDataList));
        }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responselistObject.ResponseError(500, "Có lỗi xảy ra", null);
            }
}

        public async Task<BaiViet> Timtheoid(int id)
        {
            var baiviet = await _context.BaiViet.SingleOrDefaultAsync(c => c.BaiVietId == id);
            return baiviet;
        }

        public async Task<bool> XoaBaiViet(int baiVietId)
        {
            try
            {
                var obj = await _context.BaiViet.SingleOrDefaultAsync(c => c.BaiVietId == baiVietId);
                if (obj == null)
                {
                    return false;
                }
                _context.BaiViet.Remove(obj);
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

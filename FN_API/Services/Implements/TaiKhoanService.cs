using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.Converters;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;
using FN_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FN_API.Services.Implements
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly AppDbContext _context;
        private readonly TaiKhoanConverter _tkConverter;
        private readonly ResponseObject<DataResponseTaiKhoan> _responseObject;
        private readonly ResponseObject<List<DataResponseTaiKhoan>> _responselistObject;
        public TaiKhoanService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseTaiKhoan>();
            _responselistObject = new ResponseObject<List<DataResponseTaiKhoan>>();
            _tkConverter = new TaiKhoanConverter();
        }

        public async Task<ResponseObject<List<DataResponseTaiKhoan>>> DanhSachTK(int page, int pageSize)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                pageSize = (pageSize <= 0) ? 10 : pageSize;
                var totalItems = await _context.TaiKhoan.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var listTTh = await _context.TaiKhoan
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
                List<Data_RequestTaiKhoan> data = new List<Data_RequestTaiKhoan>();
                foreach (var item in listTTh)
                {
                    Data_RequestTaiKhoan dataResponseQH = new Data_RequestTaiKhoan();
                    dataResponseQH.TenDangNhap = item.TenDangNhap;
                    dataResponseQH.TaiKhoanId = item.TaiKhoanId;
                    dataResponseQH.TenNguoiDung = item.TenNguoiDung;
                    dataResponseQH.MatKhau = item.MatKhau;
                    dataResponseQH.TaiKhoanId = item.TaiKhoanId;
                    dataResponseQH.QuyenHanId = item.QuyenHanId;
                    data.Add(dataResponseQH);
                }
                return _responselistObject.ResponseSuccses($"Trang {page}/{totalPages}", await _tkConverter.DataRespomseListChiTietHoaDon(data));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responselistObject.ResponseError(400, "", null);
            }
        }

        public async Task<ResponseObject<DataResponseTaiKhoan>> SuaTaiKhoan(Data_RequestTaiKhoan taiKhoan)
        {
            try
            {
                var existingTaiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.TenDangNhap == taiKhoan.TenDangNhap && x.TaiKhoanId != taiKhoan.TaiKhoanId);

                // Nếu tài khoản đã tồn tại, trả về lỗi
                if (existingTaiKhoan != null)
                {
                    return _responseObject.ResponseError(400, "Tên đăng nhập đã được sử dụng, vui lòng chọn tên khác.", null);
                }
                // Nếu không tìm thấy tài khoản, trả về lỗi
                var existingTaiKhoans = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.TaiKhoanId == taiKhoan.TaiKhoanId);
                if (existingTaiKhoans == null)
                {
                    return _responseObject.ResponseError(404, "Không tìm thấy tài khoản", null);
                }
                var passwordRegex = new Regex(@"^(?=.*[0-9])(?=.*[^a-zA-Z0-9\s]).+$");
                if (!passwordRegex.IsMatch(taiKhoan.MatKhau))
                {
                    return _responseObject.ResponseError(400, "Mật khẩu phải chứa ít nhất một chữ số và một kí tự đặc biệt.", null);
                }
                // Cập nhật thông tin tài khoản với dữ liệu mới
                existingTaiKhoans.TenNguoiDung = taiKhoan.TenNguoiDung;
                existingTaiKhoans.TenDangNhap = taiKhoan.TenDangNhap;
                existingTaiKhoans.MatKhau = taiKhoan.MatKhau;
                existingTaiKhoans.QuyenHanId = taiKhoan.QuyenHanId;
                _context.TaiKhoan.Update(existingTaiKhoans);
                await _context.SaveChangesAsync();

                // Tạo một đối tượng Response chứa thông tin tài khoản đã được sửa
                var responseData = new Data_RequestTaiKhoan
                {
                    TaiKhoanId = existingTaiKhoans.TaiKhoanId,
                    TenNguoiDung = existingTaiKhoans.TenNguoiDung,
                    TenDangNhap = existingTaiKhoans.TenDangNhap,
                    MatKhau = existingTaiKhoans.MatKhau,
                    QuyenHanId = existingTaiKhoans.QuyenHanId
                };

                return _responseObject.ResponseSuccses("Đã cập nhật thông tin tài khoản", await _tkConverter.DataRespomseChiTietHoaDon(responseData));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(500, "Có lỗi xẩy ra khi cập nhật thông tin tài khoản", null);
            }
        }


        public async Task<ResponseObject<DataResponseTaiKhoan>> ThemTaiKhoan(Data_RequestTaiKhoan taiKhoan)
        {
            try
            {
                var existingTaiKhoan = await _context.TaiKhoan.FirstOrDefaultAsync(x => x.TenDangNhap == taiKhoan.TenDangNhap);

                // Nếu tài khoản đã tồn tại, trả về lỗi
                if (existingTaiKhoan != null)
                {
                    return _responseObject.ResponseError(400, "Tên đăng nhập đã được sử dụng, vui lòng chọn tên khác.", null);
                }
                var passwordRegex = new Regex(@"^(?=.*[0-9])(?=.*[^a-zA-Z0-9\s]).+$");
                if (!passwordRegex.IsMatch(taiKhoan.MatKhau))
                {
                    return _responseObject.ResponseError(400, "Mật khẩu phải chứa ít nhất một chữ số và một kí tự đặc biệt.", null);
                }
                // Tạo một đối tượng mới từ dữ liệu được truyền vào
                var newTaiKhoan = new TaiKhoan
                {
                    TenNguoiDung = taiKhoan.TenNguoiDung,
                    TenDangNhap = taiKhoan.TenDangNhap,
                    MatKhau = taiKhoan.MatKhau,
                    QuyenHanId = taiKhoan.QuyenHanId
                };

                // Thêm tài khoản mới vào cơ sở dữ liệu
                await _context.TaiKhoan.AddAsync(newTaiKhoan);
                await _context.SaveChangesAsync();

                // Tạo một đối tượng Response chứa thông tin tài khoản mới được thêm vào
                var responseData = new Data_RequestTaiKhoan
                {
                    TaiKhoanId = newTaiKhoan.TaiKhoanId,
                    TenNguoiDung = newTaiKhoan.TenNguoiDung,
                    TenDangNhap = newTaiKhoan.TenDangNhap,
                    MatKhau = newTaiKhoan.MatKhau,
                    QuyenHanId = newTaiKhoan.QuyenHanId
                };
                return _responseObject.ResponseSuccses("Đã thêm tài khoản mới", await _tkConverter.DataRespomseChiTietHoaDon(responseData));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(500, "Có lỗi xảy ra khi thêm tài khoản", null);
            }
        }

        public async Task<ResponseObject<List<DataResponseTaiKhoan>>> TimKiemTaiKhoanTheoTen(string name, int page, int pageSize)
        {
            try
            {
                pageSize = (pageSize <= 0) ? 10 : pageSize;
                page = (page <= 0) ? 1 : page;

                // Tìm kiếm tất cả các tài khoản có tên chứa từ khóa name
                var query = _context.TaiKhoan.Where(x => x.TenNguoiDung.Contains(name));

                // Tính toán tổng số lượng tài khoản
                var totalItems = await query.CountAsync();

                // Tính toán tổng số trang
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                // Lấy các tài khoản cho trang cụ thể
                var taiKhoanList = await query
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

                // Chuyển đổi danh sách các tài khoản sang định dạng DataResponseTaiKhoan
                var responseDataList = taiKhoanList.Select(taiKhoan => new Data_RequestTaiKhoan
                {
                    TaiKhoanId = taiKhoan.TaiKhoanId,
                    TenNguoiDung = taiKhoan.TenNguoiDung,
                    TenDangNhap = taiKhoan.TenDangNhap,
                    MatKhau = taiKhoan.MatKhau,
                    QuyenHanId = taiKhoan.QuyenHanId
                }).ToList();

                return _responselistObject.ResponseSuccses($"Trang {page}/{totalPages}", await _tkConverter.DataRespomseenumChiTietHoaDon(responseDataList));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responselistObject.ResponseError(500, "Có lỗi xảy ra khi thêm tài khoản", null);
            }
        }


        public async Task<bool> XoaTaiKhoan(int taikhoanid)
        {
            try
            {
                var obj = await _context.TaiKhoan.SingleOrDefaultAsync(c => c.TaiKhoanId == taikhoanid);
                if (obj != null)
                {
                    _context.TaiKhoan.Remove(obj);
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

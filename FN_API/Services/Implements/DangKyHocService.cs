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
    public class DangKyHocService : IDangKyHocService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseDangKyHoc> _responseObject;
        private readonly ResponseObject<List<DataResponseDangKyHoc>> _responseListObject;
        private readonly DangKyHocConverter _dkConverter;


        public DangKyHocService()
        {
            _context = new AppDbContext();
            _responseObject = new ResponseObject<DataResponseDangKyHoc>();
            _responseListObject = new ResponseObject<List<DataResponseDangKyHoc>>();
            _dkConverter = new DangKyHocConverter();
        }

        public async Task<ResponseObject<List<DataResponseDangKyHoc>>> DanhSachDangKyHoc(int page, int pageSize)
        {
            try
            {
                page = (page <= 0) ? 1 : page;
                pageSize = (pageSize <= 0) ? 10 : pageSize;
                var totalItems = await _context.DangKyHoc.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                totalPages = (totalPages <= 0) ? 1 : totalPages;

                var listTTh = await _context.DangKyHoc
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
                return _responseListObject.ResponseSuccses($"Trang {page}/{totalPages}", await _dkConverter.DataRespomseListHocVien(listTTh));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseListObject.ResponseError(400, "Có lỗi xẩy ra", null);
            }
        }

        public async Task<ResponseObject<DataResponseDangKyHoc>> SuaDangKyHoc(DangKyHoc dangKyHoc)
        {
            try
            {
                var findKh = await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == dangKyHoc.KhoaHocId);
                if (findKh == null)
                {
                    return _responseObject.ResponseError(400, $"không tìm thấy đăng ký học có {dangKyHoc.KhoaHocId}", null);
                }
                    var obj = await _context.DangKyHoc.SingleOrDefaultAsync(c => c.DangKyHocId == dangKyHoc.DangKyHocId);
                var findKhoahocold = await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == obj.KhoaHocId);

                if (obj == null)
                {
                    return _responseObject.ResponseError(400, $"không tìm thấy đăng ký học có {dangKyHoc.DangKyHocId}", null);
                }

                if (obj.TinhTrangHocId == 1 && dangKyHoc.TinhTrangHocId > 1)
                {
                    findKh.SoHocVien += 1;
                }
                if (dangKyHoc.TinhTrangHocId == 2)
                {
                    obj.NgayBatDau = DateTime.Now;
                }
                obj.KhoaHocId = dangKyHoc.KhoaHocId;
                obj.HocVienId = dangKyHoc.HocVienId;
                obj.TinhTrangHocId = dangKyHoc.TinhTrangHocId;
                obj.TaiKhoanId = dangKyHoc.TaiKhoanId;

                // khi thay doi khoa hoc thi up date lai so hoc vien khoa hoc cu va moi || tinh lai ngay ket thuc
                if ((obj.KhoaHocId != dangKyHoc.KhoaHocId)||( dangKyHoc.TinhTrangHocId > 1 && obj.TinhTrangHocId == 1))
                {
                    findKhoahocold.SoHocVien -= 1;
                    findKh.SoHocVien += 1;
                    if (obj.NgayBatDau != null)
                    {
                        DateTime tingngayktnew = (DateTime)obj.NgayBatDau;
                        obj.NgayKetThuc = tingngayktnew.AddDays((double)findKh.ThoiGianHoc);
                    }
                    _context.KhoaHoc.Update(findKhoahocold);
                    _context.KhoaHoc.Update(findKh);
                }

                _context.DangKyHoc.Update(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Sửa thành công", await _dkConverter.DataRespomsehocvien(obj));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Sửa không thành công", null);
            }
        }

        public async Task<ResponseObject<DataResponseDangKyHoc>> ThemDangKyHoc(DangKyHoc dangKyHoc)
        {
            try
            {
                DangKyHoc obj = new DangKyHoc();
                obj.KhoaHocId = dangKyHoc.KhoaHocId;
                obj.HocVienId = dangKyHoc.HocVienId;
                obj.TinhTrangHocId = dangKyHoc.TinhTrangHocId;
                obj.TaiKhoanId = dangKyHoc.TaiKhoanId;
                obj.NgayDangKy = DateTime.Now;
                obj.NgayBatDau = null;
                obj.NgayKetThuc = null;
                var findKh = await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == obj.KhoaHocId);
                if (obj.TinhTrangHocId == 2)
                {
                    DateTime ngayHienTai = DateTime.Now;
                    DateTime ngaykt = ngayHienTai.AddDays((double)findKh.ThoiGianHoc);
                    obj.NgayBatDau = ngayHienTai;
                    obj.NgayKetThuc = ngaykt;
                    findKh.SoHocVien += 1;
                    _context.KhoaHoc.Update(findKh);
                }
                else
                {
                    obj.NgayBatDau = null;
                    obj.NgayKetThuc = null;
                }

                if (obj.TinhTrangHocId > 2)
                {
                    findKh.SoHocVien += 1;
                    _context.KhoaHoc.Update(findKh);
                    obj.NgayBatDau = null;
                    obj.NgayKetThuc = null;
                }

                var check = await _context.DangKyHoc.AddAsync(obj);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccses("Thêm thành công", await _dkConverter.DataRespomsehocvien(obj));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _responseObject.ResponseError(400, "Thêm không thành công", null);
            }
        }

        public async Task<DangKyHoc> Timtheoid(int id)
        {
            var product = await _context.DangKyHoc.SingleOrDefaultAsync(c => c.DangKyHocId == id);
            return product;
        }

        public async Task<bool> XoaDangKyHoc(int dangkyhocid)
        {
            try
            {
                var obj = await _context.DangKyHoc.SingleOrDefaultAsync(c => c.DangKyHocId == dangkyhocid);
                if (obj != null)
                {
                    if (obj.TinhTrangHocId > 1)
                    {
                        var findKh = await _context.KhoaHoc.SingleOrDefaultAsync(c => c.KhoaHocId == obj.KhoaHocId);
                        findKh.SoHocVien -= 1;
                        _context.KhoaHoc.Update(findKh);
                    }
                    _context.DangKyHoc.Remove(obj);
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

using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Services;
using FN_API.Services.Implements;
using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        private readonly IHocVienService _hocvienService;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;
        private readonly IFileService _fileService;
        public HocVienController(IHocVienService hocvienService, IFileService fileService)
        {
            _hocvienService = hocvienService;
            _fileService = fileService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachHocVien(int page)
        {
            var hocvien = await _hocvienService.DanhSachHV(page);
            return Ok(hocvien);
        }
        [HttpPost("Themhocvien")]
        public async Task<IActionResult> ThemHocVien([FromForm] Data_RequestHocVien hocVien)
        {

            try
            {
                if (hocVien.image?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = {".jpg", ".jpeg", ".png"};
                string createdImageName = await _fileService.SaveFileAsync(hocVien.image, allowedFileExtentions);

                // mapping `ProductDTO` to `Product` manually. You can use automapper.
                var hocvien = new HocVien
                {
                    Hoten = hocVien.Hoten,
                    NgaySinh = hocVien.NgaySinh,
                    SoDienThoai = hocVien.SoDienThoai,
                    Email = hocVien.Email,
                    TinhThanh = hocVien.TinhThanh,
                    QuanHuyen = hocVien.QuanHuyen,
                    PhuongXa = hocVien.PhuongXa,
                    HinhAnh = createdImageName,
                    SoNha = hocVien.SoNha
                };
                return Ok(await _hocvienService.ThemHocVien(hocvien));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("timkiemtheoten")]
        public async Task<IActionResult> TimKiemKhoaHocTheoTen(string TenKhoaHoc, int page)
        {
            return Ok(await _hocvienService.TimKiemHVTheoTenvaEmail(TenKhoaHoc, page));
        }

        [HttpPut]
        public async Task<IActionResult> SuaHocVien(int id ,[FromForm] Data_requestUpdateHV hocvien)
        {
            try
            {
                if (id != hocvien.HocVienId)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"id in url and form body does not match.");
                }

                var existingHocVien = await _hocvienService.Timtheoid(id);
                if (existingHocVien == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
                }
                string oldImage = existingHocVien.HinhAnh;
                if (hocvien.image != null)
                {
                    if (hocvien.image?.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                    }
                    string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                    string createdImageName = await _fileService.SaveFileAsync(hocvien.image, allowedFileExtentions);
                    hocvien.imageold = createdImageName;
                }

                // mapping `ProductDTO` to `Product` manually. You can use automapper.
                existingHocVien.HocVienId = (int)hocvien.HocVienId;
                existingHocVien.Hoten = hocvien.Hoten;
                existingHocVien.NgaySinh = hocvien.NgaySinh;
                existingHocVien.SoDienThoai = hocvien.SoDienThoai;
                existingHocVien.Email = hocvien.Email;
                existingHocVien.TinhThanh = hocvien.TinhThanh;
                existingHocVien.QuanHuyen = hocvien.QuanHuyen;
                existingHocVien.PhuongXa = hocvien.PhuongXa;
                existingHocVien.HinhAnh = hocvien.imageold;
                existingHocVien.SoNha = hocvien.SoNha;

            
                if (hocvien.image != null)
                    _fileService.DeleteFile(oldImage);

                return Ok(await _hocvienService.Suahocvien(existingHocVien));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> XoaHocVien(int hocvienid)
        {
            var existingHocVien = await _hocvienService.Timtheoid(hocvienid);

            if (existingHocVien.HinhAnh != null)
                _fileService.DeleteFile(existingHocVien.HinhAnh);
            return Ok(await _hocvienService.XoaHocvien(hocvienid));
        }
    }
}

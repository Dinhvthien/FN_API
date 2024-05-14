using FN_API.Payloads.DataRequests;
using FN_API.Services.Implements;
using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVienController : ControllerBase
    {
        private readonly IHocVienService _hocvienService;
        private readonly IWebHostEnvironment _env;
        public HocVienController(IHocVienService hocvienService, IWebHostEnvironment env)
        {
            _hocvienService = hocvienService;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachHocVien(int page)
        {
            var hocvien = await _hocvienService.DanhSachHV(page);
            return Ok(hocvien);
        }
        [HttpPost("Themhocvien")]
        public async Task<IActionResult> ThemHocVien(IFormFile file, Data_RequestHocVien hocVien)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Đảm bảo thư mục lưu trữ ảnh tồn tại
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Tạo tên tệp duy nhất và lưu tệp
            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Tạo URL của ảnh
            var imageUrl = $"/uploads/{fileName}";
            return Ok(await _hocvienService.ThemHocVien(imageUrl, hocVien));
        }
        [HttpPost("timkiemtheoten")]
        public async Task<IActionResult> TimKiemKhoaHocTheoTen(string TenKhoaHoc, int page)
        {
            return Ok(await _hocvienService.TimKiemHVTheoTenvaEmail(TenKhoaHoc, page));
        }

        [HttpPut]
        public async Task<IActionResult> SuaHocVien(Data_RequestHocVien khoaHoc)
        {
            return Ok(await _hocvienService.Suahocvien(khoaHoc));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaHocVien(int hocvienid)
        {
            return Ok(await _hocvienService.XoaHocvien(hocvienid));
        }
    }
}

using FN_API.Payloads.DataRequests;
using FN_API.Services.Implements;
using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _taikhoanService;
        public TaiKhoanController(ITaiKhoanService taiKhoanService)
        {
            _taikhoanService = taiKhoanService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachTaiKhoan(int page,int pagesize)
        {
            var taikhoan = await _taikhoanService.DanhSachTK(page,pagesize);
            return Ok(taikhoan);
        }
        [HttpPost("Themtaikhoan")]
        public async Task<IActionResult> ThemTaiKhoan(Data_RequestTaiKhoan taikhoan)
        {
            return Ok(await _taikhoanService.ThemTaiKhoan(taikhoan));
        }
        [HttpPost("timkiemtheoten")]
        public async Task<IActionResult> TimKiemTaiKhoanTheoTen(string tentaikhoan, int page,int pagesize)
        {
            return Ok(await _taikhoanService.TimKiemTaiKhoanTheoTen(tentaikhoan, page, pagesize));
        }

        [HttpPut]
        public async Task<IActionResult> SuaKhoaHoc(Data_RequestTaiKhoan taikhoan)
        {
            return Ok(await _taikhoanService.SuaTaiKhoan(taikhoan));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaKhoaHoc(int taikhoanid)
        {
            return Ok(await _taikhoanService.XoaTaiKhoan(taikhoanid));
        }
    }
}

using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinhTrangHocController : ControllerBase
    {
        private readonly ITinhTrangHocService _tinhTrangHocService;
        public TinhTrangHocController(ITinhTrangHocService tinhTrangHocService)
        {
            _tinhTrangHocService = tinhTrangHocService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachLoaiKhoaHoc()
        {
            var khoahoc = await _tinhTrangHocService.DanhSachTTH();
            return Ok(khoahoc);
        }
        [HttpPost("themquyenhan")]
        public async Task<IActionResult> ThemLoaiKhoaHoc(string name)
        {
            return Ok(await _tinhTrangHocService.ThemTinhTrangHoc(name));
        }

        [HttpPut]
        public async Task<IActionResult> SuaLoaiKhoaHoc(int KhoaHocId, string name)
        {
            return Ok(await _tinhTrangHocService.SuaTinhTrangHoc(KhoaHocId, name));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaLoaiKhoaHoc(int khoaHocId)
        {
            return Ok(await _tinhTrangHocService.XoaTinhTrangHoc(khoaHocId));
        }
    }
}

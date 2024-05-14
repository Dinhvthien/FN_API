using FN_API.Payloads.DataRequests;
using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiKhoaHocController : ControllerBase
    {
        private readonly ILoaiKhoaHocService _LoaikhoahocService;
        public LoaiKhoaHocController(ILoaiKhoaHocService loaiKhoaHocService)
        {
            _LoaikhoahocService = loaiKhoaHocService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachLoaiKhoaHoc()
        {
            var khoahoc = await _LoaikhoahocService.DanhSachLKh();
            return Ok(khoahoc);
        }
        [HttpPost("ThemLoaiKhoahoc")]
        public async Task<IActionResult> ThemLoaiKhoaHoc(string name)
        {
            return Ok(await _LoaikhoahocService.ThemLoaiKhoaHoc(name));
        }

        [HttpPut]
        public async Task<IActionResult> SuaLoaiKhoaHoc(int KhoaHocId, string name)
        {
            return Ok(await _LoaikhoahocService.SuaLoaiKhoaHoc(KhoaHocId, name));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaLoaiKhoaHoc(int khoaHocId)
        {
            return Ok(await _LoaikhoahocService.XoaLoaiKhoaHoc(khoaHocId));
        }
    }
}

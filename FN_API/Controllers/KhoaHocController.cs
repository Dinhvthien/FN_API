using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Services.Implements;
using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : ControllerBase
    {
        private readonly IKhoaHocService _khoahocService;
        public KhoaHocController(IKhoaHocService khoahocService)
        {
            _khoahocService = khoahocService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachKhoaHoc(int page)
        {
            var khoahoc = await _khoahocService.DanhSachKh(page);
            return Ok(khoahoc);
        }
        [HttpPost("ThemKhoahoc")]
        public async Task<IActionResult> ThemHoaDon(Data_RequetKhoaHoc khoaHoc)
        {
            return Ok(await _khoahocService.ThemKhoaHoc(khoaHoc));
        }
        [HttpPost("timkiemtheoten")]
        public async Task<IActionResult> TimKiemKhoaHocTheoTen(string TenKhoaHoc, int page)
        {
            return Ok(await _khoahocService.TimKiemKhoaHocTheoTen(TenKhoaHoc,page));
        }

        [HttpPut]
        public async Task<IActionResult> SuaKhoaHoc(Data_RequetKhoaHoc khoaHoc)
        {
            return Ok(await _khoahocService.SuaKhoaHoc(khoaHoc));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaKhoaHoc(int khoaHocId)
        {
            return Ok(await _khoahocService.XoaKhoaHoc(khoaHocId));
        }
    }
}

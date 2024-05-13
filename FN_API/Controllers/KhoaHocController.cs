using FN_API.Entities;
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
        public IActionResult DanhSachKhoaHoc()
        {
            return Ok(_khoahocService.DanhSachKh());
        }
        [HttpPost("ThemKhoahoc")]
        public IActionResult ThemHoaDon(KhoaHoc khoaHoc)
        {
            return Ok(_khoahocService.ThemKhoaHoc(khoaHoc));
        }

        [HttpPut]
        public IActionResult SuaKhoaHoc(KhoaHoc khoaHoc)
        {
            return Ok(_khoahocService.SuaKhoaHoc(khoaHoc));
        }
        [HttpDelete]
        public IActionResult XoaKhoaHoc(int khoaHocId)
        {
            return Ok(_khoahocService.XoaKhoaHoc(khoaHocId));
        }
    }
}

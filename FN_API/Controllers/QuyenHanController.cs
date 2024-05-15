using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyenHanController : ControllerBase
    {
        private readonly IQuyenHanService _quyenHanService;
        public QuyenHanController(IQuyenHanService quyenHanService)
        {
            _quyenHanService = quyenHanService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachQuyenHan(int page, int pageSize)
        {
            var khoahoc = await _quyenHanService.DanhSachQH(page,pageSize);
            return Ok(khoahoc);
        }
        [HttpPost("themtinhtranghoc")]
        public async Task<IActionResult> ThemLoaiQuyenHan(string name)
        {
            return Ok(await _quyenHanService.ThemQuyenHan(name));
        }

        [HttpPut]
        public async Task<IActionResult> SuaQuyenHan(int quyenhanId, string name)
        {
            return Ok(await _quyenHanService.SuaQuyenhan(quyenhanId, name));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaLoaiKhoaHoc(int quyenhanId)
        {
            return Ok(await _quyenHanService.XoaQuyenhan(quyenhanId));
        }
    }
}

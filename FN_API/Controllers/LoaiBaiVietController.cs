using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiBaiVietController : ControllerBase
    {
        private readonly ILoaiBaiVietServices _loaibaivietService;
        public LoaiBaiVietController(ILoaiBaiVietServices loaibaivietService)
        {
            _loaibaivietService = loaibaivietService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachLoaiBaiViet(int page, int pageSize)
        {
            var loaibaiviet = await _loaibaivietService.DanhSachLbv(page, pageSize);
            return Ok(loaibaiviet);
        }
        [HttpPost("themloaibaiviet")]
        public async Task<IActionResult> ThemLoaiBaiViet(string name)
        {
            return Ok(await _loaibaivietService.ThemLoaiBaiViet(name));
        }

        [HttpPut]
        public async Task<IActionResult> SuaLoaiBaiViet(int BaiVietId, string name)
        {
            return Ok(await _loaibaivietService.SuaLoaiBaiViet(BaiVietId, name));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaLoaiBaiViet(int BaiVietId)
        {
            return Ok(await _loaibaivietService.XoaLoaiBaiViet(BaiVietId));
        }
    }
}

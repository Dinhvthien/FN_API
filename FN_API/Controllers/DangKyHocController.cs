using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangKyHocController : ControllerBase
    {
        private readonly IDangKyHocService _dangkyhocService;
        public DangKyHocController(IDangKyHocService dangKyHocService)
        {
            _dangkyhocService = dangKyHocService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachdangkyhoc(int page,int pagesize)
        {
            var dkh = await _dangkyhocService.DanhSachDangKyHoc(page,pagesize);
            return Ok(dkh);
        }
        [HttpPost("ThemDangkyhoc")]
        public async Task<IActionResult> ThemDangkyhoc(Data_RequestDangKyHoc khoaHoc)
        {
            DangKyHoc dangKyHoc = new DangKyHoc();
            dangKyHoc.DangKyHocId = khoaHoc.DangKyHocId;
            dangKyHoc.TaiKhoanId = khoaHoc.TaiKhoanId;
            dangKyHoc.KhoaHocId = khoaHoc.KhoaHocId;
            dangKyHoc.TinhTrangHocId = khoaHoc.TinhTrangHocId;
            dangKyHoc.HocVienId = khoaHoc.HocVienId;
            return Ok(await _dangkyhocService.ThemDangKyHoc(dangKyHoc));
        }
        [HttpPut]
        public async Task<IActionResult> Suadangkyhoc(Data_RequestDangKyHoc khoaHoc)
        {
            var findkh = await _dangkyhocService.Timtheoid(khoaHoc.DangKyHocId);
            if (findkh != null)
            {
                findkh.DangKyHocId = khoaHoc.DangKyHocId;
                findkh.TaiKhoanId = khoaHoc.TaiKhoanId;
                findkh.KhoaHocId = khoaHoc.KhoaHocId;
                findkh.TinhTrangHocId = khoaHoc.TinhTrangHocId;
                findkh.HocVienId = khoaHoc.HocVienId;
            }
            else
            {
                return BadRequest();
            }
            return Ok(await _dangkyhocService.SuaDangKyHoc(findkh));
        }
        [HttpDelete]
        public async Task<IActionResult> XoaKhoaHoc(int khoaHocId)
        {
            return Ok(await _dangkyhocService.XoaDangKyHoc(khoaHocId));
        }
    }
}

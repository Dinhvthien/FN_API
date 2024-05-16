using FN_API.Services.Interfaces;
using FN_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FN_API.Services.Implements;
using FN_API.Entities;
using FN_API.Payloads.DataRequests;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiVietController : ControllerBase
    {
        private readonly IBaiVietService _baiVietService;
        private readonly IFileService _fileService;
        public BaiVietController(IBaiVietService baiVietService, IFileService fileService)
        {
            _baiVietService = baiVietService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> DanhSachBaiViet(int page, int pagesize)
        {
            var hocvien = await _baiVietService.DanhSachBV(page, pagesize);
            return Ok(hocvien);
        }
        [HttpPost("Thembaiviet")]
        public async Task<IActionResult> ThembaiViet([FromForm] Data_RequestAddBaiViet baiViet)
        {

            try
            {
                if (baiViet.Hinhanh?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                string createdImageName = await _fileService.SaveFileAsync(baiViet.Hinhanh, allowedFileExtentions);
                var baiviet = new BaiViet
                {
                    TenbaiViet = baiViet.TenbaiViet,
                    TenTacGia = baiViet.TenTacGia,
                    NoiDung = baiViet.NoiDung,
                    NoiDungNgan = baiViet.NoiDungNgan,
                    HinhAnh = createdImageName,
                    ChuDeId = baiViet.ChuDeId,
                    TaiKhoanId = baiViet.TaiKhoanId
                };
                return Ok(await _baiVietService.ThemBaiViet(baiviet));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> SuaBaiViet([FromForm] Data_RequestUpdateBaiViet baiviet)
        {
            try
            {
                var existingBaiViet = await _baiVietService.Timtheoid(baiviet.BaiVietId);
                if (existingBaiViet == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Bài viết với id: {baiviet.BaiVietId} không tìm thấy");
                }
                string oldImage = existingBaiViet.HinhAnh;
                if (baiviet.Hinhanh != null)
                {
                    if (baiviet.Hinhanh?.Length > 1 * 1024 * 1024)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                    }
                    string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                    string createdImageName = await _fileService.SaveFileAsync(baiviet.Hinhanh, allowedFileExtentions);
                    baiviet.Hinhanhold = createdImageName;
                }
                existingBaiViet.TenbaiViet = baiviet.TenbaiViet;
                existingBaiViet.TenTacGia = baiviet.TenTacGia;
                existingBaiViet.NoiDung = baiviet.NoiDung;
                existingBaiViet.NoiDungNgan = baiviet.NoiDungNgan;
                existingBaiViet.HinhAnh = baiviet.Hinhanhold;
                existingBaiViet.TaiKhoanId = baiviet.TaiKhoanId;
                existingBaiViet.ChuDeId = baiviet.ChuDeId;
                existingBaiViet.ThoiGianTao = baiviet.thoigiantao;
                existingBaiViet.HinhAnh = baiviet.Hinhanhold;


                if (baiviet.Hinhanh != null)
                    _fileService.DeleteFile(oldImage);

                return Ok(await _baiVietService.SuaBaiViet(existingBaiViet));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> XoabaiViet(int baivietid)
        {
            var existingBaiViet = await _baiVietService.Timtheoid(baivietid);
            if (existingBaiViet.HinhAnh != null)
                _fileService.DeleteFile(existingBaiViet.HinhAnh);
            return Ok(await _baiVietService.XoaBaiViet(baivietid));
        }

        [HttpPost("timkiemtheoten")]
        public async Task<IActionResult> TimKiembaiVietTheoTen(string tenbaiviet, int page,int pageSize)
        {
            return Ok(await _baiVietService.TimKiemBaiVietTheoTen(tenbaiviet, page, pageSize));
        }
    }
}

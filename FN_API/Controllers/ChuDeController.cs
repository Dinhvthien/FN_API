using FN_API.Payloads.DataRequests;
using FN_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuDeController : ControllerBase
    {
        private readonly IChuDeService _chudeService;
        public ChuDeController(IChuDeService chudeService)
        {
            _chudeService = chudeService;
        }
        [HttpGet]
        public async Task<IActionResult> DanhSachchudei(int page, int pageSize)
        {
            var chude = await _chudeService.DanhSachCD(page, pageSize);
            return Ok(chude);
        }
        [HttpPost("themchude")]
        public async Task<IActionResult> Themchudei(Data_RequestChuDe chuDe)
        {
            return Ok(await _chudeService.Themchude(chuDe));
        }

        [HttpPut]
        public async Task<IActionResult> SuachudeiViet(Data_RequestChuDe chuDe)
        {
            return Ok(await _chudeService.Suachude(chuDe));
        }
        [HttpDelete]
        public async Task<IActionResult> XoachudeiViet(int chudeid)
        {
            return Ok(await _chudeService.Xoachude(chudeid));
        }
    }
}

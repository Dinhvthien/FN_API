using FN_API.Entities;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface ILoaiKhoaHocService
    {
        public Task<ResponseObject<List<LoaiKhoaHoc>>> DanhSachLKh();
        public Task<ResponseObject<LoaiKhoaHoc>> ThemLoaiKhoaHoc(string TenLoaiKhoaHoc);
        public Task<ResponseObject<LoaiKhoaHoc>> SuaLoaiKhoaHoc(int khoahocid, string TenLoaiKhoaHoc);
        public Task<bool> XoaLoaiKhoaHoc(int khoaHocId);

    }
}

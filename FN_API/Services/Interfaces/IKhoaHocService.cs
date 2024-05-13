using FN_API.Entities;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IKhoaHocService
    {
        public Task<ResponseObject<List<KhoaHoc>>> DanhSachKh();

        public Task<ResponseObject<KhoaHoc>> ThemKhoaHoc(KhoaHoc khoaHoc);
        public Task<ResponseObject<KhoaHoc>> SuaKhoaHoc(KhoaHoc khoaHoc);
        public Task<ResponseObject<KhoaHoc>> XoaKhoaHoc(int khoaHocId);


    }
}

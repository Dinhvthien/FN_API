using FN_API.Entities;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IDangKyHocService
    {
        public Task<ResponseObject<List<DataResponseDangKyHoc>>> DanhSachDangKyHoc(int page, int pageSize);
        public Task<ResponseObject<DataResponseDangKyHoc>> ThemDangKyHoc(DangKyHoc dangKyHoc);
        public Task<ResponseObject<DataResponseDangKyHoc>> SuaDangKyHoc(DangKyHoc dangKyHoc);
        public Task<bool> XoaDangKyHoc(int dangkyhocid);
        public Task<DangKyHoc> Timtheoid(int id);

    }
}

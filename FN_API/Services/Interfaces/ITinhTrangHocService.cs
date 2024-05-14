using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface ITinhTrangHocService
    {
        public Task<ResponseObject<List<DataResponseTinhTrangHoc>>> DanhSachTTH();
        public Task<ResponseObject<DataResponseTinhTrangHoc>> ThemTinhTrangHoc(string TenTinhTrang);
        public Task<ResponseObject<DataResponseTinhTrangHoc>> SuaTinhTrangHoc(int TinhTrangId, string TenTinhTrang);
        public Task<bool> XoaTinhTrangHoc(int khoaHocId);
    }
}

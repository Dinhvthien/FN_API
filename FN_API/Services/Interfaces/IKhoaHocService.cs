using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IKhoaHocService
    {
        public Task<ResponseObject<List<DataResponseKhoaHoc>>> DanhSachKh(int page = 1);
        public Task<ResponseObject<DataResponseKhoaHoc>> ThemKhoaHoc(Data_RequetKhoaHoc khoaHoc);
        public Task<ResponseObject<DataResponseKhoaHoc>> SuaKhoaHoc(Data_RequetKhoaHoc khoaHoc);
        public Task<bool> XoaKhoaHoc(int khoaHocId);
        public Task<ResponseObject<List<DataResponseKhoaHoc>>> TimKiemKhoaHocTheoTen(string name,int page);
    }
}

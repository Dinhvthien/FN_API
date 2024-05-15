using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface ITaiKhoanService
    {
        public Task<ResponseObject<List<DataResponseTaiKhoan>>> DanhSachTK(int page, int pageSize );
        public Task<ResponseObject<DataResponseTaiKhoan>> ThemTaiKhoan(Data_RequestTaiKhoan khoaHoc);
        public Task<ResponseObject<DataResponseTaiKhoan>> SuaTaiKhoan(Data_RequestTaiKhoan khoaHoc);
        public Task<bool> XoaTaiKhoan(int khoaHocId);
        public Task<ResponseObject<List<DataResponseTaiKhoan>>> TimKiemTaiKhoanTheoTen(string name, int page, int pageSize);
    }
}

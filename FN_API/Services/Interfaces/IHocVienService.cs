using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IHocVienService
    {
        public Task<ResponseObject<List<DataResponseHocVien>>> DanhSachHV(int page = 1);
        public Task<ResponseObject<DataResponseHocVien>> ThemHocVien(string url, Data_RequestHocVien hocvien);
        public Task<ResponseObject<DataResponseHocVien>> Suahocvien(Data_RequestHocVien hocvien);
        public Task<bool> XoaHocvien(int hocvienID);
        public Task<ResponseObject<List<DataResponseHocVien>>> TimKiemHVTheoTenvaEmail(string name, int page);
    }
}

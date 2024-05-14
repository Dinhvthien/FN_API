using FN_API.Entities;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IHocVienService
    {
        public Task<ResponseObject<List<DataResponseHocVien>>> DanhSachHV(int page = 1);
        public Task<ResponseObject<DataResponseHocVien>> ThemHocVien(HocVien hocvien);
        public Task<ResponseObject<DataResponseHocVien>> Suahocvien(HocVien hocvien);
        public Task<bool> XoaHocvien(int hocvienID);
        public Task<HocVien> Timtheoid(int id);
        public Task<ResponseObject<List<DataResponseHocVien>>> TimKiemHVTheoTenvaEmail(string name, int page);
    }
}

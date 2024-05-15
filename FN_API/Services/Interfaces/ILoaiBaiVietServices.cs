using FN_API.Entities;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface ILoaiBaiVietServices
    {
        public Task<ResponseObject<List<DataResponseLoaiBaiViet>>> DanhSachLbv(int page, int pageSize);
        public Task<ResponseObject<DataResponseLoaiBaiViet>> ThemLoaiBaiViet(string tenloaibaiviet);
        public Task<ResponseObject<DataResponseLoaiBaiViet>> SuaLoaiBaiViet(int loaibaivietid, string tenloaibaiviet);
        public Task<bool> XoaLoaiBaiViet(int loaibaivietid);
    }
}

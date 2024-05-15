using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IQuyenHanService
    {
        public Task<ResponseObject<List<DataResponseQuyenHan>>> DanhSachQH(int page, int pageSize);
        public Task<ResponseObject<DataResponseQuyenHan>> ThemQuyenHan(string tenquyenhan);
        public Task<ResponseObject<DataResponseQuyenHan>> SuaQuyenhan(int quyenhanid, string tenquyenhan);
        public Task<bool> XoaQuyenhan(int quyenHanId);
    }
}

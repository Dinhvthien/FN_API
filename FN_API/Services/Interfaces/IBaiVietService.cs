using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IBaiVietService
    {
        public Task<ResponseObject<List<DataResponseBaiViet>>> DanhSachBV(int page, int pageSize);
        public Task<ResponseObject<DataResponseBaiViet>> ThemBaiViet(BaiViet baiViet);
        public Task<ResponseObject<DataResponseBaiViet>> SuaBaiViet(BaiViet baiViet);
        public Task<bool> XoaBaiViet(int baiVietId);
        public Task<BaiViet> Timtheoid(int id);

        public Task<ResponseObject<List<DataResponseBaiViet>>> TimKiemBaiVietTheoTen(string name, int page, int pageSize);
    }
}

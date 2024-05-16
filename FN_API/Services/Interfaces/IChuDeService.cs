using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using FN_API.Payloads.Responses;

namespace FN_API.Services.Interfaces
{
    public interface IChuDeService
    {
        public Task<ResponseObject<List<DataResponseChuDe>>> DanhSachCD(int page, int pageSize);
        public Task<ResponseObject<DataResponseChuDe>> Themchude(Data_RequestChuDe data);
        public Task<ResponseObject<DataResponseChuDe>> Suachude(Data_RequestChuDe data);
        public Task<bool> Xoachude(int chudeid);
    }
}

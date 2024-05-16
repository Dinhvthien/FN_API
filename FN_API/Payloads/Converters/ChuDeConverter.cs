using FN_API.DataContext;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Payloads.Converters
{
    public class ChuDeConverter
    {
        private readonly AppDbContext _context;
        public ChuDeConverter()
        {
            _context = new AppDbContext();
        }
        public async Task<List<DataResponseChuDe>> DataRespomseenumChiTietHoaDon(List<Data_RequestChuDe> data)
        {
            List<DataResponseChuDe> danhSachChuDe = new List<DataResponseChuDe>();
            foreach (var item in data)
            {
                DataResponseChuDe chudeRes = new DataResponseChuDe();
                chudeRes.TenLoaiBaiViet = (await _context.LoaiBaiViet.SingleOrDefaultAsync(c => c.LoaiBaiVietId == item.LoaiBaiVietId)).TenLoai;
                chudeRes.TenChuDe = item.TenChuDe;
                chudeRes.NoiDung = item.NoiDung;
                chudeRes.ChuDeId = item.ChuDeId;
                danhSachChuDe.Add(chudeRes);
            }
            return danhSachChuDe;
        }
        public async Task<DataResponseChuDe> DataRespomseChiTietHoaDon(Data_RequestChuDe item)
        {
            DataResponseChuDe chudeRes = new DataResponseChuDe();
            chudeRes.TenLoaiBaiViet = (await _context.LoaiBaiViet.SingleOrDefaultAsync(c => c.LoaiBaiVietId == item.LoaiBaiVietId)).TenLoai;
            chudeRes.TenChuDe = item.TenChuDe;
            chudeRes.NoiDung = item.NoiDung;
            chudeRes.ChuDeId = item.ChuDeId;
            return chudeRes;
        }
    }
}

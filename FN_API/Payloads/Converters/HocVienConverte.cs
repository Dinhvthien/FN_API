using FN_API.DataContext;
using FN_API.Entities;
using FN_API.Payloads.DataRequests;
using FN_API.Payloads.DataResponses;
using Microsoft.EntityFrameworkCore;

namespace FN_API.Payloads.Converters
{
    public class HocVienConverte
    {
        private readonly AppDbContext _context;
        public HocVienConverte()
        {
            _context = new AppDbContext();
        }
        public async Task<List<DataResponseHocVien>> DataRespomseListHocVien(IEnumerable<HocVien> data)
        {
            List<DataResponseHocVien> danhsachcHv = new List<DataResponseHocVien>();
            foreach (var item in data)
            {
                DataResponseHocVien HocVienResponse = new DataResponseHocVien();
                HocVienResponse.HocVienId = item.HocVienId;
                HocVienResponse.Hoten = item.Hoten;
                HocVienResponse.NgaySinh = item.NgaySinh;
                HocVienResponse.SoDienThoai = item.SoDienThoai;
                HocVienResponse.Email = item.Email;
                HocVienResponse.TinhThanh = item.TinhThanh;
                HocVienResponse.QuanHuyen = item.QuanHuyen;
                HocVienResponse.PhuongXa = item.PhuongXa;
                HocVienResponse.HinhAnh = item.HinhAnh;
                HocVienResponse.SoNha = item.SoNha;

                danhsachcHv.Add(HocVienResponse);
            }
            return danhsachcHv;
        }
        public async Task<DataResponseHocVien> DataRespomsehocvien(HocVien data)
        {
            DataResponseHocVien HocVienResponse = new DataResponseHocVien();
            HocVienResponse.HocVienId = data.HocVienId;
            HocVienResponse.Hoten = data.Hoten;
            HocVienResponse.NgaySinh = data.NgaySinh;
            HocVienResponse.SoDienThoai = data.SoDienThoai;
            HocVienResponse.Email = data.Email;
            HocVienResponse.TinhThanh = data.TinhThanh;
            HocVienResponse.QuanHuyen = data.QuanHuyen;
            HocVienResponse.PhuongXa = data.PhuongXa;
            HocVienResponse.HinhAnh = data.HinhAnh;
            HocVienResponse.SoNha = data.SoNha;
            return HocVienResponse;
        }
    }
}

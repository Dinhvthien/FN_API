namespace FN_API.Payloads.DataResponses
{
    public class DataResponseDangKyHoc
    {
        public int DangKyHocId { get; set; }
        public DateTime? ngaybatdau {  get; set; }
        public DateTime? ngaydangky { get; set; }
        public DateTime? ngayketthuc { get; set; }

        public string? tenkhoahoc { get; set; }
        public string? tenhocvien { get; set; }
        public string? tentinhtrang { get; set; }
        public string? tentaikhoan { get; set; }
    }
}

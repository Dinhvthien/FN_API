using System.ComponentModel.DataAnnotations;

namespace FN_API.Payloads.DataRequests
{
    public class Data_RequestChuDe
    {
        public int ChuDeId { get; set; }
        public string TenChuDe { get; set; }
        public string NoiDung { get; set; }
        public int LoaiBaiVietId { get; set; }
    }
}

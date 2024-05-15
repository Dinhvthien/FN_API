using System.ComponentModel.DataAnnotations;

namespace FN_API.Payloads.DataResponses
{
    public class DataResponseLoaiBaiViet
    {
        public int LoaiBaiVietId { get; set; }
        public string? TenLoai { get; set; }
    }
}

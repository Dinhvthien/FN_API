﻿namespace FN_API.Payloads.DataRequests
{
    public class Data_RequestListBaiViet
    {
        public int BaiVietId { get; set; }
        public string TenbaiViet { get; set; }
        public string TenTacGia { get; set; }
        public string NoiDung { get; set; }
        public string NoiDungNgan { get; set; }
        public string Hinhanh { get; set; }
        public int ChuDeId { get; set; }
        public DateTime thoigiantao { get; set; }
        public int TaiKhoanId { get; set; }
    }
}

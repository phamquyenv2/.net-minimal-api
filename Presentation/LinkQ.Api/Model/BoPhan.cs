namespace LinkQ.Api.Model;

public class BoPhan
{
    public string Ma_Bp { get; set; } = string.Empty;
    public string Ten_Bp { get; set; } = string.Empty;
    public string Ma_Bp_Cha { get; set; } = string.Empty;
    public int Stt_Bp { get; set; }
    public string Ma_Loai1 { get; set; } = string.Empty;
    public string Ma_Loai2 { get; set; } = string.Empty;
    public string Ma_Loai3 { get; set; } = string.Empty;
    public DateTime Ngay_Begin { get; set; }
    public DateTime Ngay_End { get; set; }
    public string Ma_Data { get; set; } = string.Empty;
    public bool Nh_Cuoi { get; set; }
    public string Create_Log { get; set; } = string.Empty;
    public string LastModify_Log { get; set; } = string.Empty;
    public bool Is_Bp_LaiLo { get; set; }
    public string Tk_Cp { get; set; } = string.Empty;
    public string Ma_Dt_CN { get; set; } = string.Empty;
    public string Ma_Kho { get; set; } = string.Empty;
    public string Ma_Bp_Old { get; set; } = string.Empty;
    public bool Is_PhanBoChiPhi { get; set; }
    public bool iS_OL { get; set; }
    public string Ma_CuocPhi { get; set; } = string.Empty;
    public string Thuong_Hieu { get; set; } = string.Empty;
    public string ViTri_Bp { get; set; } = string.Empty;
    public string Dia_Chi { get; set; } = string.Empty;
    public decimal So_Luong_Nhan_Su { get; set; }
    public string Cong_Viec_Phai_Lam { get; set; } = string.Empty;
    public decimal Gio_Cong_Chuan { get; set; }
    public bool Is_PhanBoDoanhThu { get; set; }
    public bool Is_PhanBo_Online_Offline { get; set; }
    public string Ma_Kv { get; set; } = string.Empty;
    public bool Is_PhanTheoMien { get; set; }
    public string Ma_Dt_Ban { get; set; } = string.Empty;
    public string Ma_Bp_VAT { get; set; } = string.Empty;
    public string Ma_DvCs_VAT { get; set; } = string.Empty;
    public string Ma_Dt_VAT { get; set; } = string.Empty;
    public string Ma_Bp_Dt { get; set; } = string.Empty;
}

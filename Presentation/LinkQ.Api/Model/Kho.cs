namespace LinkQ.Api.Model;

public class Kho
{
    public string Ma_Kho { get; set; } = string.Empty;
    public string Ten_Kho { get; set; } = string.Empty;
    public string Ma_Kho_Cha { get; set; } = string.Empty;
    public string Ma_Loai1 { get; set; } = string.Empty;
    public string Ma_Loai2 { get; set; } = string.Empty;
    public string Ma_Loai3 { get; set; } = string.Empty;
    public DateTime Ngay_Begin { get; set; }
    public DateTime Ngay_End { get; set; }
    public string Ma_Data { get; set; } = string.Empty;
    public string Create_Log { get; set; } = string.Empty;
    public string LastModify_Log { get; set; } = string.Empty;
    public bool Nh_Cuoi { get; set; }
    public string Ma_Kho_Old { get; set; } = string.Empty;
    public string Tk_Kho { get; set; } = string.Empty;
    public string Ma_Kv { get; set; } = string.Empty;
    public string Ma_Kho_VAT { get; set; } = string.Empty;
}

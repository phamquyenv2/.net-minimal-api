namespace LinkQ.Api.Model;

/// <summary>
/// Danh mục đối tượng (khách hàng, nhà cung cấp...) - Bảng L81DMDT
/// </summary>
public class DoiTuong
{
    public string Ma_Dt { get; set; } = string.Empty;
    public string Ten_Dt { get; set; } = string.Empty;
    public string Ma_Loai_Dt { get; set; } = string.Empty;
    public string Dia_Chi { get; set; } = string.Empty;
    public string Ma_So_Thue { get; set; } = string.Empty;
    public string Tk_CongNo { get; set; } = string.Empty;
    public string So_TkNh { get; set; } = string.Empty;
    public string Ten_Nh { get; set; } = string.Empty;
    public string Nguoi_Gd { get; set; } = string.Empty;
    public string So_DtGd { get; set; } = string.Empty;
    public string Ma_Dt_Cha { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;
    public string So_Dt { get; set; } = string.Empty;
    public string Ma_Loai1 { get; set; } = string.Empty;
    public string Ma_Loai2 { get; set; } = string.Empty;
    public string Ma_Loai3 { get; set; } = string.Empty;
    public DateTime Ngay_Begin { get; set; }
    public DateTime Ngay_End { get; set; }
    public string Ma_Data { get; set; } = string.Empty;
    public bool Nh_Cuoi { get; set; }
    public string Ma_CbNv { get; set; } = string.Empty;
    public string Ma_Kv { get; set; } = string.Empty;
    public string Ma_Dt_Gia { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public decimal Tien_No_Max { get; set; }
    public string Create_Log { get; set; } = string.Empty;
    public string LastModify_Log { get; set; } = string.Empty;
    public int Han_Tt { get; set; }
    public string DmDt_Text1 { get; set; } = string.Empty;
    public string DmDt_Text2 { get; set; } = string.Empty;
    public string DmDt_Text3 { get; set; } = string.Empty;
    public bool DmDt_Bit1 { get; set; }
    public bool DmDt_Bit2 { get; set; }
    public DateTime DmDt_Date1 { get; set; }
    public DateTime DmDt_Date2 { get; set; }
    public decimal DmDt_Num1 { get; set; }
    public decimal DmDt_Num2 { get; set; }
    public string So_CMND { get; set; } = string.Empty;
    public DateTime Ngay_Cap { get; set; }
    public string Noi_Cap { get; set; } = string.Empty;
    public string So_Phone_NR { get; set; } = string.Empty;
    public string So_Phone_Dd { get; set; } = string.Empty;
    public string Dia_Chi_NR { get; set; } = string.Empty;
    public string Ma_Dt_GiaMua { get; set; } = string.Empty;
    public string Dia_Chi_Gh { get; set; } = string.Empty;
    public string Ma_Dt_Old { get; set; } = string.Empty;
    public string Ma_Bp_Default { get; set; } = string.Empty;
    public string Ma_Kho_Default { get; set; } = string.Empty;
    public string Ma_Dt_CN { get; set; } = string.Empty;
    public string Ong_Ba { get; set; } = string.Empty;
    public string Ten_Dv_ThuHuong { get; set; } = string.Empty;
    public string Ma_Dt_vat { get; set; } = string.Empty;
    public int YearOfWork { get; set; }
    public string MA_DT_DT { get; set; } = string.Empty;
    public string Dia_Chi_2 { get; set; } = string.Empty;
    public string Dia_Chi_1 { get; set; } = string.Empty;
}

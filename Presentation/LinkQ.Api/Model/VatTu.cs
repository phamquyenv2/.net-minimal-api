namespace LinkQ.Api.Model;

public class VatTu
{
    public string Ma_Vt { get; set; } = string.Empty;
    public string Ten_Vt { get; set; } = string.Empty;
    public string Ma_Nh_Vt { get; set; } = string.Empty;
    public string Dvt { get; set; } = string.Empty;
    public string Dvt1 { get; set; } = string.Empty;
    public decimal He_So1 { get; set; }
    public string Dvt2 { get; set; } = string.Empty;
    public decimal He_So2 { get; set; }
    public string Dvt3 { get; set; } = string.Empty;
    public decimal He_So3 { get; set; }
    public string Ma_Loai1 { get; set; } = string.Empty;
    public string Ma_Loai2 { get; set; } = string.Empty;
    public string Ma_Loai3 { get; set; } = string.Empty;
    public DateTime Ngay_Begin { get; set; }
    public DateTime Ngay_End { get; set; }
    public string Ma_Data { get; set; } = string.Empty;
    public decimal Sl_Ton_Min { get; set; }
    public decimal Sl_Ton_Max { get; set; }
    public string Loai_Vt { get; set; } = string.Empty;
    public string Tk_Vt { get; set; } = string.Empty;
    public string Tk_Gv { get; set; } = string.Empty;
    public string Tk_Dt { get; set; } = string.Empty;
    public string Tk_Hbtl { get; set; } = string.Empty;
    public string Ma_Sp { get; set; } = string.Empty;
    public string Ma_Vt_Gt { get; set; } = string.Empty;
    public string Create_Log { get; set; } = string.Empty;
    public string LastModify_Log { get; set; } = string.Empty;
    public string Ma_Nv_Tk { get; set; } = string.Empty;
    public string Dmvt_Text1 { get; set; } = string.Empty;
    public string Dmvt_Text2 { get; set; } = string.Empty;
    public string Dmvt_Text3 { get; set; } = string.Empty;
    public bool Dmvt_Bit1 { get; set; }
    public bool Dmvt_Bit2 { get; set; }
    public DateTime Dmvt_Date1 { get; set; }
    public DateTime Dmvt_Date2 { get; set; }
    public decimal Dmvt_Num1 { get; set; }
    public decimal Dmvt_Num2 { get; set; }
    public string Ma_Vach { get; set; } = string.Empty;
    // Hinh (image) - excluded vì không cần thiết trong API response thông thường
    public bool Is_Lo { get; set; }
    public string Type_ID_LoaiVt { get; set; } = string.Empty;
    public string Ma_Job { get; set; } = string.Empty;
    public string Ma_Dt_NCC { get; set; } = string.Empty;
    public decimal Gia_Ban { get; set; }
    public string Ten_Vt_NKim { get; set; } = string.Empty;
    public string Ten_Vt_Metro { get; set; } = string.Empty;
    public string Ma_Vt_Metro { get; set; } = string.Empty;
    public string Ma_VT_NKIM { get; set; } = string.Empty;
    public string Ten_VtE { get; set; } = string.Empty;
    public string Thuong_Hieu { get; set; } = string.Empty;
    public string Mau { get; set; } = string.Empty;
    public string Chat_Lieu { get; set; } = string.Empty;
    public string Xuat_Xu { get; set; } = string.Empty;
    public string Qui_Cach { get; set; } = string.Empty;
    public string Nha_SX { get; set; } = string.Empty;
    public string DC_SX { get; set; } = string.Empty;
    public string Ghi_Chu { get; set; } = string.Empty;
    public string GiaCong_Sx { get; set; } = string.Empty;
    public bool iS_ChiPhi { get; set; }
    public string Ma_DT_CN { get; set; } = string.Empty;
    public string Ma_BP { get; set; } = string.Empty;
    public string Nha_XuatKhau { get; set; } = string.Empty;
    public bool Is_Imei { get; set; }
    public string Nhom_Vt { get; set; } = string.Empty;
    public string Mau_Sac { get; set; } = string.Empty;
    public string Dia_Chi_Nha_XK { get; set; } = string.Empty;
    public string Dong_SP { get; set; } = string.Empty;
    public string MA_GOC_VT { get; set; } = string.Empty;
    public string MA_SIZE { get; set; } = string.Empty;
    public string MAU_SAC_MAGOC { get; set; } = string.Empty;
    public bool IS_MAGOC { get; set; }
    public string TEN_MAU { get; set; } = string.Empty;
    public string MA_OLD { get; set; } = string.Empty;
    public string MA_NCC_VT { get; set; } = string.Empty;
    public string Ma_New { get; set; } = string.Empty;
    public decimal HSD { get; set; }
    public string Ma_Vt_VAT { get; set; } = string.Empty;
    public string Ma_Vt_Dt { get; set; } = string.Empty;
    public string Ten_Vt_Dt { get; set; } = string.Empty;
    public bool Is_Ma_Doi_Tra { get; set; }
}

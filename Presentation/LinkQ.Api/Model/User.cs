namespace LinkQ.Api.Model;

public class User
{
    public string Member_ID { get; set; } = string.Empty;
    public string Member_Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Member_Type { get; set; } = "U";
    public bool Is_Admin { get; set; }
    public bool Locked { get; set; }
    public string Member_ID_Allow { get; set; } = string.Empty;
    public byte[]? CheckPass { get; set; }
    public string Ma_CbNv { get; set; } = string.Empty;
    public bool Is_MemberID { get; set; }
    public string Ma_DvCs_Default { get; set; } = string.Empty;
    public string Ma_Nh_File { get; set; } = string.Empty;
}

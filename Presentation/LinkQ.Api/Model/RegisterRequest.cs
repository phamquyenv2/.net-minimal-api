namespace LinkQ.Api.Model;

public class RegisterRequest
{
    public string Member_ID { get; set; } = string.Empty;
    public string Member_Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Member_Type { get; set; } = "U";
    public bool Is_Admin { get; set; } = false;
    public bool Locked { get; set; } = false;
    public string Member_ID_Allow { get; set; } = string.Empty;
    public string Ma_CbNv { get; set; } = string.Empty;
    public bool Is_MemberID { get; set; } = false;
    public string Ma_DvCs_Default { get; set; } = string.Empty;
    public string Ma_Nh_File { get; set; } = string.Empty;
}

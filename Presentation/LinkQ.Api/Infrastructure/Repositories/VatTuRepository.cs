using Dapper;
using Microsoft.Data.SqlClient;
using LinkQ.Api.Model;

namespace LinkQ.Api.Infrastructure.Repositories;

public class VatTuRepository
{
    private readonly string _connectionString;

    public VatTuRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("L60SANGTAM")
            ?? throw new ArgumentNullException("Connection string 'L60SANGTAM' not found.");
    }

    public async Task<VatTu?> GetByIdAsync(string maVt)
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.QueryFirstOrDefaultAsync<VatTu>(
            @"SELECT Ma_Vt, Ten_Vt, Ma_Nh_Vt, Dvt, Dvt1, He_So1, Dvt2, He_So2, Dvt3, He_So3,
              Ma_Loai1, Ma_Loai2, Ma_Loai3, Ngay_Begin, Ngay_End, Ma_Data,
              Sl_Ton_Min, Sl_Ton_Max, Loai_Vt, Tk_Vt, Tk_Gv, Tk_Dt, Tk_Hbtl,
              Ma_Sp, Ma_Vt_Gt, Create_Log, LastModify_Log, Ma_Nv_Tk,
              Dmvt_Text1, Dmvt_Text2, Dmvt_Text3, Dmvt_Bit1, Dmvt_Bit2,
              Dmvt_Date1, Dmvt_Date2, Dmvt_Num1, Dmvt_Num2, Ma_Vach,
              Is_Lo, Type_ID_LoaiVt, Ma_Job, Ma_Dt_NCC, Gia_Ban,
              Thuong_Hieu, Mau, Chat_Lieu, Xuat_Xu, Qui_Cach, Nha_SX, DC_SX,
              Ghi_Chu, iS_ChiPhi, Ma_DT_CN, Ma_BP, Is_Imei, Nhom_Vt,
              MA_GOC_VT, MA_SIZE, HSD, Ma_Vt_VAT, Ma_Vt_Dt, Ten_Vt_Dt, Is_Ma_Doi_Tra
            FROM L81DMVT WHERE Ma_Vt = @MaVt",
            new { MaVt = maVt });
    }

    public async Task<(IEnumerable<VatTu> Items, int TotalCount)> GetListAsync(
        string? kw, string? maNhVt, int page, int pageSize)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var conditions = new List<string>();
        if (!string.IsNullOrWhiteSpace(kw))
            conditions.Add("(Ma_Vt LIKE @Kw OR Ten_Vt LIKE @Kw OR Ma_Vach LIKE @Kw)");
        if (!string.IsNullOrWhiteSpace(maNhVt))
            conditions.Add("Ma_Nh_Vt = @MaNhVt");

        var where = conditions.Count > 0 ? " WHERE " + string.Join(" AND ", conditions) : "";
        var param = new { Kw = $"%{kw}%", MaNhVt = maNhVt, Offset = (page - 1) * pageSize, PageSize = pageSize };

        var total = await conn.ExecuteScalarAsync<int>(
            $"SELECT COUNT(1) FROM L81DMVT{where}", param);

        var items = await conn.QueryAsync<VatTu>(
            $@"SELECT Ma_Vt, Ten_Vt, Ma_Nh_Vt, Dvt, Dvt1, He_So1, Dvt2, He_So2, Dvt3, He_So3,
              Ma_Loai1, Ma_Loai2, Ma_Loai3, Ngay_Begin, Ngay_End, Ma_Data,
              Sl_Ton_Min, Sl_Ton_Max, Loai_Vt, Tk_Vt, Tk_Gv, Tk_Dt, Tk_Hbtl,
              Ma_Sp, Ma_Vt_Gt, Create_Log, LastModify_Log, Ma_Nv_Tk,
              Dmvt_Text1, Dmvt_Text2, Dmvt_Text3, Dmvt_Bit1, Dmvt_Bit2,
              Dmvt_Date1, Dmvt_Date2, Dmvt_Num1, Dmvt_Num2, Ma_Vach,
              Is_Lo, Type_ID_LoaiVt, Ma_Job, Ma_Dt_NCC, Gia_Ban,
              Thuong_Hieu, Mau, Chat_Lieu, Xuat_Xu, Qui_Cach, Nha_SX, DC_SX,
              Ghi_Chu, iS_ChiPhi, Ma_DT_CN, Ma_BP, Is_Imei, Nhom_Vt,
              MA_GOC_VT, MA_SIZE, HSD, Ma_Vt_VAT, Ma_Vt_Dt, Ten_Vt_Dt, Is_Ma_Doi_Tra
            FROM L81DMVT{where} ORDER BY Ma_Vt OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", param);

        return (items, total);
    }

    public async Task<int> CreateAsync(VatTu vt)
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteAsync(
            @"INSERT INTO L81DMVT (Ma_Vt, Ten_Vt, Ma_Nh_Vt, Dvt, Dvt1, He_So1,
              Dvt2, He_So2, Dvt3, He_So3, Ma_Loai1, Ma_Loai2, Ma_Loai3,
              Ma_Data, Loai_Vt, Tk_Vt, Tk_Gv, Tk_Dt, Tk_Hbtl, Ma_Sp,
              Ma_Vach, Is_Lo, Ma_Dt_NCC, Gia_Ban, Thuong_Hieu, Mau, Chat_Lieu,
              Xuat_Xu, Qui_Cach, Nha_SX, Ghi_Chu, Ma_DT_CN, Ma_BP, Is_Imei, Nhom_Vt)
            VALUES (@Ma_Vt, @Ten_Vt, @Ma_Nh_Vt, @Dvt, @Dvt1, @He_So1,
              @Dvt2, @He_So2, @Dvt3, @He_So3, @Ma_Loai1, @Ma_Loai2, @Ma_Loai3,
              @Ma_Data, @Loai_Vt, @Tk_Vt, @Tk_Gv, @Tk_Dt, @Tk_Hbtl, @Ma_Sp,
              @Ma_Vach, @Is_Lo, @Ma_Dt_NCC, @Gia_Ban, @Thuong_Hieu, @Mau, @Chat_Lieu,
              @Xuat_Xu, @Qui_Cach, @Nha_SX, @Ghi_Chu, @Ma_DT_CN, @Ma_BP, @Is_Imei, @Nhom_Vt)", vt);
    }

    public async Task<int> UpdateAsync(VatTu vt)
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteAsync(
            @"UPDATE L81DMVT SET Ten_Vt=@Ten_Vt, Ma_Nh_Vt=@Ma_Nh_Vt, Dvt=@Dvt,
              Dvt1=@Dvt1, He_So1=@He_So1, Dvt2=@Dvt2, He_So2=@He_So2,
              Dvt3=@Dvt3, He_So3=@He_So3, Ma_Loai1=@Ma_Loai1, Ma_Loai2=@Ma_Loai2,
              Ma_Loai3=@Ma_Loai3, Loai_Vt=@Loai_Vt, Ma_Vach=@Ma_Vach,
              Gia_Ban=@Gia_Ban, Thuong_Hieu=@Thuong_Hieu, Mau=@Mau,
              Chat_Lieu=@Chat_Lieu, Xuat_Xu=@Xuat_Xu, Qui_Cach=@Qui_Cach,
              Nha_SX=@Nha_SX, Ghi_Chu=@Ghi_Chu, Ma_DT_CN=@Ma_DT_CN,
              Ma_BP=@Ma_BP, Is_Imei=@Is_Imei, Nhom_Vt=@Nhom_Vt
            WHERE Ma_Vt = @Ma_Vt", vt);
    }

    public async Task<int> DeleteAsync(string maVt)
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteAsync("DELETE FROM L81DMVT WHERE Ma_Vt = @MaVt", new { MaVt = maVt });
    }
}

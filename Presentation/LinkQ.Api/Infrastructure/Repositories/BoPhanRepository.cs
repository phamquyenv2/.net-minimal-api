using Dapper;
using Microsoft.Data.SqlClient;
using LinkQ.Api.Model;

namespace LinkQ.Api.Infrastructure.Repositories;

public class BoPhanRepository
{
    private readonly string connectionString;

    public BoPhanRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("L60SANGTAM")
            ?? throw new ArgumentNullException("Connection string 'L60SANGTAM' not found.");
    }

    public async Task<BoPhan?> GetByIdAsync(string maBp)
    {
        using var conn = new SqlConnection(connectionString);
        return await conn.QueryFirstOrDefaultAsync<BoPhan>(
            "SELECT * FROM L81DMBP WHERE Ma_Bp = @MaBp", new { MaBp = maBp });
    }

    public async Task<(IEnumerable<BoPhan> Items, int TotalCount)> GetListAsync(string? kw, int page, int pageSize)
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();

        var where = string.IsNullOrWhiteSpace(kw)
            ? ""
            : " WHERE Ma_Bp LIKE @Kw OR Ten_Bp LIKE @Kw";
        var param = new { Kw = $"%{kw}%", Offset = (page - 1) * pageSize, PageSize = pageSize };

        var total = await conn.ExecuteScalarAsync<int>(
            $"SELECT COUNT(1) FROM L81DMBP{where}", param);

        var items = await conn.QueryAsync<BoPhan>(
            $"SELECT * FROM L81DMBP{where} ORDER BY Ma_Bp OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", param);

        return (items, total);
    }

    public async Task<int> CreateAsync(BoPhan bp)
    {
        using var conn = new SqlConnection(connectionString);
        return await conn.ExecuteAsync(
            @"INSERT INTO L81DMBP (Ma_Bp, Ten_Bp, Ma_Bp_Cha, Stt_Bp, Ma_Loai1, Ma_Loai2, Ma_Loai3,
              Ma_Data, Nh_Cuoi, Is_Bp_LaiLo, Tk_Cp, Ma_Dt_CN, Ma_Kho,
              Is_PhanBoChiPhi, iS_OL, Thuong_Hieu, ViTri_Bp, Dia_Chi,
              So_Luong_Nhan_Su, Gio_Cong_Chuan, Ma_Kv, Ma_Dt_Ban, Ma_Bp_VAT)
            VALUES (@Ma_Bp, @Ten_Bp, @Ma_Bp_Cha, @Stt_Bp, @Ma_Loai1, @Ma_Loai2, @Ma_Loai3,
              @Ma_Data, @Nh_Cuoi, @Is_Bp_LaiLo, @Tk_Cp, @Ma_Dt_CN, @Ma_Kho,
              @Is_PhanBoChiPhi, @iS_OL, @Thuong_Hieu, @ViTri_Bp, @Dia_Chi,
              @So_Luong_Nhan_Su, @Gio_Cong_Chuan, @Ma_Kv, @Ma_Dt_Ban, @Ma_Bp_VAT)", bp);
    }

    public async Task<int> UpdateAsync(BoPhan bp)
    {
        using var conn = new SqlConnection(connectionString);
        return await conn.ExecuteAsync(
            @"UPDATE L81DMBP SET Ten_Bp=@Ten_Bp, Ma_Bp_Cha=@Ma_Bp_Cha, Stt_Bp=@Stt_Bp,
              Ma_Loai1=@Ma_Loai1, Ma_Loai2=@Ma_Loai2, Ma_Loai3=@Ma_Loai3,
              Ma_Data=@Ma_Data, Nh_Cuoi=@Nh_Cuoi, Is_Bp_LaiLo=@Is_Bp_LaiLo,
              Tk_Cp=@Tk_Cp, Ma_Dt_CN=@Ma_Dt_CN, Ma_Kho=@Ma_Kho,
              Is_PhanBoChiPhi=@Is_PhanBoChiPhi, iS_OL=@iS_OL,
              Thuong_Hieu=@Thuong_Hieu, ViTri_Bp=@ViTri_Bp, Dia_Chi=@Dia_Chi,
              So_Luong_Nhan_Su=@So_Luong_Nhan_Su, Gio_Cong_Chuan=@Gio_Cong_Chuan,
              Ma_Kv=@Ma_Kv, Ma_Dt_Ban=@Ma_Dt_Ban, Ma_Bp_VAT=@Ma_Bp_VAT
            WHERE Ma_Bp = @Ma_Bp", bp);
    }

    public async Task<int> DeleteAsync(string maBp)
    {
        using var conn = new SqlConnection(connectionString);
        return await conn.ExecuteAsync("DELETE FROM L81DMBP WHERE Ma_Bp = @MaBp", new { MaBp = maBp });
    }
}

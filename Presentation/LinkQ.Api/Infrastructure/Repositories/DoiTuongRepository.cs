using Dapper;
using Microsoft.Data.SqlClient;
using LinkQ.Api.Model;

namespace LinkQ.Api.Infrastructure.Repositories;

public class DoiTuongRepository
{
    private readonly string _connectionString;

    public DoiTuongRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("L60SANGTAM")
            ?? throw new ArgumentNullException("Connection string 'L60SANGTAM' not found.");
    }

    public async Task<DoiTuong?> GetByIdAsync(string maDt)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<DoiTuong>(
            "SELECT * FROM L81DMDT WHERE Ma_Dt = @MaDt",
            new { MaDt = maDt });
    }

    public async Task<(IEnumerable<DoiTuong> Items, int TotalCount)> GetListAsync(
        string? kw, string? maLoaiDt, int page, int pageSize)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var conditions = new List<string>();
        if (!string.IsNullOrWhiteSpace(kw))
            conditions.Add("(Ma_Dt LIKE @Kw OR Ten_Dt LIKE @Kw)");
        if (!string.IsNullOrWhiteSpace(maLoaiDt))
            conditions.Add("Ma_Loai_Dt = @MaLoaiDt");

        var where = conditions.Count > 0 ? " WHERE " + string.Join(" AND ", conditions) : "";
        var param = new { Kw = $"%{kw}%", MaLoaiDt = maLoaiDt, Offset = (page - 1) * pageSize, PageSize = pageSize };

        var totalCount = await connection.ExecuteScalarAsync<int>(
            $"SELECT COUNT(1) FROM L81DMDT{where}", param);

        var items = await connection.QueryAsync<DoiTuong>(
            $"SELECT * FROM L81DMDT{where} ORDER BY Ma_Dt OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", param);

        return (items, totalCount);
    }

    public async Task<int> CreateAsync(DoiTuong doiTuong)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            @"INSERT INTO L81DMDT (Ma_Dt, Ten_Dt, Ma_Loai_Dt, Dia_Chi, Ma_So_Thue, 
              Tk_CongNo, So_TkNh, Ten_Nh, Nguoi_Gd, So_DtGd, Ma_Dt_Cha, Email, Fax, So_Dt,
              Ma_Loai1, Ma_Loai2, Ma_Loai3, Ma_Data, Nh_Cuoi, Ma_CbNv, Ma_Kv, Ma_Dt_Gia, Note, Tien_No_Max, Han_Tt,
              DmDt_Text1, DmDt_Text2, DmDt_Text3, DmDt_Bit1, DmDt_Bit2,
              So_CMND, Noi_Cap, So_Phone_NR, So_Phone_Dd, Dia_Chi_NR,
              Ma_Dt_GiaMua, Dia_Chi_Gh, Ma_Bp_Default, Ma_Kho_Default, Ma_Dt_CN, Ong_Ba, 
              Ten_Dv_ThuHuong, Ma_Dt_vat)
            VALUES (@Ma_Dt, @Ten_Dt, @Ma_Loai_Dt, @Dia_Chi, @Ma_So_Thue,
              @Tk_CongNo, @So_TkNh, @Ten_Nh, @Nguoi_Gd, @So_DtGd, @Ma_Dt_Cha, @Email, @Fax, @So_Dt,
              @Ma_Loai1, @Ma_Loai2, @Ma_Loai3, @Ma_Data, @Nh_Cuoi, @Ma_CbNv, @Ma_Kv, @Ma_Dt_Gia, @Note, @Tien_No_Max, @Han_Tt,
              @DmDt_Text1, @DmDt_Text2, @DmDt_Text3, @DmDt_Bit1, @DmDt_Bit2,
              @So_CMND, @Noi_Cap, @So_Phone_NR, @So_Phone_Dd, @Dia_Chi_NR,
              @Ma_Dt_GiaMua, @Dia_Chi_Gh, @Ma_Bp_Default, @Ma_Kho_Default, @Ma_Dt_CN, @Ong_Ba,
              @Ten_Dv_ThuHuong, @Ma_Dt_vat)",
            doiTuong);
    }

    public async Task<int> UpdateAsync(DoiTuong doiTuong)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            @"UPDATE L81DMDT SET 
              Ten_Dt = @Ten_Dt, Ma_Loai_Dt = @Ma_Loai_Dt, Dia_Chi = @Dia_Chi, 
              Ma_So_Thue = @Ma_So_Thue, Tk_CongNo = @Tk_CongNo, So_TkNh = @So_TkNh,
              Ten_Nh = @Ten_Nh, Nguoi_Gd = @Nguoi_Gd, So_DtGd = @So_DtGd, 
              Ma_Dt_Cha = @Ma_Dt_Cha, Email = @Email, Fax = @Fax, So_Dt = @So_Dt,
              Ma_Loai1 = @Ma_Loai1, Ma_Loai2 = @Ma_Loai2, Ma_Loai3 = @Ma_Loai3,
              Ma_Data = @Ma_Data, Nh_Cuoi = @Nh_Cuoi, Ma_CbNv = @Ma_CbNv,
              Ma_Kv = @Ma_Kv, Ma_Dt_Gia = @Ma_Dt_Gia, Note = @Note, 
              Tien_No_Max = @Tien_No_Max, Han_Tt = @Han_Tt,
              DmDt_Text1 = @DmDt_Text1, DmDt_Text2 = @DmDt_Text2, DmDt_Text3 = @DmDt_Text3,
              So_CMND = @So_CMND, Noi_Cap = @Noi_Cap,
              So_Phone_NR = @So_Phone_NR, So_Phone_Dd = @So_Phone_Dd, Dia_Chi_NR = @Dia_Chi_NR,
              Ma_Dt_GiaMua = @Ma_Dt_GiaMua, Dia_Chi_Gh = @Dia_Chi_Gh,
              Ma_Bp_Default = @Ma_Bp_Default, Ma_Kho_Default = @Ma_Kho_Default,
              Ma_Dt_CN = @Ma_Dt_CN, Ong_Ba = @Ong_Ba, Ten_Dv_ThuHuong = @Ten_Dv_ThuHuong,
              Ma_Dt_vat = @Ma_Dt_vat
            WHERE Ma_Dt = @Ma_Dt",
            doiTuong);
    }

    public async Task<int> DeleteAsync(string maDt)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            "DELETE FROM L81DMDT WHERE Ma_Dt = @MaDt",
            new { MaDt = maDt });
    }
}

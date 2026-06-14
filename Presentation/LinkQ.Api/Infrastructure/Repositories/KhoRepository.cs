using Dapper;
using Microsoft.Data.SqlClient;
using LinkQ.Api.Model;

namespace LinkQ.Api.Infrastructure.Repositories;

public class KhoRepository
{
    private readonly string _connectionString;

    public KhoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("L60SANGTAM")
            ?? throw new ArgumentNullException("Connection string 'L60SANGTAM' not found.");
    }

    public async Task<Kho?> GetByIdAsync(string maKho)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Kho>(
            "SELECT * FROM L81DMKHO WHERE Ma_Kho = @MaKho",
            new { MaKho = maKho });
    }

    public async Task<(IEnumerable<Kho> Items, int TotalCount)> GetListAsync(string? kw, int page, int pageSize)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var where = string.IsNullOrWhiteSpace(kw)
            ? ""
            : " WHERE Ma_Kho LIKE @Kw OR Ten_Kho LIKE @Kw";
        var param = new { Kw = $"%{kw}%", Offset = (page - 1) * pageSize, PageSize = pageSize };

        var totalCount = await connection.ExecuteScalarAsync<int>(
            $"SELECT COUNT(1) FROM L81DMKHO{where}", param);

        var items = await connection.QueryAsync<Kho>(
            $"SELECT * FROM L81DMKHO{where} ORDER BY Ma_Kho OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", param);

        return (items, totalCount);
    }

    public async Task<int> CreateAsync(Kho kho)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            @"INSERT INTO L81DMKHO (Ma_Kho, Ten_Kho, Ma_Kho_Cha, Ma_Loai1, Ma_Loai2, Ma_Loai3,
              Ngay_Begin, Ngay_End, Ma_Data, Nh_Cuoi, Tk_Kho, Ma_Kv, Ma_Kho_VAT)
            VALUES (@Ma_Kho, @Ten_Kho, @Ma_Kho_Cha, @Ma_Loai1, @Ma_Loai2, @Ma_Loai3,
              @Ngay_Begin, @Ngay_End, @Ma_Data, @Nh_Cuoi, @Tk_Kho, @Ma_Kv, @Ma_Kho_VAT)",
            kho);
    }

    public async Task<int> UpdateAsync(Kho kho)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            @"UPDATE L81DMKHO SET 
              Ten_Kho = @Ten_Kho, Ma_Kho_Cha = @Ma_Kho_Cha,
              Ma_Loai1 = @Ma_Loai1, Ma_Loai2 = @Ma_Loai2, Ma_Loai3 = @Ma_Loai3,
              Ngay_Begin = @Ngay_Begin, Ngay_End = @Ngay_End, Ma_Data = @Ma_Data,
              Nh_Cuoi = @Nh_Cuoi, Tk_Kho = @Tk_Kho, Ma_Kv = @Ma_Kv, Ma_Kho_VAT = @Ma_Kho_VAT
            WHERE Ma_Kho = @Ma_Kho",
            kho);
    }

    public async Task<int> DeleteAsync(string maKho)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            "DELETE FROM L81DMKHO WHERE Ma_Kho = @MaKho",
            new { MaKho = maKho });
    }
}

using Dapper;
using Microsoft.Data.SqlClient;

namespace LinkQ.Api.Infrastructure.Repositories;

public class DataRepository
{
    private readonly string _connectionString;

    public DataRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("L60SANGTAM") 
            ?? throw new ArgumentNullException("Connection string 'L60SANGTAM' not found.");
    }

    public async Task<IEnumerable<dynamic>> QueryDataAsync(string tableName)
    {
        using var connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();

        string sql = $"SELECT * FROM [{tableName}]";
        
        var result = await connection.QueryAsync(sql);
        
        return result;
    }
}

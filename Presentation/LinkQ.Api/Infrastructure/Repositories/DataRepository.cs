using Dapper;
using Microsoft.Data.SqlClient;

namespace LinkQ.Api.Infrastructure.Repositories;

public class DataRepository
{
    private readonly string connectionString;

    public DataRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("L60SANGTAM") 
            ?? throw new ArgumentNullException("Connection string 'L60SANGTAM' not found.");
    }

    public async Task<IEnumerable<dynamic>> QueryDataAsync(string tableName)
    {
        using var connection = new SqlConnection(connectionString);
        
        await connection.OpenAsync();

        string sql = $"SELECT * FROM [{tableName}]";
        
        var result = await connection.QueryAsync(sql);
        
        return result;
    }
}

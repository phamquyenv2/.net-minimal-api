using Dapper;
using Microsoft.Data.SqlClient;

namespace LinkQ.Api.Api;

public static class DataEndpoints
{
    public static void MapDataEndpoints(this WebApplication app)
    {
        app.MapGet("/api/data/{tableName}", async (
            string tableName,
            IConfiguration config) =>
        {
            var allowedTables = new[] { "Products", "Categories", "Customers", "Users", "L81DMTK", "L00member" };
            if (!allowedTables.Contains(tableName, StringComparer.OrdinalIgnoreCase))
                return Results.BadRequest(new { message = $"Table '{tableName}' is not allowed." });

            using var connection = new SqlConnection(
                config.GetConnectionString("L60SANGTAM"));

            var data = await connection.QueryAsync($"SELECT * FROM [{tableName}]");
            return Results.Ok(data);
        })
        .WithName("GetData")
        .WithTags("Data")
        .RequireAuthorization(); 
    }
}

namespace LinkQ.Api.Api;

public static class TestEndpoints
{
    public static void MapTestEndpoints(this WebApplication app)
    {
        app.MapGet("/api/test-db", async (IConfiguration config) =>
        {
            try
            {
                using var connection = new Microsoft.Data.SqlClient.SqlConnection(config.GetConnectionString("L60SANGTAM"));
                await connection.OpenAsync();
                return Results.Ok(new { message = "Kết nối Database L60SANGTAM thành công!" });
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, title: "Lỗi kết nối Database");
            }
        })
        .WithName("TestDb")
        .WithTags("Test");
    }
}

using LinkQ.Api.Infrastructure.Repositories;
using LinkQ.Api.Model;

namespace LinkQ.Api.Api;

public static class KhoEndpoints
{
    public static void MapKhoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/kho")
            .WithTags("Danh mục kho")
            .RequireAuthorization();

        group.MapGet("/", async (string? kw, int? page, int? pageSize, KhoRepository repo) =>
        {
            var (items, totalCount) = await repo.GetListAsync(kw, page ?? 1, pageSize ?? 20);
            return Results.Ok(new
            {
                success = true, data = items,
                pagination = new { page = page ?? 1, pageSize = pageSize ?? 20, totalCount }
            });
        }).WithName("GetKho");

        group.MapGet("/{maKho}", async (string maKho, KhoRepository repo) =>
        {
            var item = await repo.GetByIdAsync(maKho);
            return item is null
                ? Results.NotFound(new { success = false, message = $"Không tìm thấy kho '{maKho}'" })
                : Results.Ok(new { success = true, data = item });
        }).WithName("GetKhoById");

        group.MapPost("/", async (Kho kho, KhoRepository repo) =>
        {
            var result = await repo.CreateAsync(kho);
            return result > 0
                ? Results.Created($"/api/kho/{kho.Ma_Kho}", new { success = true, data = kho })
                : Results.BadRequest(new { success = false, message = "Không thể thêm kho" });
        }).WithName("CreateKho");

        group.MapPut("/{maKho}", async (string maKho, Kho kho, KhoRepository repo) =>
        {
            kho.Ma_Kho = maKho;
            var result = await repo.UpdateAsync(kho);
            return result > 0
                ? Results.Ok(new { success = true, message = "Cập nhật thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy kho '{maKho}'" });
        }).WithName("UpdateKho");

        group.MapDelete("/{maKho}", async (string maKho, KhoRepository repo) =>
        {
            var result = await repo.DeleteAsync(maKho);
            return result > 0
                ? Results.Ok(new { success = true, message = "Xóa thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy kho '{maKho}'" });
        }).WithName("DeleteKho");
    }
}

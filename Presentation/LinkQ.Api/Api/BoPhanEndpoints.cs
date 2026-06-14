using LinkQ.Api.Infrastructure.Repositories;
using LinkQ.Api.Model;

namespace LinkQ.Api.Api;

public static class BoPhanEndpoints
{
    public static void MapBoPhanEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/bo-phan")
            .WithTags("Danh mục bộ phận")
            .RequireAuthorization();

        group.MapGet("/", async (string? kw, int? page, int? pageSize, BoPhanRepository repo) =>
        {
            var (items, totalCount) = await repo.GetListAsync(kw, page ?? 1, pageSize ?? 20);
            return Results.Ok(new
            {
                success = true, data = items,
                pagination = new { page = page ?? 1, pageSize = pageSize ?? 20, totalCount }
            });
        }).WithName("GetBoPhan");

        group.MapGet("/{maBp}", async (string maBp, BoPhanRepository repo) =>
        {
            var item = await repo.GetByIdAsync(maBp);
            return item is null
                ? Results.NotFound(new { success = false, message = $"Không tìm thấy bộ phận '{maBp}'" })
                : Results.Ok(new { success = true, data = item });
        }).WithName("GetBoPhanById");

        group.MapPost("/", async (BoPhan boPhan, BoPhanRepository repo) =>
        {
            var result = await repo.CreateAsync(boPhan);
            return result > 0
                ? Results.Created($"/api/bo-phan/{boPhan.Ma_Bp}", new { success = true, data = boPhan })
                : Results.BadRequest(new { success = false, message = "Không thể thêm bộ phận" });
        }).WithName("CreateBoPhan");

        group.MapPut("/{maBp}", async (string maBp, BoPhan boPhan, BoPhanRepository repo) =>
        {
            boPhan.Ma_Bp = maBp;
            var result = await repo.UpdateAsync(boPhan);
            return result > 0
                ? Results.Ok(new { success = true, message = "Cập nhật thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy bộ phận '{maBp}'" });
        }).WithName("UpdateBoPhan");

        group.MapDelete("/{maBp}", async (string maBp, BoPhanRepository repo) =>
        {
            var result = await repo.DeleteAsync(maBp);
            return result > 0
                ? Results.Ok(new { success = true, message = "Xóa thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy bộ phận '{maBp}'" });
        }).WithName("DeleteBoPhan");
    }
}

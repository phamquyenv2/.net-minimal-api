using LinkQ.Api.Infrastructure.Repositories;
using LinkQ.Api.Model;

namespace LinkQ.Api.Api;

public static class DoiTuongEndpoints
{
    public static void MapDoiTuongEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/doi-tuong")
            .WithTags("Danh mục đối tượng")
            .RequireAuthorization();

        group.MapGet("/", async (string? kw, string? maLoaiDt, int? page, int? pageSize, DoiTuongRepository repo) =>
        {
            var (items, totalCount) = await repo.GetListAsync(kw, maLoaiDt, page ?? 1, pageSize ?? 20);
            return Results.Ok(new
            {
                success = true, data = items,
                pagination = new { page = page ?? 1, pageSize = pageSize ?? 20, totalCount }
            });
        }).WithName("GetDoiTuong");

        group.MapGet("/{maDt}", async (string maDt, DoiTuongRepository repo) =>
        {
            var item = await repo.GetByIdAsync(maDt);
            return item is null
                ? Results.NotFound(new { success = false, message = $"Không tìm thấy đối tượng '{maDt}'" })
                : Results.Ok(new { success = true, data = item });
        }).WithName("GetDoiTuongById");

        group.MapPost("/", async (DoiTuong doiTuong, DoiTuongRepository repo) =>
        {
            var result = await repo.CreateAsync(doiTuong);
            return result > 0
                ? Results.Created($"/api/doi-tuong/{doiTuong.Ma_Dt}", new { success = true, data = doiTuong })
                : Results.BadRequest(new { success = false, message = "Không thể thêm đối tượng" });
        }).WithName("CreateDoiTuong");

        group.MapPut("/{maDt}", async (string maDt, DoiTuong doiTuong, DoiTuongRepository repo) =>
        {
            doiTuong.Ma_Dt = maDt;
            var result = await repo.UpdateAsync(doiTuong);
            return result > 0
                ? Results.Ok(new { success = true, message = "Cập nhật thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy đối tượng '{maDt}'" });
        }).WithName("UpdateDoiTuong");

        group.MapDelete("/{maDt}", async (string maDt, DoiTuongRepository repo) =>
        {
            var result = await repo.DeleteAsync(maDt);
            return result > 0
                ? Results.Ok(new { success = true, message = "Xóa thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy đối tượng '{maDt}'" });
        }).WithName("DeleteDoiTuong");
    }
}

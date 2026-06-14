using LinkQ.Api.Infrastructure.Repositories;
using LinkQ.Api.Model;

namespace LinkQ.Api.Api;

public static class VatTuEndpoints
{
    public static void MapVatTuEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/vat-tu")
            .WithTags("Danh mục vật tư hàng hóa")
            .RequireAuthorization();

        group.MapGet("/", async (string? kw, string? maNhVt, int? page, int? pageSize, VatTuRepository repo) =>
        {
            var (items, totalCount) = await repo.GetListAsync(kw, maNhVt, page ?? 1, pageSize ?? 20);
            return Results.Ok(new
            {
                success = true, data = items,
                pagination = new { page = page ?? 1, pageSize = pageSize ?? 20, totalCount }
            });
        }).WithName("GetVatTu");

        group.MapGet("/{maVt}", async (string maVt, VatTuRepository repo) =>
        {
            var item = await repo.GetByIdAsync(maVt);
            return item is null
                ? Results.NotFound(new { success = false, message = $"Không tìm thấy vật tư '{maVt}'" })
                : Results.Ok(new { success = true, data = item });
        }).WithName("GetVatTuById");

        group.MapPost("/", async (VatTu vatTu, VatTuRepository repo) =>
        {
            var result = await repo.CreateAsync(vatTu);
            return result > 0
                ? Results.Created($"/api/vat-tu/{vatTu.Ma_Vt}", new { success = true, data = vatTu })
                : Results.BadRequest(new { success = false, message = "Không thể thêm vật tư" });
        }).WithName("CreateVatTu");

        group.MapPut("/{maVt}", async (string maVt, VatTu vatTu, VatTuRepository repo) =>
        {
            vatTu.Ma_Vt = maVt;
            var result = await repo.UpdateAsync(vatTu);
            return result > 0
                ? Results.Ok(new { success = true, message = "Cập nhật thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy vật tư '{maVt}'" });
        }).WithName("UpdateVatTu");

        group.MapDelete("/{maVt}", async (string maVt, VatTuRepository repo) =>
        {
            var result = await repo.DeleteAsync(maVt);
            return result > 0
                ? Results.Ok(new { success = true, message = "Xóa thành công" })
                : Results.NotFound(new { success = false, message = $"Không tìm thấy vật tư '{maVt}'" });
        }).WithName("DeleteVatTu");
    }
}

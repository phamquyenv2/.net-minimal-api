using LinkQ.Api.Infrastructure.Authentication;
using LinkQ.Api.Infrastructure.Repositories;
using LinkQ.Api.Model;

namespace LinkQ.Api.Api;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async (
            RegisterRequest request,
            UserRepository userRepo) =>
        {
            if (await userRepo.UsernameExistsAsync(request.Member_ID))
                return Results.Conflict(new { message = $"Member_ID '{request.Member_ID}' đã tồn tại!" });

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await userRepo.CreateUserAsync(request, passwordHash);

            return Results.Ok(new { message = $"Đăng ký thành công cho '{request.Member_ID}'!" });
        })
        .WithName("Register")
        .WithTags("Authentication")
        .AllowAnonymous();

        app.MapPost("/api/auth/login", async (
            LoginRequest request,
            UserRepository userRepo,
            JwtTokenService jwtService) =>
        {
            var user = await userRepo.GetByUsernameAsync(request.Username);
            if (user is null)
                return Results.Unauthorized();

            if (string.IsNullOrEmpty(user.Password))
                return Results.BadRequest(new { message = "User chưa có password BCrypt. Hãy đăng ký lại." });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Results.Unauthorized();

            var response = jwtService.GenerateToken(user);
            return Results.Ok(response);
        })
        .WithName("Login")
        .WithTags("Authentication")
        .AllowAnonymous();
    }
}

using Dapper;
using Microsoft.Data.SqlClient;
using LinkQ.Api.Model;

namespace LinkQ.Api.Infrastructure.Repositories;

public class UserRepository
{
    private readonly string connectionString;

    public UserRepository(IConfiguration config)
    {
        connectionString = config.GetConnectionString("L60SANGTAM")
            ?? throw new ArgumentNullException("Connection string 'L60SANGTAM' not found.");
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = new SqlConnection(connectionString);
        return await connection.QueryFirstOrDefaultAsync<User>(
            @"SELECT Member_ID, Member_Name, Password, Member_Type, Is_Admin, 
                     Locked, Member_ID_Allow, CheckPass, Ma_CbNv, 
                     Is_MemberID, Ma_DvCs_Default, Ma_Nh_File 
              FROM L00member WHERE Member_ID = @Username",
            new { Username = username }
        );
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        using var connection = new SqlConnection(connectionString);
        var count = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM L00member WHERE Member_ID = @Username",
            new { Username = username }
        );
        return count > 0;
    }

    public async Task<int> CreateUserAsync(RegisterRequest request, string passwordHash)
    {
        using var connection = new SqlConnection(connectionString);
        return await connection.ExecuteAsync(
            @"INSERT INTO L00member 
                (Member_ID, Member_Name, Password, Member_Type, Is_Admin, 
                 Locked, Member_ID_Allow, Ma_CbNv, Is_MemberID, 
                 Ma_DvCs_Default, Ma_Nh_File) 
              VALUES 
                (@Member_ID, @Member_Name, @Password, @Member_Type, @Is_Admin, 
                 @Locked, @Member_ID_Allow, @Ma_CbNv, @Is_MemberID, 
                 @Ma_DvCs_Default, @Ma_Nh_File)",
            new
            {
                request.Member_ID,
                request.Member_Name,
                Password = passwordHash,
                request.Member_Type,
                request.Is_Admin,
                request.Locked,
                request.Member_ID_Allow,
                request.Ma_CbNv,
                request.Is_MemberID,
                request.Ma_DvCs_Default,
                request.Ma_Nh_File
            }
        );
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Api.Migrations
{
    /// <inheritdoc />
    public partial class CheckAndSeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE UserName = 'admin')
                BEGIN
                    INSERT INTO dbo.Users (UserName, PasswordHash, Role, CreatedAt)
                    VALUES ('admin', '$2a$11$Ad4mVpUfjuIC.5IZJWCtAe21Jw9i4a9IEQTCPJ2eb5JPcX5IH/oLO', 'Admin', GETDATE());
                END
                ELSE
                BEGIN
                    UPDATE dbo.Users 
                    SET PasswordHash = '$2a$11$Ad4mVpUfjuIC.5IZJWCtAe21Jw9i4a9IEQTCPJ2eb5JPcX5IH/oLO',
                        Role = 'Admin'
                    WHERE UserName = 'admin';
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.Users WHERE UserName = 'admin';");
        }
    }
}

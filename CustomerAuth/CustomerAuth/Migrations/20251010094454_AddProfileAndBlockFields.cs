using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerAuth.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileAndBlockFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "User_Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "User_Accounts");
        }
    }
}

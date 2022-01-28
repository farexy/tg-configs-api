using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TG.Configs.Api.Migrations
{
    public partial class ConfigHasSecrets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "has_secrets",
                table: "configs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "has_secrets",
                table: "configs");
        }
    }
}

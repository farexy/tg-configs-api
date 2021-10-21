using Microsoft.EntityFrameworkCore.Migrations;

namespace TG.Configs.Api.Migrations
{
    public partial class CallbackTgApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tg_app",
                table: "callbacks",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tg_app",
                table: "callbacks");
        }
    }
}

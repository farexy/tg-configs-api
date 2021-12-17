using Microsoft.EntityFrameworkCore.Migrations;

namespace TG.Configs.Api.Migrations
{
    public partial class Variables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "config_variables",
                columns: table => new
                {
                    config_id = table.Column<string>(type: "text", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_config_variables", x => new { x.config_id, x.key });
                    table.ForeignKey(
                        name: "fk_config_variables_configs_config_id",
                        column: x => x.config_id,
                        principalTable: "configs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "config_variables");
        }
    }
}

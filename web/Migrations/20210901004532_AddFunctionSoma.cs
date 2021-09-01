using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Migrations
{
    public partial class AddFunctionSoma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalVisualizacao",
                table: "Maluco",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVisualizacao",
                table: "Maluco");
        }
    }
}

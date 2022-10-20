using Microsoft.EntityFrameworkCore.Migrations;

namespace Capoeira.Persistence.Migrations
{
    public partial class TipoInMestres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Mestres",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Mestres");
        }
    }
}

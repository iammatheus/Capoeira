using Microsoft.EntityFrameworkCore.Migrations;

namespace Capoeira.Persistence.Migrations
{
    public partial class UpdateEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tema",
                table: "Eventos",
                newName: "Nome");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Eventos",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Eventos",
                newName: "Tema");
        }
    }
}

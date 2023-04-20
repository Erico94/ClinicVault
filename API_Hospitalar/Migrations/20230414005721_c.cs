using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class c : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MEDICO_ID",
                table: "ATENDIMENTOS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PACIENTE_ID",
                table: "ATENDIMENTOS",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MEDICO_ID",
                table: "ATENDIMENTOS");

            migrationBuilder.DropColumn(
                name: "PACIENTE_ID",
                table: "ATENDIMENTOS");
        }
    }
}

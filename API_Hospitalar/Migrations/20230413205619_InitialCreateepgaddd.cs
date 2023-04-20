using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateepgaddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PACIENTE_ID",
                table: "CUIDADOS-ESPECIFICOS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "CUIDADOS-ESPECIFICOS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PacienteId",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PacienteId",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PacienteId",
                principalTable: "PACIENTES",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PacienteId",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PacienteId",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropColumn(
                name: "PACIENTE_ID",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "CUIDADOS-ESPECIFICOS");
        }
    }
}

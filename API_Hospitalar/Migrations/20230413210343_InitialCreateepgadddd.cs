using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateepgadddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ALERGIAS_PACIENTES_PACIENTE_ID",
                table: "ALERGIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PacienteId",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PacienteId",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropIndex(
                name: "IX_ALERGIAS_PACIENTE_ID",
                table: "ALERGIAS");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PACIENTE_ID",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PACIENTE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PACIENTE_ID",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PACIENTE_ID",
                principalTable: "PACIENTES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PACIENTE_ID",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PACIENTE_ID",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "CUIDADOS-ESPECIFICOS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PacienteId",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ALERGIAS_PACIENTE_ID",
                table: "ALERGIAS",
                column: "PACIENTE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ALERGIAS_PACIENTES_PACIENTE_ID",
                table: "ALERGIAS",
                column: "PACIENTE_ID",
                principalTable: "PACIENTES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PacienteId",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PacienteId",
                principalTable: "PACIENTES",
                principalColumn: "ID");
        }
    }
}

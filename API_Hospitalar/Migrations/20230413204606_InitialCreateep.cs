using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ALERGIAS_PACIENTES_PACIENTE_ID",
                table: "ALERGIAS");

            migrationBuilder.DropForeignKey(
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PacienteModelId",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PacienteModelId",
                table: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropIndex(
                name: "IX_ALERGIAS_PACIENTE_ID",
                table: "ALERGIAS");

            migrationBuilder.DropColumn(
                name: "PacienteModelId",
                table: "CUIDADOS-ESPECIFICOS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteModelId",
                table: "CUIDADOS-ESPECIFICOS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PacienteModelId",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PacienteModelId");

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
                name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PacienteModelId",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PacienteModelId",
                principalTable: "PACIENTES",
                principalColumn: "ID");
        }
    }
}

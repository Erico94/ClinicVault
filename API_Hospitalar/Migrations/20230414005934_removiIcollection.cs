using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class removiIcollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATENDIMENTOS_MEDICOS_MedicoModelId",
                table: "ATENDIMENTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_ATENDIMENTOS_PACIENTES_PacienteModelId",
                table: "ATENDIMENTOS");

            migrationBuilder.DropIndex(
                name: "IX_ATENDIMENTOS_MedicoModelId",
                table: "ATENDIMENTOS");

            migrationBuilder.DropIndex(
                name: "IX_ATENDIMENTOS_PacienteModelId",
                table: "ATENDIMENTOS");

            migrationBuilder.DropColumn(
                name: "MedicoModelId",
                table: "ATENDIMENTOS");

            migrationBuilder.DropColumn(
                name: "PacienteModelId",
                table: "ATENDIMENTOS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicoModelId",
                table: "ATENDIMENTOS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteModelId",
                table: "ATENDIMENTOS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_MedicoModelId",
                table: "ATENDIMENTOS",
                column: "MedicoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_PacienteModelId",
                table: "ATENDIMENTOS",
                column: "PacienteModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ATENDIMENTOS_MEDICOS_MedicoModelId",
                table: "ATENDIMENTOS",
                column: "MedicoModelId",
                principalTable: "MEDICOS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ATENDIMENTOS_PACIENTES_PacienteModelId",
                table: "ATENDIMENTOS",
                column: "PacienteModelId",
                principalTable: "PACIENTES",
                principalColumn: "ID");
        }
    }
}

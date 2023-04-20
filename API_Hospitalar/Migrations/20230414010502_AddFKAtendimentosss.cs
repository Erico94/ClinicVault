using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class AddFKAtendimentosss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_MEDICO_ID",
                table: "ATENDIMENTOS",
                column: "MEDICO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_PACIENTE_ID",
                table: "ATENDIMENTOS",
                column: "PACIENTE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ATENDIMENTOS_MEDICOS_MEDICO_ID",
                table: "ATENDIMENTOS",
                column: "MEDICO_ID",
                principalTable: "MEDICOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ATENDIMENTOS_PACIENTES_PACIENTE_ID",
                table: "ATENDIMENTOS",
                column: "PACIENTE_ID",
                principalTable: "PACIENTES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATENDIMENTOS_MEDICOS_MEDICO_ID",
                table: "ATENDIMENTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_ATENDIMENTOS_PACIENTES_PACIENTE_ID",
                table: "ATENDIMENTOS");

            migrationBuilder.DropIndex(
                name: "IX_ATENDIMENTOS_MEDICO_ID",
                table: "ATENDIMENTOS");

            migrationBuilder.DropIndex(
                name: "IX_ATENDIMENTOS_PACIENTE_ID",
                table: "ATENDIMENTOS");
        }
    }
}

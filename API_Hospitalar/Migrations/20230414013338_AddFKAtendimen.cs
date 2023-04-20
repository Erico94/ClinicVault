using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class AddFKAtendimen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ATENDIMENTOS_MEDICOS_MEDICO_ID",
                table: "ATENDIMENTOS");

            migrationBuilder.DropIndex(
                name: "IX_ATENDIMENTOS_MEDICO_ID",
                table: "ATENDIMENTOS");

            migrationBuilder.AddColumn<int>(
                name: "AtendimentosId_Atendimento",
                table: "MEDICOS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MEDICOS_AtendimentosId_Atendimento",
                table: "MEDICOS",
                column: "AtendimentosId_Atendimento");

            migrationBuilder.AddForeignKey(
                name: "FK_MEDICOS_ATENDIMENTOS_AtendimentosId_Atendimento",
                table: "MEDICOS",
                column: "AtendimentosId_Atendimento",
                principalTable: "ATENDIMENTOS",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MEDICOS_ATENDIMENTOS_AtendimentosId_Atendimento",
                table: "MEDICOS");

            migrationBuilder.DropIndex(
                name: "IX_MEDICOS_AtendimentosId_Atendimento",
                table: "MEDICOS");

            migrationBuilder.DropColumn(
                name: "AtendimentosId_Atendimento",
                table: "MEDICOS");

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_MEDICO_ID",
                table: "ATENDIMENTOS",
                column: "MEDICO_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ATENDIMENTOS_MEDICOS_MEDICO_ID",
                table: "ATENDIMENTOS",
                column: "MEDICO_ID",
                principalTable: "MEDICOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateepga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ALERGIAS_PACIENTES_PACIENTE_ID",
                table: "ALERGIAS");

            migrationBuilder.DropIndex(
                name: "IX_ALERGIAS_PACIENTE_ID",
                table: "ALERGIAS");
        }
    }
}

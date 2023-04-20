using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENFERMEIROS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INSTITUICAODEFORMACAO = table.Column<string>(name: "INSTITUICAO-DE-FORMACAO", type: "nvarchar(max)", nullable: false),
                    CADASTROCOFENUF = table.Column<string>(name: "CADASTRO-COFEN-UF", type: "nvarchar(max)", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GENERO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DATANASCIMENTO = table.Column<DateTime>(name: "DATA-NASCIMENTO", type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TELEFONE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENFERMEIROS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MEDICOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INSTITUICAOFORMACAO = table.Column<string>(name: "INSTITUICAO-FORMACAO", type: "nvarchar(max)", nullable: false),
                    CRMUF = table.Column<string>(name: "CRM-UF", type: "nvarchar(max)", nullable: false),
                    ESPECIALIZACAOCLINICA = table.Column<string>(name: "ESPECIALIZACAO-CLINICA", type: "nvarchar(max)", nullable: false),
                    ESTADONOSISTEMA = table.Column<bool>(name: "ESTADO-NO-SISTEMA", type: "bit", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GENERO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DATANASCIMENTO = table.Column<DateTime>(name: "DATA-NASCIMENTO", type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TELEFONE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PACIENTES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CONTATOEMERGENCIA = table.Column<string>(name: "CONTATO-EMERGENCIA", type: "nvarchar(max)", nullable: false),
                    CONVENIO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATUSATENDIMENTO = table.Column<string>(name: "STATUS-ATENDIMENTO", type: "nvarchar(max)", nullable: true),
                    TOTAL_DE_ATENDIMENTOS = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GENERO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DATANASCIMENTO = table.Column<DateTime>(name: "DATA-NASCIMENTO", type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TELEFONE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACIENTES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ALERGIAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ALERGIA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PACIENTE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALERGIAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ALERGIAS_PACIENTES_PACIENTE_ID",
                        column: x => x.PACIENTE_ID,
                        principalTable: "PACIENTES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATENDIMENTOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MedicoModelId = table.Column<int>(type: "int", nullable: true),
                    PacienteModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATENDIMENTOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ATENDIMENTOS_MEDICOS_MedicoModelId",
                        column: x => x.MedicoModelId,
                        principalTable: "MEDICOS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ATENDIMENTOS_PACIENTES_PacienteModelId",
                        column: x => x.PacienteModelId,
                        principalTable: "PACIENTES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CUIDADOS-ESPECIFICOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUIDADO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PacienteModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUIDADOS-ESPECIFICOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PacienteModelId",
                        column: x => x.PacienteModelId,
                        principalTable: "PACIENTES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALERGIAS_PACIENTE_ID",
                table: "ALERGIAS",
                column: "PACIENTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_MedicoModelId",
                table: "ATENDIMENTOS",
                column: "MedicoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_PacienteModelId",
                table: "ATENDIMENTOS",
                column: "PacienteModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PacienteModelId",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PacienteModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALERGIAS");

            migrationBuilder.DropTable(
                name: "ATENDIMENTOS");

            migrationBuilder.DropTable(
                name: "CUIDADOS-ESPECIFICOS");

            migrationBuilder.DropTable(
                name: "ENFERMEIROS");

            migrationBuilder.DropTable(
                name: "MEDICOS");

            migrationBuilder.DropTable(
                name: "PACIENTES");
        }
    }
}

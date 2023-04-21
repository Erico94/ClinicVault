using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Hospitalar.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    GENERO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DATANASCIMENTO = table.Column<string>(name: "DATA-NASCIMENTO", type: "nvarchar(max)", nullable: false),
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
                    ESTADONOSISTEMA = table.Column<string>(name: "ESTADO-NO-SISTEMA", type: "nvarchar(max)", nullable: true),
                    ATENDIMENTOS = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GENERO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DATANASCIMENTO = table.Column<string>(name: "DATA-NASCIMENTO", type: "nvarchar(max)", nullable: false),
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
                    GENERO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DATANASCIMENTO = table.Column<string>(name: "DATA-NASCIMENTO", type: "nvarchar(max)", nullable: false),
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
                    PACIENTE_ID = table.Column<int>(type: "int", nullable: false),
                    MEDICO_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATENDIMENTOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ATENDIMENTOS_MEDICOS_MEDICO_ID",
                        column: x => x.MEDICO_ID,
                        principalTable: "MEDICOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATENDIMENTOS_PACIENTES_PACIENTE_ID",
                        column: x => x.PACIENTE_ID,
                        principalTable: "PACIENTES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CUIDADOS-ESPECIFICOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUIDADO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PACIENTE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUIDADOS-ESPECIFICOS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUIDADOS-ESPECIFICOS_PACIENTES_PACIENTE_ID",
                        column: x => x.PACIENTE_ID,
                        principalTable: "PACIENTES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ENFERMEIROS",
                columns: new[] { "ID", "CPF", "CADASTRO-COFEN-UF", "DATA-NASCIMENTO", "GENERO", "INSTITUICAO-DE-FORMACAO", "NOME", "TELEFONE" },
                values: new object[,]
                {
                    { 1, "65433256985", "065423/PR", "03/dez/1999", "F", "UFPR", "Janaína Pascoal", "4599778635" },
                    { 2, "06458799652", "005423/PR", "03/fev/2003", "F", "UFPR", "Zuleide Silva", "45998784896" }
                });

            migrationBuilder.InsertData(
                table: "MEDICOS",
                columns: new[] { "ID", "CPF", "CRM-UF", "DATA-NASCIMENTO", "ESPECIALIZACAO-CLINICA", "ESTADO-NO-SISTEMA", "GENERO", "INSTITUICAO-FORMACAO", "NOME", "TELEFONE", "ATENDIMENTOS" },
                values: new object[,]
                {
                    { 1, "32546879526", "032654/PR", "23/jan/1990", "CLINICO_GERAL", "true", "F", "UFPR", "Amanda Ciqueira Gomés", "45998165487", 0 },
                    { 2, "03265146588", "017654/PR", "03/dez/1995", "CLINICO_GERAL", "true", "M", "UFPR", "Aldair Plínio Mossolin", "45998781256", 0 }
                });

            migrationBuilder.InsertData(
                table: "PACIENTES",
                columns: new[] { "ID", "CPF", "CONTATO-EMERGENCIA", "CONVENIO", "DATA-NASCIMENTO", "GENERO", "NOME", "STATUS-ATENDIMENTO", "TELEFONE", "TOTAL_DE_ATENDIMENTOS" },
                values: new object[,]
                {
                    { 1, "32451118946", "45998487596", "Amil", "09/mar/1999", "M", "José Alves Siqueira", "NAO_ATENDIDO", "4532659874", 0 },
                    { 2, "33889369944", "45998741523", "Unimed", "09/abr/1987", "M", "Adair José", "NAO_ATENDIDO", "45998742103", 0 },
                    { 3, "82052437940", "45987452163", "HB Saude", "19/dez/1991", "M", "Marcos Anderman", "NAO_ATENDIDO", "4532569874", 0 },
                    { 4, "03254879685", "4532365874", "Medi Saude", "01/jan/1979", "F", "Maria de Fatima", "NAO_ATENDIDO", "45988771365", 0 },
                    { 5, "03256874987", "45998658742", "Ameplan", "28/jul/1978", "M", "José Aldo", "NAO_ATENDIDO", "45998715236", 0 },
                    { 6, "06548756982", "4536230198", "Promed", "15/nov/1996", "F", "Ana Paula Stritzsch", "NAO_ATENDIDO", "4532263584", 0 },
                    { 7, "03215236685", "45998487596", "Promed", "01/mar/1965", "F", "Fátima Andrade Silva", "NAO_ATENDIDO", "45998748579", 0 },
                    { 8, "09658748695", "45998547821", "Premium Saude", "09/abr/1972", "M", "Mário Silva", "NAO_ATENDIDO", "45998546325", 0 },
                    { 9, "03265878965", "45996365247", "Amil", "28/mar/1996", "M", "André Heimn", "NAO_ATENDIDO", "45996854702", 0 },
                    { 10, "06325418759", "45998487596", "Samaritano", "19/abr/2005", "F", "Priscila Boliviar Cácer", "NAO_ATENDIDO", "4532659874", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALERGIAS_PACIENTE_ID",
                table: "ALERGIAS",
                column: "PACIENTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_MEDICO_ID",
                table: "ATENDIMENTOS",
                column: "MEDICO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTOS_PACIENTE_ID",
                table: "ATENDIMENTOS",
                column: "PACIENTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOS-ESPECIFICOS_PACIENTE_ID",
                table: "CUIDADOS-ESPECIFICOS",
                column: "PACIENTE_ID");
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

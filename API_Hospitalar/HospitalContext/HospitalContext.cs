using API_Hospitalar.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Hospitalar.HospitalContextDb
{
    public class HospitalContext:DbContext
    {
        public DbSet<PacienteModel> DbPacientes { get; set;}
        public DbSet<MedicoModel> DbMedicos { get; set;}
        public DbSet<EnfermeiroModel> DbEnfermeiros { get; set; }
        public DbSet<Atendimentos> DbAtendimentos { get; set;}
        public DbSet<Alergias> DbAlergias { get; set; }
        public DbSet<Cuidados> DbCuidados { get; set; }
        
        public HospitalContext() { }
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacienteModel>().HasData(
                new PacienteModel
                {
                    Id = 1,
                    Nome = "José Alves Siqueira",
                    Genero = "M",
                    Data_de_Nascimento = "09/mar/1999",
                    CPF = "32451118946",
                    Telefone = "4532659874",
                    Contato_de_Emergencia = "45998487596",
                    Convenio = "Amil",
                    Status_De_Atendimento = "NAO_ATENDIDO"
                },
                new PacienteModel
                {
                    Id = 2,
                    Nome = "Adair José",
                    Genero = "M",
                    Data_de_Nascimento = "09/abr/1987",
                    CPF = "33889369944",
                    Telefone = "45998742103",
                    Contato_de_Emergencia = "45998741523",
                    Convenio = "Unimed",
                    Status_De_Atendimento = "NAO_ATENDIDO"
                },
                 new PacienteModel
                 {
                     Id = 3,
                     Nome = "Marcos Anderman",
                     Genero = "M",
                     Data_de_Nascimento = "19/dez/1991",
                     CPF = "82052437940",
                     Telefone = "4532569874",
                     Contato_de_Emergencia = "45987452163",
                     Convenio = "HB Saude",
                     Status_De_Atendimento = "NAO_ATENDIDO"
                 },
                new PacienteModel
                {
                    Id = 4,
                    Nome = "Maria de Fatima",
                    Genero = "F",
                    Data_de_Nascimento = "01/jan/1979",
                    CPF = "03254879685",
                    Telefone = "45988771365",
                    Contato_de_Emergencia = "4532365874",
                    Convenio = "Medi Saude",
                    Status_De_Atendimento = "NAO_ATENDIDO"
                },
                new PacienteModel
                {
                    Id = 5,
                    Nome = "José Aldo",
                    Genero = "M",
                    Data_de_Nascimento = "28/jul/1978",
                    CPF = "03256874987",
                    Telefone = "45998715236",
                    Contato_de_Emergencia = "45998658742",
                    Convenio = "Ameplan",
                    Status_De_Atendimento = "NAO_ATENDIDO"
                },
                new PacienteModel
                {
                    Id = 6,
                    Nome = "Ana Paula Stritzsch",
                    Genero = "F",
                    Data_de_Nascimento = "15/nov/1996",
                    CPF = "06548756982",
                    Telefone = "4532263584",
                    Contato_de_Emergencia = "4536230198",
                    Convenio = "Promed",
                    Status_De_Atendimento = "NAO_ATENDIDO"
                },
                 new PacienteModel
                 {
                     Id = 7,
                     Nome = "Fátima Andrade Silva",
                     Genero = "F",
                     Data_de_Nascimento = "01/mar/1965",
                     CPF = "03215236685",
                     Telefone = "45998748579",
                     Contato_de_Emergencia = "45998487596",
                     Convenio = "Promed",
                     Status_De_Atendimento = "NAO_ATENDIDO"
                 },
                new PacienteModel
                {
                    Id = 8,
                    Nome = "Mário Silva",
                    Genero = "M",
                    Data_de_Nascimento = "09/abr/1972",
                    CPF = "09658748695",
                    Telefone = "45998546325",
                    Contato_de_Emergencia = "45998547821",
                    Convenio = "Premium Saude",
                    Status_De_Atendimento = "NAO_ATENDIDO"
                },
                 new PacienteModel
                 {
                     Id = 9,
                     Nome = "André Heimn",
                     Genero = "M",
                     Data_de_Nascimento = "28/mar/1996",
                     CPF = "03265878965",
                     Telefone = "45996854702",
                     Contato_de_Emergencia = "45996365247",
                     Convenio = "Amil",
                     Status_De_Atendimento = "NAO_ATENDIDO"
                 },
                new PacienteModel
                {
                    Id = 10,
                    Nome = "Priscila Boliviar Cácer",
                    Genero = "F",
                    Data_de_Nascimento = "19/abr/2005",
                    CPF = "06325418759",
                    Telefone = "4532659874",
                    Contato_de_Emergencia = "45998487596",
                    Convenio = "Samaritano",
                    Status_De_Atendimento = "NAO_ATENDIDO"
                }
                );
            modelBuilder.Entity<MedicoModel>().HasData(
                new MedicoModel
                {
                    Id = 1,
                    Nome = "Amanda Ciqueira Gomés",
                    Genero = "F",
                    Data_de_Nascimento = "23/jan/1990",
                    CPF = "32546879526",
                    Telefone = "45998165487",
                    InstituicaoDeFormacao = "UFPR",
                    CRM_UF = "032654/PR",
                    EspecializacaoClinica = "CLINICO_GERAL",
                    EstadoNoSistema = "true"
                },
                new MedicoModel
                {
                    Id = 2,
                    Nome = "Aldair Plínio Mossolin",
                    Genero = "M",
                    Data_de_Nascimento = "03/dez/1995",
                    CPF = "03265146588",
                    Telefone = "45998781256",
                    InstituicaoDeFormacao = "UFPR",
                    CRM_UF = "017654/PR",
                    EspecializacaoClinica = "CLINICO_GERAL",
                    EstadoNoSistema = "true"
                }
                );
            modelBuilder.Entity<EnfermeiroModel>().HasData(
                new EnfermeiroModel
                {
                    Id = 1,
                    Nome = "Janaína Pascoal",
                    Genero = "F",
                    Data_de_Nascimento = "03/dez/1999",
                    CPF = "65433256985",
                    Telefone = "4599778635",
                    InstituicaoDeFormacao = "UFPR",
                    CadastroCOFEN_UF = "065423/PR"
                },
                new EnfermeiroModel
                {
                    Id = 2,
                    Nome = "Zuleide Silva",
                    Genero = "F",
                    Data_de_Nascimento = "03/fev/2003",
                    CPF = "06458799652",
                    Telefone = "45998784896",
                    InstituicaoDeFormacao = "UFPR",
                    CadastroCOFEN_UF = "005423/PR"
                }
                );
        }
    }
}

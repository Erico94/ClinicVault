﻿// <auto-generated />
using System;
using API_Hospitalar.HospitalContextDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_Hospitalar.Migrations
{
    [DbContext(typeof(HospitalContext))]
    [Migration("20230413210343_InitialCreateepgadddd")]
    partial class InitialCreateepgadddd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API_Hospitalar.Models.Alergias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescricaoAlergia")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ALERGIA");

                    b.Property<int>("PacienteID")
                        .HasColumnType("int")
                        .HasColumnName("PACIENTE_ID");

                    b.HasKey("Id");

                    b.ToTable("ALERGIAS");
                });

            modelBuilder.Entity("API_Hospitalar.Models.Atendimentos", b =>
                {
                    b.Property<int>("Id_Atendimento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Atendimento"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("DESCRICAO");

                    b.Property<int?>("MedicoModelId")
                        .HasColumnType("int");

                    b.Property<int?>("PacienteModelId")
                        .HasColumnType("int");

                    b.HasKey("Id_Atendimento");

                    b.HasIndex("MedicoModelId");

                    b.HasIndex("PacienteModelId");

                    b.ToTable("ATENDIMENTOS");
                });

            modelBuilder.Entity("API_Hospitalar.Models.Cuidados", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescricaoCuidado")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CUIDADO");

                    b.Property<int>("PacienteID")
                        .HasColumnType("int")
                        .HasColumnName("PACIENTE_ID");

                    b.HasKey("Id");

                    b.HasIndex("PacienteID");

                    b.ToTable("CUIDADOS-ESPECIFICOS");
                });

            modelBuilder.Entity("API_Hospitalar.Models.EnfermeiroModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CPF");

                    b.Property<string>("CadastroCOFEN_UF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CADASTRO-COFEN-UF");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATA-NASCIMENTO");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("GENERO");

                    b.Property<string>("InstituicaoDeFormacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("INSTITUICAO-DE-FORMACAO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NOME");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TELEFONE");

                    b.HasKey("Id");

                    b.ToTable("ENFERMEIROS");
                });

            modelBuilder.Entity("API_Hospitalar.Models.MedicoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CPF");

                    b.Property<string>("CRM_UF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CRM-UF");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATA-NASCIMENTO");

                    b.Property<string>("EspecializacaoClinica")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ESPECIALIZACAO-CLINICA");

                    b.Property<bool>("EstadoNoSistema")
                        .HasColumnType("bit")
                        .HasColumnName("ESTADO-NO-SISTEMA");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("GENERO");

                    b.Property<string>("InstituicaoDeFormacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("INSTITUICAO-FORMACAO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NOME");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TELEFONE");

                    b.HasKey("Id");

                    b.ToTable("MEDICOS");
                });

            modelBuilder.Entity("API_Hospitalar.Models.PacienteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CPF");

                    b.Property<string>("ContatoDeEmergência")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CONTATO-EMERGENCIA");

                    b.Property<string>("Convenio")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CONVENIO");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATA-NASCIMENTO");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("GENERO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NOME");

                    b.Property<string>("StatusDeAtendimento")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("STATUS-ATENDIMENTO");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TELEFONE");

                    b.Property<int>("TotalAtendimentos")
                        .HasColumnType("int")
                        .HasColumnName("TOTAL_DE_ATENDIMENTOS");

                    b.HasKey("Id");

                    b.ToTable("PACIENTES");
                });

            modelBuilder.Entity("API_Hospitalar.Models.Atendimentos", b =>
                {
                    b.HasOne("API_Hospitalar.Models.MedicoModel", null)
                        .WithMany("Atendimentos")
                        .HasForeignKey("MedicoModelId");

                    b.HasOne("API_Hospitalar.Models.PacienteModel", null)
                        .WithMany("Atendimentos")
                        .HasForeignKey("PacienteModelId");
                });

            modelBuilder.Entity("API_Hospitalar.Models.Cuidados", b =>
                {
                    b.HasOne("API_Hospitalar.Models.PacienteModel", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("API_Hospitalar.Models.MedicoModel", b =>
                {
                    b.Navigation("Atendimentos");
                });

            modelBuilder.Entity("API_Hospitalar.Models.PacienteModel", b =>
                {
                    b.Navigation("Atendimentos");
                });
#pragma warning restore 612, 618
        }
    }
}

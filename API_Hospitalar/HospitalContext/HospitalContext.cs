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
    }
}

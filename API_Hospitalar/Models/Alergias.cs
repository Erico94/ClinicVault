using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Hospitalar.Models
{
    [Table("ALERGIAS")]
    public class Alergias
    {
        [Column("ID"), Key] public int Id { get; set; }
        [Column("ALERGIA")] public string DescricaoAlergia { get; set; }
        [Column("PACIENTE_ID")] public int PacienteID { get; set; }
        public PacienteModel Paciente { get; set; }
    }
}

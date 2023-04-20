using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Hospitalar.Models
{
    [Table("CUIDADOS-ESPECIFICOS")]
    public class Cuidados
    {
        [Column("ID"), Key] public int Id { get; set; }
        [Column("CUIDADO")] public string DescricaoCuidado { get; set; }
        [Column("PACIENTE_ID")] public int PacienteID { get; set; }
        public PacienteModel Paciente { get; set; }
    }

}

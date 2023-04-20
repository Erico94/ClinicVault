using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Hospitalar.Models
{
    [Table("ATENDIMENTOS")]
    public class Atendimentos
    {
        [Column("ID"), Key] public int Id_Atendimento { get; set; }
        [Column("DESCRICAO"), MaxLength(150), Required] public string Descricao { get; set; }
        [Column("PACIENTE_ID"),ForeignKey("PACIENTES")] public int PacienteID { get; set; }
        public PacienteModel Paciente { get; set; }
        [Column("MEDICO_ID"),ForeignKey("MEDICOS")] public int MedicoId { get; set; }
        public MedicoModel Medico { get; set; }
    }
}

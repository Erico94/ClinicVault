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
        //[Column("RELACAO_PACIENTES")] public ICollection<Cuidados> Cuidados_especificos { get; set; }
    }

    //[Table("CUIDADOS_X_PACIENTES")]
    //public class Cuidados_x_Pacientes
    //{
    //    [Column("ID"),Key] public int Id { get; set; }
    //    [Column("ID_CUIDADO")] public int Id_cuidado { get; set; }
    //    [Column("ID_PACIENTE")] public int Id_paciente { get; set; }
    //}
}

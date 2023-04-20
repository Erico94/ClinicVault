using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace API_Hospitalar.Models
{
    [Table("PACIENTES")]
    public class PacienteModel:Pessoa
    {
        [Column("CONTATO-EMERGENCIA"),Required] public string Contato_de_Emergencia { get; set; }
        [Column("CONVENIO")] public string Convenio { get; set; }
        [Column("STATUS-ATENDIMENTO")] public string Status_De_Atendimento { get; set; }
        [Column("TOTAL_DE_ATENDIMENTOS")] public int TotalAtendimentos { get; set; }
        //[NotMapped] public ICollection<Alergias> Alergias { get; set; }
        //[NotMapped] public List<Cuidados> Cuidados_Especificos { get; set; }
        //[NotMapped] public List<Atendimentos> ListaAtendimentos { get; set; }
    }

   
}

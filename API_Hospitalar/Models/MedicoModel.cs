using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hospitalar.Models
{
    [Table("MEDICOS")]
    public class MedicoModel : Pessoa
    {
        [Column("INSTITUICAO-FORMACAO"), Required] public string InstituicaoDeFormacao { get; set; }
        [Column("CRM-UF"),Required] public string CRM_UF { get; set; }
        [Column("ESPECIALIZACAO-CLINICA"),Required] public string EspecializacaoClinica { get; set; }
        [Column("ESTADO-NO-SISTEMA")] public string EstadoNoSistema { get; set; }
        [Column("ATENDIMENTOS")] public int TotalAtendimentos { get; set;}
    }
}

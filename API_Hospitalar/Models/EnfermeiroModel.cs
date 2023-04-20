using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Hospitalar.Models
{
    [Table("ENFERMEIROS")]
    public class EnfermeiroModel:Pessoa
    {
        [Column("INSTITUICAO-DE-FORMACAO"),Required]public string InstituicaoDeFormacao { get; set; }
        [Column("CADASTRO-COFEN-UF"),Required]public string CadastroCOFEN_UF { get; set; }

    }
}

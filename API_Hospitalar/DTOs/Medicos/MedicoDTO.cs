using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using API_Hospitalar.Models;
using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.Medicos
{
    public class MedicoDTO
    {
        [NotNull] public string Nome { get; set; }
        public string Genero { get; set; }
        public DateTime Data_de_Nascimento { get; set; }
        [NotNull] public string CPF { get; set; }
        [NotNull] public string Telefone { get; set; }
        [MedicoNotNull] public string InstituicaoDeFormacao { get; set; }
        [MedicoNotNull] public string CRM_UF { get; set; }
        [EspecialidadeAceita] public string EspecializacaoClinica { get; set; }
        [MedicoNotNull] public bool EstadoNoSistema { get; set; }
    }
}

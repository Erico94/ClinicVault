using API_Hospitalar.Models;
using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.Enfermeiros
{
    public class EnfermeiroDTO
    {
        [NotNull] public string Nome { get; set; }
        public string Genero { get; set; }
        public DateTime Data_de_Nascimento { get; set; }
        [NotNull] public string CPF { get; set; }
        [NotNull] public string Telefone { get; set; }
        [NotNull] public string InstituicaoDeFormacao { get; set; }
        [NotNull] public string CadastroCOFEN_UF { get; set; }
    }
}

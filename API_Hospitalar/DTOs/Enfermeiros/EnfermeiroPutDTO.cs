using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.Enfermeiros
{
    public class EnfermeiroPutDTO
    {
        [NotNull] public string Nome { get; set; }
        public string Genero { get; set; }
        [NotNull] public string Telefone { get; set; }
        [NotNull] public string InstituicaoDeFormacao { get; set; }
        [NotNull] public string CadastroCOFEN_UF { get; set; }
    }
}

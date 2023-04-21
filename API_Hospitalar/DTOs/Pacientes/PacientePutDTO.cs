using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.Pacientes
{
    public class PacientePutDTO
    {
        [NotNull] public string Nome { get; set; }
        [NotNull] public string Telefone { get; set; }
        public string Convenio { get; set; }
        public string Genero { get; set; }
        [NotNull] public string Contato_de_Emergencia { get; set; }
    }
}

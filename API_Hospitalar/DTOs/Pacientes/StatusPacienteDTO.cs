using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.Pacientes
{
    public class StatusPacienteDTO
    {
        [NotNull] public string Status_de_Atendimento { get; set; }
    }
}

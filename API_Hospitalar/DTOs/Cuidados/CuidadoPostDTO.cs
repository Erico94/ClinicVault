using static API_Hospitalar.CustomValidation;

namespace ClinicVault.DTOs.Cuidados
{
    public class CuidadoPostDTO
    {
        [CampoObrigatorio]public string Descricao_cuidado { get; set; }
        [CampoObrigatorio]public int Identificador_paciente { get; set; }
    }
}

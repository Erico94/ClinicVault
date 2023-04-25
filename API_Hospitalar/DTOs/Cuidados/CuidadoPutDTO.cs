using static API_Hospitalar.CustomValidation;

namespace ClinicVault.DTOs.Cuidados
{
    public class CuidadoPutDTO
    {
        [CampoObrigatorio] public string Descricao_cuidado { get; set; }
    }
}

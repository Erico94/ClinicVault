using static API_Hospitalar.CustomValidation;

namespace ClinicVault.DTOs.Alergias
{
    public class AlergiaPutDTO
    {
        [CampoObrigatorio]public string Descricao_alergia { get; set; }
    }
}

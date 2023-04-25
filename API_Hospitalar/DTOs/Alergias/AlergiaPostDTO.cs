using static API_Hospitalar.CustomValidation;

namespace ClinicVault.DTOs.Alergias
{
    public class AlergiaPostDTO
    {
        [CampoObrigatorio] public string Descricao { get; set; }
        [CampoObrigatorio] public int Identificador_do_paciente { get; set; }
    }
}

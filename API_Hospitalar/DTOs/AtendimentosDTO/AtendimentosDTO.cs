using System.ComponentModel.DataAnnotations;
using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.AtendimentosDTO
{
    public class AtendimentosDTO
    {
        [Required][CampoObrigatorio] public string Descricao { get; set; }
        [Required][CampoObrigatorio] public int Identificador_medico { get; set; }
        [Required][CampoObrigatorio] public int Identificador_paciente { get; set; }
    }
}

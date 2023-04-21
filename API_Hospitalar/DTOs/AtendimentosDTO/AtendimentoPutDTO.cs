using System.ComponentModel.DataAnnotations;
using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.AtendimentosDTO
{
    public class AtendimentoPutDTO
    {
        [Required][CampoObrigatorio] public string Descricao { get; set; }
    }
}

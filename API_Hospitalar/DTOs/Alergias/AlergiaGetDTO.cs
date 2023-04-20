using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Hospitalar.DTOs.Alergias
{
    public class AlergiaGetDTO
    {
        public int Id_alergia { get; set; }
        public int Identificador_paciente { get; set; }
        public string DescricaoAlergia { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Hospitalar.DTOs.Alergias
{
    public class AlergiaGetDTO
    {
        public int Identificador_alergia { get; set; }
        public int Identificador_paciente { get; set; }
        public string Descricao_Alergia { get; set; }
    }
}

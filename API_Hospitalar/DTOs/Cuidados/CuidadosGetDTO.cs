using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Hospitalar.DTOs.Cuidados
{
    public class CuidadosGetDTO
    {
        public int Id { get; set; }
        public string DescricaoCuidado { get; set; }
        public int Identificador_paciente { get; set; }
    }
}

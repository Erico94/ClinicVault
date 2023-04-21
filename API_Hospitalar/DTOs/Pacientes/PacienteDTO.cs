using API_Hospitalar.Models;
using System.ComponentModel.DataAnnotations;
using API_Hospitalar;
using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTO.Paciente
{
    public class PacienteDTO
    {
        [Required][NotNull] public string Nome { get; set; }
        [Required][NotNull] public string CPF { get; set; }
        [Required][NotNull] public string Telefone { get; set; }
        public string Convenio { get; set; }
        public DateTime Data_de_Nascimento { get; set; }
        public string Genero { get; set; }
        [Required][NotNull] public string Contato_de_Emergencia { get; set; }
        [Required][CheckStatus]public string Status_De_Atendimento { get; set; }
        public string Alergias { get; set; }
        public string Cuidados_especificos { get; set; }

    }
}

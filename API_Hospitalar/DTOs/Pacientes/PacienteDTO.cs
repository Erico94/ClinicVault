using API_Hospitalar.Models;

namespace API_Hospitalar.DTO.Paciente
{
    public class PacienteDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Convenio { get; set; }
        public DateTime Data_de_Nascimento { get; set; }
        public string Genero { get; set; }
        public string Contato_de_Emergencia { get; set; }
        public string Status_De_Atendimento { get; set; }
        public string Alergias { get; set; }
        public string Cuidados_especificos { get; set; }

    }
}

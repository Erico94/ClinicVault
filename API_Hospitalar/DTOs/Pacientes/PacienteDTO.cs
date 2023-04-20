using API_Hospitalar.Models;

namespace API_Hospitalar.DTO.Paciente
{
    public class PacienteDTO
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Convenio { get; set; }
        public DateTime Data_de_Nascimento { get; set; }
        public string Genero { get; set; }
        public string Contato_de_Emergencia { get; set; }
        public string Status_De_Atendimento { get; set; }
        public string Descricao_alergia { get; set; }
        public string Descricao_cuidados { get; set; }

    }
}

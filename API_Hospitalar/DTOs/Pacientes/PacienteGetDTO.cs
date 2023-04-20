using API_Hospitalar.DTOs.Alergias;
using API_Hospitalar.DTOs.AtendimentosDTO;
using API_Hospitalar.DTOs.Cuidados;
using API_Hospitalar.Models;

namespace API_Hospitalar.DTO.Paciente
{
    public class PacienteGetDTO
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Convenio { get; set; }
        public string Data_de_Nascimento { get; set; }
        public string Genero { get; set; }
        public string Contato_de_Emergencia { get; set; }
        public string Status_De_Atendimento { get; set; }
        public int TotalAtendimentos { get; set; }
        public List<AlergiaGetDTO> Alergias { get; set; }
        public List<CuidadosGetDTO> Cuidados_Especificos { get; set; }
        public List<AtendimentosGetDTO> Atendimentos { get; set; }
    }
}

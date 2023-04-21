using API_Hospitalar.DTOs.AtendimentosDTO;
using API_Hospitalar.Models;

namespace API_Hospitalar.DTOs.Medicos
{
    public class MedicoGetDTO
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Data_de_Nascimento { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string InstituicaoDeFormacao { get; set; }
        public string CRM_UF { get; set; }
        public string EspecializacaoClinica { get; set; }
        public string EstadoNoSistema { get; set; }
        public int Atendimentos { get; set; }
        public List<AtendimentosGetDTO> Lista_de_Atendimentos { get; set; }
    }
}

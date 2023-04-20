using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using API_Hospitalar.Models;

namespace API_Hospitalar.DTOs.Medicos
{
    public class MedicoDTO
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public DateTime Data_de_Nascimento { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string InstituicaoDeFormacao { get; set; }
        public string CRM_UF { get; set; }
        public string EspecializacaoClinica { get; set; }
        public bool EstadoNoSistema { get; set; }
        public int Atendimentos { get; set; }
    }
}

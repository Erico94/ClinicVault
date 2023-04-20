namespace API_Hospitalar.DTOs.Medicos
{
    public class MedicoPutDTO
    {
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Telefone { get; set; }
        public string InstituicaoDeFormacao { get; set; }
        public string CRM_UF { get; set; }
        public string EspecializacaoClinica { get; set; }
    }
}

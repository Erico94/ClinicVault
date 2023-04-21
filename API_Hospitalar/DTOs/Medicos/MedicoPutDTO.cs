using static API_Hospitalar.CustomValidation;

namespace API_Hospitalar.DTOs.Medicos
{
    public class MedicoPutDTO
    {
        [NotNull]public string Nome { get; set; }
        public string Genero { get; set; }
        [NotNull] public string Telefone { get; set; }
        [MedicoNotNull] public string InstituicaoDeFormacao { get; set; }
        [MedicoNotNull] public string CRM_UF { get; set; }
        [EspecialidadeAceita]public string EspecializacaoClinica { get; set; }
    }
}

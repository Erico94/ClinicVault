using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Security.Cryptography;

namespace API_Hospitalar
{
    public  class CustomValidation
    {
        public sealed class CheckStatus : ValidationAttribute
        {
            protected override ValidationResult IsValid(object statusAtendimento, ValidationContext validationContext)
            {
                List<string> ValoresAceitos = new List<string>() { "AGUARDANDO_ATENDIMENTO", "ATENDIDO", "NAO_ATENDIDO", "EM_ATENDIMENTO" };
                if (ValoresAceitos.Contains(statusAtendimento))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Por favor insira algum dos seguintes status de atendimento: AGUARDANDO_ATENDIMENTO, ATENDIDO, NAO_ATENDIDO, EM_ATENDIMENTO");
                }
            }
        }
        public sealed class NotNull : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is not null and not (object)"string")
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(" Cpf, nome, data de nascimento, telefone e contato de emergência são de preenchimento obrigatório");
                }
            }
            
        }
        public sealed class MedicoNotNull : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is not null and not (object)"string")
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Especialização, CRM-UF, Instituição de formação e estado no sistema são de preenchimento obrigatório.");
                }
            }
        }
        public sealed class EspecialidadeAceita : ValidationAttribute
        {
            
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                List<string> ValoresAceitos = new List<string>() { "CLINICO_GERAL", "ANESTESISTA", "DERMATOLOGIA",
                    "GINECOLOGIA", "NEUROLOGIA", "PEDIATRIA", "PSIQUIATRIA","ORTOPEDIA"};
                if (ValoresAceitos.Contains(value)){
                    return  ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("por favor, insira uma das seguintes especializações: CLINICO_GERAL, ANESTESISTA, DERMATOLOGIA," +
                        "GINECOLOGIA, NEUROLOGIA, PEDIATRIA, PSIQUIATRIA ou ORTOPEDIA.");
                }
            }
        }
        public sealed class CampoObrigatorio : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is not null and not (object)"string")
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(" Todos os campos são de preenchimento obrigatório");
                }
            }

        }


    }
}

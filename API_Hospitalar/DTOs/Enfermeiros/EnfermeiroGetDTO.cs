﻿namespace API_Hospitalar.DTOs.Enfermeiros
{
    public class EnfermeiroGetDTO
    {
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Data_de_Nascimento { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string InstituicaoDeFormacao { get; set; }
        public string CadastroCOFEN_UF { get; set; }
    }
}

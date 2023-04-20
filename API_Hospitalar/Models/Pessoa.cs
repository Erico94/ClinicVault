using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace API_Hospitalar.Models
{
    
    public abstract class Pessoa
    {
        [Column("ID"),Key]public int Id { get; set; }
        [Column("NOME"),Required,MaxLength(100)]public string Nome { get; set; }
        [Column("GENERO"), MaxLength(15)]public string Genero { get; set; }
        [Column("DATA-NASCIMENTO"),Required]public string Data_de_Nascimento { get; set; }
        [Column("CPF"),Required]public string CPF { get; set; }
        [Column("TELEFONE"),Required]public string Telefone { get; set; }
    }
}

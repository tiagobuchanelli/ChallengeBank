using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuncionalHealthChallenge.Models
{
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Column("nome_usuario")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 100 caracteres")]
        [DataType("varchar")]
        public string Nome { get; set; }

        [Column("cpf_usuario")]
        [MaxLength(11, ErrorMessage = "CPF Obrigatório")]
        [MinLength(11, ErrorMessage = "CPF Obrigatório")]
        [DataType("varchar")]
        public string CPF { get; set; }


        public Usuario()
        {
        }
        public Usuario(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;

        }




    }
}
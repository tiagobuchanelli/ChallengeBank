using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FuncionalHealthChallenge.Models
{
    public class OperacoesFinanceirasContaCorrente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o valor da operação")]
        [Range(0.01, 999999, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Valor { get; set; }


        [Required(ErrorMessage = "Obrigatório informar o número da conta")]
        public int Numero { get; set; }



    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuncionalHealthChallenge.Models
{
    public class ContaCorrente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Column("titular")]
        [Required(ErrorMessage = "Obrigatório informar um Titular")]
        [DataType("int")]
        public int TitularId { get; set; }

        [Column("numero")]
        [Required(ErrorMessage = "Obrigatório informar o número da conta")]
        [DataType("int")]
        public int Numero { get; set; }

        [Column("agencia")]
        [Required(ErrorMessage = "Obrigatório informar o número da agencia")]
        [DataType("int")]
        public int Agencia { get; set; }

        private decimal _saldo;

        [Column("saldo")]
        [DataType("decimal")]
        public decimal Saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                _saldo = value;
            }
        }

        public ContaCorrente()
        { }
        public ContaCorrente(Usuario titular, int numeroAgencia, int numeroConta)
        {
            if (titular == null)
            {
                throw new NullReferenceException("O titular não pode ser nulo!");
            }

            if (numeroAgencia <= 0)
            {
                throw new ArgumentException("A Agencia deve ser maior que 0", nameof(numeroAgencia));
            }

            if (numeroConta <= 0)
            {
                throw new ArgumentException("O número da conta deve ser maior que 0", nameof(numeroConta));
            }



            TitularId = titular.Id;
            Agencia = numeroAgencia;
            Numero = numeroConta;
        }


        public virtual void Sacar(decimal valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para o saque", nameof(valor));
            }

            if (_saldo < valor)
            {
                throw new ArgumentException("Saldo insuficiente para o saque");
            }

            _saldo -= valor;
        }

        public void Depositar(decimal valor)
        {
            _saldo += valor;
        }












    }
}
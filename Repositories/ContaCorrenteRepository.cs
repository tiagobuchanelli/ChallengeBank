using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuncionalHealthChallenge.Data;
using FuncionalHealthChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace FuncionalHealthChallenge.Repositories
{
    public class ContaCorrenteRepository
    {
        private readonly DataContext _context;
        public ContaCorrenteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ContaCorrente>> GetContasCorrentes()
        {
            var contas = await _context.ContasCorrentes.AsNoTracking().ToListAsync();
            if (contas.Count == 0)
            {
                throw new ArgumentException("Nenhuma Conta Corrente encontrada!");
            }

            return contas;
        }

        public async Task<ContaCorrente> GetConta(int numeroConta)
        {
            if (numeroConta <= 0)
                return null;

            var contaCorrente = await _context
            .ContasCorrentes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Numero == numeroConta);

            return contaCorrente;

        }

        public async Task<decimal> SaldoContaCorrente(int numeroConta)
        {
            if (numeroConta <= 0)
                throw new ArgumentException("Conta Corrente inválida!");

            var contaCorrente = await GetConta(numeroConta);
            if (contaCorrente == null)
                throw new NullReferenceException("Conta corrente não existe");

            return contaCorrente.Saldo;

        }

        public async Task<ContaCorrente> SetConta(ContaCorrente conta)
        {
            if (conta.Numero <= 0 || conta.Agencia <= 0 || conta.TitularId <= 0)
                throw new ArgumentException("Dados incorretos, por favor verificque: Agencia/Conta/Titular!");

            //verificar se a conta corrente já existe
            var contaCorrente = await GetConta(conta.Numero);
            if (contaCorrente != null)
                throw new ArgumentException("Conta já cadastrada : " + contaCorrente.Numero);

            //Recuperar dados do titular   
            var userRepo = new UsuarioRepository(_context);
            var titular = await userRepo.GetUsuario(conta.TitularId);
            if (titular == null)
                throw new NullReferenceException("Usuário não cadastrado");

            //criar nova conta corrente  
            await _context.ContasCorrentes.AddAsync(conta);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? conta : throw new ArgumentException("Erro ao cadastrar a conta corrente!"); ;

        }

        public async Task<ContaCorrente> DepositarConta(OperacoesFinanceirasContaCorrente operacao)
        {
            if (operacao.Valor <= 0 || operacao.Numero <= 0)
                throw new ArgumentException("Dados incorretos, por favor verificque: Valor/Número!");

            //verificar se a conta corrente já existe
            var contaCorrente = await GetConta(operacao.Numero);
            if (contaCorrente == null)
                throw new ArgumentException("Conta não cadastrada");

            //criar nova conta corrente  
            contaCorrente.Depositar(operacao.Valor);
            _context.Entry<ContaCorrente>(contaCorrente).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            return result > 0 ? contaCorrente : throw new ArgumentException("Erro ao realizar o depósito na conta corrente!"); ;

        }


        public async Task<ContaCorrente> SacarConta(OperacoesFinanceirasContaCorrente operacao)
        {
            if (operacao.Valor <= 0 || operacao.Numero <= 0)
                throw new ArgumentException("Dados incorretos, por favor verificque: Valor/Número!");

            //verificar se a conta corrente já existe
            var contaCorrente = await GetConta(operacao.Numero);
            if (contaCorrente == null)
                throw new ArgumentException("Conta não cadastrada");

            //criar nova conta corrente  
            contaCorrente.Sacar(operacao.Valor);
            _context.Entry<ContaCorrente>(contaCorrente).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            return result > 0 ? contaCorrente : throw new ArgumentException("Erro ao realizar o saque na conta corrente!"); ;

        }








    }
}
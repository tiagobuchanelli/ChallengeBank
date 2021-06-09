using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuncionalHealthChallenge.Data;
using FuncionalHealthChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuncionalHealthChallenge.Controllers
{
    [Route("v1/conta-corrente")]
    public class ContaCorrenteController : Controller
    {
        //recuperar contas correntes
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ContaCorrente>>> Get([FromServices] DataContext context)
        {
            var contasCorrentes = await context.ContasCorrentes.AsNoTracking().ToListAsync();

            if (contasCorrentes.Count == 0)
            {
                return Ok("Nenhuma conta corrente encontrada!");
            }
            return contasCorrentes;
        }

        //Recuperar Saldo
        [HttpGet]
        [Route("saldo/{id:int}")]
        public async Task<ActionResult<ContaCorrente>> GetSaldo(
            int id,
            [FromServices] DataContext context)
        {
            var contaCorrente = await context
            .ContasCorrentes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Numero == id);

            if (contaCorrente == null)
            {
                return Ok("Nenhuma conta corrente encontrada!");
            }
            return Ok("Saldo da Conta: " + contaCorrente.Saldo);
        }


        //criar conta corrente
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ContaCorrente>> Post(
            [FromServices] DataContext context,
            [FromBody] ContaCorrente model
        )
        {
            //validar model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                //verificar se a conta corrente já existe
                var contaCheck = await context
                .ContasCorrentes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Numero == model.Numero);

                if (contaCheck != null)
                    return BadRequest("Conta já cadastrada : " + contaCheck.Numero);


                //Recuperar dados do titular 
                var titular = await context
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == model.TitularId);

                if (titular == null)
                    return BadRequest("Usuário não cadastrado");


                //criar nova conta corrente  
                ContaCorrente conta = new ContaCorrente(titular, model.Agencia, model.Numero);
                context.ContasCorrentes.Add(conta);
                await context.SaveChangesAsync();
                return conta;

            }
            catch (ArgumentException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (NullReferenceException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Não foi possível criar a conta corrente: Erro: " + e.Message });
            }
        }


        //depositar na conta corrente
        [HttpPut]
        [Route("depositar")]
        public async Task<ActionResult<ContaCorrente>> PutDepositar(
            [FromServices] DataContext context,
            [FromBody] OperacoesFinanceirasContaCorrente model
        )
        {
            //validar model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                //verificar se a conta corrente já existe
                var conta = await context
                .ContasCorrentes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Numero == model.Numero);

                if (conta == null)
                    return BadRequest("Conta Corrente não existente");


                //realizar deposito
                conta.Depositar(model.Valor);
                context.Entry<ContaCorrente>(conta).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return conta;

            }

            catch (Exception e)
            {
                return BadRequest(new { message = "Não foi possível criar a conta corrente: Erro: " + e.Message });
            }
        }



    }

}
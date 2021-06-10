using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuncionalHealthChallenge.Data;
using FuncionalHealthChallenge.Models;
using FuncionalHealthChallenge.Repositories;
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

            try
            {
                var contaCRepo = new ContaCorrenteRepository(context);
                var contasCorrentes = await contaCRepo.GetContasCorrentes();
                return contasCorrentes;

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
                return BadRequest(new { message = "Não foi possível recuperar as contas correntes: Erro: " + e.Message });
            }

        }

        //Recuperar Saldo
        [HttpGet]
        [Route("saldo/{id:int}")]
        public async Task<ActionResult<ContaCorrente>> GetSaldo(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var contaCRepo = new ContaCorrenteRepository(context);
                var contaCorrente = await contaCRepo.SaldoContaCorrente(id);
                return Ok("Saldo da Conta: " + contaCorrente);

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
                return BadRequest(new { message = "Não foi possível recuperar o saldo: Erro: " + e.Message });
            }


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
                var contaCRepo = new ContaCorrenteRepository(context);
                var contaCorrente = await contaCRepo.SetConta(model);
                return contaCorrente;

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

                var contaCRepo = new ContaCorrenteRepository(context);
                var deposito = await contaCRepo.DepositarConta(model);
                return Ok("Conta: " + deposito.Numero + " Saldo: " + deposito.Saldo);

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (NullReferenceException e)
            {
                return BadRequest(new { message = e.Message });
            }

            catch (Exception e)
            {
                return BadRequest(new { message = "Não foi possível realizar a transação: Erro: " + e.Message });
            }
        }



        //sacar na conta corrente
        [HttpPut]
        [Route("sacar")]
        public async Task<ActionResult<ContaCorrente>> PutSacar(
            [FromServices] DataContext context,
            [FromBody] OperacoesFinanceirasContaCorrente model
        )
        {
            //validar model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var contaCRepo = new ContaCorrenteRepository(context);
                var saque = await contaCRepo.SacarConta(model);
                return Ok("Conta: " + saque.Numero + " Saldo: " + saque.Saldo);
                ;

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (NullReferenceException e)
            {
                return BadRequest(new { message = e.Message });
            }

            catch (Exception e)
            {
                return BadRequest(new { message = "Não foi possível realizar a transação: Erro: " + e.Message });
            }
        }



    }

}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuncionalHealthChallenge.Data;
using FuncionalHealthChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuncionalHealthChallenge.Controllers
{
    [Route("v1/usuarios")]
    public class UsuarioController : Controller
    {
        //recuperar usuarios
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Usuario>>> Get([FromServices] DataContext context)
        {
            var users = await context.Usuarios.AsNoTracking().ToListAsync();

            if (users.Count == 0)
            {
                return Ok("Nenhum usuário encontrado!");
            }
            return users;
        }


        //criar usuarios
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Usuario>> Post(
            [FromServices] DataContext context,
            [FromBody] Usuario model
        )
        {
            //validar model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                //verificar se usuário ja existe - CPF
                var usuariocheck = await context
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CPF == model.CPF);

                if (usuariocheck != null)
                    return BadRequest("Usuário já cadastrado : " + usuariocheck.Nome);

                //criar novo usuário
                Usuario usuario = new Usuario(model.Nome, model.CPF);
                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();
                return usuario;

            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário: Erro: " + e.Message });
            }
        }

    }
}
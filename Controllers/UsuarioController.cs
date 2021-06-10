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
    [Route("v1/usuarios")]
    public class UsuarioController : Controller
    {

        //recuperar usuarios
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Usuario>>> Get([FromServices] DataContext context)
        {
            var userRepo = new UsuarioRepository(context);
            var users = await userRepo.GetUsuarios();

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
                var userRepo = new UsuarioRepository(context);

                //verificar se usuário ja existe - CPF
                var usuariocheck = await context
                .Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CPF == model.CPF);

                if (usuariocheck != null)
                    return BadRequest("Usuário já cadastrado : " + usuariocheck.Nome);

                //criar novo usuário
                Usuario usuario = new Usuario(model.Nome, model.CPF);
                await userRepo.SetUsuario(usuario);
                return usuario;

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
                return BadRequest(new { message = "Não foi possível criar o usuário: Erro: " + e.Message });
            }
        }

    }
}
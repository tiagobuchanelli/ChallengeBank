using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FuncionalHealthChallenge.Data;
using FuncionalHealthChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FuncionalHealthChallenge.Repositories
{
    public class UsuarioRepository
    {
        private readonly DataContext _context;
        public UsuarioRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();

        }

        public async Task<Usuario> GetUsuario(int idTitular)
        {
            if (idTitular <= 0)
                return null;

            var usuario = await _context
            .Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == idTitular);

            return usuario;

        }

        public async Task<bool> SetUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new NullReferenceException("O Usuário não pode ser nulo!");

            await _context.Usuarios.AddAsync(usuario);
            var result = await _context.SaveChangesAsync();

            if (result < 0)
                throw new ArgumentException("Erro ao cadastrar usuário!");

            return result > 0;
        }


    }
}
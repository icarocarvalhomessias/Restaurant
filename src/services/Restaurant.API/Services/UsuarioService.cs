using Restaurant.API.Data.Repositories;
using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _usuarioRepository.GetAll();
        }

        public async Task<Usuario> GetById(Guid? id)
        {
            return await _usuarioRepository.GetById(id);
        }

        public async Task Add(Usuario usuario)
        {
            await _usuarioRepository.Add(usuario);
        }

        public async Task Update(Usuario usuario)
        {
            await _usuarioRepository.Update(usuario);
        }

        public async Task Delete(Guid id)
        {
            await _usuarioRepository.Delete(id);
        }

        public async Task GetUsuarioByIdAsync(Guid value)
        {
            await _usuarioRepository.GetById(value);
        }
    }
}

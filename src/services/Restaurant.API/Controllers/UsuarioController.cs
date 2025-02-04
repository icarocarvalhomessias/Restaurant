using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models;
using Restaurant.API.Services.Interfaces;
using Restaurant.WebApi.Core.Controller;
using System;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [Route("api/usuarios")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            return CustomResponse(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var usuario = await _usuarioService.GetById(id);
            if (usuario == null) return NotFound();

            return CustomResponse(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Usuario usuario)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _usuarioService.Add(usuario);
            return CustomResponse(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _usuarioService.Update(usuario);
            return CustomResponse(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var usuario = await _usuarioService.GetById(id);
            if (usuario == null) return NotFound();

            await _usuarioService.Delete(id);
            return CustomResponse();
        }
    }
}

using Restaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.API.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(Guid? id);
        Task Add(Usuario usuario);
        Task Update(Usuario usuario);
        Task Delete(Guid id);
    }
}

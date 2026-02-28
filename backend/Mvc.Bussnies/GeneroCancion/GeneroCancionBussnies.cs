using DtoModel.GeneroCancion;
using Mvc.Repository.GeneroCancionRepo.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.GeneroCancion
{
    public class GeneroCancionBussnies : IGeneroCancionBussnies
    {
        private readonly IGeneroCancionRepository _repo;

        public GeneroCancionBussnies(IGeneroCancionRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<GeneroCancionDto>> GetAll()
        {
            List<GeneroCancionDto> lista = await _repo.GetAll();
            return lista;
        }

        public async Task<GeneroCancionDto?> GetById(int id)
        {
            GeneroCancionDto? genero = await _repo.GetById(id);
            return genero;
        }
    }
}
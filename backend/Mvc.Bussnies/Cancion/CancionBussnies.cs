using DtoModel.Cancion;
using Mvc.Repository.CancionRepo.Contratos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mvc.Bussnies.Cancion
{
    public class CancionBussnies : ICancionBussnies
    {
        private readonly ICancionRepository _cancionRepository;

        public CancionBussnies(ICancionRepository cancionRepository)
        {
            _cancionRepository = cancionRepository;
        }

        public async Task<CancionDto> Create(CancionDto request)
        {
            CancionDto result = await _cancionRepository.Create(request);
            return result;
        }

        public async Task<List<CancionDto>> GetAll()
        {
            List<CancionDto> lista = await _cancionRepository.GetAll();
            return lista;
        }

        public async Task<CancionDto?> GetById(int id)
        {
            CancionDto? cancion = await _cancionRepository.GetById(id);
            return cancion;
        }

        public async Task<CancionDto?> Update(CancionDto request)
        {
            CancionDto? cancionDb = await _cancionRepository.GetById(request.Id);

            if (cancionDb == null)
            {
                throw new Exception("Canción a actualizar no existe");
            }

            CancionDto result = await _cancionRepository.Update(request);
            return result;
        }

        public async Task Delete(int id)
        {
            await _cancionRepository.Delete(id);
        }
    }
}

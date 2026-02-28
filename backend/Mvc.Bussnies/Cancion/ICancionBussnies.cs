using DtoModel.Cancion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mvc.Bussnies.Cancion
{
    public interface ICancionBussnies
    {
        Task<List<CancionDto>> GetAll();
        Task<CancionDto?> GetById(int id);
        Task<CancionDto> Create(CancionDto request);
        Task<CancionDto?> Update(CancionDto request);
        Task Delete(int id);
    }
}

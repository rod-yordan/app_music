using DtoModel.GeneroCancion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.GeneroCancion
{
    public interface IGeneroCancionBussnies
    {
        Task<List<GeneroCancionDto>> GetAll();
        Task<GeneroCancionDto?> GetById(int id);
    }
}

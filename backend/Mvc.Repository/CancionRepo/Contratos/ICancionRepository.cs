using DtoModel.Cancion;
using Mvc.Repository.General.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.CancionRepo.Contratos
{
    public interface ICancionRepository: ICrudRepository<CancionDto>, IDisposable
    {
    }
}

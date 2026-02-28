using DtoModel.GeneroCancion;
using Mvc.Repository.General.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.GeneroCancionRepo.Contratos
{
    public interface IGeneroCancionRepository: ICrudRepository<GeneroCancionDto>, IDisposable
    {
    }
}

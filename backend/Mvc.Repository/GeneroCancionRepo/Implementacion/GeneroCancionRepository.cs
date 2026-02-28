using DbModel.demoDb;
using DtoModel.GeneroCancion;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.GeneroCancionRepo.Contratos;
using Mvc.Repository.GeneroCancionRepo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.GeneroCancionRepo.Implementacion
{
    public class GeneroCancionRepository : IGeneroCancionRepository
    {
        private readonly _demoContext _db;

        public GeneroCancionRepository(_demoContext db)
        {
            _db = db;
        }

        public Task<GeneroCancionDto> Create(GeneroCancionDto request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<GeneroCancionDto>> GetAll()
        {
            List<GeneroCancionDto> res = new List<GeneroCancionDto>();
            List<GeneroCancion> data = await _db.GeneroCancion.ToListAsync();
            res = GeneroCancionMapping.ToDtoList(data);
            return res;
        }

        public async Task<GeneroCancionDto?> GetById(int id)
        {
            GeneroCancion? genero = await _db.GeneroCancion.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (genero == null) { return null; }
            GeneroCancionDto res = GeneroCancionMapping.ToDto(genero);
            return res;
        }

        public Task<GeneroCancionDto> Update(GeneroCancionDto request)
        {
            throw new NotImplementedException();
        }
    }
}

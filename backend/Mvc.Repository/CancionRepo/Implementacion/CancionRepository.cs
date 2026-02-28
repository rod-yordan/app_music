using DbModel.demoDb;
using DtoModel.Cancion;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.CancionRepo.Contratos;
using Mvc.Repository.CancionRepo.Mapping;

namespace Mvc.Repository.CancionRepo.Implementacion
{
    public class CancionRepository : ICancionRepository
    {
        private readonly _demoContext _db;

        public CancionRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<CancionDto> Create(CancionDto request)
        {
            Cancion cancion = CancionMapping.ToEntity(request);
            await _db.Cancion.AddAsync(cancion);
            await _db.SaveChangesAsync();

            request = CancionMapping.ToDto(cancion);
            return request;
        }

        public async Task Delete(int id)
        {
            await _db.Cancion.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<CancionDto>> GetAll()
        {
            List<CancionDto> res = new List<CancionDto>();
            List<Cancion> data = await _db.Cancion.ToListAsync();
            res = CancionMapping.ToDtoList(data);
            return res;
        }

        public async Task<CancionDto?> GetById(int id)
        {
            Cancion? cancion = await _db.Cancion.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (cancion == null) { return null; }
            CancionDto res = CancionMapping.ToDto(cancion);
            return res;
        }

        public async Task<CancionDto> Update(CancionDto request)
        {
            var cancion = await _db.Cancion.FindAsync(request.Id);
            try
            {
                if (cancion == null)
                {
                    throw new Exception("Canción no encontrada");
                }

                cancion.Nombre = request.Nombre;
                cancion.Artista = request.Artista;
                cancion.FechaLanzamiento = request.FechaLanzamiento;
                cancion.IdGeneroCancion = request.IdGeneroCancion;

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return CancionMapping.ToDto(cancion);
        }
    }
}

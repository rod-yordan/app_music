using DbModel.demoDb;
using DtoModel.Cancion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.CancionRepo.Mapping
{
    public static class CancionMapping
    {
        /// <summary>
        /// Cambia el objeto Cancion a CancionDto para ser utilizado en la capa de servicio o presentación
        /// </summary>
        /// <param name="cancion"></param>
        /// <returns></returns>
        public static CancionDto ToDto(this Cancion cancion)
        {
            return new CancionDto
            {
                Id = cancion.Id,
                Nombre = cancion.Nombre,
                Artista = cancion.Artista,
                FechaLanzamiento = cancion.FechaLanzamiento,
                IdGeneroCancion = cancion.IdGeneroCancion
            };
        }

        /// <summary>
        /// Cambia el objeto CancionDto a Cancion para ser utilizado en la capa de acceso a datos
        /// </summary>
        /// <param name="cancionDto"></param>
        /// <returns></returns>
        public static Cancion ToEntity(this CancionDto cancionDto)
        {
            return new Cancion
            {
                Id = cancionDto.Id,
                Nombre = cancionDto.Nombre,
                Artista = cancionDto.Artista,
                FechaLanzamiento = cancionDto.FechaLanzamiento,
                IdGeneroCancion = cancionDto.IdGeneroCancion
            };
        }

        /// <summary>
        /// Cambia una lista de Cancion a lista de CancionDto
        /// </summary>
        /// <param name="canciones"></param>
        /// <returns></returns>
        public static List<CancionDto> ToDtoList(this List<Cancion> canciones)
        {
            return canciones.Select(c => c.ToDto()).ToList();
        }

        /// <summary>
        /// Cambia una lista de CancionDto a lista de Cancion
        /// </summary>
        /// <param name="cancionDtos"></param>
        /// <returns></returns>
        public static List<Cancion> ToEntityList(this List<CancionDto> cancionDtos)
        {
            return cancionDtos.Select(c => c.ToEntity()).ToList();
        }
    }
}

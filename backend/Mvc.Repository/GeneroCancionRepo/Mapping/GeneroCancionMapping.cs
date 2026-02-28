using DbModel.demoDb;
using DtoModel.GeneroCancion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.GeneroCancionRepo.Mapping
{
    public static class GeneroCancionMapping
    {
        /// <summary>
        /// Cambia el objeto GeneroCancion a GeneroCancionDto para ser utilizado en la capa de servicio o presentación
        /// </summary>
        /// <param name="genero"></param>
        /// <returns></returns>
        public static GeneroCancionDto ToDto(this GeneroCancion genero)
        {
            return new GeneroCancionDto
            {
                Id = genero.Id,
                Nombre = genero.Nombre
            };
        }

        /// <summary>
        /// Cambia el objeto GeneroCancionDto a GeneroCancion para ser utilizado en la capa de acceso a datos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GeneroCancion ToEntity(this GeneroCancionDto request)
        {
            return new GeneroCancion
            {
                Id = request.Id,
                Nombre = request.Nombre
            };
        }

        /// <summary>
        /// Cambia una lista de GeneroCancion a lista de GeneroCancionDto
        /// </summary>
        /// <param name="generos"></param>
        /// <returns></returns>
        public static List<GeneroCancionDto> ToDtoList(this List<GeneroCancion> generos)
        {
            return generos.Select(g => g.ToDto()).ToList();
        }

        /// <summary>
        /// Cambia una lista de GeneroCancionDto a lista de GeneroCancion
        /// </summary>
        /// <param name="generoDtos"></param>
        /// <returns></returns>
        public static List<GeneroCancion> ToEntityList(this List<GeneroCancionDto> generoDtos)
        {
            return generoDtos.Select(g => g.ToEntity()).ToList();
        }
    }
}

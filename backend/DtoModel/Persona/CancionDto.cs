using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModel.Cancion
{
    public class CancionDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Artista { get; set; }
        public DateTime? FechaLanzamiento { get; set; }
        public int IdGeneroCancion { get; set; }
    }
}

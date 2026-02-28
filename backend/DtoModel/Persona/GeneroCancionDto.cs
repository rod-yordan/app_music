using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModel.GeneroCancion
{
    public class GeneroCancionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}

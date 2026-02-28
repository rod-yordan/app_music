using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("genero_cancion")]
public partial class GeneroCancion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdGeneroCancionNavigation")]
    public virtual ICollection<Cancion> Cancion { get; set; } = new List<Cancion>();
}

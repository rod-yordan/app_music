using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.demoDb;

[Table("cancion")]
[Index("IdGeneroCancion", Name = "fk_cancion_genero")]
public partial class Cancion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombre")]
    [StringLength(255)]
    public string? Nombre { get; set; }

    [Column("artista")]
    [StringLength(255)]
    public string? Artista { get; set; }

    [Column("fecha_lanzamiento", TypeName = "date")]
    public DateTime? FechaLanzamiento { get; set; }

    [Column("id_genero_cancion")]
    public int IdGeneroCancion { get; set; }

    [ForeignKey("IdGeneroCancion")]
    [InverseProperty("Cancion")]
    public virtual GeneroCancion IdGeneroCancionNavigation { get; set; } = null!;
}

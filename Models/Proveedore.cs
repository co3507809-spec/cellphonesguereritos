using System;
using System.Collections.Generic;

namespace cellphonesguereritos.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public string? NombreContacto { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

using System;
using System.Collections.Generic;

namespace cellphonesguereritos.Models;

public partial class MetodosPago
{
    public int IdMetodoPago { get; set; }

    public string NombreMetodo { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}

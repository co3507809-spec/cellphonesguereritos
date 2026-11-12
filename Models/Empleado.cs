using System;
using System.Collections.Generic;

namespace cellphonesguereritos.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string DniCurp { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public int IdCargo { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}

using System;
using System.Collections.Generic;

namespace cellphonesguereritos.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string NombreCargo { get; set; } = null!;

    public decimal SalarioBase { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

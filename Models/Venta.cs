using System;
using System.Collections.Generic;

namespace cellphonesguereritos.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public decimal Total { get; set; }

    public int IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public int IdMetodoPago { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual MetodosPago IdMetodoPagoNavigation { get; set; } = null!;
}

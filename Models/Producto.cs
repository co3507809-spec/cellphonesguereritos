using System;
using System.Collections.Generic;

namespace cellphonesguereritos.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public decimal Precio { get; set; }

    public decimal Costo { get; set; }

    public int Stock { get; set; }

    public int? AlmacenamientoGb { get; set; }

    public int? RamGb { get; set; }

    public int IdMarca { get; set; }

    public int IdCategoria { get; set; }

    public int IdProveedor { get; set; }

    public string? UrlImagen { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedorNavigation { get; set; } = null!;
}

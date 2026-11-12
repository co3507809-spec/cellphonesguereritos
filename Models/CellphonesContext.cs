using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cellphonesguereritos.Models;

public partial class CellphonesContext : DbContext
{
    public CellphonesContext(DbContextOptions<CellphonesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__Cargos__6C9856258AF38559");

            entity.HasIndex(e => e.NombreCargo, "UQ__Cargos__B281D7B50A150A85").IsUnique();

            entity.Property(e => e.NombreCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SalarioBase).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A1075CF89DB");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF3487D72D").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D594664259E6C687");

            entity.HasIndex(e => e.Email, "UQ__Clientes__A9D105340D299C5A").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleV__E43646A54BD633C3");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", false)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__IdPro__6D0D32F4");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVe__IdVen__6C190EBB");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E15ED284F");

            entity.HasIndex(e => e.DniCurp, "UQ__Empleado__E35554DD8FB906C8").IsUnique();

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DniCurp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DNI_CURP");
            entity.Property(e => e.FechaIngreso).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleados__IdCar__59FA5E80");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marcas__4076A887D22FF1E2");

            entity.HasIndex(e => e.Nombre, "UQ__Marcas__75E3EFCF316BBAD9").IsUnique();

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodosP__6F49A9BEB9B99AF4");

            entity.ToTable("MetodosPago");

            entity.HasIndex(e => e.NombreMetodo, "UQ__MetodosP__EDA0070F8F4C67F2").IsUnique();

            entity.Property(e => e.NombreMetodo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210971881AA");

            entity.Property(e => e.AlmacenamientoGb).HasColumnName("AlmacenamientoGB");
            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RamGb).HasColumnName("RamGB");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IdCat__619B8048");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IdMar__60A75C0F");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__IdPro__628FA481");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AF125C2801");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__BC1240BD283164B5");

            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__IdClient__6754599E");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__IdEmplea__68487DD7");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__IdMetodo__693CA210");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuarios");

            entity.HasIndex(e => e.Correo).IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
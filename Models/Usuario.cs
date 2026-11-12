using System.ComponentModel.DataAnnotations;

namespace cellphonesguereritos.Models;

public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Rol { get; set; } = null!;
}
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cellphonesguereritos.Models;


namespace cellphonesguereritos.Controllers;

public class HomeController : Controller
{
    private readonly CellphonesContext _context;

    public HomeController(CellphonesContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

   
    public async Task<IActionResult> Catalogo(string? marca = null, int? idCategoria = null)
    {
        var query = _context.Productos
            .Include(p => p.IdMarcaNavigation)
            .Include(p => p.IdCategoriaNavigation)
            .AsQueryable();

        if (!string.IsNullOrEmpty(marca))
        {
            query = query.Where(p => p.IdMarcaNavigation.Nombre == marca);
        }
        
        if (idCategoria.HasValue)
        {
            query = query.Where(p => p.IdCategoria == idCategoria.Value);
        }

        return View(await query.ToListAsync());
    }

    public async Task<IActionResult> Detalles(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.IdMarcaNavigation)
            .Include(p => p.IdCategoriaNavigation)
            .FirstOrDefaultAsync(p => p.IdProducto == id);

        if (producto == null)
        {
            return NotFound();
        }

        return View(producto);
    }

    // Muestra la vista del Login
    public IActionResult Login()
    {
        // Si el usuario ya inició sesión, lo mandamos a su cuenta
        if (User.Identity!.IsAuthenticated) return RedirectToAction("MiCuenta");
        return View();
    }

    // Procesa los datos cuando el usuario da clic en "Iniciar Sesion"
    [HttpPost]
    public async Task<IActionResult> Login(string correo, string password)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == correo && u.Password == password);

        if (usuario != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index");
        }

        ViewBag.Error = "Correo o contraseña incorrectos.";
        return View();
    }

    // Muestra el formulario de registro
    public IActionResult Registrar()
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("MiCuenta");
        return View();
    }

    // Crea una cuenta nueva de cliente
    [HttpPost]
    public async Task<IActionResult> Registrar(string nombre, string correo, string password, string confirmar)
    {
        nombre = (nombre ?? "").Trim();
        correo = (correo ?? "").Trim().ToLower();

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrEmpty(password))
        {
            ViewBag.Error = "Llena todos los campos.";
            return View();
        }

        if (password.Length < 6)
        {
            ViewBag.Error = "La contraseña debe tener al menos 6 caracteres.";
            return View();
        }

        if (password != confirmar)
        {
            ViewBag.Error = "Las contraseñas no coinciden.";
            return View();
        }

        if (nombre.Length > 100 || correo.Length > 100 || password.Length > 100)
        {
            ViewBag.Error = "Alguno de los campos es demasiado largo (máximo 100 caracteres).";
            return View();
        }

        if (await _context.Usuarios.AnyAsync(u => u.Correo == correo))
        {
            ViewBag.Error = "Ya existe una cuenta con ese correo.";
            return View();
        }

        // Todo registro público es Cliente; el rol Admin solo se asigna desde SQL
        var usuario = new Usuario
        {
            Nombre = nombre,
            Correo = correo,
            Password = password,
            Rol = "Cliente"
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Inicia sesión automáticamente
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return RedirectToAction("MiCuenta");
    }

    // Cierra la sesión
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }

    // Protegemos Mi Cuenta para que solo entren los que iniciaron sesión
    [Authorize]
    public IActionResult MiCuenta()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
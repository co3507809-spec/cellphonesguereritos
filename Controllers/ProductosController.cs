using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cellphonesguereritos.Models;

namespace cellphonesguereritos.Controllers;

[Authorize(Roles = "Admin")]
public class ProductosController : Controller
{
    private readonly CellphonesContext _context;

    public ProductosController(CellphonesContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Extrae la lista de celulares desde la base de datos
        var celulares = await _context.Productos.ToListAsync();
        return View(celulares);
    }
}
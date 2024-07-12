using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Valhalla_Music.Entities;
using Valhalla_Music.Models;

namespace Valhalla_Music.Controllers
{
    public class ProductoController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ApplicationDbContext context, ILogger<ProductoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult ProductosList()
        {
            List<ProductoModel> list = _context.Productos.Select(p => new ProductoModel()
            {
                Id=p.Id,
                Name= p.Name,
                Precio=p.Precio,
                Stock=p.Stock,
           
            }).ToList();
            return View(list);

        }

        public IActionResult ProductosAdd()
        {
            return View(new ProductoModel());
        }

        [HttpPost]
        public IActionResult ProductosAdd(ProductoModel producto)
        {
            var ProductoEntity=new Producto();
            ProductoEntity.Id=new Guid();
            ProductoEntity.Name=producto.Name;
            ProductoEntity.Precio=producto.Precio;
            ProductoEntity.Stock=producto.Stock;

            //Esto es para guardar en la base de datos.
            _context.Productos.Add(ProductoEntity);
            _context.SaveChanges();
            return View();

        }

        public async Task<IActionResult> ProductosEdit(Guid Id)
        {
            Producto? product = await this._context.Productos
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                _logger.LogError("Producto no encontrado con ID: {Id}", Id); // Log with specific ID
                return RedirectToAction("ProductList", "Producto");
            }

            ProductoModel model = new ProductoModel()
            {
                Id = product.Id,
                Name = product.Name,
                Precio = product.Precio,
                Stock = product.Stock
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductosEdit(ProductoModel productoModel)
        {
            if (!ModelState.IsValid)
            {
                // Add specific validation error messages to ModelState
                return View(productoModel);
            }

            Producto productoEntity = await this._context.Productos
                .Where(p => p.Id == productoModel.Id)
                .FirstAsync();

            productoEntity.Name = productoModel.Name;
            productoEntity.Precio = productoModel.Precio;
            productoEntity.Stock = productoModel.Stock;

            this._context.Productos.Update(productoEntity);
            await this._context.SaveChangesAsync();

            return RedirectToAction("ProductosList", "Producto");
        }

        public async Task<IActionResult> ProductosDeleted(Guid Id)
        {
            Producto? producto = await this._context.Productos
            .Where(p => p.Id == Id).FirstOrDefaultAsync();
            
            if (producto == null)
            {
                _logger.LogError("No se encontro el producto");
                return RedirectToAction("ProductList","Product");
            }

            ProductoModel model = new ProductoModel();
            model.Id = producto.Id;
            model.Name = producto.Name;
            model.Stock = producto.Stock;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductosDeleted(ProductoModel producto)
        {
            bool exits = await this._context.Productos.AnyAsync(p => p.Id == producto.Id);
            if (!exits)
            {
                _logger.LogError("No se encontro el producto");
                return View(producto);
            }

            Producto productEntity = await this._context.Productos
            .Where(p => p.Id == producto.Id).FirstAsync();

            this._context.Productos.Remove(productEntity);
            await this._context.SaveChangesAsync();
            
            return RedirectToAction("ProductosList","Producto");
        }


        
















    }
}
using e_commercee.Data;
using e_commercee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commercee.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddtoCart(int productId , int qty=1)
        {
            var product = await _context.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            if(product == null)
            {
                return BadRequest();
            }
            var Cart = new Cart { ProductId = productId, Qty = qty };
            return View(Cart);
        }
    }
}

using e_commercee.Data;
using e_commercee.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commercee.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;
        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var Cart = await _context.Carts
                .Include(x=>x.Product)
                .Where(x=>x.UserId == currentUser.Id)
                .ToListAsync();
            int totalCost = 0;
            foreach (var CartItem in Cart)
            {
                totalCost += CartItem.Product.Price * CartItem.Qty;

            }
            ViewBag.TotalCost = totalCost;

            return View(Cart);
        }

        public async Task<IActionResult> AddtoCart(int productId , int qty=1)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var product = await _context.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            if(product == null)
            {
                return BadRequest();
            }
            var Cart = new Cart { ProductId = productId, Qty = qty,UserId = currentUser.Id };
            _context.Add(Cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Remove(int id)
        {
            var cartItem = await _context.Carts.FindAsync(id);
            if(cartItem == null)
            {
                return BadRequest();
            }
            _context.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

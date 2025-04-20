using e_commercee.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commercee.Controllers
{
    public class OrdersController : Controller
    {
        public readonly ApplicationDbContext _Context;

        public OrdersController(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _Context.Orders.ToListAsync();

            return View(orders);
        }
        public async Task<IActionResult> Details(int id)
        {
            var order = await _Context.Orders
               
               .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
                .Include(x=>x.Address)
                .FirstOrDefaultAsync(x=>x.Id == id);

            return View(order);
        }
    }
}

using e_commercee.Data;
using e_commercee.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commercee.Controllers
{
    public class CheckOutController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<IdentityUser> _userManager;

        public CheckOutController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(HttpContext.User);

            var addresses = await _context.Address
                .Include(x => x.User)
                 .Where(x=>x.UserId == currentuser.Id)
                .ToListAsync();

            ViewBag.Addresses = addresses;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Address address)
        {
            if (ModelState.IsValid)
            {
                var currentuser = await _userManager.GetUserAsync(HttpContext.User);
                address.UserId = currentuser.Id;
                _context.Address.Add(address);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }
            return View(address);

        }
    }
}

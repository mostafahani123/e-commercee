using e_commercee.Data;
using e_commercee.Models;
using e_commercee.Viewmodels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commercee.ViewCommponent
{
    [ViewComponent(Name = "Navigation")]
    public class Navigation : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

      
        public Navigation(ApplicationDbContext Context , UserManager<ApplicationUser> usermanager)
        {
            _context = Context;
            _usermanager = usermanager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentuser = await _usermanager.GetUserAsync(HttpContext.User);
            if(currentuser == null)
            {
                var model1 = new NavigationViewModel
                {
                    
                };
                return View("Index", model1);
            }
            var cart = await _context.Carts.Where(x => x.UserId == currentuser.Id).ToListAsync();
            var model = new NavigationViewModel
            {
                Cart = cart
            };

            return View("Index",model);
        }
    }
}

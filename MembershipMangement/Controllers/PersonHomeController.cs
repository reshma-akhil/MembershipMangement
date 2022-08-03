using MembershipMangement.Data;
using Microsoft.AspNetCore.Mvc;

namespace MembershipMangement.Controllers
{
    public class PersonHomeController : Controller
    {
        private readonly PersonContext _context;

        public PersonHomeController(PersonContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Person);
        }
    }
}

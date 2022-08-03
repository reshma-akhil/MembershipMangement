using MembershipMangement.Data;
using MembershipMangement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MembershipMangement.Controllers
{

    public class MembershipController : Controller
    {
        private readonly MembershipContext _context;

        public MembershipController(MembershipContext context)
        {
            _context = context;
        }
        public IActionResult Index() //Membership
        {
            List<MembershipModel> model = (from m in _context.Membership
            //var model = (from m in _context.Membership
                                           join p in _context.Person
                                               on m.PersonId equals p.Id
                                           select new MembershipModel
                                           {
                                               PersonId = p.Id,
                                               //MembershipId = m.Id,
                                               FirstName = p.FirstName,
                                               SurName = p.SurName,
                                               EmailId = p.EmailId,
                                               Number = m.Number,
                                               Type = m.Type,
                                               Balance = m.Balance
                                           }).ToList<MembershipModel>();
            return View(model);
        }

        [HttpGet]
       // [Route("Member/CreateMembership/{id:int}")]
        public IActionResult CreateMembership(int id, MembershipType type)
        {
            Membership membership;
            if (id == 0)
            {
                membership = new Membership();
            }
            else
            {
                membership = _context.Membership.First(m => m.PersonId == id && m.Type == type);
            }
            List<SelectListItem> members = (from p in _context.Person select new SelectListItem { 
                Value = p.Id.ToString(),
                Text = $"{p.FirstName} {p.SurName}"
            }).ToList();
            ViewBag.Members = members;
            //ViewBag.Members = members;
            return View(membership);
        }
        [HttpPost]
        public IActionResult CreateMembership(Membership membership)
        {
            if (ModelState.IsValid)
            {
                _context.Membership.Add(membership);
                _context.SaveChanges();
            }
            return View(membership);
        }
    }
}

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
            /*
            List<MembershipModel> model = (from m in _context.Membership
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
            */
            return View(_context.Person);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewBag.Memberships = _context.Membership.Where(m => m.PersonId == id).ToList();
            return View(_context.Person.First(p => p.Id == id));
        }

        #region Membership

        [HttpGet]
        public IActionResult CreateMembership(int id, MembershipType type)
        {
            Membership membership = new Membership() { PersonId = id, Type = type };
            List<SelectListItem> members = (from p in _context.Person select new SelectListItem { 
                Value = p.Id.ToString(),
                Text = $"{p.FirstName} {p.SurName}"
            }).ToList();
            ViewBag.Members = members;
            return View(membership);
        }

        [HttpPost]
        public IActionResult CreateMembership(Membership membership)
        {
            if (ModelState.IsValid)
            {
                _context.Membership.Add(membership);
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction("Index","Membership");
                }
                catch(Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
                {
                    _context.Membership.Remove(membership);
                    ViewBag.DbError = "Unable to update data";
                }
            }
            List<SelectListItem> members = (from p in _context.Person
                                            select new SelectListItem
                                            {
                                                Value = p.Id.ToString(),
                                                Text = $"{p.FirstName} {p.SurName}"
                                            }).ToList();
            ViewBag.Members = members;
            return View(membership);
        }

        [HttpGet]
        public IActionResult EditMembership(int id, MembershipType type)
        {
            Membership membership = _context.Membership.First(m => m.PersonId == id && m.Type == type);
            
            List<SelectListItem> members = (from p in _context.Person
                                            select new SelectListItem
                                            {
                                                Value = p.Id.ToString(),
                                                Text = $"{p.FirstName} {p.SurName}"
                                            }).ToList();
            ViewBag.Members = members;
            //ViewBag.Members = members;
            return View(membership);
        }

        [HttpPost]
        public IActionResult EditMembership(Membership membership)
        {
            if (ModelState.IsValid)
            {
                var actual = _context.Membership.First(m => m.PersonId == membership.PersonId && m.Type == membership.Type);
                actual.Number = membership.Number;
                actual.Balance = membership.Balance;
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Membership");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
                {
                    ViewBag.DbError = "Unable to update data";
                }
            }
            List<SelectListItem> members = (from p in _context.Person
                                            select new SelectListItem
                                            {
                                                Value = p.Id.ToString(),
                                                Text = $"{p.FirstName} {p.SurName}"
                                            }).ToList();
            ViewBag.Members = members;
            return View(membership);
        }

        #endregion

        #region Member
        [HttpGet]
        public IActionResult CreateMember(int id)
        {
            Person person;
            if (id == 0)
            {
                person = new Person();
            }
            else
            {
                person = _context.Person.First(m => m.Id == id );
            }
            
            return View(person);
        }

        [HttpPost]
        public IActionResult CreateMember(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Person.Add(person);
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Membership");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
                {
                    _context.Person.Remove(person);
                    ViewBag.DbError = "Unable to update data";
                }
            }
            
            return View(person);
        }

        [HttpGet]
        public IActionResult EditMember(int id)
        {
            ViewBag.Memberships = _context.Membership.Where(m => m.PersonId == id).ToList();
            return View(_context.Person.First(m => m.Id == id));
        }

        [HttpPost]
        public IActionResult EditMember(Person person)
        {
            if (ModelState.IsValid)
            {
                var actual = _context.Person.First(m => m.Id == person.Id);
                actual.FirstName = person.FirstName;
                actual.SurName = person.SurName;
                actual.EmailId = person.EmailId;
                try
                {
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Membership");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException sqlEx)
                {
                    _context.Person.Remove(person);
                    ViewBag.DbError = "Unable to update data";
                }
            }

            return View(person);
        }
        #endregion
    }
}

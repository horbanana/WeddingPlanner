using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext context;
        
        public HomeController(MyContext mc)
        {
            context = mc;
        }

        [HttpGet("")]

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("RegCheck")]
        public IActionResult RegCheck(User user)
        {
            User CheckUser = context.Users.SingleOrDefault(u=>u.Email == user.Email);
            if(CheckUser !=null)
            {
                ModelState.AddModelError("Email", "Email Already exist!");
            }
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                context.Add(user);
                context.SaveChanges();
                HttpContext.Session.SetInt32("UID", user.UserId);
                return Redirect("Dashboard");
            }
            return View("Index");
        }

        [HttpPost("LogCheck")]
        public IActionResult LogCheck(LUser l)
        {
            if(ModelState.IsValid)
            {
                var userInDB = context.Users.FirstOrDefault(u => u.Email == l.LEmail);
                if(userInDB == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid Email/Password!");
                    return View("Index");
                }
                
                var hasher = new PasswordHasher<LUser>();
                var result = hasher.VerifyHashedPassword(l, userInDB.Password, l.LPassword);
                if(result == 0)
                {
                   ModelState.AddModelError("LEmail", "Invalid Email/Password!");
                   return View("Index");
                }
                HttpContext.Session.SetInt32("UID", userInDB.UserId);
                return Redirect("Dashboard");
            } 
            return View("Index");
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("Logout");
            }
            ViewBag.User = context.Users
            .Include(user => user.Occasions)
            .ThenInclude(oc => oc.Plan)
            .FirstOrDefault(user => user.UserId == (int)Sess);

            ViewBag.AllPlans = context.Plans.Include(plan => plan.Creator).Include(plan => plan.People).ToList();
            return View("Dashboard");
        }

        [HttpGet("Join/{UserId}/{PlanId}")]
        public IActionResult Join(int UserId, int PlanId)
        {
            Occasion Join = new Occasion();
            Join.UserId = UserId;
            Join.PlanId = PlanId;
            
            context.Add(Join);
            context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("UnJoin/{UserId}/{PlanId}")]
        public IActionResult Unjoin(int UserId, int PlanId)
        {
            var Unjoin = context.Occasions.Where(a => a.UserId == UserId).FirstOrDefault(plan => plan.PlanId == PlanId);
            context.Remove(Unjoin);
            context.SaveChanges();
            return RedirectToAction("Dashboard");

        }

        [HttpGet("Delete/{PlanId}")]
        public IActionResult Delete(int PlanId)
        {
            var Delete = context.Plans.FirstOrDefault(plan => plan.PlanId == PlanId);
            context.Remove(Delete);
            context.SaveChanges();
            return RedirectToAction("Dashboard");

        }


        [HttpGet("About/{PlanId}")]
        public IActionResult About(int PlanId)
        {
            ViewBag.PlanWithGuests = context.Plans.Include(plan => plan.People).ThenInclude(user => user.User).FirstOrDefault(plan => plan.PlanId == PlanId);
            return View("About");


        }



        [HttpGet("NewPlanPage")]
        public IActionResult NewPlanPage()
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("Logout");
            }
            ViewBag.User = context.Users
            .FirstOrDefault(user => user.UserId == (int)Sess);
            return View("NewPlanPage");
        }

        [HttpPost("New")]
        public IActionResult New(Plan p)
        {
            int? Sess = HttpContext.Session.GetInt32("UID");
            if(Sess == null)
            {
                return Redirect("Logout");
            }
            if(p.Date < DateTime.Now)
                {
                    ModelState.AddModelError("Date", "The Wedding cant be in the past!!!!!!");
                }
            if(ModelState.IsValid)
            {
                p.UserId = (int)Sess;
                context.Add(p);
                context.SaveChanges();
                return Redirect("Dashboard");
            }
            return View("NewPlanPage");
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}

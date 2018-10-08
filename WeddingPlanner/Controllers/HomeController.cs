using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Data;
using Microsoft.AspNetCore.Http;


namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                return RedirectToAction("Index", "Weddings");
            }
            
                return View("Register");
            
        }
        [HttpGet ("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        [Route("Dash")]
        public IActionResult Dash()
        {
            if(HttpContext.Session.GetInt32("UserID") == null){
                RedirectToAction("Index");
                //get userid from session
                //return index
                
            }
            List<Wedding> AllWeddings=_context.Weddings.Include(wedding => wedding.Guests).ThenInclude(guest=>guest.User).ToList();
            bool GuestLoggedIn = false;
            foreach(var wedding in AllWeddings)
            {
                foreach (var guest in wedding.Guests)
                {
                   if (guest.User.UserID == (int)TempData["UserID"] )
                        GuestLoggedIn = true;
                }
            }
            ViewBag.Weddings=AllWeddings;
            ViewBag.Guest = GuestLoggedIn;
            //session

            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User ExistingUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                if (ExistingUser != null)
                {
                    ViewBag.Message = "User with this email already exists!";
                    return View("Index", model);
                }
                User NewUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
  
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                NewUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("UserId", NewUser.UserID);
                
                return RedirectToAction("Index", "Messages");
                
            }
                return View("Index", model);
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string LogEmail, string LogPassword)
        {
            User FoundUser = _context.Users.SingleOrDefault(user => user.Email == LogEmail && user.Password == LogPassword);
            if (FoundUser == null)
            {
                ViewBag.Message = "Login failed.";
                return View("Index");
            }
        
                HttpContext.Session.SetInt32("UserId", FoundUser.UserID);
                return RedirectToAction("Index", "Weddings");
            }//add hasher

//add in new wedding 

        [HttpGet]
        [Route("Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}

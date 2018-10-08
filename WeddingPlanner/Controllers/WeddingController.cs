using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlanner.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Data;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
{
  private Context _context;

        public WeddingController(Context context)
        {
          _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
              List<Wedding> AllWeddings = _context.Weddings
                .Include(post => post.User)
                .OrderByDescending(post => post.CreatedAt)
                .Include(post => post.Date)
                .ToList();
              int? logId = HttpContext.Session.GetInt32("UserId");
              ViewBag.AllWeddings = _context.Weddings;
              ViewBag.LoggedUser = _context.Users.SingleOrDefault(user => user.UserID == logId);
              return View("Dash");
            }
            else
            {
              return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [Route("newWedding")]
        public IActionResult newWedding(Wedding wedding)
        {
          if (HttpContext.Session.GetInt32("UserId") != null)
          {
            Wedding newWedding = new Wedding{
              WedderOne = wedding.WedderOne,
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
              UserID = (int)HttpContext.Session.GetInt32("UserId")
            };
            _context.Weddings.Add(newWedding);
            _context.SaveChanges();
            ViewBag.AllWeddings = _context.Weddings
              .Include(post => post.User)
              .OrderByDescending(post => post.CreatedAt)
              .Include(post => post.Date)
              .Include(thisComment => thisComment.User)
              .ToList();
            int? logId = HttpContext.Session.GetInt32("UserId");
            ViewBag.LoggedUser = _context.Users.SingleOrDefault(user => user.UserID == logId);
            ModelState.Clear();
            return RedirectToAction("Index");
          }
          else
          {
            return RedirectToAction("Index", "Login");
          }
        }
}
}
        
  
 
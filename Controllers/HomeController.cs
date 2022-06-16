using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefNDishes.Models;

namespace ChefNDishes.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.AllChefs = _context.Chefs.Include(a => a.Dishes).ToList();
        return View();
    }

    ///////////////////DISHES/////////////////////
    [HttpGet("dishes")]
    public IActionResult Dishes()
    {   
        ViewBag.AllDishes = _context.Dishes.Include(a => a.Chef).ToList();
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View();
    }
    ///////////////ADD CHEF PAGE//////////////////
    [HttpGet("new")]
    public IActionResult AddChef()
    {

        return View();
    }
    ///////////////ADD DISH PAGE//////////////////
    [HttpGet("dishes/new")]
    public IActionResult AddDish()
    {
        ViewBag.AllChefs = _context.Chefs.ToList();
        return View();
    }
    ///////////////ADD CHEF TO DB/////////////////
    [HttpPost("add/chef")]
    public IActionResult AddChefToDB(Chef newChef)
    {
        System.Console.WriteLine(newChef.FirstName);
        System.Console.WriteLine(newChef.LastName);
        System.Console.WriteLine(newChef.DoB);
        if (ModelState.IsValid)
        {
            System.Console.WriteLine("Model is valid");
            // If age is less than 18 years old
            if ((DateTime.Now.Year - newChef.DoB.Year) < 18)
            {
                System.Console.WriteLine("Chef too young");
                ModelState.AddModelError("DoB", "All new Chefs must be at least 18 years old");
                return View("AddChef");
            }
            else
            {
                _context.Chefs.Add(newChef);
                _context.SaveChanges();

                ViewBag.AllChefs = _context.Chefs.ToList();
                return RedirectToAction("Index");
            }
        }
        System.Console.WriteLine("Model is invalid");

        return View("AddChef");
    }
    //////////////ADD DISH TO DB//////////////add
    [HttpPost("add/dish")]
    public IActionResult AddDishToDB(Dish newDish)
    {
        System.Console.WriteLine(newDish.DishName);
        System.Console.WriteLine(newDish.Calories);
        System.Console.WriteLine(newDish.Description);
        System.Console.WriteLine(newDish.ChefId);
        if (ModelState.IsValid)
        {   
            _context.Add(newDish);
            _context.SaveChanges();

            return RedirectToAction("dishes");
        } else {
            ViewBag.AllChefs = _context.Chefs.ToList();
            return View("AddDish");
        }
    }
    //////////////////////////////////////////

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

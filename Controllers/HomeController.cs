using AspCrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace AspCrudApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AspCrudAppContext context;
        private readonly IWebHostEnvironment web;

        public HomeController(AspCrudAppContext context, IWebHostEnvironment web)
        {
            this.context = context;
            this.web = web;
        }
        public IActionResult Index()
        {
            var show = context.Products.ToList();
            return View(show);
        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User signup)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(signup);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "signup failed";
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User login)
        {
            var myuser = context.Users.Where(x => x.Email == login.Email && x.Password == login.Password);
            if (myuser != null)
            {
                HttpContext.Session.SetString("mystring", login.Email);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Login failed";
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("mystring") != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Remove("mystring");
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Create()
        {
            var session = HttpContext.Session.GetString("mystring");
            if (session != null)
            {
                ViewBag.Mysession = HttpContext.Session.GetString("mystring").ToString();

            }
            else
            {
                return View("Login");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Create(image img)
        {


            if (ModelState.IsValid)
            {
                var folder = Path.Combine(web.WebRootPath, "images");
                var fileName = img.Image.FileName;
                var file = Path.Combine(folder, fileName);
                img.Image.CopyTo(new FileStream(file, FileMode.Create));

                Product item = new Product()
                {
                    Pname = img.Pname,
                    Pdescription = img.Pdescription,
                    Pquantity = img.Pquantity,
                    Pprice = img.Pprice,
                    Image = fileName


                };
                context.Products.Add(item);
                context.SaveChanges();
                return RedirectToAction("Index");


            }

            else
            {
                ViewBag.Message = "Your Record Cannot be Inserted";
            }


            return View();

        }
        public IActionResult Edit(int id)
        {
            var show = context.Products.Find(id);

            return View(show);
        }
        [HttpPost]
        public IActionResult Edit(int id, image img)
        {
            var show = context.Products.Find(id);
            if (show != null)
            {
                show.Pname = img.Pname;
                show.Pdescription = img.Pdescription;
                show.Pquantity = img.Pquantity;
                show.Pprice = img.Pprice;
                if (img.Image != null)
                {
                    var folder = Path.Combine(web.WebRootPath, "images");
                    var fileName = img.Image.FileName;
                    var file = Path.Combine(folder, fileName);
                    img.Image.CopyTo(new FileStream(file, FileMode.Create));
                    show.Image = fileName;
                    context.Products.Update(show);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var show = context.Products.Find(id);
            return View(show);
        }
        [HttpPost]
        public IActionResult Delete(int id, Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("index");


        }
        public IActionResult Details(int id)
        {
            var show = context.Products.Find(id);
            return View(show);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

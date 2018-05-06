using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KuProStore.Models;
using KuProStore.UI.ViewModel;
using KuProStore.BL.Service;
using KuProStore.BL.Service.Cacher;
using KuProStore.BL.Auth;
using System.IO;

namespace KuProStore.Controllers
{
    public class AccountController : Controller
    {

        private IUserManager userManager;        
        private IProductManager productManager;
        private ILocationManager locationManager;
        private IImageManager imageManager;
        private AuthHelper authHelper;        

        public AccountController(IUserManager userManager, IProductManager productManager, 
            ILocationManager locationManager, IImageManager imageManager)
        {
            this.userManager = userManager;
            this.productManager = productManager;
            this.locationManager = locationManager;
            this.imageManager = imageManager;
            this.authHelper = new AuthHelper(userManager);
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            if (authHelper.IsAuthenticated(HttpContext))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                User user = userManager.GetUser(login);

                if (user == null)
                {
                    ViewBag.ValidationMessage = "Неверный логин или пароль";
                    return View(login);
                }
                
                authHelper.UserSetCookie(HttpContext, user, Guid.NewGuid().ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            if (authHelper.IsAuthenticated(HttpContext))
                ViewBag.IsLogged = true;
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                if (authHelper.IsAuthenticated(HttpContext))
                    ViewBag.IsLogged = true;

                if (!userManager.CheckIfRegistered(user.ContactPhone))
                {
                    User newUser = new User
                    {
                        Balance = 0,
                        ContactPhone = user.ContactPhone,
                        Cookie = Guid.NewGuid().ToString(),
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        RegDate = DateTime.Now,
                        Password = "12345",
                        IsActive = false,
                        ContactInfo = (user.ContactInfo != null)?user.ContactInfo:user.ContactPhone
                    };
                    userManager.AddUser(newUser);
                    return RedirectToAction("Login", "Account");
                }
                ViewBag.ValidationMessage = "Пользователь с таким номером уже существует";
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            authHelper.LogOutUser(HttpContext);
            return RedirectToAction("Index","Home");
        }

        public ActionResult Cabinet(string filter, string SearchOptions, string TownName, int page = 1, int Regions = 1, bool IncludeInactive = false)
        {
            if (authHelper.IsAuthenticated(HttpContext))
            {
                ViewBag.IsLogged = true;

                HttpCookie cookie = HttpContext.Request.Cookies[Constants.NameCookie];
                User user = userManager.GetUserByCookies(cookie.Value);
                SearchFilter sf = new SearchFilter { FilterString = filter, SearchOptions = SearchOptions,
                    TownName = TownName, Page = page, Regions = Regions, UserId = user.UserId, IncludeInactive = IncludeInactive
                };
                ProductsViewModel vm = productManager.GetViewModel(sf);
                vm.User = user;

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult EditProduct(int productId = -1)
        {
            if (authHelper.IsAuthenticated(HttpContext))
            {
                ViewBag.IsLogged = true;

                if (productId == 0)
                    return new HttpNotFoundResult("Такого товара не существует");

                AddProductViewModel vm = productManager.GetAddProductViewModel(productId);
                if (vm == null)
                    return new HttpNotFoundResult("Такого товара не существует");

                vm.User = userManager.GetUserByCookies(HttpContext.Request.Cookies[Constants.NameCookie].Value);
                TempData["Images"] = vm.Images;
                //check if it's the same user whom created product
                if (vm.UserId != vm.User.UserId) 
                    return RedirectToAction("Cabinet", "Account");
                    
                return View(vm);
                
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditProduct(AddProductViewModel vm, IEnumerable<HttpPostedFileBase> images = null)
        {
            ViewBag.IsLogged = true;
            vm.Images = TempData["Images"] as Lazy<List<Image>>;
            TempData["Images"] = vm.Images; //refresh tempdata value

            if (ModelState.IsValid)
            {                
                vm.Regions = locationManager.GetRegions().Skip(1).ToList();
                vm.User = userManager.GetUserByCookies(HttpContext.Request.Cookies[Constants.NameCookie].Value);                
                productManager.UpdateProduct(vm, images);
                //TempData["SuccessEdit"] = true;
                return RedirectToAction("Cabinet", "Account");
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            Product product = productManager.GetProductById(productId);
            if (product != null)
            {
                if (product.UserId == userManager.GetUserByCookies(HttpContext.Request.Cookies[Constants.NameCookie].Value).UserId)
                {
                    productManager.DeleteProduct(productId);
                    return Json(new {result="success"});
                }

            }
            return View("Cabinet", "Account");
        }

        [HttpPost]
        public ActionResult DeleteImage(int imageId)
        {
            imageManager.DeleteImage(imageId);
            return Json(new { result = "success" });
        }

        [HttpGet]
        public ActionResult EditUser(int userId = -1)
        {
            if (authHelper.IsAuthenticated(HttpContext))
            {
                User user = userManager.GetUserByCookies(HttpContext.Request.Cookies[Constants.NameCookie].Value);
                if (user.UserId == userId)
                {
                    ViewBag.IsLogged = true;
                    return View(user);
                }
            }
            return RedirectToAction("Cabinet", "Account");
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
                        
            if (ModelState.IsValid)
            {
                userManager.UpdateUser(user);
                //ViewBag.Success = "Изменения сохранены";
                //need to refresh cookies
                return RedirectToAction("Cabinet", "Account");
            }
            ViewBag.IsLogged = true;
            return View(user);
        }
        
    }
}
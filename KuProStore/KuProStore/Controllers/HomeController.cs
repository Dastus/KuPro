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
    public class HomeController : Controller
    {
        
        private IUserManager userManager;
        private IImageManager imageManager;                
        private IProductManager productManager;
        private ILocationManager locationManager;
        private AuthHelper authHelper;                

        public HomeController(IUserManager userManager, IImageManager imageManager, 
            IProductManager productManager, ILocationManager locationManager)
        { 
            this.userManager = userManager;
            this.imageManager = imageManager;
            this.productManager = productManager;
            this.locationManager = locationManager;
            this.authHelper = new AuthHelper(userManager);
        }

        // GET: Home        
        public ActionResult Index(string filter, string SearchOptions, string TownName, int page = 1, int Regions = 1)
        {
            ViewBag.Test = Regions;
            ViewBag.Test2 = TownName;
            ViewBag.Test3 = SearchOptions;

            SearchFilter sf = new SearchFilter { FilterString = filter, SearchOptions = SearchOptions, TownName = TownName, Page = page, Regions = Regions};
            ProductsViewModel vm = productManager.GetViewModel(sf);
      
            HttpCookie cookie = HttpContext.Request.Cookies[Constants.NameCookie];
            if (cookie != null)
            {
                User user = userManager.GetUserByCookies(cookie.Value);
                if (user != null)
                {
                    vm.User = user;
                    ViewBag.IsLogged = true;
                }
            }   

            return View(vm);
        }

        public ActionResult AutoCompleteTownSearch(string town, int region)
        {
            var towns = locationManager.GetTownsByRegion(region)
                .Where(e => e.TownName.ToLower().Contains(town.ToLower()))
                .Select(e => e.TownName);

            return Json(towns, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddProduct(AddProductViewModel productVM)
        {
            if (authHelper.IsAuthenticated(HttpContext))
            {
                ViewBag.IsLogged = true;
                ViewBag.FirstAttempt = true;
                productVM.Regions = locationManager.GetRegions().Skip(1).ToList();
                return View(productVM);
            }
            TempData["ErrorMessage"] = true;
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductViewModel productVM, IEnumerable<HttpPostedFileBase> images = null)
        {
            productVM.Regions = locationManager.GetRegions().Skip(1).ToList();
            ViewBag.IsLogged = true;

            if (ModelState.IsValid)
            {  
                if (images != null)
                {
                    foreach (var file in images)
                    {
                        if (file != null)
                        {
                            if (file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".png") || file.FileName.EndsWith(".img"))
                            {
                                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                                file.SaveAs(Server.MapPath("~/Images/" + fileName));
                                productVM.Images.Value.Add(new Image { ImageUrl = "/Images/" + fileName });
                            }
                        }
                    }
                }
                HttpCookie cookie = HttpContext.Request.Cookies[Constants.NameCookie];
                productVM.User = userManager.GetUserByCookies(cookie.Value);
                productManager.AddProduct(productVM);

                return RedirectToAction("Index","Home");
            }

            return View(productVM);
        }

        [HttpGet]
        public ActionResult ViewProduct(int productId = -1)
        {
            Product product = productManager.GetProductById(productId);
            if (product == null)
                return new HttpNotFoundResult("Продукт не найден.");

            if (authHelper.IsAuthenticated(HttpContext))
            {
                ViewBag.IsLogged = true;
            }
            ViewBag.RegionName = locationManager.GetRegions().
                Where(e =>e.ID == product.Town.RegionId)
                .FirstOrDefault().RegionName;

            return View(product);
        }

    }
}

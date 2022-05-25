using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminPanelWeb.Controllers
{
    public class AdressController : Controller
    {
        public readonly IAdressOperation _adressOperation;
        public AdressController(IAdressOperation adressOperation)
        {
            _adressOperation = adressOperation;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return Redirect("~/Home/Login");
            }
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                var list = _adressOperation.GetAllAdress();
                return View(list);
            }
            return Redirect("~/Home/Login");
        }
        [HttpGet]
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                Adress model = new Adress();
                return View(model);
            }

            return Redirect("~/Home/Login");

        }
        [HttpPost]
        public IActionResult Create(Adress model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("CurrentUser") != null)
                {
                    model = _adressOperation.CreateAdress(model);
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }
        [HttpGet("Adress/Update/{id:int}")]
        public IActionResult Update(int id)
        {
            Adress model = new Adress();
            var adress = _adressOperation.GetAllAdress().Where(x => x.Id == id).FirstOrDefault();
            return View(adress);
        }
        [HttpPost]
        public IActionResult Update(Adress model)
        {
            if (ModelState.IsValid)
            {
                model = _adressOperation.UpdateAdress(model);
                if (model != null && model.Id > 0)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }
    }
}
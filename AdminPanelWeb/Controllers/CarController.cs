using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanelWeb.Controllers
{
    public class CarController : Controller
    {
        public readonly ICarOperation _carOperation;
        public CarController(ICarOperation carOperation)
        {
            _carOperation = carOperation;
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
                var list = _carOperation.GetAllCars();
                return View(list);
            }
            return Redirect("~/Home/Login");
        }
        [HttpGet]
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                Cars model = new Cars();
                return View(model);
            }

            return Redirect("~/Home/Login");

        }
        [HttpPost]
        public IActionResult Create(Cars model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("CurrentUser") != null)
                {
                    model = _carOperation.CreateCars(model);
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }
        [HttpGet("Car/Update/{id:int}")]
        public IActionResult Update(int id)
        {
            Cars model = new Cars();
            var cars = _carOperation.GetAllCars().Where(x => x.Id == id).FirstOrDefault();
            return View(cars);
        }
        [HttpPost]
        public IActionResult Update(Cars model)
        {
            if (ModelState.IsValid)
            {
                model = _carOperation.UpdateCars(model);
                if (model != null && model.Id > 0)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

    }
}

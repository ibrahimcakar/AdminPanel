using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdminPanelWeb.Controllers
{
    public class ContactController : Controller
    {
        public readonly IContactOperation _contactOperation;
        public ContactController(IContactOperation contactOperation)
        {
            _contactOperation = contactOperation;
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
                var list = _contactOperation.GetAllContact();
                return View(list);
            }
            return Redirect("~/Home/Login");
        }
        [HttpGet]
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                Contact model = new Contact();
                return View(model);
            }

            return Redirect("~/Home/Login");

        }
        [HttpPost]
        public IActionResult Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("CurrentUser") != null)
                {
                    model = _contactOperation.CreateContact(model);
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }
        [HttpGet("Contact/Update/{id:int}")]
        public IActionResult Update(int id)
        {
            Contact model = new Contact();
            var adress = _contactOperation.GetAllContact().Where(x => x.Id == id).FirstOrDefault();
            return View(adress);
        }
        [HttpPost]
        public IActionResult Update(Contact model)
        {
            if (ModelState.IsValid)
            {
                model = _contactOperation.UpdateContact(model);
                if (model != null && model.Id > 0)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }
    }
}

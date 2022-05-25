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
    public class CategoryController : Controller
    {
        private readonly CategoryOperation _categoryOperation;
        public CategoryController(CategoryOperation categoryOperation)
        {
            _categoryOperation = categoryOperation;

        }
        [HttpGet]
        [Route("[action]/(id)")]
        public IActionResult GetByIdCategory(int id)
        {
            var category = _categoryOperation.GetCategoryById(id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _categoryOperation.GetAllCategories();
            return Ok(list);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateCar([FromBody] Category category)
        {
            var createdCategory = _categoryOperation.CreateCategory(category);
            return CreatedAtAction("Get", new { id = createdCategory.Id }, createdCategory);
        }
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateCategory([FromBody] Category category)
        {
            if (_categoryOperation.GetCategoryById(category.Id) != null)
            {
                return Ok(_categoryOperation.UpdateCategory(category));
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [Route("[action]/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (_categoryOperation.GetCategoryById(id) != null)
            {
                _categoryOperation.DeleteCategory(id);
                return Ok();
            }
            return NotFound();
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
                var list = _categoryOperation.GetAllCategories();
                return View(list);
            }
            return Redirect("~/Home/Login");
        }
        [HttpGet]
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                Category model = new Category();
                return View(model);
            }

            return Redirect("~/Home/Login");


        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("CurrentUser") != null)
                {
                    model = _categoryOperation.CreateCategory(model);
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }
         [HttpGet("Category/Update/{id:int}")]
        public IActionResult Update(int id)
        {
            Adress model = new Adress();
            var adress = _categoryOperation.GetAllCategories().Where(x => x.Id == id).FirstOrDefault();
            return View(adress);
        }
        [HttpPost]
        public IActionResult Update(Category model)
        {
            if (ModelState.IsValid)
            {
                model = _categoryOperation.UpdateCategory(model);
                if (model != null && model.Id > 0)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

    }
}

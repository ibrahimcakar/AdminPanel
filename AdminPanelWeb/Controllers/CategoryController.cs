using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
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
    }
}

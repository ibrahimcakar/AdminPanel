using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarApiController : Controller
    {
        private readonly ICarOperation _carOperation;
        public CarApiController(ICarOperation carOperation)
        {
            _carOperation = carOperation;

        }
        [HttpGet]
        [Route("[action]/(id)")]
        public IActionResult GetByCar(int id)
        {
            var car = _carOperation.GetCars(id);
            if (car != null)
            {
                return Ok(car);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _carOperation.GetAllCars();
            return Ok(list);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateCar([FromBody] Cars car)
        {
            var createdCar = _carOperation.CreateCars(car);
            return CreatedAtAction("Get", new { id = createdCar.Id }, createdCar);
        }
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateCar([FromBody] Cars car)
        {
            if (_carOperation.GetCars(car.Id) != null)
            {
                return Ok(_carOperation.UpdateCars(car));
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [Route("[action]/{id}")]

        public IActionResult DeleteCar(int id)
        {
            if (_carOperation.GetCars(id) != null)
            {
                _carOperation.DeleteCars(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GetBrand(Cars car)
        {
            var list = _carOperation.GetCarByBrand(car);
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult GetBrandCategory(Cars car)
        {
            var list = _carOperation.GetCarBrandCategory(car);
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }

        }
    }
}

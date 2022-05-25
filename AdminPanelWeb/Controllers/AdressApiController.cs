using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanelWeb.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressApiController : Controller
    {
        private readonly IAdressOperation _adressOperation;
        public AdressApiController(IAdressOperation adressOperation)
        {
            _adressOperation = adressOperation;

        }
        [HttpGet]
        [Route("[action]/(id)")]
        public IActionResult GetByAdress(int id)
        {
            var adress = _adressOperation.GetAdress(id);
            if (adress != null)
            {
                return Ok(adress);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _adressOperation.GetAllAdress();
            return Ok(list);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateAdress([FromBody] Adress adress)
        {
            var createdAdress = _adressOperation.CreateAdress(adress);
            return CreatedAtAction("Get", new { id = createdAdress.Id }, createdAdress);
        }
        [HttpPut]
        [Route("[action]")]
        public IActionResult UpdateAdress([FromBody] Adress adress)
        {
            if (_adressOperation.GetAdress(adress.Id) != null)
            {
                return Ok(_adressOperation.UpdateAdress(adress));
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [Route("[action]/{id}")]

        public IActionResult DeleteAdress(int id)
        {
            if (_adressOperation.GetAdress(id) != null)
            {
                _adressOperation.DeleteAdress(id);
                return Ok();
            }
            return NotFound();
        }

    }
}

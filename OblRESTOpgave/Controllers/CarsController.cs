using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OblRESTOpgave.Managers;
using OblRESTOpgave.Models;

namespace OblRESTOpgave.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller{

        private CarsManager _manager = new CarsManager();

        
        //// GET: api/cars
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpGet]
        //public ActionResult<IEnumerable<Car>> GetAll() {
        //    IEnumerable<Car> cars = _manager.GetCars();
        //    return Ok(cars);
        //}

        // GET: api/cars
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetFilteredList([FromQuery]string filterString,[FromQuery] int maxPrice){
            IEnumerable<Car> cars = _manager.GetAll(filterString, maxPrice);
            if (!cars.Any()){
                return NoContent();
            }

            return Ok(cars);
        }

        // GET: CarsController/Details/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id){
            Car car = _manager.GetById(id);
            if (car == null){
                return NotFound("No car with given Id: " + id);
            }
            return Ok(car);
        }

        // POST: CarsController/Create
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult Create([FromBody] Car car) {
            Car createdCar = new Car();
            if (car.Model == null || car.Brand == null || car.Price <= 0) {
                return BadRequest(car);
            }

            createdCar = _manager.AddCar(car);
            return Created("api/cars/" + createdCar.Id, createdCar);
        }

        // GET: CarsController/Delete/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Car> Delete(int id){
            Car car = _manager.GetById(id);
            if (car == null){
                return NotFound("No car with given Id: " + id);
            }

            return Ok(_manager.DeleteCar(id));
        }

    }
}

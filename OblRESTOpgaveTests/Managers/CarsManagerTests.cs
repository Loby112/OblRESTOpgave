using Microsoft.VisualStudio.TestTools.UnitTesting;
using OblRESTOpgave.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OblRESTOpgave.Models;

namespace OblRESTOpgave.Managers.Tests {
    [TestClass()]
    public class CarsManagerTests{

        private CarsManager _manager;
        private List<Car> _cars;

        [TestInitialize]
        public void Initialize(){
            _manager = new CarsManager();
            
        }

        [TestMethod()]
        public void GetAllTest(){
            _cars = _manager.GetAll("mercedes", 1000000);
            Assert.IsNotNull(_cars);
            Assert.AreEqual("Mercedes",_cars[0].Brand);
            Assert.AreEqual(1, _cars.Count);
            Assert.AreEqual(1, _cars[0].Id);
            Assert.AreEqual("Benz", _cars[0].Model);
            Assert.AreEqual(800000, _cars[0].Price);

            _cars = _manager.GetAll("", 50000000);

            Assert.AreEqual(3, _cars.Count);
            Assert.AreEqual("R8", _cars[1].Model);
            Assert.AreEqual(4, _cars[2].Id);
            Assert.AreEqual(5000000, _cars[1].Price);


            _cars = _manager.GetAll("audi", null);

            Assert.AreEqual(1, _cars.Count);
            Assert.AreEqual("Audi", _cars[0].Brand);
            Assert.AreEqual(5000000, _cars[0].Price);
        }

        [TestMethod()]
        public void GetCarsTest(){
            _cars = _manager.GetCars();
            Assert.IsNotNull(_cars);
            Assert.AreEqual(4, _cars.Count);
        }

        [TestMethod()]
        public void GetByIdTest(){
            Car car = new Car();
            car = _manager.GetById(1);
            Assert.IsNotNull(car);
            Assert.AreEqual(car.Id, 1);
            Assert.AreEqual(car.Model, "Benz");
        }

        [TestMethod()]
        public void AddCarTest(){
            Car car = new Car(){Brand = "BMW", Model = "Vroom Vroom", Price = 500000};
            car = _manager.AddCar(car);
            _cars = _manager.GetCars();
            Assert.AreEqual(5, _cars.Count);
            Assert.AreEqual("BMW", car.Brand );
            Assert.AreEqual(5, car.Id);
            Assert.AreEqual("Vroom Vroom", car.Model);
        }

        [TestMethod()]
        public void DeleteCarTest(){
            _cars = _manager.GetCars();
            Assert.AreEqual(5, _cars.Count);
            _manager.DeleteCar(5);
            _cars = _manager.GetCars();
            Assert.AreEqual(4, _cars.Count);
            

        }
    }
}
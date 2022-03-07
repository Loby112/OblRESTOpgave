using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using OblRESTOpgave.Models;

namespace OblRESTOpgave.Managers {
    public class CarsManager{
        private static int _nextId = 1;

        private static readonly List<Car> Data = new List<Car>(){
            new Car(){Id = _nextId++, Brand = "Mercedes", Model = "Benz", Price = 800000},
            new Car(){Id = _nextId++, Brand = "Audi", Model = "R8", Price = 5000000},
            new Car(){Id = _nextId++, Brand = "Ferrari", Model = "Enzo", Price = 100000000},
            new Car(){Id = _nextId++, Brand = "VW", Model = "Golf", Price = 300000}
        };

        public List<Car> GetAll(string filterString, int? maxPrice){
            List<Car> result = new List<Car>(Data);
            if (!string.IsNullOrWhiteSpace(filterString)){
                result = Data.FindAll(c => c.Brand.Contains(filterString, StringComparison.OrdinalIgnoreCase));
            }

            if (maxPrice != 0){
                if (!string.IsNullOrWhiteSpace(filterString)){
                    result = Data.FindAll(c =>
                        c.Brand.Contains(filterString, StringComparison.OrdinalIgnoreCase) && c.Price <= maxPrice);
                }
                else{
                    result = Data.FindAll(c => c.Price <= maxPrice);
                }
            }
            return result;
        }

        public List<Car> GetCars(){
            List<Car> result = new List<Car>(Data);
            return result;
        }

        public Car GetById(int id){
            return Data.Find(c => c.Id == id);
        }

        public Car AddCar(Car newCar){
            newCar.Id = _nextId++;
            Data.Add(newCar);
            return newCar;
        }

        public Car DeleteCar(int id){
            int index = Data.FindIndex(c => c.Id == id);
            Car tempCar = Data[index];
            Data.Remove(Data[index]);   
            return tempCar;
        }
    }
}

using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public class CarOperation : ICarOperation
    {
        public Cars CreateCars(Cars cars)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Cars.Add(cars);
                AdminPanelDbContext.SaveChanges();
                return cars;
            }
        }

        public void DeleteCars(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                var deletedCars= GetCars(id);
                AdminPanelDbContext.Cars.Remove(deletedCars);
                AdminPanelDbContext.SaveChanges();
            }
        }
        public List<Cars> GetCarByBrand(Cars entity)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                var branddd= AdminPanelDbContext.Cars.Where(x => x.Brand==entity.Brand).ToList();
                return branddd;
            }
        }
        public List<Cars> GetCarBrandCategory(Cars entity)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Cars.Where(x => x.Brand == entity.Brand && x.Category == entity.Category).ToList();
            }
        }
        public List<Cars> GetAllCars()
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Cars.ToList();
            }
        }

        public Cars GetCars(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Cars.Find(id);
            }
        }

        public Cars UpdateCars(Cars cars)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Cars.Update(cars);
                return cars;
            }
        }
    }
}

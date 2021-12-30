using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public interface ICarOperation
    {
        List<Cars> GetCarBrandCategory(Cars entity);
        List<Cars> GetCarByBrand(Cars entity);
        List<Cars> GetAllCars();
        Cars GetCars(int id);
        Cars CreateCars(Cars cars);
        Cars UpdateCars(Cars cars);
        void DeleteCars(int id);
    }
}

using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public class AdressOperation:IAdressOperation
    {
        public Adress CreateAdress(Adress adresses)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Adresses.Add(adresses);
                AdminPanelDbContext.SaveChanges();
                return adresses;
            }
        }

        public void DeleteAdress(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                var deletedAdress = GetAdress(id);
                AdminPanelDbContext.Adresses.Remove(deletedAdress);
                AdminPanelDbContext.SaveChanges();
            }
        }
      
       
        public List<Adress> GetAllAdress()
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Adresses.ToList();
            }
        }

        public Adress GetAdress(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Adresses.Find(id);
            }
        }

        public Adress UpdateAdress(Adress Adress)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Adresses.Update(Adress);
                return Adress;
            }
        }
    }
}

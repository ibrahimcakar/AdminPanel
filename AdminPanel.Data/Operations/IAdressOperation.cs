using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public interface IAdressOperation

    {
        Adress CreateAdress(Adress adresses);
        void DeleteAdress(int id);
        List<Adress> GetAllAdress();
        Adress GetAdress(int id);
        Adress UpdateAdress(Adress Adress);
    }
}

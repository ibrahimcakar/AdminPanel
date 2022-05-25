using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public interface IContactOperation
    {
        Contact CreateContact(Contact contacts);
        void DeleteContact(int id);
        List<Contact> GetAllContact();
        Contact GetContact(int id);
        Contact UpdateContact(Contact contact);

    }
}

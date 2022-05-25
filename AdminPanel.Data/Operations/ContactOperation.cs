using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public class ContactOperation:IContactOperation
    {
        public Contact CreateContact(Contact contacts)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Contacts.Add(contacts);
                AdminPanelDbContext.SaveChanges();
                return contacts;
            }
        }

        public void DeleteContact(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                var deletedContacts = GetContact(id);
                AdminPanelDbContext.Contacts.Remove(deletedContacts);
                AdminPanelDbContext.SaveChanges();
            }
        }


        public List<Contact> GetAllContact()
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Contacts.ToList();
            }
        }

        public Contact GetContact(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Contacts.Find(id);
            }
        }

        public Contact UpdateContact(Contact contact)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Contacts.Update(contact);
                return contact;
            }
        }
    }
}

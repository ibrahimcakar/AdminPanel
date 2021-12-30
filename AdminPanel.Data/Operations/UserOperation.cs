using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public class UserOperation:IUserOperation
    {
        public User CreateUser(User user)
        {
            using (var userDbContext = new AdminPanelDb())
            {
                userDbContext.Users.Add(user);
                userDbContext.SaveChanges();
                return user;
            }
        }

        public void DeleteUser(int id)
        {
            using (var userDbContext = new AdminPanelDb())
            {
                var deletedUser = GetUserById(id);
                userDbContext.Users.Remove(deletedUser);
                userDbContext.SaveChanges();
            }
        }
        public User GetMail(User user)
        {
            using (var userDbContext = new AdminPanelDb())
            {
                return userDbContext.Users.FirstOrDefault(x => x.Email == user.Email );
            }
        }
        public List<User> GetAllUser()
        {
            using (var userDbContext = new AdminPanelDb())
            {
                return userDbContext.Users.ToList();
            }
        }

        public User GetUserById(int id)
        {
            using (var userDbContext = new AdminPanelDb())
            {
                return userDbContext.Users.Find(id);
            }
        }

        public User GetUserByUsernamePassword(User entity)
        {
            using (var userDbContext = new AdminPanelDb())
            {
                return userDbContext.Users.FirstOrDefault(x => x.UserName == entity.UserName && x.Password == entity.Password);
            }
        }     
        public User GetUserByMailPassword(User entity)
        {
            using (var userDbContext = new AdminPanelDb())
            {
                return userDbContext.Users.FirstOrDefault(x => x.Email == entity.Email && x.Password == entity.Password);
            }
        }

  
        public User UpdateUser(User user)
        {
            using (var userDbContext = new AdminPanelDb())
            {
                userDbContext.Users.Update(user);
                return user;
            }
        }
    }
}

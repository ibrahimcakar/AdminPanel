using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public interface IUserOperation
    {
        List<User> GetAllUser();
        User GetUserByUsernamePassword(User entity);
        User GetUserById(int id);
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int id);
        User GetMail(User user);
    }
}

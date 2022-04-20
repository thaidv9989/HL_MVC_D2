using ManageOnline.Models;
using System.Collections.Generic;

namespace ManageOnline.Service
{
    public interface IUserServices
    {
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        void AddUser(User user);
        void RemoveUser(int id);
        void UpdateUser(User user);
        bool IsExistUserHaveSameUsername(string username);
    }
}

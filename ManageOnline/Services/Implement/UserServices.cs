using ManageOnline.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace ManageOnline.Service.Implement
{
    public class UserServices : IUserServices
    {
        private readonly MProductEntities _dbContext;

        public UserServices(MProductEntities dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddUser(User user)
        {
            string[] lastNameArray = user.LastName.Split(' ');
            string[] firstNameArray = user.FirstName.Split(' ');
            string username = "";
            foreach (string firstName in firstNameArray)
            {
                username = String.Concat(username, firstName.ToLower());
            }
            foreach (string lastName in lastNameArray)
            {
                username = String.Concat(username, lastName.ToLower().Substring(0, 1));
            }

            // Generate Password
            string password = String.Concat(user.UserName, "@", user.DOB.ToString("ddMMyyyy"));
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _dbContext.Users.FirstOrDefault(x=>x.UserID == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void RemoveUser(int id)
        {
            var rs = _dbContext.Users.FirstOrDefault(x => x.UserID == id);
            if(rs != null)
            {
                _dbContext.Users.Remove(rs);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            var rs = _dbContext.Users.FirstOrDefault(x => x.UserID == user.UserID);
            if (rs == null)
            {
                throw new Exception("Not found");
            }
            rs.Password = user.Password;
            rs.FirstName = user.FirstName;
            rs.LastName = user.LastName;
            rs.DOB = user.DOB;
            rs.State = user.State;
            rs.Email = user.Email;
            _dbContext.Entry(rs);
            _dbContext.SaveChanges();
        }
        public bool IsExistUserHaveSameUsername(string username)
        {
            return _dbContext.Users.Any(x => x.UserName.Trim().ToLower() == username.Trim().ToLower());
        }
    }
}

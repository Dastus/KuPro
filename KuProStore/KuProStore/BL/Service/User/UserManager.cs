using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;
using KuProStore.BL.Repository;
using KuProStore.BL.Service.Cacher;

namespace KuProStore.BL.Service
{
    public class UserManager : IUserManager
    {
        private IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetUser(Login login)
        {
            if (login == null)
                return null;

            return userRepository.GetUser(login);
        }

        public void AddUser(User user)
        {            
            userRepository.AddUser(user);
        }

        public User GetUserByCookies(string cookie)
        {
            if (cookie == null)
                return null;

            User user = AppCache.Instance.GetValue(cookie) as User;
            if (user == null)
            {
               user = userRepository.GetUserByCookies(cookie);
                AppCache.Instance.Add(cookie, user, 5);
            }
            return (user == null) ? null : user;
        }

        public void UpdateCookies(User user, string cookie)
        {
            if (user != null && cookie != null)
                userRepository.UpdateCookies(user, cookie);
            else
                throw new ArgumentNullException("user or cookie is empty");
        }

        public bool CheckIfRegistered(string phonenum)
        {
            if (phonenum == null)
                return false;

            return userRepository.CheckIfRegistered(phonenum);
        }

        public void UpdateUser(User user)
        {
            if (user != null)
                userRepository.UpdateUser(user);
        }
    }
}
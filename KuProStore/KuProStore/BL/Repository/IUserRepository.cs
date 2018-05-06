using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;

namespace KuProStore.BL.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUser(Login login);
        User GetUserByCookies(string cookie);
        void UpdateCookies(User user, string cookie);
        bool CheckIfRegistered(string phonenum);
        void UpdateUser(User user);
    }
}
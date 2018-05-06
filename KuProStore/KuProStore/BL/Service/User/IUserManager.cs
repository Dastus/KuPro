using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuProStore.Models;

namespace KuProStore.BL.Service
{
    public interface IUserManager
    {
        User GetUser(Login login);
        void AddUser(User user);
        User GetUserByCookies(string cookie);
        void UpdateCookies(User user, string cookie);
        bool CheckIfRegistered(string phonenum);
        void UpdateUser(User user);
    }
}

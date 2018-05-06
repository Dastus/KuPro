using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;
using KuProStore.DAL.EF;

namespace KuProStore.BL.Repository.EF
{
    public class EfUserRepository : IUserRepository
    {

        public User GetUser(Login login)
        {
            if (login == null)
                return null;
            using (StoreDBcontext context = new StoreDBcontext())
            {
                var user = context.UserEntities.AsNoTracking().
                    Where(e => e.ContactPhone == login.LoginString && e.Password == login.Password).
                    FirstOrDefault();

                return (user != null) ? user.ConvertToApplicationModel() : null;
            }
        }

        public void AddUser(User user)
        {
            if (user != null)
            {
                using (StoreDBcontext context = new StoreDBcontext())
                {
                    context.UserEntities.Add((UserEntity)new UserEntity().ConvertFromApplicationModel(user));
                    context.SaveChanges();
                }
            }
        }

        public User GetUserByCookies(string cookie)
        {
            if (cookie == null)
                return null;

            using (StoreDBcontext context = new StoreDBcontext())
            {
                var user = context.UserEntities.AsNoTracking().
                    Where(e => e.Cookie == cookie).FirstOrDefault();

                return (user != null) ? user.ConvertToApplicationModel() : null;
            }
        }

        public void UpdateCookies(User user, string cookie)
        {
            if (user != null && cookie != null)
            {
                using (StoreDBcontext context = new StoreDBcontext())
                {
                    context.UserEntities.Where(e => e.ContactPhone == user.ContactPhone)
                        .FirstOrDefault().Cookie = cookie;
                    context.SaveChanges();
                }
            }
        }

        public bool CheckIfRegistered(string phonenum)
        {
            if (phonenum == null)
                return false;

            using (StoreDBcontext context = new StoreDBcontext())
            {
                var p = context.UserEntities.Where(e => e.ContactPhone == phonenum).FirstOrDefault();
                return (context.UserEntities.Where(e => e.ContactPhone == phonenum).FirstOrDefault() == null) ? false : true;
            }
        }

        public void UpdateUser(User user)
        {
            if (user != null)
            {
                bool needUpdate = false;
                user.Password = Guid.NewGuid().ToString().Take(5).ToString();

                using (StoreDBcontext context = new StoreDBcontext())
                {
                    UserEntity userEntity = context.UserEntities.Find(user.UserId);     
                   
                    if (userEntity != null)
                    {
                        if (userEntity.FirstName != user.FirstName)
                        {
                            userEntity.FirstName = user.FirstName;
                            needUpdate = true;
                        }
                        if (userEntity.LastName != user.LastName)
                        {
                            userEntity.LastName = user.LastName;
                            needUpdate = true;
                        }
                        if (userEntity.Email != user.Email)
                        {
                            userEntity.Email = user.Email;
                            needUpdate = true;
                        }
                        if (userEntity.ContactInfo != user.ContactInfo)
                        {
                            userEntity.ContactInfo = user.ContactInfo;
                            needUpdate = true;
                        }
                    }

                    if (needUpdate)
                        context.SaveChanges();
                }
            }
        }

    }
}
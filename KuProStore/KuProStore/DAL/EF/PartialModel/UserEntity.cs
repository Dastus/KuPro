using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KuProStore.Models;
using KuProStore.BL.Service;

namespace KuProStore.DAL.EF
{
    public partial class UserEntity : IStorageModel<User>
    {
        public User ConvertToApplicationModel()
        {
            User user = new User
            {
                UserId = this.UserId,
                Balance = this.Balance,
                ContactPhone = this.ContactPhone,
                Cookie = this.Cookie,
                Email = this.Email,
                IsActive = this.IsActive,
                FirstName = this.FirstName,
                LastName = this.LastName,                
                ContactInfo = this.ContactInfo
            };

            return user;
        }

        public IStorageModel<User> ConvertFromApplicationModel(User user)
        {
            if (user == null)
                return null;

            UserEntity userEntity = new UserEntity
            {                
                Balance = user.Balance,
                ContactPhone = user.ContactPhone,
                Cookie = user.Cookie,
                Email = user.Email,
                IsActive = user.IsActive,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                ContactInfo = user.ContactInfo
            };

            return userEntity;
        }
    }
}
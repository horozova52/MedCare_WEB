using AutoMapper;
using MedCare_WEB.BusinessLogic.DBModel;
using MedCare_WEB.Domains.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MedCare_WEB.Domains.Enums;
using MedCare_WEB.Helpers;
using System.Web.UI.WebControls;

namespace MedCare_WEB.BusinessLogic.Core
{
    public class UserApi
    {
        internal UResponseLogin UserRegistrationAction(URegisterDomains dataUserDomain)
        {
            var validate = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!validate.IsValid(dataUserDomain.email))
            {
                return new UResponseLogin { Status = false, StatusMsg = "Email format invalid" };
            }

            if (dataUserDomain.firstName.Length < 3)
            {
                return new UResponseLogin { Status = false, StatusMsg = "username must contain minim 3 characters" };
            }

            if (dataUserDomain.firstName.Length > 50)
            {
                return new UResponseLogin { Status = false, StatusMsg = "username must contain maxim 50 characters" };
            }

            if (dataUserDomain.lastName.Length < 3)
            {
                return new UResponseLogin { Status = false, StatusMsg = "username must contain minim 3 characters" };
            }

            if (dataUserDomain.lastName.Length > 50)
            {
                return new UResponseLogin { Status = false, StatusMsg = "username must contain maxim 50 characters" };
            }

            if (dataUserDomain.password.Length < 8)
            {
                return new UResponseLogin { Status = false, StatusMsg = "Password must contain minim 8 characters" };
            }

            if (dataUserDomain.email.Length > 256)
            {
                return new UResponseLogin { Status = false, StatusMsg = "Email must contain maxim 256 characters" };
            }
            

            if (dataUserDomain.password.Length > 50)
            {
                return new UResponseLogin { Status = false, StatusMsg = "Password must contain maxim 50 characters" };
            }

            var pass = LoginHelper.HashGen(dataUserDomain.password);
            using (var db = new DBUserContext())
            {
                if (db.Users.FirstOrDefault(item => item.email == dataUserDomain.email) != null)
                {
                    return new UResponseLogin {Status = false, StatusMsg = "This user already exists"};
                }

                var newUser = new DBUserTable
                {
                    lastName = dataUserDomain.lastName,
                    firstName = dataUserDomain.firstName,
                    email = dataUserDomain.email,
                    password = pass,
                    role = URole.user,
                    lastLogin = DateTime.Now,
                    banStatus = false
                };

                db.Users.Add(newUser);
                db.SaveChanges();
            }
            return new UResponseLogin { Status = true, StatusMsg = "Successful register"};
        }
        internal UResponseLogin UserLoginAction(ULoginDataDomains dataDomainsUserDomain)
        {
            var pass = LoginHelper.HashGen(dataDomainsUserDomain.password);
            var validate = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!validate.IsValid(dataDomainsUserDomain.email))
                return new UResponseLogin { Status = false, StatusMsg = "Login or Password is incorrect" };
            DBUserTable userTable;
            using (var db = new DBUserContext())
            {
                userTable = db.Users.FirstOrDefault(itemDb => itemDb.email == dataDomainsUserDomain.email && itemDb.password == pass);
            }

            if (userTable == null)
            {
                return new UResponseLogin { Status = false, StatusMsg = "Login or Password is incorrect" };
            }

            if (userTable.banStatus)
            {
                return new UResponseLogin { Status = false, StatusMsg = "You are blocked!" };
            }

            using (var todo = new DBUserContext())
            {
                userTable.lastLogin = DateTime.Now;
                todo.Entry(userTable).State = EntityState.Modified;
                todo.SaveChanges();
            }

            return new UResponseLogin { Status = true , StatusMsg = "Successful login"};
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };
            using (var db = new DBSessionContext())
            {
                var validate = new EmailAddressAttribute();
                var currentDbSession = validate.IsValid(loginCredential) ? db.Sessions.FirstOrDefault(itemDb => itemDb.User.email == loginCredential) : null;

                if (currentDbSession != null)
                {
                    currentDbSession.cookieValue = apiCookie.Value;
                    currentDbSession.expireTime = DateTime.Now.AddMinutes(60);
                    db.Entry(currentDbSession).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    DBUserTable currentUserNoSession = null;
                    using (var dbUser = new DBUserContext())
                    {
                        currentUserNoSession = validate.IsValid(loginCredential) ? dbUser.Users.FirstOrDefault(itemDb => itemDb.email == loginCredential) : null;
                    }

                    if (currentUserNoSession == null) return apiCookie;
                    db.Sessions.Add(new DBSessionTable
                    {
                        userId = currentUserNoSession.userId,
                        cookieValue = apiCookie.Value,
                        expireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            DBSessionTable dbSession = null;
            DBUserTable currentUser = null;

            using (var db = new DBSessionContext())
            {
                dbSession = db.Sessions.Include(s => s.User)
                                     .FirstOrDefault(itemDb => itemDb.cookieValue == cookie && itemDb.expireTime > DateTime.Now);
            }

            if (dbSession == null) return null;
            using (var db = new DBUserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(dbSession.User.email))
                {
                    currentUser = db.Users.FirstOrDefault(u => u.email == dbSession.User.email);
                }
            }

            if (currentUser == null) return null;
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<DBUserTable, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(currentUser);

            return userminimal;
        }

    }
}

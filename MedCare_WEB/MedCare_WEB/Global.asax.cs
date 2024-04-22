using AutoMapper;
using eUseControl.Web;
using MedCare_WEB.Domains.Entities.User;
using MedCare_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MedCare_WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeAutoMapper();
        }

        protected static void InitializeAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserLogin, ULoginData>();
                cfg.CreateMap<UserRegister, URegisterData>();
                cfg.CreateMap<UserTable, UserMinimal>();
            });
        }
    }
}

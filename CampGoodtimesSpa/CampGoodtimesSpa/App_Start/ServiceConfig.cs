using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Controllers;
using System.Web.Http;
using Microsoft.Practices.Unity;
using CampGoodtimesSpa.Services;
using CampGoodtimesSpa.Controllers;

namespace CampGoodtimesSpa
{
    public static class ServiceConfig
    {
        public static void RegisterServices(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ISharepointService, SharepointService>();
            container.RegisterType<IHttpController, DataController>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
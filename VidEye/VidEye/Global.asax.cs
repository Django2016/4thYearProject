using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace VidEye
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            /*VidEyeContext ctx = new VidEyeContext();
            Subscription sub = new Subscription
            {
                Price = 10.00,
                SubscriptionDesc = "First Sub",
                SubscriptionDateCreated = DateTime.Now
            };
            ctx.Subscriptions.Add(sub);
            ctx.SaveChanges();*/
        }
    }
}

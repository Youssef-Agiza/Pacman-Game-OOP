using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebProject1.Models;

namespace WebProject1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<WebProject1Context>(new CreateDatabaseIfNotExists<WebProject1Context>());
            using (var context = new WebProject1Context())
            {
                context.Database.Initialize(force: true);
                // If exception was thrown here, 
                // Please open Server Explorer -> Data Connection ->
                // if WebProject1Context-20190411102821.mdf is existed, please delete it at first, then re-build this probject and run it again.

                // If WebProject1Context-20190411102821.mdf doesn't exist (right click) -> Add Connection -> 
                // Data Source: Microsoft SQL Server Database File (SqlClient)
                // Database file name -> browse ... select App_Data folder, 
                // input WebProject1Context-20190411102821.mdf, then click OK, the mdf file will be created.
            }

            Services.AsyncSendMailService.Init();
            Services.TempMailTaskStore.Init();
            Services.MassSendMailService.Init();
            Services.DbSendMailService.Init();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

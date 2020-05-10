Imports System.Data.Entity
Imports System.Web.Optimization
Imports WebProject1.Models

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()

        Database.SetInitializer(Of WebProject1Context)(New CreateDatabaseIfNotExists(Of WebProject1Context)())

        Using context = New WebProject1Context()
            context.Database.Initialize(force:=True)

            ' If exception was thrown here, 
            ' Please open Server Explorer -> Data Connection ->
            ' if WebProject1Context-20190503215512.mdf is existed, please delete it at first, then re-build this probject and run it again.

            ' If WebProject1Context-20190503215512.mdf doesn't exist (right click) -> Add Connection -> 
            ' Data Source: Microsoft SQL Server Database File (SqlClient)
            ' Database file name -> browse ... select App_Data folder, 
            ' input WebProject1Context-20190503215512.mdf, then click OK, the mdf file will be created.
        End Using

        AsyncSendMailService.Init()
        TempMailTaskStore.Init()
        MassSendMailService.Init()
        DbSendMailService.Init()

        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
End Class

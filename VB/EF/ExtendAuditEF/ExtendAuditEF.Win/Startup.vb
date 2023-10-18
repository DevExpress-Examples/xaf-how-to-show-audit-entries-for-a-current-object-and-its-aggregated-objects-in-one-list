Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.ApplicationBuilder
Imports DevExpress.ExpressApp.Win.ApplicationBuilder
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Design

Namespace ExtendAuditEF.Win

    Public Class ApplicationBuilder
        Implements IDesignTimeApplicationFactory

        Public Shared Function BuildApplication(ByVal connectionString As String) As WinApplication
            Dim builder = DevExpress.ExpressApp.Win.WinApplication.CreateBuilder()
            ' Register custom services for Dependency Injection. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/404430/
            ' builder.Services.AddScoped<CustomService>();
            ' Register 3rd-party IoC containers (like Autofac, Dryloc, etc.)
            ' builder.UseServiceProviderFactory(new DryIocServiceProviderFactory());
            ' builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.UseApplication(Of ExtendAuditEFWindowsFormsApplication)()
            builder.Modules.AddAuditTrailEFCore().Add(Of ExtendAuditEF.Module.ExtendAuditEFModule)().Add(Of ExtendAuditEFWinModule)()
            builder.ObjectSpaceProviders.AddEFCore(Function(options) options.PreFetchReferenceProperties()).WithAuditedDbContext(Function(contexts)
                contexts.Configure(Of ExtendAuditEF.Module.BusinessObjects.ExtendAuditEFEFCoreDbContext, ExtendAuditEF.Module.BusinessObjects.ExtendAuditEFAuditingDbContext)(Function(application, businessObjectDbContextOptions)
                    ' Uncomment this code to use an in-memory database. This database is recreated each time the server starts. With the in-memory database, you don't need to make a migration when the data model is changed.
                    ' Do not use this code in production environment to avoid data loss.
                    ' We recommend that you refer to the following help topic before you use an in-memory database: https://docs.microsoft.com/en-us/ef/core/testing/in-memory
                    'businessObjectDbContextOptions.UseInMemoryDatabase("InMemory");
                    businessObjectDbContextOptions.UseSqlServer(connectionString)
                    businessObjectDbContextOptions.UseChangeTrackingProxies()
                    businessObjectDbContextOptions.UseObjectSpaceLinkProxies()
                End Function, Function(application, auditHistoryDbContextOptions)
                    auditHistoryDbContextOptions.UseSqlServer(connectionString)
                    auditHistoryDbContextOptions.UseChangeTrackingProxies()
                    auditHistoryDbContextOptions.UseObjectSpaceLinkProxies()
                End Function, Function(auditTrailOptions)
                    auditTrailOptions.AuditUserProviderType = GetType(DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEmptyUserProvider)
                End Function)
            End Function).AddNonPersistent()
            builder.AddBuildStep(Function(application)
                application.ConnectionString = connectionString
#If DEBUG
                If System.Diagnostics.Debugger.IsAttached AndAlso application.CheckCompatibilityType Is CheckCompatibilityType.DatabaseSchema Then
                    application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
                End If
#End If
            End Function)
            Dim winApplication = builder.Build()
            Return winApplication
        End Function

        Private Function Create() As XafApplication Implements IDesignTimeApplicationFactory.Create
            Return BuildApplication(XafApplication.DesignTimeConnectionString)
        End Function
    End Class
End Namespace

﻿using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore;
using DevExpress.ExpressApp.EFCore;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Design;

namespace ExtendAuditEF.Win;

public class ApplicationBuilder : IDesignTimeApplicationFactory {
    public static WinApplication BuildApplication(string connectionString) {
        var builder = WinApplication.CreateBuilder();
        // Register custom services for Dependency Injection. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/404430/
        // builder.Services.AddScoped<CustomService>();
        // Register 3rd-party IoC containers (like Autofac, Dryloc, etc.)
        // builder.UseServiceProviderFactory(new DryIocServiceProviderFactory());
        // builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.UseApplication<ExtendAuditEFWindowsFormsApplication>();
        builder.Modules
            .AddAuditTrailEFCore()
            .Add<ExtendAuditEF.Module.ExtendAuditEFModule>()
        	.Add<ExtendAuditEFWinModule>();
        builder.ObjectSpaceProviders
            .AddEFCore(options => options.PreFetchReferenceProperties())
                .WithAuditedDbContext(contexts => {
                    contexts.Configure<ExtendAuditEF.Module.BusinessObjects.ExtendAuditEFEFCoreDbContext, ExtendAuditEF.Module.BusinessObjects.ExtendAuditEFAuditingDbContext>(
                        (application, businessObjectDbContextOptions) => {
                            // Uncomment this code to use an in-memory database. This database is recreated each time the server starts. With the in-memory database, you don't need to make a migration when the data model is changed.
                            // Do not use this code in production environment to avoid data loss.
                            // We recommend that you refer to the following help topic before you use an in-memory database: https://docs.microsoft.com/en-us/ef/core/testing/in-memory
                            //businessObjectDbContextOptions.UseInMemoryDatabase("InMemory");
                            businessObjectDbContextOptions.UseSqlServer(connectionString);
                            businessObjectDbContextOptions.UseChangeTrackingProxies();
                            businessObjectDbContextOptions.UseObjectSpaceLinkProxies();
                        },
                        (application, auditHistoryDbContextOptions) => {
                            auditHistoryDbContextOptions.UseSqlServer(connectionString);
                            auditHistoryDbContextOptions.UseChangeTrackingProxies();
                            auditHistoryDbContextOptions.UseObjectSpaceLinkProxies();
                        },
                        auditTrailOptions => {
                            auditTrailOptions.AuditUserProviderType = typeof(DevExpress.Persistent.BaseImpl.EFCore.AuditTrail.AuditEmptyUserProvider);
                        });
                })
            .AddNonPersistent();
        builder.AddBuildStep(application => {
            application.ConnectionString = connectionString;
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && application.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        });
        var winApplication = builder.Build();
        return winApplication;
    }

    XafApplication IDesignTimeApplicationFactory.Create()
        => BuildApplication(XafApplication.DesignTimeConnectionString);
}

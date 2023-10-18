Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.ApplicationBuilder
Imports DevExpress.ExpressApp.Win.ApplicationBuilder
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Design

Namespace ExtendAudit.Win

    Public Class ApplicationBuilder
        Implements IDesignTimeApplicationFactory

        Public Shared Function BuildApplication(ByVal connectionString As String) As WinApplication
            Dim builder = DevExpress.ExpressApp.Win.WinApplication.CreateBuilder()
            ' Register custom services for Dependency Injection. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/404430/
            ' builder.Services.AddScoped<CustomService>();
            ' Register 3rd-party IoC containers (like Autofac, Dryloc, etc.)
            ' builder.UseServiceProviderFactory(new DryIocServiceProviderFactory());
            ' builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.UseApplication(Of ExtendAuditWindowsFormsApplication)()
            builder.Modules.AddAuditTrailXpo().Add(Of ExtendAudit.Module.ExtendAuditModule)().Add(Of ExtendAuditWinModule)()
            builder.ObjectSpaceProviders.AddXpo(Function(application, options)
                options.ConnectionString = connectionString
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

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Xpo

Partial Public Class E4565_VBWindowsFormsApplication
    Inherits WinApplication
    Public Sub New()
        InitializeComponent()
        DelayedViewItemsInitialization = True
    End Sub

    Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
        args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection)
    End Sub
    Private Sub E4565_VBWindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
        e.Updater.Update()
        e.Handled = True
#Else
        If System.Diagnostics.Debugger.IsAttached Then
            e.Updater.Update()
            e.Handled = True
        Else
            Throw New InvalidOperationException( _
             "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & Constants.vbCrLf & _
             "This error occurred  because the automatic database update was disabled when the application was started without debugging." & Constants.vbCrLf & _
             "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " & _
             "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & _
             "or manually create a database using the 'DBUpdater' tool." & Constants.vbCrLf & _
             "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " & _
             "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/")
        End If
#End If
    End Sub
    Private Shared Sub E4565_VBWindowsFormsApplication_CustomizeLanguagesList(ByVal sender As Object, ByVal e As CustomizeLanguagesListEventArgs)
        Dim userLanguageName As String = System.Threading.Thread.CurrentThread.CurrentUICulture.Name
        If userLanguageName <> "en-US" And e.Languages.IndexOf(userLanguageName) = -1 Then
            e.Languages.Add(userLanguageName)
        End If
    End Sub
End Class

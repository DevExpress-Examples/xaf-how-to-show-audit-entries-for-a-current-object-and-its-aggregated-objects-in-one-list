Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl

Partial Public NotInheritable Class [E4565_VBModule]
    Inherits ModuleBase
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
        Dim updater As ModuleUpdater = New Updater(objectSpace, versionFromDB)
        Return New ModuleUpdater() {updater}
    End Function
End Class

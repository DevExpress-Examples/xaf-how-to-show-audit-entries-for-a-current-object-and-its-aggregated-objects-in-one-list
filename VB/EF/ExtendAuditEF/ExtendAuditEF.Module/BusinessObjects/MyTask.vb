Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.EF

Namespace dxTestSolution.Module.BusinessObjects

    <DefaultClassOptions>
    Public Class MyTask
        Inherits BaseObject

        Public Overridable Property Subject As String

        Public Overridable Property AssignedTo As Contact
    End Class
End Namespace

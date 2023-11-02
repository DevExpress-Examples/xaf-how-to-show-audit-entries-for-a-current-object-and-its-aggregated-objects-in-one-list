Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.DC

Namespace E4565.Module.BusinessObjects

    <DomainComponent>
    Public Class MyAddress
        Inherits XPObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Property AddressLine As String
            Get
                Return GetPropertyValue(Of String)("AddressLine")
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)("AddressLine", value)
            End Set
        End Property

        <Association>
        Public Property Person As MyPerson
            Get
                Return GetPropertyValue(Of MyPerson)("Person")
            End Get

            Set(ByVal value As MyPerson)
                SetPropertyValue(Of MyPerson)("Person", value)
            End Set
        End Property
    End Class
End Namespace

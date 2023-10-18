Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace dxTestSolution.Module.BusinessObjects

    <DefaultClassOptions>
    <DefaultProperty("Subject")>
    Public Class MyTask
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub

        Private _subject As String

        Public Property Subject As String
            Get
                Return _subject
            End Get

            Set(ByVal value As String)
                SetPropertyValue(NameOf(MyTask.Subject), _subject, value)
            End Set
        End Property

        Private _assignedTo As Contact

        <Association("Contact-Tasks")>
        Public Property AssignedTo As Contact
            Get
                Return _assignedTo
            End Get

            Set(ByVal value As Contact)
                SetPropertyValue(NameOf(MyTask.AssignedTo), _assignedTo, value)
            End Set
        End Property

        Private _isActive As Boolean

        Public Property IsActive As Boolean
            Get
                Return _isActive
            End Get

            Set(ByVal value As Boolean)
                SetPropertyValue(NameOf(MyTask.IsActive), _isActive, value)
            End Set
        End Property

        Private _priority As Priority

        Public Property Priority As Priority
            Get
                Return _priority
            End Get

            Set(ByVal value As Priority)
                SetPropertyValue(NameOf(MyTask.Priority), _priority, value)
            End Set
        End Property
    End Class

    Public Enum Priority
        <ImageName("State_Priority_Low")>
        Low = 0
        <ImageName("State_Priority_Normal")>
        Normal = 1
        <ImageName("State_Priority_High")>
        High = 2
    End Enum
End Namespace

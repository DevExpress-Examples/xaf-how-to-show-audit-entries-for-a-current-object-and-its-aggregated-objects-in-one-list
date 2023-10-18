Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports System.Diagnostics

Namespace dxTestSolution.Module.BusinessObjects

    <DefaultClassOptions>
    Public Class Contact
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub

        Private _firstName As String

        Public Property FirstName As String
            Get
                Return _firstName
            End Get

            Set(ByVal value As String)
                SetPropertyValue(NameOf(Contact.FirstName), _firstName, value)
            End Set
        End Property

        Private _lastName As String

        Public Property LastName As String
            Get
                Return _lastName
            End Get

            Set(ByVal value As String)
                SetPropertyValue(NameOf(Contact.LastName), _lastName, value)
            End Set
        End Property

        Private _age As Integer

        Public Property Age As Integer
            Get
                Return _age
            End Get

            Set(ByVal value As Integer)
                SetPropertyValue(NameOf(Contact.Age), _age, value)
            End Set
        End Property

        <Association("Contact-Tasks")>
        Public ReadOnly Property Tasks As XPCollection(Of MyTask)
            Get
                Return GetCollection(Of MyTask)(NameOf(Contact.Tasks))
            End Get
        End Property

        Private auditTrailField As BindingList(Of CustomAuditDataItem)

        Public ReadOnly Property AuditTrail As BindingList(Of CustomAuditDataItem)
            Get
                If auditTrailField Is Nothing Then
                    auditTrailField = New BindingList(Of CustomAuditDataItem)()
                    auditTrailField.AllowEdit = False
                    auditTrailField.AllowNew = False
                    auditTrailField.AllowRemove = False
                    Dim rootItems As XPCollection(Of AuditDataItemPersistent) = AuditedObjectWeakReference.GetAuditTrail(Session, Me)
                    If rootItems IsNot Nothing Then
                        For Each entry As AuditDataItemPersistent In rootItems
                            auditTrailField.Add(New CustomAuditDataItem(entry, "Person"))
                        Next
                    End If

                    For Each task As MyTask In Tasks
                        Dim taskItems As XPCollection(Of AuditDataItemPersistent) = AuditedObjectWeakReference.GetAuditTrail(Session, task)
                        If taskItems IsNot Nothing Then
                            For Each entry As AuditDataItemPersistent In taskItems
                                auditTrailField.Add(New CustomAuditDataItem(entry, "Task - " & task.Oid.ToString() & ", " & task.Subject))
                            Next
                        End If
                    Next
                End If

                Return auditTrailField
            End Get
        End Property
    End Class
End Namespace

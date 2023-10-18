Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.EF
Imports DevExpress.Persistent.BaseImpl.EFCore.AuditTrail
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations.Schema

Namespace dxTestSolution.Module.BusinessObjects

    <DefaultClassOptions>
    Public Class Contact
        Inherits BaseObject

        Public Overridable Property FirstName As String

        Public Overridable Property LastName As String

        Public Overridable Property Age As Integer

        Public Overridable Property BirthDate As Date

        Public Overridable Property MyTasks As ObservableCollection(Of MyTask) = New ObservableCollection(Of MyTask)()

        '[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        '[NotMapped]
        'public virtual IList<AuditDataItemPersistent> ChangeHistory {
        '    get { return AuditDataItemPersistent.GetAuditTrail(ObjectSpace, this); }
        '}
        Private auditTrailField As BindingList(Of CustomAuditDataItem)

        <CollectionOperationSet(AllowAdd:=False, AllowRemove:=False)>
        <NotMapped>
        Public Overridable ReadOnly Property AuditTrail As BindingList(Of CustomAuditDataItem)
            Get
                If auditTrailField Is Nothing Then
                    auditTrailField = New BindingList(Of CustomAuditDataItem)()
                    auditTrailField.AllowEdit = False
                    auditTrailField.AllowNew = False
                    auditTrailField.AllowRemove = False
                    Dim rootItems As IList(Of AuditDataItemPersistent) = AuditDataItemPersistent.GetAuditTrail(ObjectSpace, Me)
                    If rootItems IsNot Nothing Then
                        For Each entry As AuditDataItemPersistent In rootItems
                            auditTrailField.Add(New CustomAuditDataItem(entry, "Person"))
                        Next
                    End If

                    For Each task As MyTask In MyTasks
                        Dim taskItems As IList(Of AuditDataItemPersistent) = AuditDataItemPersistent.GetAuditTrail(ObjectSpace, task)
                        If taskItems IsNot Nothing Then
                            For Each entry As AuditDataItemPersistent In taskItems
                                auditTrailField.Add(New CustomAuditDataItem(entry, "Task - " & task.ID.ToString() & ", " & task.Subject))
                            Next
                        End If
                    Next
                End If

                Return auditTrailField
            End Get
        End Property
    End Class
End Namespace

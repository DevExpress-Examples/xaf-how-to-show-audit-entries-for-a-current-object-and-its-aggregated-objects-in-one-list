Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.BaseImpl

Namespace E4565.Module.BusinessObjects

    <DefaultClassOptions>
    Public Class MyPerson
        Inherits XPObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Property FullName As String
            Get
                Return GetPropertyValue(Of String)("FullName")
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)("FullName", value)
            End Set
        End Property

        <Association, Aggregated>
        Public ReadOnly Property Addresses As XPCollection(Of MyAddress)
            Get
                Return GetCollection(Of MyAddress)("Addresses")
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

                    For Each address As MyAddress In Addresses
                        Dim addressItems As XPCollection(Of AuditDataItemPersistent) = AuditedObjectWeakReference.GetAuditTrail(Session, address)
                        If addressItems IsNot Nothing Then
                            For Each entry As AuditDataItemPersistent In addressItems
                                auditTrailField.Add(New CustomAuditDataItem(entry, "Address - " & address.Oid.ToString() & ", " & address.AddressLine))
                            Next
                        End If
                    Next
                End If

                Return auditTrailField
            End Get
        End Property
    End Class
End Namespace

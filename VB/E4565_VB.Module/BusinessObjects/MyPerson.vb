Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.BaseImpl

<DefaultClassOptions()> _
Public Class MyPerson
    Inherits XPObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Property FullName() As String
        Get
            Return GetPropertyValue(Of String)("FullName")
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullName", value)
        End Set
    End Property
    <Association(), Aggregated()> _
    Public ReadOnly Property Addresses() As XPCollection(Of MyAddress)
        Get
            Return GetCollection(Of MyAddress)("Addresses")
        End Get
    End Property
    Private auditTrail_Renamed As BindingList(Of CustomAuditDataItem)
    Public ReadOnly Property AuditTrail() As BindingList(Of CustomAuditDataItem)
        Get
            If auditTrail_Renamed Is Nothing Then
                auditTrail_Renamed = New BindingList(Of CustomAuditDataItem)()
                auditTrail_Renamed.AllowEdit = False
                auditTrail_Renamed.AllowNew = False
                auditTrail_Renamed.AllowRemove = False
                Dim rootItems As XPCollection(Of AuditDataItemPersistent) = AuditedObjectWeakReference.GetAuditTrail(Session, Me)
                If rootItems IsNot Nothing Then
                    For Each entry As AuditDataItemPersistent In rootItems
                        auditTrail_Renamed.Add(New CustomAuditDataItem(entry, "Person"))
                    Next entry
                End If
                For Each address As MyAddress In Addresses
                    Dim addressItems As XPCollection(Of AuditDataItemPersistent) = AuditedObjectWeakReference.GetAuditTrail(Session, address)
                    If addressItems IsNot Nothing Then
                        For Each entry As AuditDataItemPersistent In addressItems
                            auditTrail_Renamed.Add(New CustomAuditDataItem(entry, "Address - " & address.Oid.ToString() & ", " & address.AddressLine))
                        Next entry
                    End If
                Next address
            End If
            Return auditTrail_Renamed
        End Get
    End Property
End Class
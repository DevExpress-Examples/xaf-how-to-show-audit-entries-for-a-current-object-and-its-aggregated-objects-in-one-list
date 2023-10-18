Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl.EFCore.AuditTrail

Namespace dxTestSolution.Module.BusinessObjects

    <DomainComponent>
    Public Class CustomAuditDataItem
        Inherits NonPersistentBaseObject

        Private sourceAuditDataItem As AuditDataItemPersistent

        Private targetObjectNameField As String

        Public Sub New(ByVal sourceAuditDataItem As AuditDataItemPersistent, ByVal targetObjectName As String)
            Me.sourceAuditDataItem = sourceAuditDataItem
            targetObjectNameField = targetObjectName
        End Sub

        Public ReadOnly Property TargetObjectName As String
            Get
                Return targetObjectNameField
            End Get
        End Property

        Public ReadOnly Property Description As String
            Get
                Return sourceAuditDataItem.Description
            End Get
        End Property

        Public ReadOnly Property ModifiedOn As Date
            Get
                Return sourceAuditDataItem.ModifiedOn
            End Get
        End Property

        Public ReadOnly Property NewValue As String
            Get
                Return sourceAuditDataItem.NewValue
            End Get
        End Property

        Public ReadOnly Property OldValue As String
            Get
                Return sourceAuditDataItem.OldValue
            End Get
        End Property

        Public ReadOnly Property OperationType As String
            Get
                Return sourceAuditDataItem.OperationType
            End Get
        End Property

        Public ReadOnly Property PropertyName As String
            Get
                Return sourceAuditDataItem.PropertyName
            End Get
        End Property

        Public ReadOnly Property UserName As String
            Get
                Return sourceAuditDataItem.UserName
            End Get
        End Property
    End Class
End Namespace

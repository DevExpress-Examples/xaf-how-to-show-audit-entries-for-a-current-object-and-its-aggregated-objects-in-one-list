',new MyUpdater(objectSpace,versionFromDB)
'            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/Contact_ListView", SecurityPermissionState.Allow);
'defaultRole.AddTypePermissionsRecursively<Contact>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports dxTestSolution.Module.BusinessObjects
Imports System

Namespace ExtendAuditEF.Module.DatabaseUpdate

    ' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    Public Class MyUpdater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            ',new MyUpdater(objectSpace,versionFromDB)
            'string name = "MyName";
            'DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            'if(theObject == null) {
            '    theObject = ObjectSpace.CreateObject<DomainObject1>();
            '    theObject.Name = name;
            '}
            Dim cnt = ObjectSpace.GetObjects(Of Contact)().Count
            If cnt > 0 Then
                Return
            End If

            For i As Integer = 0 To 5 - 1
                Dim contact = ObjectSpace.CreateObject(Of Contact)()
                contact.FirstName = "FirstName" & i
                contact.LastName = "LastName" & i
                contact.Age = i * 10
                For j As Integer = 0 To 2 - 1
                    Dim task = ObjectSpace.CreateObject(Of MyTask)()
                    task.Subject = "Subject" & i & " - " & j
                    task.AssignedTo = contact
                Next
            Next

            'secur#0  
            ObjectSpace.CommitChanges() 'Uncomment this line to persist created object(s).
        End Sub

        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
        'if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        '}
        End Sub
    End Class
End Namespace

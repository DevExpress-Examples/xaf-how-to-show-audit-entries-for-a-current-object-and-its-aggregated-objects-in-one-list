Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating

Namespace ExtendAuditEF.Module.DatabaseUpdate

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
        'string name = "MyName";
        'EntityObject1 theObject = ObjectSpace.FirstOrDefault<EntityObject1>(u => u.Name == name);
        'if(theObject == null) {
        '    theObject = ObjectSpace.CreateObject<EntityObject1>();
        '    theObject.Name = name;
        '}
        'ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
        End Sub

        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
        End Sub
    End Class
End Namespace

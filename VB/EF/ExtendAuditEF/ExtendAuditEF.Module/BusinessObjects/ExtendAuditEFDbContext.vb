Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Design
Imports DevExpress.ExpressApp.Design
Imports DevExpress.ExpressApp.EFCore.DesignTime
Imports DevExpress.Persistent.BaseImpl.EFCore.AuditTrail
Imports dxTestSolution.Module.BusinessObjects

Namespace ExtendAuditEF.Module.BusinessObjects

    ' This code allows our Model Editor to get relevant EF Core metadata at design time.
    ' For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
    Public Class ExtendAuditEFContextInitializer
        Inherits DbContextTypesInfoInitializerBase

        Protected Overrides Function CreateDbContext() As DbContext
            Dim optionsBuilder = New DbContextOptionsBuilder(Of ExtendAuditEFEFCoreDbContext)().UseSqlServer(";").UseChangeTrackingProxies().UseObjectSpaceLinkProxies()
            Return New ExtendAuditEFEFCoreDbContext(optionsBuilder.Options)
        End Function
    End Class

    'This factory creates DbContext for design-time services. For example, it is required for database migration.
    Public Class ExtendAuditEFDesignTimeDbContextFactory
        Implements IDesignTimeDbContextFactory(Of ExtendAuditEFEFCoreDbContext)

        Public Function CreateDbContext(ByVal args As String()) As ExtendAuditEFEFCoreDbContext Implements IDesignTimeDbContextFactory(Of ExtendAuditEFEFCoreDbContext).CreateDbContext
            Throw New InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.")
        'var optionsBuilder = new DbContextOptionsBuilder<ExtendAuditEFEFCoreDbContext>();
        'optionsBuilder.UseSqlServer("Integrated Security=SSPI;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ExtendAuditEF");
        'optionsBuilder.UseChangeTrackingProxies();
        'optionsBuilder.UseObjectSpaceLinkProxies();
        'return new ExtendAuditEFEFCoreDbContext(optionsBuilder.Options);
        End Function
    End Class

    <TypesInfoInitializer(GetType(ExtendAuditEFContextInitializer))>
    Public Class ExtendAuditEFEFCoreDbContext
        Inherits DbContext

        Public Sub New(ByVal options As DbContextOptions(Of ExtendAuditEFEFCoreDbContext))
            MyBase.New(options)
        End Sub

        'public DbSet<ModuleInfo> ModulesInfo { get; set; }
        Public Property AuditData As DbSet(Of AuditDataItemPersistent)

        Public Property AuditEFCoreWeakReference As DbSet(Of AuditEFCoreWeakReference)

        Public Property MyTasks As DbSet(Of MyTask)

        Public Property Contacts As DbSet(Of Contact)

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As ModelBuilder)
            MyBase.OnModelCreating(modelBuilder)
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues)
            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.AuditItems).WithOne(Function(p) p.AuditedObject)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.OldItems).WithOne(Function(p) p.OldObject)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.NewItems).WithOne(Function(p) p.NewObject)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.UserItems).WithOne(Function(p) p.UserObject)
        End Sub
    End Class

    Public Class ExtendAuditEFAuditingDbContext
        Inherits DbContext

        Public Sub New(ByVal options As DbContextOptions(Of ExtendAuditEFAuditingDbContext))
            MyBase.New(options)
        End Sub

        Public Property AuditData As DbSet(Of AuditDataItemPersistent)

        Public Property AuditEFCoreWeakReference As DbSet(Of AuditEFCoreWeakReference)

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As ModelBuilder)
            MyBase.OnModelCreating(modelBuilder)
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.AuditItems).WithOne(Function(p) p.AuditedObject)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.OldItems).WithOne(Function(p) p.OldObject)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.NewItems).WithOne(Function(p) p.NewObject)
            modelBuilder.Entity(Of AuditEFCoreWeakReference)().HasMany(Function(p) p.UserItems).WithOne(Function(p) p.UserObject)
        End Sub
    End Class
End Namespace

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace dxTestSolution.Module.BusinessObjects;
[DefaultClassOptions]
public class Contact : BaseObject {
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual int Age { get; set; }
    public virtual DateTime BirthDate { get; set; }

    public virtual ObservableCollection<MyTask> MyTasks { get; set; } = new ObservableCollection<MyTask>();

    private BindingList<CustomAuditDataItem> auditTrail;
    [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
    [NotMapped]
    public virtual BindingList<CustomAuditDataItem> AuditTrail {
        get {
            if (auditTrail == null) {
                auditTrail = new BindingList<CustomAuditDataItem>();
                auditTrail.AllowEdit = false;
                auditTrail.AllowNew = false;
                auditTrail.AllowRemove = false;
                IList<AuditDataItemPersistent> rootItems = AuditDataItemPersistent.GetAuditTrail(ObjectSpace, this);
                if (rootItems != null) {
                    foreach (AuditDataItemPersistent entry in rootItems) {
                        auditTrail.Add(new CustomAuditDataItem(entry, "Contact"));
                    }
                }
                foreach (MyTask task in MyTasks) {
                    IList<AuditDataItemPersistent> taskItems = AuditDataItemPersistent.GetAuditTrail(ObjectSpace, task);
                    if (taskItems != null) {
                        foreach (AuditDataItemPersistent entry in taskItems) {
                            auditTrail.Add(new CustomAuditDataItem(entry, "Task - " +  task.Subject));
                        }
                    }
                }
            }
            return auditTrail;
        }
    }
}




using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxTestSolution.Module.BusinessObjects;
[DefaultClassOptions]
public class Contact : BaseObject {
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual int Age { get; set; }
    public virtual DateTime BirthDate { get; set; }

    public virtual ObservableCollection<MyTask> MyTasks { get; set; } = new ObservableCollection<MyTask>();

    //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
    //[NotMapped]
    //public virtual IList<AuditDataItemPersistent> ChangeHistory {
    //    get { return AuditDataItemPersistent.GetAuditTrail(ObjectSpace, this); }
    //}

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
                        auditTrail.Add(new CustomAuditDataItem(entry, "Person"));
                    }
                }
                foreach (MyTask task in MyTasks) {
                    IList<AuditDataItemPersistent> taskItems = AuditDataItemPersistent.GetAuditTrail(ObjectSpace, task);
                    if (taskItems != null) {
                        foreach (AuditDataItemPersistent entry in taskItems) {
                            auditTrail.Add(new CustomAuditDataItem(entry, "Task - " + task.ID.ToString() + ", " + task.Subject));
                        }
                    }
                }
            }
            return auditTrail;
        }
    }
}

 


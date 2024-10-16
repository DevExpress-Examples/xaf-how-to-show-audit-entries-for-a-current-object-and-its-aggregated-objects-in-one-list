<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128593609/23.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4565)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->


<!-- default file list end -->
# XAF - How to show audit entries for a current object and its aggregated objects in one list

This example demonstrated how to show audit information about a current object and each object in its collection property:
<kbd>![image](https://github.com/DevExpress-Examples/XAF_how-to-show-audit-entries-for-a-current-object-and-its-aggregated-objects-in-one-list-e4565/assets/14300209/cc683e55-8757-474b-958f-004e8cf3a291)</kbd>

## Implementation Details
1. Introduce a new non-persistent CustomAuditDataItem class that is used to display the audit information:

```cs
    [DomainComponent]
    public class CustomAuditDataItem {
        private AuditDataItemPersistent sourceAuditDataItem;
        private string targetObjectName;
        public CustomAuditDataItem(AuditDataItemPersistent sourceAuditDataItem, string targetObjectName) {
            this.sourceAuditDataItem = sourceAuditDataItem;
            this.targetObjectName = targetObjectName;
        }
        public string TargetObjectName { get { return targetObjectName; } }
        public string Description { get { return sourceAuditDataItem.Description; } }
        public DateTime ModifiedOn { get { return sourceAuditDataItem.ModifiedOn; } }
        public string NewValue { get { return sourceAuditDataItem.NewValue; } }
        public string OldValue { get { return sourceAuditDataItem.OldValue; } }
        public string OperationType { get { return sourceAuditDataItem.OperationType; } }
        public string PropertyName { get { return sourceAuditDataItem.PropertyName; } }
        public string UserName { get { return sourceAuditDataItem.UserName; } }
    }

```

2. In the business class, introduce a new collection property to return an extended list of required audit entries:

```cs
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

```

## Files to Review

* [CustomAuditDataItem.cs](CS/EF/ExtendAuditEF/ExtendAuditEF.Module/BusinessObjects/CustomAuditDataItem.cs)
* [Contact.cs](CS/EF/ExtendAuditEF/ExtendAuditEF.Module/BusinessObjects/Contact.cs)

<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-show-audit-entries-for-a-current-object-and-its-aggregated-objects-in-one-list&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-show-audit-entries-for-a-current-object-and-its-aggregated-objects-in-one-list&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->

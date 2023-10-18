<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128593609/23.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4565)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomAuditDataItem.cs](CS/EF/ExtendAuditEF/ExtendAuditEF.Module/BusinessObjects/CustomAuditDataItem.cs)
* [Contact.cs](CS/EF/ExtendAuditEF/ExtendAuditEF.Module/BusinessObjects/Contact.cs)

<!-- default file list end -->
# How to show audit entries for a current object and its aggregated objects in one list


<p>The AuditTrail module is not designed to show audit information about a current object and each object from its collection property.</p><p>However, you can implement this behavior manually, and this example demonstrates how to do this:</p><br />
<p>1. Introduce a new collection property to return an extended list with necessary audit entries:</p>

```cs
    public class MyPerson : XPObject {
        public MyPerson(Session session) : base(session) { }
        public string FullName {
            get { return GetPropertyValue<string>("FullName"); }
            set { SetPropertyValue<string>("FullName", value); }
        }
        [Association, Aggregated]
        public XPCollection<MyAddress> Addresses {
            get { return GetCollection<MyAddress>("Addresses"); }
        }
        private BindingList<CustomAuditDataItem> auditTrail;
        public BindingList<CustomAuditDataItem> AuditTrail {
            get {
                if(auditTrail == null) {
                    auditTrail = new BindingList<CustomAuditDataItem>();
                    auditTrail.AllowEdit = false;
                    auditTrail.AllowNew = false;
                    auditTrail.AllowRemove = false;
                    XPCollection<AuditDataItemPersistent> rootItems = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                    if(rootItems != null) {
                        foreach(AuditDataItemPersistent entry in rootItems) {
                            auditTrail.Add(new CustomAuditDataItem(entry, "Person"));
                        }
                    }
                    foreach(MyAddress address in Addresses) {
                        XPCollection<AuditDataItemPersistent> addressItems = AuditedObjectWeakReference.GetAuditTrail(Session, address);
                        if(addressItems != null) {
                            foreach(AuditDataItemPersistent entry in addressItems) {
                                auditTrail.Add(new CustomAuditDataItem(entry, "Address - " + address.Oid.ToString() + ", " + address.AddressLine));
                            }
                        }
                    }
                }
                return auditTrail;
            }
        }
    }

```

<p>2. The standard AuditDataItemPersistent class is not designed to show information about a related object, so introduce a new CustomAuditDataItem class with an additional property:</p>

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

<p>As a result, the application will look as follows (in addition, I have customized the layout to demonstrate items from the 'Addresses' and 'Audit' properties at once):

<br/>



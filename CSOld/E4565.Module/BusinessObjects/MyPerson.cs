using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.BaseImpl;

namespace E4565.Module.BusinessObjects {
    [DefaultClassOptions]
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
}

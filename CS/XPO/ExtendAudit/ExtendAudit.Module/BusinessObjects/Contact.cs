using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;

using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using System.Diagnostics;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.SystemModule;

namespace dxTestSolution.Module.BusinessObjects {
     [DefaultClassOptions]
	  
    public class Contact : BaseObject { 
        public Contact(Session session)
            : base(session) {
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
        }
        string _firstName;
        public string FirstName {
            get {
                return _firstName;
            }
            set {
                SetPropertyValue(nameof(FirstName), ref _firstName, value);
            }
        }
        string _lastName;
        public string LastName {
            get {
                return _lastName;
            }
            set {
                SetPropertyValue(nameof(LastName), ref _lastName, value);
            }
        }
		int _age;
        public int Age {
            get {
                return _age;
            }
            set {
                SetPropertyValue(nameof(Age), ref _age, value);
            }
        }
  	
        [Association("Contact-Tasks")]
        public XPCollection<MyTask> Tasks {
            get {
                return GetCollection<MyTask>(nameof(Tasks));
            }
        }

   

        private BindingList<CustomAuditDataItem> auditTrail;
        public BindingList<CustomAuditDataItem> AuditTrail {
            get {
                if (auditTrail == null) {
                    auditTrail = new BindingList<CustomAuditDataItem>();
                    auditTrail.AllowEdit = false;
                    auditTrail.AllowNew = false;
                    auditTrail.AllowRemove = false;
                    XPCollection<AuditDataItemPersistent> rootItems = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                    if (rootItems != null) {
                        foreach (AuditDataItemPersistent entry in rootItems) {
                            auditTrail.Add(new CustomAuditDataItem(entry, "Person"));
                        }
                    }
                    foreach (MyTask task in Tasks) {
                        XPCollection<AuditDataItemPersistent> taskItems = AuditedObjectWeakReference.GetAuditTrail(Session, task);
                        if (taskItems != null) {
                            foreach (AuditDataItemPersistent entry in taskItems) {
                                auditTrail.Add(new CustomAuditDataItem(entry, "Task - " + task.Oid.ToString() + ", " + task.Subject));
                            }
                        }
                    }
                }
                return auditTrail;
            }
        }
    }
}
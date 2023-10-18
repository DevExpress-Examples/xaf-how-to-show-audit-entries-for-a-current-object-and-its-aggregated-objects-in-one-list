using System;
using System.Collections.Generic;
using System.Text;

using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;

namespace dxTestSolution.Module.BusinessObjects {
    [ DomainComponent]
    public class CustomAuditDataItem: NonPersistentBaseObject {
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
}

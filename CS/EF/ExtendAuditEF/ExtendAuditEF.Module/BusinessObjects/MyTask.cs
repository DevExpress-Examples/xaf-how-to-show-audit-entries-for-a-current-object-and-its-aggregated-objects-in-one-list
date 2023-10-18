using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace dxTestSolution.Module.BusinessObjects;

[DefaultClassOptions]
public class MyTask : BaseObject {
    public virtual string Subject{get;set;}
    public virtual Contact AssignedTo{ get; set; }
}

 


using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;

namespace E4565.Module.BusinessObjects {
    [DomainComponent]
    public class MyAddress : XPObject {
        public MyAddress(Session session) : base(session) { }
        public string AddressLine {
            get { return GetPropertyValue<string>("AddressLine"); }
            set { SetPropertyValue<string>("AddressLine", value); }
        }
        [Association]
        public MyPerson Person {
            get { return GetPropertyValue<MyPerson>("Person"); }
            set { SetPropertyValue<MyPerson>("Person", value); }
        }
    }
}

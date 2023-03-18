//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyCalendar.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int EventID { get; set; }
        public Nullable<System.Guid> CreationID { get; set; }
        public Nullable<System.Guid> RecurID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public Nullable<System.DateTime> RecurEnd { get; set; }
        public Nullable<bool> IsFullDay { get; set; }
        public Nullable<bool> IsRecur { get; set; }
        public string RecurType { get; set; }
        public string ThemeColor { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string EventType { get; set; }
        public string EventLocation { get; set; }
    }
}

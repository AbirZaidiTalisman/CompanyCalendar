using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyCalendar.Models
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        public Nullable<System.Guid> RecurID { get; set; }
        public string Subject { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public Nullable<System.DateTime> RecurEnd { get; set; }
        public Nullable<bool> IsFullDay { get; set; }
        public Nullable<bool> IsRecur { get; set; }
        public string RecurType { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string Description { get; set; }
        public string ThemeColor { get; set; }
        public string EventType { get; set; }
        public string EventLocation { get; set; }
        public string [] selectedValues { get; set; }
        public string editType { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyCalendar.Models
{
    public class CheckBoxModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

    public class CheckBoxList
    {
        public List<CheckBoxModel> CheckBoxItems { get; set; }
    }
}
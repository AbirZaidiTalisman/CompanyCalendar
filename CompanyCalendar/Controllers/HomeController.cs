using CompanyCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CompanyCalendar.Controllers
{
    public class HomeController : Controller
    {

        CalendarDBEntities1 gdc = new CalendarDBEntities1();
        public ActionResult Index()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];


            if (getCookie != null)
            {
                int cookieUserID = Convert.ToInt32(getCookie["UserID"]);
                var details = gdc.Users.Where(u => u.UserID == cookieUserID).FirstOrDefault();
                ViewBag.uname = getCookie["email"].ToString();
                ViewBag.uid = getCookie["UserID"].ToString();

                if (details != null)
                {
                    CalendarDBEntities1 dc1 = new CalendarDBEntities1();

                    var users = dc1.Users.ToList();
                    List<CheckBoxModel> participantlist = new List<CheckBoxModel>();


                    foreach (var user in users)
                    {

                        participantlist.Add(new CheckBoxModel { Value = user.UserID, Text = user.UserName, IsChecked = false });
                    };

                    ViewBag.party = participantlist;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }


        public ActionResult LeaveCalendar()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];


            if (getCookie != null)
            {
                int cookieUserID = Convert.ToInt32(getCookie["UserID"]);
                var details = gdc.Users.Where(u => u.UserID == cookieUserID).FirstOrDefault();
                ViewBag.uname = getCookie["email"].ToString();
                ViewBag.uid = getCookie["UserID"].ToString();

                if (details != null)
                {
                    CalendarDBEntities1 dc1 = new CalendarDBEntities1();

                    var users = dc1.Users.ToList();
                    List<CheckBoxModel> participantlist = new List<CheckBoxModel>();


                    foreach (var user in users)
                    {

                        participantlist.Add(new CheckBoxModel { Value = user.UserID, Text = user.UserName, IsChecked = false });
                    };

                    ViewBag.party = participantlist;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


            //CalendarDBEntities1 dc1 = new CalendarDBEntities1();

            //var users = dc1.Users.ToList();
            //List<CheckBoxModel> participantlist = new List<CheckBoxModel>();


            //foreach (var user in users)
            //{

            //    participantlist.Add(new CheckBoxModel { Value = user.UserID, Text = user.UserName, IsChecked = false });
            //};

            //ViewBag.party = participantlist;
            //return View();
        }

        public ActionResult MyCalendar()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];


            if (getCookie != null)
            {
                int cookieUserID = Convert.ToInt32(getCookie["UserID"]);
                var details = gdc.Users.Where(u => u.UserID == cookieUserID).FirstOrDefault();
                ViewBag.uname = getCookie["email"].ToString();
                ViewBag.uid = getCookie["UserID"].ToString();

                if (details != null)
                {
                    CalendarDBEntities1 dc1 = new CalendarDBEntities1();

                    var users = dc1.Users.ToList();
                    List<CheckBoxModel> participantlist = new List<CheckBoxModel>();


                    foreach (var user in users)
                    {

                        participantlist.Add(new CheckBoxModel { Value = user.UserID, Text = user.UserName, IsChecked = false });
                    };

                    ViewBag.party = participantlist;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }



            //var users = gdc.Users.ToList();
            //List<CheckBoxModel> participantlist = new List<CheckBoxModel>();


            //foreach (var user in users)
            //{

            //    participantlist.Add(new CheckBoxModel { Value = user.UserID, Text = user.UserName, IsChecked = false });
            //};

            //ViewBag.party = participantlist;
            //return View();
        }

        //office calendar Controller
        public JsonResult GetEvents()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];
            int cookieUserID = 0;

            if (getCookie != null)
            {
                cookieUserID = Convert.ToInt32(getCookie["UserID"]);
            }

            using (CalendarDBEntities1 dc = new CalendarDBEntities1())
            {
                //var events = dc.Events.ToList();
                var events = dc.GetEventsByID(cookieUserID, "CompanyCalendar").ToList();

                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        //My calander Controller
        public JsonResult GetEventsMyCalendar()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];
            int cookieUserID = 0;


            if (getCookie != null)
            {
                cookieUserID = Convert.ToInt32(getCookie["UserID"]);
            }

            using (CalendarDBEntities1 dc = new CalendarDBEntities1())
            {
                //var events = dc.Events.ToList();
                var events = dc.GetEventsByID(cookieUserID, "MyCalendar").ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        //Leave calander Controller
        public JsonResult GetEventsLeaveCalendar()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];
            int cookieUserID = 0;

            if (getCookie != null)
            {
                cookieUserID = Convert.ToInt32(getCookie["UserID"]);
            }

            using (CalendarDBEntities1 dc = new CalendarDBEntities1())
            {
                //var events = dc.Events.ToList();
                var events = dc.GetEventsByID(cookieUserID, "LeaveCalendar").ToList();

                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(EventViewModel vm)
        {
            Event e = new Event();
            e.Subject = vm.Subject;
            e.EventID = vm.EventID;
            e.Start = vm.Start;
            e.End = vm.End;
            e.Description = vm.Description;
            e.IsFullDay = vm.IsFullDay;
            //e.ThemeColor = vm.ThemeColor;
            e.CreatedOn = DateTime.UtcNow.Date;
            e.EventType = vm.EventType;
            e.EventLocation = vm.EventLocation;
            e.RecurID = vm.RecurID;
            e.IsRecur = vm.IsRecur;
            e.RecurEnd = vm.RecurEnd;
            e.RecurType = vm.RecurType;



            HttpCookie getCookie = Request.Cookies["LoggedIn"];
            int cookieUserID = 0;

            if (getCookie != null)
            {
                cookieUserID = Convert.ToInt32(getCookie["UserID"]);
            }

            var status = false;
            using (CalendarDBEntities1 dc = new CalendarDBEntities1())
            {
                Event returnE = new Event();
                if (e.EventID > 0)
                {
                    //Update the event

                    if (vm.editType == "onward")
                    {
                        if (e.IsRecur == true)
                        {
                            //Delete previous Recur events
                            Event[] getIDs = gdc.Events.Where(a => a.EventID >= e.EventID && a.RecurID == e.RecurID).ToArray();

                            foreach (Event getID in getIDs)
                            {
                                DeleteEvent(getID.EventID.ToString());
                            }

                            //Create edit events newly
                            returnE = NewEvent(e, true);

                            int maxEventID = 0;
                            var gID = gdc.Events.Where(a => a.CreationID == returnE.CreationID).FirstOrDefault();
                            maxEventID = gID.EventID;

                            try
                            {
                                if (!(vm.selectedValues.Length < 1 || vm.selectedValues == null))
                                {
                                    SaveMembers(vm.selectedValues, maxEventID, vm.EventType, vm.editType);
                                }
                            }
                            catch
                            {

                            }

                        }
                        else
                        {
                            returnE = UpdateEvent(e);

                            int maxEventID = 0;
                            //var getID = gdc.Events.Where(a => a.CreationID == returnE.CreationID).FirstOrDefault();
                            maxEventID = returnE.EventID;

                            try
                            {
                                if (!(vm.selectedValues.Length < 1 || vm.selectedValues == null))
                                {
                                    SaveMembers(vm.selectedValues, maxEventID, vm.EventType, vm.editType);
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                    else
                    {
                        returnE = UpdateEvent(e);

                        int maxEventID = 0;
                        //var getID = gdc.Events.Where(a => a.CreationID == returnE.CreationID).FirstOrDefault();
                        maxEventID = returnE.EventID;

                        try
                        {
                            if (!(vm.selectedValues.Length < 1 || vm.selectedValues == null))
                            {
                                SaveMembers(vm.selectedValues, maxEventID, vm.EventType, vm.editType);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    //New event insert
                    returnE = NewEvent(e, false);

                    int maxEventID = 0;
                    var getID = gdc.Events.Where(a => a.CreationID == returnE.CreationID).FirstOrDefault();
                    maxEventID = getID.EventID;

                    try
                    {
                        if (!(vm.selectedValues.Length < 1 || vm.selectedValues == null))
                        {
                            SaveMembers(vm.selectedValues, maxEventID, vm.EventType, vm.editType);
                        }
                    }
                    catch
                    {

                    }

                }

                status = true;

                //int maxEventID = 0;


                //if (returnE.EventID == 0)
                //{
                //    var getID = gdc.Events.Where(a => a.CreationID == returnE.CreationID).FirstOrDefault();
                //    maxEventID = getID.EventID;
                //}
                //else
                //{
                //    maxEventID = vm.EventID;
                //}

                //try
                //{
                //    if (!(vm.selectedValues.Length < 1 || vm.selectedValues == null))
                //    {
                //        SaveMembers(vm.selectedValues, maxEventID, vm.EventType, vm.editType );
                //    }
                //}
                //catch
                //{

                //}

            }
            return new JsonResult { Data = new { status = status } };
        }

        Event NewEvent(Event e, bool isUpdate)
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];
            int cookieUserID = 0;

            if (getCookie != null)
            {
                cookieUserID = Convert.ToInt32(getCookie["UserID"]);
            }


            Guid creationID = Guid.NewGuid();
            e.CreationID = creationID;

            if (e.IsRecur == true && isUpdate == false)
            {
                Guid newRId = Guid.NewGuid();
                e.RecurID = newRId;

            }
            else if (e.IsRecur == true && isUpdate == true)
            {
                if (e.RecurID == null)
                {
                    Guid newRId = Guid.NewGuid();
                    e.RecurID = newRId;
                }
                else
                {
                    e.RecurID = e.RecurID;
                }
            }
            else
            {
                e.RecurID = null;
            }

            if (e.EventType == "Meeting")
            {
                e.CreatedBy = cookieUserID;
                e.ThemeColor = "#e6e6ff";
                e.CreatedOn = DateTime.UtcNow.Date;

                // dc.Events.Add(e);
            }
            else if (e.EventType == "Leave")
            {
                e.CreatedBy = cookieUserID;
                e.Subject = "On Leave";
                e.CreatedOn = DateTime.UtcNow.Date;
                e.EventLocation = null;
                e.ThemeColor = "#ffe6f7";
                //  dc.Events.Add(e);
            }
            else if (e.EventType == "Personal")
            {
                e.CreatedBy = cookieUserID;
                e.CreatedOn = DateTime.UtcNow.Date;
                e.EventLocation = null;
                e.ThemeColor = "#ffe6e6";
                // dc.Events.Add(e);
            }
            else
            {
                e.CreatedBy = cookieUserID;
                e.CreatedOn = DateTime.UtcNow.Date;
                //dc.Events.Add(e);
            }

            gdc.Events.Add(e);
            gdc.SaveChanges();

            return e;
        }
        Event UpdateEvent(Event e)
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];
            int cookieUserID = 0;

            if (getCookie != null)
            {
                cookieUserID = Convert.ToInt32(getCookie["UserID"]);
            }

            var v = gdc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
            if (v != null)
            {
                if (e.EventType == "Leave")
                {
                    v.Subject = "On Leave";
                }
                else
                {
                    v.Subject = e.Subject;
                }

                v.Start = e.Start;
                v.End = e.End;
                v.Description = e.Description;
                v.IsFullDay = e.IsFullDay;

                if (e.EventType == "Leave")
                {
                    v.ThemeColor = "#ffe6f7";
                }
                else if (e.EventType == "Personal")
                {
                    v.ThemeColor = "#ffebe6";
                }
                else if (e.EventType == "Meeting")
                {
                    v.ThemeColor = "#e6e6ff";
                }
                else
                {
                    v.ThemeColor = e.ThemeColor;
                }

                v.CreatedBy = cookieUserID;
                v.CreatedOn = DateTime.UtcNow.Date;
                v.EventType = e.EventType;

                if (e.EventType == "Leave")
                {
                    v.EventLocation = null;
                }
                else if (e.EventType == "Personal")
                {
                    v.EventLocation = null;
                }
                else
                {
                    v.EventLocation = e.EventLocation;
                }
                gdc.SaveChanges();


            }
            return v;
        }

        [HttpPost]
        public JsonResult DeleteEvent(string eventID)
        {
            var status = false;
            using (CalendarDBEntities1 dc = new CalendarDBEntities1())
            {
                int id = Convert.ToInt32(eventID);
                var v = dc.Events.Where(a => a.EventID == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult RecurEventRemove(int EventID, string removeType, Guid RecurID)
        {
            var status = false;
            if (removeType == "onwardRemove")
            {
                Event[] getIDs = gdc.Events.Where(a => a.EventID >= EventID && a.RecurID == RecurID).ToArray();

                foreach (Event getID in getIDs)
                {
                    DeleteEvent(getID.EventID.ToString());
                }
            }
            else
            {
                DeleteEvent(EventID.ToString());
            }

            status = true;
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult EditEvent(string eventID)
        {
            var parties = gdc.Participants.ToList();
            string[] parId;
            List<string> parList = new List<string>();
            foreach (var par in parties)
            {
                if (par.EventID.ToString() == eventID)
                {
                    parList.Add(par.EmpID.ToString());
                }
            }
            parId = parList.ToArray();




            return new JsonResult { Data = parId };
        }

        [HttpPost]
        public void SaveMembers(string[] selectedValues, int eventsId, string type, string editType)
        {
            try
            {
                var eventData = gdc.Events.Where(e => e.EventID == eventsId).FirstOrDefault();
                bool isRecur = eventData.IsRecur.Value;

                if (isRecur)
                {
                    //Event [] eventList =  ;

                    //if (editType== "onward")
                    //{
                    //    Guid recurID = eventData.RecurID.Value;
                    //    eventList = gdc.Events.Where(e => e.RecurID == recurID).ToArray();
                    //}
                    //else
                    //{

                    //}
                    Guid recurID = eventData.RecurID.Value;
                    var eventList = gdc.Events.Where(e => e.RecurID == recurID).ToList();

                    foreach (var e in eventList)
                    {
                        if (type == "Meeting")
                        {
                            if (e.EventID > 0)
                            {
                                var partyMembers = gdc.Participants.Where(u => u.EventID == e.EventID).ToList();

                                foreach (var partyMember in partyMembers)
                                {
                                    if (partyMember != null)
                                    {
                                        gdc.Participants.Remove(partyMember);
                                        gdc.SaveChanges();
                                    }
                                }

                                foreach (string selectedValue in selectedValues)
                                {
                                    User user = gdc.Users.Where(u => u.UserName == selectedValue).FirstOrDefault();

                                    var saveMembers = new Participant();
                                    saveMembers.EmpID = user.UserID;

                                    saveMembers.EventID = Convert.ToInt32(e.EventID);
                                    gdc.Participants.Add(saveMembers);
                                    gdc.SaveChanges();
                                }

                            }


                        }
                        if ((type == "Leave" || type == "Personal"))
                        {
                            if (eventsId > 0)
                            {
                                var partyMembers = gdc.Participants.Where(u => u.EventID == e.EventID).ToList();

                                foreach (var partyMember in partyMembers)
                                {
                                    if (partyMember != null)
                                    {
                                        gdc.Participants.Remove(partyMember);
                                        gdc.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                    if (type == "Meeting")
                    {
                        if (eventsId > 0)
                        {
                            var partyMembers = gdc.Participants.Where(u => u.EventID == eventsId).ToList();

                            foreach (var partyMember in partyMembers)
                            {
                                if (partyMember != null)
                                {
                                    gdc.Participants.Remove(partyMember);
                                    gdc.SaveChanges();
                                }
                            }

                            foreach (string selectedValue in selectedValues)
                            {
                                User user = gdc.Users.Where(u => u.UserName == selectedValue).FirstOrDefault();

                                var saveMembers = new Participant();
                                saveMembers.EmpID = user.UserID;

                                saveMembers.EventID = Convert.ToInt32(eventsId);
                                gdc.Participants.Add(saveMembers);
                                gdc.SaveChanges();
                            }

                        }


                    }
                    if ((type == "Leave" || type == "Personal"))
                    {
                        if (eventsId > 0)
                        {
                            var partyMembers = gdc.Participants.Where(u => u.EventID == eventsId).ToList();

                            foreach (var partyMember in partyMembers)
                            {
                                if (partyMember != null)
                                {
                                    gdc.Participants.Remove(partyMember);
                                    gdc.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
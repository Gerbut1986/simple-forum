namespace My_Forum_Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using My_Forum_Web.Models;
    using System.Collections.Generic;
    using System.Web;
    using System.IO;

    public class HomeController : Controller
    {
        static MyUser user { get; set; } = null;
        public static MyContext Db { get; set; }

        public HomeController() => Db = new MyContext();

        [HttpGet]
        public ActionResult Index(int? logout)
        {
            List<ForumMsg> list = Db.ForumMsgs.ToList();
            user = Db.Users.FirstOrDefault(log => log.Login == User.Identity.Name);
            try
            {
                // When user Logout:
                if (user == null)
                {
                    int[] mins = new int[list.Count];
                    string[] ns = new string[list.Count];
                    string[] uss = new string[list.Count];
                    ViewBag.currUser = 0;
                    List<MyUser> users = null;
                    if (users == null)
                    {
                        users = new List<MyUser>();
                        foreach (var f in list)
                        {
                            MyUser us;
                            us = Db.Users.Find(f.UserId);
                            users.Add(us);
                        }
                        ViewBag.outUsers = users;
                        for (int i = 0; i < list.Count; i++)
                        {
                            TimeSpan timePosted = DateTime.Now - list[i].Date_Added;
                            mins[i] = (int)timePosted.TotalMinutes;
                            int mid = list[i].UserId;
                            MyUser us = Db.Users.FirstOrDefault(u => u.Id == mid);
                            string fname = us.F_Name;
                            string lname = us.L_Name;
                            uss[i] = us.Login;
                            ns[i] = $"{fname} {lname}";
                        }
                        if (mins != null)
                        {
                            for (int i = 0; i < mins.Length; i++)
                                if (mins[i] < 60)
                                    ViewBag.TimePosted = $"{mins[i]} minutes ago.";
                                else if (mins[i] >= 60)
                                    ViewBag.TimePosted = $"{1} days ago";
                                else if (mins[i] > 43800)
                                    ViewBag.TimePosted = $"{1} month ago";
                        }
                        if (ns != null)
                            ViewBag.UserName = ns;
                        if (uss != null)
                            ViewBag.Nicknames = uss;
                    }
                    return View(list);
                }
                AccountController.CurrentUser = user.Id;
            }
            catch (NullReferenceException) { }
            // When user Log In:
            ViewBag.currUser = user.Id;
            int[] minutes = new int[list.Count];
            string[] names = new string[list.Count];
            string[] usernames = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                TimeSpan timePosted = DateTime.Now - list[i].Date_Added;
                minutes[i] = (int)timePosted.TotalMinutes;
                int mid = list[i].UserId;
                MyUser us = Db.Users.FirstOrDefault(u => u.Id == mid);
                string fname = us.F_Name;
                string lname = us.L_Name;
                usernames[i] = us.Login;
                names[i] = $"{fname} {lname}";
            }
            if (minutes != null)
            {
                for (int i = 0; i < minutes.Length; i++)
                    if (minutes[i] < 60)
                        ViewBag.TimePosted = $"{minutes[i]} minutes ago.";
                    else if (minutes[i] >= 60)
                        ViewBag.TimePosted = $"{1} days ago";
                    else if (minutes[i] > 43800)
                        ViewBag.TimePosted = $"{1} month ago";
            }
            if (names != null)
                ViewBag.UserName = names;
            if (usernames != null)
                ViewBag.Nicknames = usernames;

            return View(list);
        }

        [HttpPost]
        public ActionResult Index(ForumMsg entity)
        {
            List<ForumMsg> list = null;
            if (user == null || user.Id == 0)
            {
                ViewBag.EmptyMsg = "First you must Login to your account! Press Log in on right top corner.";
                list = Db.ForumMsgs.ToList();
                int[] mins = new int[list.Count];
                string[] ns = new string[list.Count];
                string[] uss = new string[list.Count];
                ViewBag.currUser = 0;
                List<MyUser> users = new List<MyUser>();
                    foreach (var f in list)
                    {
                        MyUser us;
                        us = Db.Users.Find(f.UserId);
                        users.Add(us);
                    }
                    ViewBag.outUsers = users;
                    for (int i = 0; i < list.Count; i++)
                    {
                        TimeSpan timePosted = DateTime.Now - list[i].Date_Added;
                        mins[i] = (int)timePosted.TotalMinutes;
                        int mid = list[i].UserId;
                        MyUser us = Db.Users.FirstOrDefault(u => u.Id == mid);
                        string fname = us.F_Name;
                        string lname = us.L_Name;
                        uss[i] = us.Login;
                        ns[i] = $"{fname} {lname}";
                    }
                    if (mins != null)
                    {
                        for (int i = 0; i < mins.Length; i++)
                            if (mins[i] < 60)
                                ViewBag.TimePosted = $"{mins[i]} minutes ago.";
                            else if (mins[i] >= 60)
                                ViewBag.TimePosted = $"{1} days ago";
                            else if (mins[i] > 43800)
                                ViewBag.TimePosted = $"{1} month ago";
                    }
                    if (ns != null)
                        ViewBag.UserName = ns;
                    if (uss != null)
                        ViewBag.Nicknames = uss;
                    return View(list);
            }
            else if (entity.Note == null)
            {
                ViewBag.EmptyMsg = "Empty text... Please make some post, and try again.";
                list = Db.ForumMsgs.ToList();
                // When user Log In:
                ViewBag.currUser = user.Id;
                int[] minutes = new int[list.Count];
                string[] names = new string[list.Count];
                string[] usernames = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    TimeSpan timePosted = DateTime.Now - list[i].Date_Added;
                    minutes[i] = (int)timePosted.TotalMinutes;
                    int mid = list[i].UserId;
                    MyUser us = Db.Users.FirstOrDefault(u => u.Id == mid);
                    string fname = us.F_Name;
                    string lname = us.L_Name;
                    usernames[i] = us.Login;
                    names[i] = $"{fname} {lname}";
                }
                if (minutes != null)
                {
                    for (int i = 0; i < minutes.Length; i++)
                        if (minutes[i] < 60)
                            ViewBag.TimePosted = $"{minutes[i]} minutes ago.";
                        else if (minutes[i] >= 60)
                            ViewBag.TimePosted = $"{1} days ago";
                        else if (minutes[i] > 43800)
                            ViewBag.TimePosted = $"{1} month ago";
                }
                if (names != null)
                    ViewBag.UserName = names;
                if (usernames != null)
                    ViewBag.Nicknames = usernames;
                return View(list);
            }
            else
            {
                string createdBy = entity.Note;
                if (createdBy != null)
                {
                    ForumMsg msg = null;
                    msg = new ForumMsg();
                    msg.Date_Added = DateTime.Now;
                    msg.Note = createdBy;
                    msg.UserId = AccountController.CurrentUser;

                    try
                    {
                        Db.ForumMsgs.Add(msg);
                        int res = Db.SaveChanges();
                        list = Db.ForumMsgs.ToList();
                        // When user Log In:
                        ViewBag.currUser = user.Id;
                        int[] minutes = new int[list.Count];
                        string[] names = new string[list.Count];
                        string[] usernames = new string[list.Count];
                        for (int i = 0; i < list.Count; i++)
                        {
                            TimeSpan timePosted = DateTime.Now - list[i].Date_Added;
                            minutes[i] = (int)timePosted.TotalMinutes;
                            int mid = list[i].UserId;
                            MyUser us = Db.Users.FirstOrDefault(u => u.Id == mid);
                            string fname = us.F_Name;
                            string lname = us.L_Name;
                            usernames[i] = us.Login;
                            names[i] = $"{fname} {lname}";
                        }
                        if (minutes != null)
                        {
                            for (int i = 0; i < minutes.Length; i++)
                                if (minutes[i] < 60)
                                    ViewBag.TimePosted = $"{minutes[i]} minutes ago.";
                                else if (minutes[i] >= 60)
                                    ViewBag.TimePosted = $"{1} days ago";
                                else if (minutes[i] > 43800)
                                    ViewBag.TimePosted = $"{1} month ago";
                        }
                        if (names != null)
                            ViewBag.UserName = names;
                        if (usernames != null)
                            ViewBag.Nicknames = usernames;
                        return View(list);
                    }
                    catch { return View(list); }
                }
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else fname = file.FileName;

                        fname = Path.Combine(Server.MapPath("~/UploadsFiles/"), fname);
                        file.SaveAs(fname);
                    }
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else return Json("No files selected.");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Users_Tbl()
        {
            var l = Db.Users.ToList();
            return View(l);
        }
    }
}
using DataLayer;
using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace Kurosh.Controllers
{
    public class HomeController : Controller
    {


        /*
         * viewbag
         * viewdata
         * tempdata
         * session
         * 
         */




        KuroshDB db = new KuroshDB();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Message message)
        {
            if (ModelState.IsValid)
            {

                message.IsRead = false;
                message.ReleaseDate = DateTime.Now;
                message.IpAddress = HttpContext.Request.UserHostAddress;
                db.MessageRepository.Insert(message);
                db.Save();
                ModelState.Clear();
                ModelState.AddModelError("FullName", "پیام با موفقیت ارسال شد");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(ViewModel.Register reg)
        {
            if (ModelState.IsValid)
            {
                if (db.UserRepository.Get(u => u.UserName == reg.UserName).Count() > 0)
                {
                    ModelState.AddModelError("UserName", "این نام کاربری در سایت موجود است");
                    return View();
                }
                if (db.UserRepository.Get(u => u.Email == reg.Email).Count() > 0)
                {
                    ModelState.AddModelError("Email", "این پست الکترونی در سایت ثبت نام کرده است");
                    return View();
                }
                if (db.UserRepository.Get(u => u.PhoneNumber == reg.PhoneNumber).Count() > 0)
                {
                    ModelState.AddModelError("PhoneNumber", "این شماره در سایت ثبت نام کرده است");
                    return View();
                }



                var user = new User();
                user.UserName = reg.UserName;
                user.Email = reg.Email;
                user.Image = "21.png";
                user.PhoneNumber = reg.PhoneNumber;
#pragma warning disable CS0618 // Type or member is obsolete
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(reg.Password, "MD5");
#pragma warning restore CS0618 // Type or member is obsolete
                user.FullName = reg.FullName;
                user.Student = new Student();
                user.RoleId = 3;
                db.UserRepository.Insert(user);
                db.Save();
            }
            ModelState.AddModelError("FullName", "ثبت نام شما با موفقیت انجام شد");
            return RedirectToAction("Login", "home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = db.user.GetByUserName(User.Identity.Name);
                if (user.RoleId == 3)
                {
                    return Redirect("/Student/Dashboard/index");
                }
                else if (user.RoleId == 2)
                {
                    return Redirect("/Teacher/Dashboard/index");
                }
                else
                {
                    return Redirect("/Admin/Dashboard/index");
                }


            }

            return View();
        }

        [HttpPost]
        [Obsolete]
        public ActionResult Login(ViewModel.Login login)
        {



            if (ModelState.IsValid)
            {

                var pass = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");

                var user = db.user.GetIdentity(login.UserName, pass);

                if (user == null)
                {
                    ModelState.AddModelError("UserName", "نام کاربری یا رمز عبور صحیح نمیباشد");

                }
                else
                {
                    Session["UserName"] = user.UserName;
                    FormsAuthentication.SetAuthCookie(login.UserName, true);
                    if (user.RoleId == 3)
                    {

                        return Redirect("/Student/Dashboard/index");
                    }
                    else if (user.RoleId == 2)
                    {
                        return Redirect("/Teacher/Dashboard/index");

                    }
                    else
                    {
                        return Redirect("/Admin/Dashboard/index");
                    }
                }
            }

            return View();
        }

        public ActionResult lilFaq()
        {
            return View(db.FAQRepository.Get().OrderBy(f => f.Level).Take(3));
        }

        public ActionResult FAQs()
        {
            return View(db.FAQRepository.Get().OrderBy(f => f.Level));
        }

        public ActionResult lilCategory()
        {
            return View(db.CategoryRepository.Get().Take(8));
        }


        public ActionResult Team()
        {
            return View(db.TeacherRepository.Get());
        }

        public ActionResult Blogs(int page = 0)
        {
            ViewBag.Page = page;
            var blogs = db.BlogRepository.Get();
            ViewBag.Count = blogs.Count() / 9;
            return View(blogs.Skip(page * 9).Take(9));
        }

        public ActionResult Search(string KeyWord, int page = 0)
        {
            ViewBag.Page = page;
            var blogs = db.BlogRepository.Get(
                b => b.Title.Contains(KeyWord) ||
            b.Tags.Contains(KeyWord) ||
            b.Descreption.Contains(KeyWord)
            ).ToList();
            ViewBag.KeyWord = KeyWord;
            ViewBag.Count = blogs.Count() / 9;
            return View(blogs);
        }

        public ActionResult LilCat()
        {
            return View(db.CategoryRepository.Get());
        }

        public ActionResult LilCourse()
        {
            return View(db.CategoryRepository.Get().OrderByDescending(c => c.Coures.Count).Take(5));
        }
        public ActionResult NewBlog()
        {
            return View(db.BlogRepository.Get().OrderByDescending(b => b.ReleaseDate).Take(3).ToList());
        }
        public ActionResult LilBlog()
        {
            return View(db.BlogRepository.Get().Take(10));
        }
        public ActionResult Blog(int Id, string Name)
        {
            var blog = db.BlogRepository.GetById(Id);
            blog.Seen++;
            db.Save();
            return View(blog);
        }
        public ActionResult Teacher(int Id, String Name)
        {
            return View(db.TeacherRepository.GetById(Id));
        }
        public ActionResult LilTeam()
        {
            return View(db.TeacherRepository.Get());
        }


        public ActionResult Categoreis()
        {
            return View(db.CategoryRepository.Get());
        }
        public ActionResult Category(int id, int page = 0)
        {
            var cat = db.CategoryRepository.GetById(id);
            ViewBag.CatName = cat.Name;
            ViewBag.Page = page;
            var courses = cat.Coures;
            ViewBag.Count = courses.Count() / 9;
            ViewBag.catId = cat.Id;
            return View(cat.Coures.Skip(page * 9).Take(9));
        }
        public ActionResult Courses(int page = 0)
        {
            var Course = db.CourseRepository.Get();
            ViewBag.Page = page;
            ViewBag.Count = Course.Count() / 9;
            return View(Course.Skip(page * 9).Take(9));
        }
        public ActionResult Course(int id, string Name)
        {
            var course = db.CourseRepository.GetById(id);
            course.Seen = course.Seen + 1;
            db.CourseRepository.Update(course);
            db.Save();
            return View(course);
        }
        public ActionResult RndCourse(int id)
        {
            return View(db.CourseRepository.Get(c => c.CategoryId == id).OrderBy(C => Guid.NewGuid()).Take(3));
        }
        public ActionResult GetCourse(int Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.user.GetByUserName(User.Identity.Name);
                if (user.RoleId == 3)
                {
                    if (db.StudentCourseRepository.Get(c => c.StudentId == user.Id && c.CourseId == Id).Count() >= 1)
                    {
                        Session["Message"] = ("شما قبلا در این دوره ثبت نام کرده اید");
                    }
                    else
                    {
                        db.StudentCourseRepository.Insert(new StudentCourse { CourseId = Id, StudentId = user.Id });
                    }

                    db.Save();
                    return RedirectToAction("MyCourses", "Student/Dashboard");
                }
                else
                {
                    Session["Message"] = ("فقط دانش آموزان میتوانند ثبت نام کنند");
                }
            }
            else
            {
                Session["Message"] = ("برای ثبت نام در دوره لطفا اطلاعات زیر را پر کنید");
            }
            return RedirectToAction("Register", "Home");
        }
    }
}
using DomainClass;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;

namespace Kurosh.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        DataLayer.KuroshDB db = new DataLayer.KuroshDB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LilMessage()
        {
            return View(db.MessageRepository.Get(m => m.IsRead == false).Take(5));
        }

        public ActionResult Messages()
        {
            return View(db.MessageRepository.Get());
        }

        public ActionResult Message(int Id)
        {
            var message = db.MessageRepository.GetById(Id);
            message.IsRead = true;
            db.MessageRepository.Update(message);
            db.Save();
            return View(message);
        }

        [HttpGet]
        public ActionResult MyProfile()
        {
            return View(db.user.GetByUserName(User.Identity.Name));
        }

        [HttpPost]
        public ActionResult MyProfile(DomainClass.User user, HttpPostedFileBase FileUpload)
        {
            var _user = db.user.GetByUserName(User.Identity.Name);
            if (FileUpload == null)
            {
                _user.FullName = user.FullName;
                _user.Email = user.Email;
                _user.PhoneNumber = user.PhoneNumber;
                db.UserRepository.Update(_user);
            }
            else
            {
                if (Path.GetExtension(FileUpload.FileName).ToLower() == ".jpg" || Path.GetExtension(FileUpload.FileName).ToLower() == ".png" || Path.GetExtension(FileUpload.FileName).ToLower() == ".jepg")
                {

                    var pickName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(FileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/assets/ImageProfileUser/"), pickName);
                    FileUpload.SaveAs(path);
                    _user.Image = pickName;
                    _user.FullName = user.FullName;
                    _user.Email = user.Email;
                    _user.PhoneNumber = user.PhoneNumber;
                    db.UserRepository.Update(_user);
                }
                else
                {
                    ModelState.AddModelError("Image", "فایل انتخابی باید  با پسنود jpg  یا jpeg  باشد");

                }


            }
            db.Save();
            return View(_user);
        }

        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(ViewModel.ChangePass pass)
        {
            if (ModelState.IsValid)
            {

                var user = db.user.GetByUserName(User.Identity.Name);
                if (user.Password == FormsAuthentication.HashPasswordForStoringInConfigFile(pass.OldPassword, "MD5"))
                {
                    if (pass.Password == pass.RepPassword)
                    {
                        user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pass.Password, "MD5");
                        db.Save();
                        ModelState.AddModelError("OldPassword", "رمز عبور با موفقیت تغییر پیدا کرد");

                    }
                    else
                    {

                        ModelState.AddModelError("OldPassword", "تکرار رمز عبور اشتباه است");
                    }
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "رمز عبور سابق درست نیست ");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("\\");
        }

        public ActionResult Admins()
        {
            return View(db.UserRepository.Get(u => u.RoleId == 1));

        }

        public ActionResult DeleteAdmin(int Id)
        {
            db.UserRepository.Delete(Id);
            db.Save();
            return RedirectToAction("Admins", "Dashboard");
        }

        public ActionResult Teachers()
        {
            return View(db.UserRepository.Get(u => u.RoleId == 2));

        }

        public ActionResult DeleteTeacher(int Id)
        {
            db.UserRepository.Delete(Id);
            db.Save();
            return RedirectToAction("Teachers", "Dashboard");
        }

        public ActionResult Students()
        {
            return View(db.UserRepository.Get(u => u.RoleId == 3));

        }

        public ActionResult DeleteUser(int Id)
        {
            db.UserRepository.Delete(Id);
            db.Save();
            return RedirectToAction("Students", "Dashboard");
        }

        [HttpGet]
        public ActionResult UserAdd(int? Id)
        {
            return View(db.UserRepository.GetById(Id));
        }

        [HttpPost]
        public ActionResult UserAdd(DomainClass.User user, int RoleId)
        {
            if (user.Id == 0)
            {
                user.Password= FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
                user.RoleId = RoleId;
                user.Image = "21.png";
                db.UserRepository.Insert(user);
            }
            else
            {
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
                user.RoleId = RoleId;
                if (user.RoleId == 2)
                {
                    user.Teacher = new DomainClass.Teacher() { };

                }
                else if (user.RoleId == 3)
                {
                    user.Student=new DomainClass.Student();

                }
                user.Image = "21.png";
                db.UserRepository.Update(user);
                db.Save();
            }
            db.Save();
            if (user.RoleId == 1)
            {
                return RedirectToAction("Admins", "Dashboard");
            }
            else if (user.RoleId == 2)
            {
                return RedirectToAction("Teachers", "Dashboard");

            }
            else
            {
                return RedirectToAction("Students", "Dashboard");

            }
        }

        public ActionResult Categorys()
        {
            return View(db.CategoryRepository.Get());
        }
        [HttpGet]
        public ActionResult Category(int? Id)
        {
            return View(db.CategoryRepository.GetById(Id));
        }
        [HttpPost]
        public ActionResult Category(DomainClass.Category category, HttpPostedFileBase FileUpload)
        {
            if (category.Id == 0)
            {

                if (Path.GetExtension(FileUpload.FileName).ToLower() == ".jpg" || Path.GetExtension(FileUpload.FileName).ToLower() == ".png" || Path.GetExtension(FileUpload.FileName).ToLower() == ".jepg")
                {
                    var pickName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(FileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/assets/CategoryImage/"), pickName);
                    FileUpload.SaveAs(path);
                    category.image = pickName;
                    db.CategoryRepository.Insert(category);
                }


            }
            else
            {

                if (FileUpload == null)
                {
                    //ویرایش بددون عکس
                    db.CategoryRepository.Update(category);
                }
                else
         
                {
                    //ویرایش با عکس

                    if (Path.GetExtension(FileUpload.FileName).ToLower() == ".jpg" || Path.GetExtension(FileUpload.FileName).ToLower() == ".png" || Path.GetExtension(FileUpload.FileName).ToLower() == ".jepg")
                    {
                        DateTime d = DateTime.Now;
                        string name = d.ToString("yyyyMMdd-HHmmss") + d.Millisecond;

                        var pickName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(FileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Areas/assets/CategoryImage/"), pickName);
                        FileUpload.SaveAs(path);
                        category.image = pickName;
                        db.CategoryRepository.Update(category);
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "فایل انتخابی باید  با پسنود jpg  یا jpeg  باشد");
                    }



                }
                db.CategoryRepository.Update(category);
            }
            db.Save();
            return RedirectToAction("Categorys", "Dashboard");
        }

        public ActionResult FAQs()
        {
            return View(db.FAQRepository.Get());
        }

        [HttpGet]
        public ActionResult faq(int? Id)
        {
            return View(db.FAQRepository.GetById(Id));
        }

        [HttpPost]
        public ActionResult faq(DomainClass.FAQ fAQ)
        {
            if (fAQ.Id == 0)
            {
                db.FAQRepository.Insert(fAQ);
            }
            else
            {
                db.FAQRepository.Update(fAQ);
            }
            db.Save();
            return RedirectToAction("FAQs", "Dashboard");
        }


        public ActionResult Blogs()
        {
            return View(db.BlogRepository.Get());
        }

        [HttpGet]
        public ActionResult blog(int? Id)
        {
            return View(db.BlogRepository.GetById(Id));
        }

        [HttpPost]
        public ActionResult blog(DomainClass.Blog blog, HttpPostedFileBase FileUpload)
        {

            if (blog.Id == 0)
            {

                if (FileUpload != null)
                {
                    if (Path.GetExtension(FileUpload.FileName).ToLower() == ".jpg" || Path.GetExtension(FileUpload.FileName).ToLower() == ".png" || Path.GetExtension(FileUpload.FileName).ToLower() == ".jepg")
                    {
                        DateTime d = DateTime.Now;
                        string name = d.ToString("yyyyMMdd-HHmmss") + d.Millisecond;

                        var pickName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(FileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Areas/assets/ImageBlogs/"), pickName);
                        FileUpload.SaveAs(path);
                        var user = db.user.GetByUserName(User.Identity.Name);
                        blog.ReleaseDate = DateTime.Now;
                        blog.Publisher = db.user.GetByUserName(User.Identity.Name);
                        blog.Seen = 0;
                        blog.Image = pickName;
                        blog.PublisherId = user.Id;
                        db.BlogRepository.Insert(blog);
                        db.Save();
                        return RedirectToAction("Blogs", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "فایل انتخابی باید  با پسنود jpg  یا jpeg  باشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "لطفا تصویر را بارگذاری کنید");
                }
            }
            else
            {

                if (FileUpload == null)
                {
                    //ویرایش بددون عکس

                    var _blog = db.BlogRepository.GetById(blog.Id);
                    _blog.Title = blog.Title;
                    _blog.Descreption = blog.Descreption;
                    _blog.Tags = blog.Tags;
                    db.Save();
                    return RedirectToAction("Blogs", "Dashboard");
                }
                else
                {

                }
                {
                    //ویرایش با عکس

                    if (Path.GetExtension(FileUpload.FileName).ToLower() == ".jpg" || Path.GetExtension(FileUpload.FileName).ToLower() == ".png" || Path.GetExtension(FileUpload.FileName).ToLower() == ".jepg")
                    {

                        var pickName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(FileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Areas/assets/ImageBlogs/"), pickName);
                        FileUpload.SaveAs(path);
                        var _blog = db.BlogRepository.GetById(blog.Id);
                        _blog.Image = pickName;
                        _blog.Descreption = blog.Descreption;
                        _blog.Tags = blog.Tags;
                        _blog.Title = blog.Title;
                        db.Save();
                        return RedirectToAction("Blogs", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "فایل انتخابی باید  با پسنود jpg  یا jpeg  باشد");
                    }



                }


            }
            return View();

        }

        public ActionResult Courses()
        {
            return View(db.CourseRepository.Get());

        }

        [HttpGet]
        public ActionResult Course(int? Id)
        {
            return View(db.CourseRepository.GetById(Id));

        }

        [HttpPost]
        public ActionResult Course(DomainClass.Coures course,int TeacherId ,int CatId,HttpPostedFileBase FileUpload)
        {
            if (course.Id==0)
            {

                if (Path.GetExtension(FileUpload.FileName).ToLower() == ".jpg" || Path.GetExtension(FileUpload.FileName).ToLower() == ".png" || Path.GetExtension(FileUpload.FileName).ToLower() == ".jepg")
                {
                    var pickName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(FileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Areas/assets/CoursesImage/"), pickName);
                    FileUpload.SaveAs(path);
                    course.Image = pickName;
                    course.TeacherId= TeacherId;
                    course.CategoryId= CatId;
                    db.CourseRepository.Insert(course);
                    db.Save();
                }
                else
                {
                    ModelState.AddModelError("Image", "فایل انتخابی باید  با پسنود jpg  یا jpeg  باشد");
                    return View();
                }
            }
            else
            {

                if (FileUpload == null)
                {
                    //ویرایش بددون عکس

                    var _course = db.CourseRepository.GetById(course.Id);
                    _course.HoursCout = course.HoursCout;
                    _course.OldPrice = course.OldPrice;
                    _course.Price = course.Price;
                    _course.Descreption = course.Descreption;
                    _course.Name = course.Name;
                    _course.Summery = course.Summery;
                    _course.TeacherId = TeacherId;
                    _course.CategoryId = CatId;
                    db.CourseRepository.Update(_course);
                    db.Save();

                }
                else
                {
                    //ویرایش با عکس
               

                    if (Path.GetExtension(FileUpload.FileName).ToLower() == ".jpg" || Path.GetExtension(FileUpload.FileName).ToLower() == ".png" || Path.GetExtension(FileUpload.FileName).ToLower() == ".jepg")
                    {
                        

                        var pickName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(FileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Areas/assets/CoursesImage/"), pickName);
                        FileUpload.SaveAs(path);
                        var _course = db.CourseRepository.GetById(course.Id);
                        _course.HoursCout = course.HoursCout;
                        _course.OldPrice = course.OldPrice;
                        _course.Price = course.Price;
                        _course.Descreption = course.Descreption;
                        _course.Name = course.Name;
                        _course.Summery = course.Summery;
                        _course.Image = pickName;
                        _course.TeacherId = TeacherId;
                        _course.CategoryId = CatId;
                        db.CourseRepository.Update(_course);
                        db.Save();
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "فایل انتخابی باید  با پسنود jpg  یا jpeg  باشد");
                        return View();
                    }



                }

            }

            return RedirectToAction("Courses","Dashboard");

        }

        [HttpGet]
        public ActionResult Setting()
        {
            return View(db.SettingRepository.Get().SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Setting(DomainClass.Setting setting)
        {
            db.SettingRepository.Update(setting);
            db.Save();
            return View(setting);
        }
        public ActionResult test()
        {
            return View();
        }
    }
}
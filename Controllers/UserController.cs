using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using LoginAuthorDemo.Data;
using LoginAuthorDemo.Models;
using LoginAuthorDemo.Util;
using LoginAuthorDemo.ViewModels;

namespace LoginAuthorDemo.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowBookDetails()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var authors = session.Query<Book>().ToList();
                return View(authors);
            }
        }

        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Author author)
        {
            if (ModelState.IsValid)
            {
                author.Password = Hashing.HashPasword(author.Password);
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var txn = session.BeginTransaction())
                    {
                        session.Save(author);
                        txn.Commit();
                        return RedirectToAction("Login");
                    }
                }
            }
            return View(author);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM, string password)
        {
            string hashedPassword = Hashing.HashPasword(password);
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var user = session.Query<Author>().SingleOrDefault(u => u.UserName == loginVM.UserName);
                    if (user != null && Hashing.ValidatePassword(password, user.Password))
                    {
                        FormsAuthentication.SetAuthCookie(loginVM.UserName, true);
                        return RedirectToAction("Index", "Author");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username/Password doesn't match");
                        return View("Login", loginVM);
                    }
                }

            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
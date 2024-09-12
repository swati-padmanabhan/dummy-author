using System;
using System.Linq;
using System.Web.Mvc;
using LoginAuthorDemo.Data;
using LoginAuthorDemo.Models;

namespace LoginAuthorDemo.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var authors = session.Query<Author>().ToList();
                return View(authors);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    author.AuthorDetails.Author = author;
                    session.Save(author);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Edit(Guid id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var author = session.Query<Author>().FirstOrDefault(a => a.Id == id);
                return View(author);
            }
        }
        [HttpPost]
        public ActionResult Edit(Author author)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    author.AuthorDetails.Author = author;
                    session.Update(author);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(Guid id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var author = session.Get<Author>(id);
                return View(author);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAuthor(Guid id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var author = session.Get<Author>(id);
                    session.Delete(author);
                    txn.Commit();
                    return RedirectToAction("Index");
                }
            }
        }


    }
}
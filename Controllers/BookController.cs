using System;
using System.Linq;
using System.Web.Mvc;
using LoginAuthorDemo.Data;
using LoginAuthorDemo.Models;

namespace LoginAuthorDemo.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookDetails(Guid authorId)
        {
            TempData["authorId"] = authorId;
            using (var session = NHibernateHelper.CreateSession())
            {
                var books = session.Query<Book>().Where(b => b.Author.Id == authorId).ToList();
                return View(books);
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            int authorId = (int)TempData.Peek("authorId");
            if (book != null)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var txn = session.BeginTransaction())
                    {
                        book.Author = session.Get<Author>(authorId);
                        session.Save(book);
                        txn.Commit();
                        return RedirectToAction("BookDetails", new { authorId = authorId });
                    }
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var book = session.Get<Book>(id);
                    return View(book);
                }
            }
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            int authorId = (int)TempData.Peek("authorId");
            if (book != null)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var txn = session.BeginTransaction())
                    {
                        book.Author = session.Get<Author>(authorId);
                        session.Update(book);
                        txn.Commit();
                        return RedirectToAction("BookDetails", new { authorId = authorId });
                    }
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var book = session.Get<Book>(id);
                    return View(book);
                }
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteBook(int id)
        {
            int authorId = (int)TempData.Peek("authorId");
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var book = session.Get<Book>(id);
                    session.Delete(book);
                    txn.Commit();
                    return RedirectToAction("BookDetails", new { authorId = authorId });
                }
            }
        }


    }
}
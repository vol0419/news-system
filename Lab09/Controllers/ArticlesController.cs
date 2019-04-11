using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab09.DAL;
using Lab09.Models;
using PagedList;

namespace Lab09.Controllers
{
    public class ArticlesController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: Articles
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.TopicSortParm = sortOrder == "Topic" ? "topic_desc" : "Topic";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_desc" : "Rating";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var articles = from s in db.Articles
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Topic.Contains(searchString)
                                       || s.Region.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    articles = articles.OrderByDescending(s => s.ID);
                    break;
                case "Rating":
                    articles = articles.OrderBy(s => s.Rating);
                    break;
                case "rating_desc":
                    articles = articles.OrderByDescending(s => s.Rating);
                    break;
                case "Category":
                    articles = articles.OrderByDescending(s => s.Category);
                    break;
                case "category_desc":
                    articles = articles.OrderByDescending(s => s.Category);
                    break;
                case "Topic":
                    articles = articles.OrderByDescending(s => s.Topic);
                    break;
                case "topic_desc":
                    articles = articles.OrderByDescending(s => s.Topic);
                    break;
                default:
                    //articles = articles.OrderBy(s => s.ID);
                    articles = articles.OrderByDescending(s => s.ID);
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(articles.ToPagedList(pageNumber, pageSize));

        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [HttpPost]
        public ActionResult Details(int? id, int rating)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return View(article);
            }
            else
            {
                    

                //int x = article.RatingCount + 1;
                //int y = article.Rating;
                //double z = ((y + rating) / (x * 5)) * 100;
                article.RatingCount += 1;
                //double ratingPercentage = ((article.Rating + rating) / (article.RatingCount * 5)) * 100;

                //article.Rating = Convert.ToInt32(z);
                //article.RatingCount = article.RatingCount + 1;
                //article.Rating = article.Rating + rating;
                //article.Rating = Convert.ToInt32(ratingPercentage);
                article.Rating += rating;
                db.SaveChanges();
            }
            return View(article);
        }



        // GET: Articles/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Topic,Description,Region,Category,Rating,AuthorID, Author")] Article article)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["imgFile"];
                if(file != null && file.ContentLength > 0)
                {
                    article.ImgName = System.Guid.NewGuid().ToString();
                    article.ImgName = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + article.ImgName);
                }

                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Topic,Description,Region,Category,Rating,AuthorID")] Article article)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["imgFile"];

                if (file != null && file.ContentLength > 0)
                {
                    article.ImgName = System.Guid.NewGuid().ToString();
                    article.ImgName = file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/") + article.ImgName);
                }
               
             

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

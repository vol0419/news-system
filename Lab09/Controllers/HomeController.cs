using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab09.DAL;
using Lab09.ViewModels;
using System.Configuration;
using Lab09.Models;

namespace Lab09.Controllers
{
    public class HomeController : Controller
    {


        private NewsContext db = new NewsContext();
        public ActionResult Index()
        {          
            return View();
        }

        public ActionResult About()
        {
            IQueryable<CategoryGroup> data = from article in db.Articles
                                                   group article by article.Category into dateGroup
                                                   select new CategoryGroup()
                                                   {
                                                       Category = dateGroup.Key,
                                                       ArticlesCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }





    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab09.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab09.DAL
{
    public class NewsInitializer :DropCreateDatabaseIfModelChanges<NewsContext>
    {
        protected override void Seed(NewsContext context)
        {
            var profiles = new List<Profile>
            {
                new Profile { UserName = "Gogo", Name = "Jan", Surname = "Brzechwa", Mail = "gogo@gogo.com"},
                new Profile { UserName = "Sharp", Name = "Maciej", Surname = "Zklanu", Mail = "maciek@zklanu.eu"}
            };
            profiles.ForEach(p => context.Profiles.Add(p));
            context.SaveChanges();

            var articles = new List<Article>
            {
                new Article { Topic = "Zapytania LINQ", Category = (Category)1, Description = "Zaptania w jezyku c#", Profile = profiles.ElementAt(1), Region = "Mazowieckie" },
                new Article { Topic = "Języki programowania", Category = (Category)1, Description = "Ile jest językow programowania na świecie", Profile = profiles.ElementAt(0), Region = "Swiat" },
                new Article { Topic = "Nowy aa", Category = (Category)1, Description = "Co to jest?", Profile = profiles.ElementAt(0), Region = "Swiat" }
            };
            articles.ForEach(p => context.Articles.Add(p));
            context.SaveChanges();
            /*
            var comments = new List<Comment>
            {
                new Comment { Article = articles.ElementAt(1), Description = "Some commentary for this article....", Profile = profiles.ElementAt(1) },
                new Comment { Article = articles.ElementAt(1), Description = "Another comment on this article....", Profile = profiles.ElementAt(0) }
            };
            comments.ForEach(p => context.Comments.Add(p));
            context.SaveChanges();
            */
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("User"));

            context.SaveChanges();

            var admin = new ApplicationUser { UserName = "admin", Email = "admin@admin.com" };
            string password = "Admin!1";

            userManager.Create(admin, password);
            userManager.AddToRole(admin.Id, "Admin");

            context.SaveChanges();
        }
    }
}
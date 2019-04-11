
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab09.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }

        //public int dupa { get; set; }

        public virtual List<Article> FavoriteArticles { get; set; }
        public virtual List<Article> WrittenArticles { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
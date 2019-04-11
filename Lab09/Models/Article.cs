using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lab09.Models
{
    public class Article
    {
        public int ID { get; set; }
        [StringLength(50, ErrorMessage = "To pole nie może mieć więcej niż 50 znaków.")]
        public string Topic { get; set; }
        public string Description { get; set; }
        [StringLength(25, ErrorMessage = "To pole nie może mieć więcej niż 25 znaków.")]
        public string Region { get; set; }
        public Category Category { get; set; }
        public int Rating { get; set; }

        public int RatingCount { get; set; }

        public String Author { get; set; }
        public int AuthorID { get; set; }

        public string ImgName { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual List<Comment> Comments { get; set; }

    }

    public enum Category
    {
        Sport, Sience, Nature, Politics, Fun, Other
    }
}
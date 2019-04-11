using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lab09.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Komentarz musi mieć od 1 do 500 znaków.")]
        public string Description { get; set; }

        public string Nickname { get; set; }
        public int ArticleID { get; set; }

        public DateTime Date { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Article Article { get; set; }
    }
}
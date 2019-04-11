using System;
using System.ComponentModel.DataAnnotations;

namespace Lab09.ViewModels
{
    public class CategoryGroup
    {

        public Lab09.Models.Category? Category { get; set; }

        public int ArticlesCount { get; set; }
    }
}
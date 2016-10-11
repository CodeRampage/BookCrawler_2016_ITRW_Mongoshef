using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BookCrawler_2016_ITRW_Mongoshef
{
    public class BookMetaData
    {
        public HtmlNodeCollection Name { get; set; }
        public HtmlNodeCollection Author { get; set; }
        public HtmlNodeCollection Stars { get; set; }
        public HtmlNodeCollection Ratings { get; set; }
        public HtmlNodeCollection Reviews { get; set; }
        public IEnumerable<string> ISBN { get; set; }
        public HtmlNodeCollection Pages { get; set; }
        public HtmlNodeCollection PublishDate { get; set; }
        public HtmlNodeCollection Language { get; set; }
        public IEnumerable<string> Synopsis { get; set; }
    }
}

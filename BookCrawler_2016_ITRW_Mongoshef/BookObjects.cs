using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MongoDB.Bson;

namespace BookCrawler_2016_ITRW_Mongoshef
{
    public class BookMetaData
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Stars { get; set; }
        public string Ratings { get; set; }
        public string Reviews { get; set; }
        public string ISBN { get; set; }
        public string Pages { get; set; }
        public string PublishDate { get; set; }
        public string Language { get; set; }
        public string Link { get; set; }

        public IEnumerable<string> Synopsis { get; set; }
    }
}

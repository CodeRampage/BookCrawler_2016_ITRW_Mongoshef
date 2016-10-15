using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace BookCrawler_2016_ITRW_Mongoshef
{
    public class DataProcessor
    {
        private static IMongoCollection<BookMetaData> bookCollection;
        private static IMongoClient client = new MongoClient("mongodb://MongoChef:mongochef@196.253.61.51:27017/MongoChefDB");
        private static IMongoDatabase db = client.GetDatabase("MongoChefDB");

        private static Cleaner dataCleaner = new Cleaner();

        public class Mongo
        {
            public void insertRecord(BookMetaData book)
            {
                bookCollection = db.GetCollection<BookMetaData>("Books");
                bookCollection.InsertOne(book);
            }

            public async void getCollections()
            {
                bookCollection = db.GetCollection<BookMetaData>("Books");

                string bookName = "";
                string author = "";
                string rating = "";
                string review = "";
                string ISBN = "";
                string pages = "";
                string publishedDate = "";
                string language = "";
                string stars = "";
                string link = "";

                IEnumerable<string> synopsisNumerable = null;
                string synopsis = "";

                var cursor = await bookCollection.Find(new BsonDocument()).ToCursorAsync();

                using (cursor)
                {
                    while (await cursor.MoveNextAsync())
                    {
                        foreach (var book in cursor.Current)
                        {
                            bookName = book.Name;
                            author = book.Author;
                            rating = book.Ratings;
                            review = book.Reviews;
                            ISBN = book.ISBN;
                            pages = book.Pages;
                            publishedDate = book.PublishDate;
                            language = book.Language;
                            stars = book.Stars;
                            synopsisNumerable = book.Synopsis;
                            link = book.Link;

                            foreach(var line in synopsisNumerable)
                            {
                                synopsis += line;
                            }

                            dataCleaner.cleanData(bookName, author, rating, review, ISBN, pages, publishedDate, language,stars, synopsis,link);
                        }
                    }
                }
            }//End getCollections
        }

        public class Oracle
        {
            public void insertBook()
            {

            }
        }

        public class Cleaner
        {
            public void cleanData(string bookName, string author,string rating,string review,string ISBN,string pages,string publishedDate,string language, string stars,string synopsis, string link)
            {
                bookName = bookName.Trim();
                string[] authorNames = author.Trim().Split(' ');
                string authorFirst = authorNames[0];
                string authoLast = authorNames[1];
                string [] ratingsArr  = rating.Trim().Split(' ');
                rating = ratingsArr[0];
                review = review.Trim();
                ISBN = ISBN.Trim();
                string[] pagesArr = pages.Trim().Split(' ');
                pages = pagesArr[0];
                publishedDate = publishedDate.Trim();
                stars = stars.Trim();
                language = language.Trim();
                synopsis = synopsis.Trim();
                link = link.Trim();

                Console.WriteLine(stars);
            }
        }
    }
}

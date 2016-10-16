using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;


namespace BookCrawler_2016_ITRW_Mongoshef
{
    public class DataProcessor
    {
        private static IMongoCollection<BookMetaData> bookCollection;
        private static IMongoClient client = new MongoClient("mongodb://MongoChef:mongochef@196.253.61.51:27017/MongoChefDB");
        private static IMongoDatabase db = client.GetDatabase("MongoChefDB");

        private static Cleaner dataCleaner = new Cleaner();

        private static OracleCommand cmd;
        private static OracleConnection conn;

        public static void openConnection()
        {
            conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=196.253.61.51)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL))); User Id = MongoChef; Password = mongochef");
            conn.Open();
        }
        
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
            public void insertBook(string isbn, string bookname, string link, string stars, string rating, string reviews, string page_nums, string p_date, string first,string last)
            {
                OracleConnection conn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=196.253.61.51)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL))); User Id = MongoChef; Password = mongochef");
                OracleCommand cmd;

                conn.Open();
                using (conn)
                {
                    cmd = new OracleCommand("INSERT_BOOK", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("BOOK_ISBN", OracleDbType.Varchar2, ParameterDirection.Input).Value = isbn;
                    cmd.Parameters.Add("BOOK_N", OracleDbType.Varchar2, ParameterDirection.Input).Value = bookname;
                    cmd.Parameters.Add("BOOK_LINK", OracleDbType.Varchar2, ParameterDirection.Input).Value = link;
                    cmd.Parameters.Add("STARS", OracleDbType.Varchar2, ParameterDirection.Input).Value = stars;
                    cmd.Parameters.Add("RATING", OracleDbType.Varchar2, ParameterDirection.Input).Value = rating;
                    cmd.Parameters.Add("REVIEWS", OracleDbType.Varchar2, ParameterDirection.Input).Value = reviews;
                    cmd.Parameters.Add("PAGE_NUMS", OracleDbType.Varchar2, ParameterDirection.Input).Value = page_nums;
                    cmd.Parameters.Add("PUBLISH_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = p_date;
                    cmd.Parameters.Add("FIRSTN", OracleDbType.Varchar2, ParameterDirection.Input).Value = first;
                    cmd.Parameters.Add("LASTN", OracleDbType.Varchar2, ParameterDirection.Input).Value = last;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    conn.Close();
                }
            }
        }

        public class Cleaner
        {
            Oracle oracle = new Oracle();

            public async void cleanData(string bookName, string author,string rating,string review,string ISBN,string pages,string publishedDate,string language, string stars,string synopsis, string link)
            {
                bookName = bookName.Trim();
                bookName = Regex.Replace(bookName, @"[\n]","");
                string[] authorNames = author.Trim().Split(' ');
                string authorFirst = Regex.Replace(authorNames[0], @"[\n]", ""); 
                string authoLast = Regex.Replace(authorNames[1], @"[\n]", "");
                string [] ratingsArr  = rating.Trim().Split(' ');
                rating = Regex.Replace(ratingsArr[0].Trim(),@"[,]","");
                review = review.Trim();
                ISBN = ISBN.Trim();
                string[] pagesArr = pages.Trim().Split(' ');
                pages = pagesArr[0];
                string [] publishedDates = publishedDate.Trim().Split(' ');
                stars = stars.Trim();
                language = language.Trim();
                synopsis = synopsis.Trim();
                link = link.Trim();
                publishedDates[9] = Regex.Replace(publishedDates[9], @"[^\d]", "");

                string date = publishedDates[9].Trim() + "/" + publishedDates[8].Trim() + "/" + publishedDates[10].Trim();

                Console.WriteLine(pages);

                await Task.Factory.StartNew(() => oracle.insertBook(ISBN, bookName, link, stars.Trim(), rating.Trim(), review.Trim(), pages, date, authorFirst, authoLast));
            }
        }
    }
}

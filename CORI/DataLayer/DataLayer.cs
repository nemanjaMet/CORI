using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using ServiceStack.Text;
using System.Runtime.Serialization;

namespace DataLayer
{
    public class DataLayer
    {
        readonly RedisClient redis = new RedisClient(Config.SingleHost);
        public DataLayer()
        {
            //test();
        }
        public  bool testing(int howMany)
        {
            string testHtml = " <div>\n\t <h1>Testing purpose</h1> \n </div>";

            for (int i = 0; i < howMany; i++)
            {
                var page = new HtmlPage 
                {
                    id = i,
                    name = "pageNumber"+(i+1).ToString(),
                    html = testHtml,
                    css = "",
                    javaScript = ""
                };
                redis.StoreAsHash<HtmlPage>(page);
            }

                return true;
        }

        // --- Mislim da ne dodaje duplikate po istom id-u
        public void AddPerson()
        {
            // var redisUsers = redis.As<Person>();
            // redisUsers.GetNextSequence()
            
            var toske = new Person { id = 1, name = "Toske pijanac" };
            var milojko = new Person { id = 2, name = "Milojko bre" };
            var tripuskic = new Person { id = 3, name = "Tripuskic bez komentara" };
            // redisUsers.Store(toske);
            // redisUsers.Store(milojko);

            // Dodaje objekat tipa Person u bazu
            redis.As<Person>().Store(toske);
            redis.As<Person>().Store(milojko);
            redis.As<Person>().Store(tripuskic);
        }

        public void AddUserHtml(string html,long UserID)
        {
            

            string html1 = "";
            string css1 = "";
            string javaScript1 = "";

            html1 = System.IO.File.ReadAllText(@"C:\Users\Darko Velickovic\Desktop\TxtHtml.txt");

            var stranica1 = new HtmlPage { id = UserID ,name = "stranica1", html = html1, css = css1, javaScript = javaScript1 };
           
            redis.StoreAsHash<HtmlPage>(stranica1);
            
        }

        public string GetHtml(long UserID)
        {
            //string html = "";

           // HtmlPage hp = redis.GetFromHash<HtmlPage>("urn:htmlpage:51438283"); // ne radi
            HtmlPage hp2 = redis.GetFromHash<HtmlPage>(UserID);
          
            return hp2.html;
        }

        public void GetPerson()
        {
            //---- Vraca listu objekata ----
             IList<Person> p1 = redis.As<Person>().GetAll();
           
             if (p1 != null)
             {
                 foreach (Person p in p1)
                 {
                     // Console.WriteLine("Id osobe: " + p.id );
                     // Console.WriteLine("Ime osobe: " + p.name );
                     string outputJson = ServiceStack.Text.JsonSerializer.SerializeToString<Person>(p);
                     File.AppendAllText(@"C:\Users\Neca\Desktop\Test.txt", outputJson  + Environment.NewLine);
                 }
             }

            // Nacin 2 
            // treba se pristupi podacima nekako, ili da se konvertuje u pravi objekat
           var redisPersons = redis.As<Person>();
           // look into : redis.GetByIds<Person>();
          /* File.AppendAllText(@"C:\Users\Neca\Desktop\Test.txt", ???? + Environment.NewLine);*/

            //--- Vracanje osobe po ID-u ---
             Person per1 = redis.As<Person>().GetValue("urn:person:2");
             string toJson = ServiceStack.Text.JsonSerializer.SerializeToString<Person>(per1);
             // ili ovako
             var test = redis.Get<Person>("urn:person:2");

           
            
        }

        public void DeletePerson()
        {
            // --- Brisanje osobu po ID-u ---
            redis.DeleteById<Person>("2");
            
            /* redis.FlushDb(); Flesuje celu bazu wtf ? // to wipe a single database, 0 by default
             redis.FlushAll(); Flesuje celu bazu wtf ? // to wipe all databases*/
        }

        public void RenamePerson()
        {
            // --- Izmena imena ---
            // izgleda da je ovako
            var milojko = new Person { id = 2, name = "Milojko Komarac" };
            redis.As<Person>().Store(milojko);
        }

        public class Person
        {
            public long id { get; set; }
            public string name { get; set; }
        }

        public class HtmlPage
        {
            public long id { get; set; }
            public string name { get; set; }
            public string html { get; set; }
            public string css { get; set; }
            public string javaScript { get; set; }
        }

        /// <summary>
        /// full database wipe (be carefull)
        /// </summary>
        private void clearDataBase()
        {
            redis.FlushAll();
        }

      /*  public string ToJsonString()
        {
            return JsonSerializer.SerializeToString<Person>(this);
        }*/

    }
}

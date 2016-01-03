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
            test();
        }
        public void test()
        {
            //  redis.SetValue("test", "PeraBosko", TimeSpan.FromSeconds(20));
            //   redis.SetValueIfNotExists("test", "JankoMarko");
           // AddPerson();  
          // GetPerson();
          //  DeletePerson();
          //  RenamePerson();

          //  clearDataBase(); NE CACKAJ AKO NE ZNAS !!!

            //AddHtml();
            GetHtml();
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

        public void AddHtml()
        {
            string html1 = "";
            string css1 = "";
            string javaScript1 = "";

            html1 = System.IO.File.ReadAllText(@"C:\Users\Neca\Desktop\TxtHtml.txt");

            var stranica1 = new HtmlPage { name = "stranica1", html = html1, css = css1, javaScript = javaScript1 };
           //var stranica2 = new HtmlPage { name = "stranica2", html = "", css = "", javaScript = "" };
            //var stranica3 = new HtmlPage { name = "stranica3", html = "", css = "", javaScript = "" };

            redis.As<HtmlPage>().StoreAsHash(stranica1);
            //redis.As<HtmlPage>().StoreAsHash(stranica2);
            //redis.As<HtmlPage>().StoreAsHash(stranica3);
        }

        public void GetHtml()
        {
            string html = "";

            HtmlPage hp = redis.GetFromHash<HtmlPage>("urn:htmlpage:51438283"); // ne radi

            IList<string> s1 = redis.GetHashValues("urn:htmlpage:51438283"); // vraća sve atribute kao stringove

            var hash = redis.GetAllEntriesFromHash("urn:htmlpage:51438283");  // treba se kastuje verovatno
            
            // Ne radi
          /*  IList<HtmlPage> p1 = redis.As<HtmlPage>().GetAll();

            if (p1 != null)
            {
                foreach (HtmlPage p in p1)
                {
                    // Console.WriteLine("Id osobe: " + p.id );
                    // Console.WriteLine("Ime osobe: " + p.name );
                    html = p.html;
                    File.AppendAllText(@"C:\Users\Neca\Desktop\Test.html", html);
                }
            }*/
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
<<<<<<< HEAD

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
            public string name { get; set; }
            public string html { get; set; }
            public string css { get; set; }
            public string javaScript { get; set; }
        }

        public void clearDataBase()
        {
            redis.FlushAll();
        }

      /*  public string ToJsonString()
        {
            return JsonSerializer.SerializeToString<Person>(this);
        }*/
=======
        
>>>>>>> refs/remotes/darko14velickovic/master
    }
}

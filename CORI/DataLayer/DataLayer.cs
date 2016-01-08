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
using System.Text.RegularExpressions; // za preciscavanje texta 'Regex'

namespace DataLayer
{
    public class DataLayer
    {
        readonly RedisClient redis = new RedisClient(Config.SingleHost);
        public DataLayer()
        {
            //test();

            //--- test funkcije ---
           // AddPerson();
           // searchPersons();
      
            /*AddUser("userLista1 100");
            AddUser("userLista1 200");
            AddUser("userLista1 300");

            AddUser("userLista2 900");
            AddUser("userLista2 800");
            AddUser("userLista2 700");
            AddUser("userLista2 600");*/
        }
        public bool testing(int howMany)
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

       /* public void searchPersons()
        {
            string searchString = "*2*";

            var getSpecificKeys = redis.SearchKeys(searchString);
            List<Person> listaOsoba = new List<Person>();
          
            // Vraca sve sta nađe string, hash, lista
            // tako da moze da se desi da vrati neki drugi objekat
            foreach (var getKey in getSpecificKeys)
            {
                // filtriranje po objektu
                if (getKey.Contains("person"))
                {
                    Person per = redis.As<Person>().GetValue(getKey.ToString());
                    listaOsoba.Add(per);
                }
            }
        }*/

        // Nalazi kljuceve koji sadrzi zadati podstring --- NIJE TESTIRANO ---
        public List<string> SearchKeys(string subString)
        {
            string searchString = "*" + subString + "*";

            var getSpecificKeys = redis.SearchKeys(searchString);
            List<string> listaKljuceva = new List<string>();

            // Vraca sve sta nađe string, hash, lista
            // tako da moze da se desi da vrati neki drugi objekat
            foreach (var getKey in getSpecificKeys)
            {
                listaKljuceva.Add(getKey.ToString());
            }

            return listaKljuceva;
        }

        public void AddUserHtml(string html,long UserID)
        {
            

            string html1 = "";
            string css1 = "";
            string javaScript1 = "";

            html1 = System.IO.File.ReadAllText(@"C:\Users\Darko Velickovic\Desktop\TxtHtml.txt");

            // --- Za preciscavanje texta ---            
            html1 = Regex.Replace(html1, @"\t|\r", " ");
            html1 = Regex.Replace(html1, @"( \n){2,}", "\n");
            html1 = Regex.Replace(html1, " {2,}", " ");
            

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

        // PushItemToList radi kao rpush
        // Ako ne postoji takav 'nameID' liste onda pravi novu listu
        // Ukoliko postoji takav 'nameID' liste onda ubacuje element u postojecu listu
        public bool AddUser(string userName, string pageID)
        {
            bool successfully = false;

            // Ako se salje kao jedan string
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(pageID))
            {
                // Ako zadati kljuc ne postoji u bazi
                if (!redis.ContainsKey("userLista3"))
                {
                    // Mozda treba da se limitira lista
                    // Do work...
                }

                // konvertovanje pageID-a u byte array
                byte[] idToByte = Encoding.ASCII.GetBytes(pageID);

                // Ubacivanje liste u bazu, LPush ubacuje sa leve strane (kao stek, red)
                // Vraca broj elemenata u listi, zajedno sa dodatim elementom
                long elementsNumber = redis.LPush(userName, idToByte);

                successfully = true;
            }
            return successfully;
        }

        // Ne znam sta da vrati listu verovatno
        public string GetUser(string userName)
        {
            if (redis.ContainsKey(userName))
            {
                // --- Vraca sve elemente iz liste sa zadatim ID-om liste ---
                List<string> allItemsFromList = redis.GetAllItemsFromList(userName);

                // --- Ako treba sa LRANGE da se vrati ---
                int start = 0, end = -1;
                byte[][] byteItems = redis.LRange(userName, start, end);
                List<string> someItemsFromList = new List<string>();
                foreach (byte[] b in byteItems)
                {
                    someItemsFromList.Add(Encoding.ASCII.GetString(b));
                }

                return "---STA GOD TREBA DA VRATI---";
            }
            else
                return "false";             
        }

        // Za ubacivanje elementa u list 'LPUSH'
        public bool AddItemToList(string listID, string item)
        {
            bool successfully = false;

            // Ako se salje kao jedan string
            if (!String.IsNullOrEmpty(listID) && !String.IsNullOrEmpty(item))
            {
      
                // konvertovanje pageID-a u byte array
                byte[] idToByte = Encoding.ASCII.GetBytes(item);

                // Ubacivanje liste u bazu, LPush ubacuje sa leve strane (kao stek, red)
                // Vraca broj elemenata u listi, zajedno sa dodatim elementom
                long elementsNumber = redis.LPush(listID, idToByte);

                successfully = true;
            }
            return successfully;
        }

        // Vraca sve elemente kao lista stringova
        public List<string> GetItemsFromList(string listID)
        {
            if (redis.ContainsKey(listID))
            {
                // --- Vraca sve elemente iz liste sa zadatim ID-om liste ---
                List<string> allItemsFromList = redis.GetAllItemsFromList(listID);
                return allItemsFromList;
            }
            else
                return null; 
        }

        // Brise element u listu, bez obzira na kojem je mestu
        public bool DeleteItemFromList(string listID, string itemID)
        {
             long successfully = redis.RemoveItemFromList(listID, itemID);

             if (successfully == 1)
             {
                 return true;
             }
             else
                 return false;
        }

        // Brise i vraca item sa pocetka liste 
        public string PopFromStart(string listID)
        {
            if (redis.ContainsKey(listID))
            {
                byte[] byteItem = redis.LPop(listID);

                return Encoding.ASCII.GetString(byteItem);

                // Mozda radi i ovo, ali samo brisanje (nije testirano)
                // return redis.RemoveStartFromList(listID);
            }
            else
                return "false";                
        }

        // Brise i vraca item sa kraja liste
        public string PopFromEnd(string listID)
        {
            if (redis.ContainsKey(listID))
            {
                byte[] byteItem = redis.RPop(listID);

                return Encoding.ASCII.GetString(byteItem);

                // Mozda radi i ovo, ali samo brisanje (nije testirano)
                //return redis.RemoveEndFromList(listID);
            }
            else
                return "false";    
            
        }

        // Prvo brise stari element iz liste, a zatim dodaje novi;; Na foru promenjen element je u stvari novi element
        public bool RenameItemFromList(string listID, string oldItem, string newItem)
        {
            bool successfully = false;
            if (redis.ContainsKey(listID))
            {
                if (DeleteItemFromList(listID, oldItem))
                {
                    successfully = AddItemToList(listID, newItem);
                }
            }
            return successfully;
        }

        // Clear-uje celu listu ;; --- NIJE TESTIRANO ---
        public bool ClearList(string listID)
        {
            // Ako zadati kljuc postoji u bazi
            if (redis.ContainsKey(listID))
            {
                redis.RemoveAllFromList(listID);
                return true;
            }
            else
                return false;         
        }

        // Brise bilo sta iz redisa sto poseduje takav kljuc (lista, hash, string)
        public bool DeleteByKey(string key)
        {
            return redis.Remove(key);
        }

        // --- Mislim da ne dodaje duplikate po istom id-u (radi rewrite)
        // public void AddUser(string ID, string val)
        // --- Dodavanje novog usera ---
       /* public bool AddUser(string dataUser)
        {
            // --- STA DA SE RADI UKOLIKO POSTOJI TAKAV ID ---
            // --- KAKO DA SE SREDI ZA ID, KAKAV DA SE UBACI ----

            // Ako se salje kao jedan string
            //string[] idValue = dataUser.Split(' ');
            //User user = new User { id = idValue[0], value = Convert.ToInt64(idValue[1]) };

            //ako se salje kao 2 stringa odvojena
            // User user = new User { id = ID, value = Convert.ToInt64(val) };

            List<long> listaValue = new List<long>();
            listaValue.Add(1000);
            listaValue.Add(2000);
            listaValue.Add(3000);
            listaValue.Add(6000);
            listaValue.Add(5000);
            listaValue.Add(9000);
            listaValue.Add(7000);
            listaValue.Add(8000);
            listaValue.Add(1000);

            User user = new User { id = "rip3", value = listaValue };

            bool uspesno = redis.Set<User>("userIDTest3", user);


            return uspesno;
        }

        public void testUser()
        {
            string searchString = "user*";

            var getSpecificKeys = redis.SearchKeys(searchString);
            List<User> listaOsoba = new List<User>();

            // Vraca sve sta nađe string, hash, lista
            // tako da moze da se desi da vrati neki drugi objekat
            foreach (var getKey in getSpecificKeys)
            {
                // filtriranje po objektu
                if (getKey.Contains("user"))
                {
                    User per = redis.As<User>().GetValue(getKey.ToString());
                    listaOsoba.Add(per);
                }
            }
        }

        // --- Vracanje usera po ID-u ---
        public string GetUser(string userId)
        {
            User user = redis.As<User>().GetValue("urn:user:" + userId);

            if (user != null)
            {
                // Ako treba da se serijalizuje
                string userJsonFormat = ServiceStack.Text.JsonSerializer.SerializeToString<User>(user);

                // A ako ne treba
                string simple = user.id + user.value.ToString();

                // Vrati serijalizovani ili ne
                return userJsonFormat;
            }
            else
            {
                // Ukoliko ne postoji
                return "false";
            }
        }

        // --- Brisanje usera po ID-u ---
        public bool DeleteUser(string userId)
        {
            // ZA TESTIRANJE
           bool uspesno = redis.Remove(userId);
           
            // Radi ali nema feedback od redisa
           //redis.DeleteById<User>(userId);

           return uspesno;
 
        }

        // --- Izmena usera ---
        public void RenameUser(string userValue)
        {
            // --- Izmena value kod usera ---
            // izgleda da je ovako                      
        }

        public class User
        {
            public string id { get; set; }
            //public long value { get; set; }
            public List<long> value { get; set; }
        }*/

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

    }
}

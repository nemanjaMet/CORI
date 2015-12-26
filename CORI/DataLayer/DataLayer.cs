using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            redis.SetValue("test", "PeraBosko", TimeSpan.FromSeconds(20));
            
        }
    }
}

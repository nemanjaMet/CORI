using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class Config
    {
        public static string SingleHost
        {
            get { return "localhost"; }
        }
        public const int RedisPort = 6379;
        public static string SingleHostConnectionString
        {
            get
            {
                return SingleHost + ":" + RedisPort;
            }
        }
        public static BasicRedisClientManager BasicClientManger
        {
            get
            {
                return new BasicRedisClientManager(new[] {
					SingleHostConnectionString
				});
            }
        }
    }
}

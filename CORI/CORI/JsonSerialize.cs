using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft;

namespace CORI
{
    public class JsonSerialize
    {
        public static string ToJson(object obj)
        {
            string outputJson = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return outputJson;
        }
    }
}
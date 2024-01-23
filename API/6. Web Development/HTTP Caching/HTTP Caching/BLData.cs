using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP_Caching
{
    public class BLData
    {
        public static Dictionary<string,string> Data()
        {
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            dicData.Add("1", "Punjab");
            dicData.Add("2", "Assam");
            dicData.Add("3", "UP");
            dicData.Add("4", "AP");
            dicData.Add("5", "J&K");
            dicData.Add("6", "Odisha");
            dicData.Add("7", "Delhi");
            dicData.Add("9", "Karnataka");
            dicData.Add("10", "Bangalore");
            dicData.Add("21", "Rajesthan");
            dicData.Add("31", "Jharkhand");
            dicData.Add("41", "chennai");
            dicData.Add("51", "jammu");
            dicData.Add("61", "Bhubaneshwar");
            dicData.Add("71", "Delhi");
            dicData.Add("19", "Karnataka");

            return dicData;
        }
    }
}
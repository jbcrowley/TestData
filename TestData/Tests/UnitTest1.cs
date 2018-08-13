using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestData
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCsv()
        {
            string fileName = "testdata.csv";
            Dictionary<string, string> data = Read(fileName);

            string accountname = data["accountname"];
            string accounttype = data["accounttype"];
            string daysinfuture = data["daysinfuture"];
            string password = data["password"];
            string username = data["username"];
            int number = int.Parse(data["number"]);
            bool boolean = bool.Parse(data["boolean"]);
            DateTime date = DateTime.Parse(data["date"]);

            Console.WriteLine(accountname);
            Console.WriteLine(accounttype);
            Console.WriteLine(daysinfuture);
            Console.WriteLine(password);
            Console.WriteLine(username);
            Console.WriteLine(number);
            Console.WriteLine(boolean);
            Console.WriteLine(date);
        }
        [TestMethod]
        public void TestJson()
        {
            string fileName = "testdata.json";
            JObject data = JObject.Parse(File.ReadAllText(fileName));
            string accountname = data["accountname"].ToString();
            Console.WriteLine(accountname.GetType());

            string accounttype = data["accounttype"].ToString();
            string daysinfuture = data["daysinfuture"].ToString();
            string password = data["password"].ToString();
            string username = data["username"].ToString();

            int number = data["number"].ToObject<int>();
            Console.WriteLine(number.GetType());

            bool boolean = data["boolean"].ToObject<bool>();
            Console.WriteLine(boolean.GetType());

            DateTime date = DateTime.Parse(data["date"].ToString());
            Console.WriteLine(date.GetType());

            JArray array = JArray.Parse(data["array"].ToString());
            string[] stringArray = new string[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                stringArray[i] = array[i].ToString();
            }
            Console.WriteLine(array.GetType());

            Console.WriteLine();
            Console.WriteLine(accountname);
            Console.WriteLine(accounttype);
            Console.WriteLine(daysinfuture);
            Console.WriteLine(password);
            Console.WriteLine(username);
            Console.WriteLine(number);
            Console.WriteLine(boolean);
            Console.WriteLine(date);
            Console.WriteLine(string.Join(",", stringArray));
        }
        [TestMethod]
        public void TestXml()
        {
        }

        public static Dictionary<string, string> Read(string fileName)
        {
            var dict = new Dictionary<string, string>();

            using (var fileReader = File.OpenText(fileName))
            using (var csvReader = new CsvReader(fileReader))
            {
                csvReader.Configuration.HasHeaderRecord = false;
                while (csvReader.Read())
                {
                    var record = csvReader.GetRecord<Record>();
                    dict.Add(record.Key, record.Value);
                }
            }

            return dict;
        }

        public class Record
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
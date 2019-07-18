using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;
using Newtonsoft.Json;
using static Microsoft.Spark.Sql.Functions;

namespace Saturn
{
    class Program
    {
        static void Main(string[] args)
        {

            File.Copy(@"bin\Debug\netcoreapp2.2\Saturn.dll", @"C:\spark\Microsoft.Spark.Worker-0.3.0\Saturn.dll", true);

            var rules = new List<RuleSet>();

            using (var r = new StreamReader("rules.json"))
            {
                string json = r.ReadToEnd();
                rules = JsonConvert.DeserializeObject<List<RuleSet>>(json);
                Console.WriteLine($"Found {rules.Count} rules");
            }

            SparkSession spark = SparkSession
                         .Builder()
                         .AppName("saturn")
                         .GetOrCreate();

            DataFrame df = spark.Read().Json("sample-application");

            df.PrintSchema();

            df.Show();

            foreach (var rule in rules)
            {
                df = df.WithColumn(rule.PropertyName, df[rule.MapSource]);
            }


            df.Show();

            df = df.Select(rules.Select(i => df[i.PropertyName]).ToArray());

            df.Write().Json("output");
        }
    }
}

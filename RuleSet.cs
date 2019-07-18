using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;
using static Microsoft.Spark.Sql.Functions;

namespace Saturn
{
    public class RuleSet
    {
        public RuleSet()
        {

        }

        public string PropertyName { get; set; }
        public string MapSource { get; set; }
    }
}

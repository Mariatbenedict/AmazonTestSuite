using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Amazon_Test_Project.Utils
{
    class DataUtils
    {
        public TestDataModel LoadData()
        {           
           TestDataModel data = JsonConvert.DeserializeObject<TestDataModel>(File.ReadAllText(@"D:\Documents\AmazonTest\Amazon.json"));
            return data;
        }
    }
}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplelabs.DynamoDBAutoScale.AWS;

namespace Simplelabs.DynamoDBAutoScale.Test
{
    [TestClass]
    public class CloudWatchClientTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new CloudWatchClient();
            client.GetTableInformation("TestTable", DateTime.UtcNow.AddHours(-8), DateTime.UtcNow);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplelabs.DynamoDBAutoScale.AWS;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using System.Globalization;
using System.Threading.Tasks;

namespace Simplelabs.DynamoDBAutoScale.Test
{
    [TestClass]
    public class DynamoDBClientTest
    {
        [TestMethod]
        public void ListTables()
        {
            var cliente = new DynamoDBClient();
            var l = cliente.ListTables();
        }

        [TestMethod]
        public void DescribeTable()
        {
            var cliente = new DynamoDBClient();
            var t = cliente.DescribeTable("TestTable");
        }

        [TestMethod]
        public void UpdateProvisionedThroughput()
        {
            var cliente = new DynamoDBClient();
            var u = cliente.UpdateProvisionedThroughput("TestTable", 2, 2);
        }

        [TestMethod]
        public void PutItem()
        {

            var cliente = new DynamoDBClient();
            var attributes = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>();
            attributes.Add("ID Tabla", new AttributeValue() { S = Guid.NewGuid().ToString() });
            attributes.Add("Nombre", new AttributeValue() { S = "Pedro" });
            attributes.Add("Apellido", new AttributeValue() { S = "Perez" });
            attributes.Add("FechaNacimiento", new AttributeValue() { S = DateTime.Now.ToLongDateString() });
            attributes.Add("Sueldo", new AttributeValue() { N = "10023444.230" });

            var p = cliente.PutItem("TestTable", attributes);
        }

        [TestMethod]
        public void PutItem2()
        {
            var cliente = new DynamoDBClient();

            for (int i = 0; i < 100; i++)
            {
                var attributes = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>();
                attributes.Add("ID Tabla", new AttributeValue() { S = Guid.NewGuid().ToString() });
                attributes.Add("Nombre", new AttributeValue() { S = "Pedro" + i });
                attributes.Add("Apellido", new AttributeValue() { S = "Perez" + i });
                attributes.Add("FechaNacimiento", new AttributeValue() { S = DateTime.Now.ToString() });
                attributes.Add("Sueldo", new AttributeValue() { N = (10023444.230 / (i + 1)).ToString(CultureInfo.InvariantCulture) });
                var p = cliente.PutItem("TestTable", attributes);
            }
        }

        [TestMethod]
        public void PutItem3()
        {
            Parallel.For(0, 500, new ParallelOptions() { MaxDegreeOfParallelism = 4 }, (i) =>
            {
                using (var cliente = new DynamoDBClient())
                {
                    var attributes = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>();
                    attributes.Add("ID Tabla", new AttributeValue() { S = Guid.NewGuid().ToString() });
                    attributes.Add("Nombre", new AttributeValue() { S = "Pedro" + i });
                    attributes.Add("Apellido", new AttributeValue() { S = "Perez" + i });
                    attributes.Add("FechaNacimiento", new AttributeValue() { S = DateTime.Now.ToString() });
                    attributes.Add("Sueldo", new AttributeValue() { N = (10023444.230 / (i + 1)).ToString(CultureInfo.InvariantCulture) });
                    var p = cliente.PutItem("TestTable", attributes);
                }
            });
        }
    }
}

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Simplelabs.DynamoDBAutoScale.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplelabs.DynamoDBAutoScale.AWS
{
    public class DynamoDBClient : IDisposable   
    {
        private IAmazonDynamoDB _service;
        public DynamoDBClient()
        {
            var aws = CManager.Settings.AWS;
            _service = AWSClientFactory.CreateAmazonDynamoDBClient(aws.AccessKey, aws.SecretAccessKey, RegionEndpoint.GetBySystemName(aws.RegionEndpoint));
        }

        ~DynamoDBClient()
        {
            Dispose(false);
        }

        public ListTablesResponse ListTables()
        {
            return _service.ListTables();
        }

        public DescribeTableResponse DescribeTable(string tableName)
        {
            return _service.DescribeTable(new DescribeTableRequest()
            {
                TableName= tableName
            });
        }

        public UpdateTableResponse UpdateProvisionedThroughput(string tableName, long readCapacityUnits, long writeCapacityUnits)
        {
            return _service.UpdateTable(new UpdateTableRequest() {
                TableName = tableName,
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = readCapacityUnits,
                    WriteCapacityUnits = writeCapacityUnits
                }
            });
        }

        public PutItemResponse PutItem(string tableName, Dictionary<string, AttributeValue> attributes)
        {
            return _service.PutItem(new PutItemRequest()
            {
                TableName = tableName,
                Item = attributes
            });
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_service != null)
                {
                    _service.Dispose();
                    _service = null;
                }
            }
        }
    }
}

using Amazon;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Simplelabs.DynamoDBAutoScale.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplelabs.DynamoDBAutoScale.AWS
{
    public class CloudWatchClient : IDisposable
    {
        private const string nameSpaceDynamoDB = "AWS/DynamoDB";
        private IAmazonCloudWatch _service;

        public CloudWatchClient()
        {
            var aws = CManager.Settings.AWS;
            _service = AWSClientFactory.CreateAmazonCloudWatchClient(aws.AccessKey, aws.SecretAccessKey, RegionEndpoint.GetBySystemName(aws.RegionEndpoint));
        }

        ~CloudWatchClient()
        {
            Dispose(false);
        }

        public void GetTableInformation(string tableName, DateTime startTime, DateTime endTime)
        {
            var dimensions = new List<Dimension>();
            dimensions.Add(new Dimension() { Name = "TableName", Value = tableName });
            var r = _service.GetMetricStatistics(new GetMetricStatisticsRequest() {
                Dimensions = dimensions,
                StartTime = startTime,
                EndTime = endTime,
                Period = 300,
                MetricName = "ConsumedWriteCapacityUnits",
                Namespace = nameSpaceDynamoDB,
                Statistics = new List<string>() { "Sum" }
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

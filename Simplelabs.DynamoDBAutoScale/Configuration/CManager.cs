using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplelabs.DynamoDBAutoScale.Configuration
{
    internal static class CManager
    {
        private const string sectionName = "simplelabs.dynamodbautoscale";
        private static DynamoDBAutoScaleSettings _setting;

        static CManager()
        {
            _setting = (DynamoDBAutoScaleSettings)ConfigurationManager.GetSection(sectionName);
        }

        internal static DynamoDBAutoScaleSettings Settings
        {
            get
            {
                return _setting;
            }
        }
    }
}

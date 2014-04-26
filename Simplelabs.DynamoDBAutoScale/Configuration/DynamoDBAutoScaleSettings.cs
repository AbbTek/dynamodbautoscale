using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplelabs.DynamoDBAutoScale.Configuration
{
    public class DynamoDBAutoScaleSettings : ConfigurationSection
    {
        private const string aws = "aws";

        [ConfigurationProperty(aws)]
        public AWSSettings AWS
        {
            get
            {
                return (AWSSettings)this[aws];
            }
        }
    }
}

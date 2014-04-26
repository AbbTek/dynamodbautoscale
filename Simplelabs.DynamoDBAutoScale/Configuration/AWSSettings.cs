using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplelabs.DynamoDBAutoScale.Configuration
{
    public class AWSSettings : ConfigurationElement
    {
        private const string accessKey = "accessKey";
        private const string secretAccessKey = "secretAccessKey";
        private const string regionEndpoint = "regionEndpoint";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(accessKey, IsRequired = true)]
        public string AccessKey
        {
            get
            {
                return (string)this[accessKey];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(secretAccessKey, IsRequired = true)]
        public string SecretAccessKey
        {
            get
            {
                return (string)this[secretAccessKey];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(regionEndpoint, IsRequired = true)]
        public string RegionEndpoint
        {
            get
            {
                return (string)this[regionEndpoint];
            }
        }
    }
}

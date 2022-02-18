using Gringotts.Core.DbUtil.Interface;

namespace Gringotts.Core.DbUtil.Config
{
    public class GringottsDbSettings : IGringottsDbSettings
    {
        public string DatabaseName { get; set; }
        public string CustomerCollection { get; set; }
        public string AccountCollection { get; set; }
        public string TransactionCollection { get; set; }
        public string ConnectionString { get; set; }
    }
}

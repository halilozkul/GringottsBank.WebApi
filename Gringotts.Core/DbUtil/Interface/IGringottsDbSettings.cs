using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Core.DbUtil.Interface
{
    public interface IGringottsDbSettings
    {
        string DatabaseName { get; set; }
        string CustomerCollection { get; set; }
        string AccountCollection { get; set; }
        string TransactionCollection { get; set; }
        string ConnectionString { get; set; }
    }
}

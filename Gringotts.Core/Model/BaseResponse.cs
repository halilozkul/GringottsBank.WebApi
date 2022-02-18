using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.Core.Model
{
    public class BaseResponse<T>
    {
        public const string RESPONSE_CODE_SUCCESS = "000";
        public const string RESPONSE_MESSAGE_SUCCESS = "SUCCESS";
        public string RspCode { get; set; } = BaseResponse<T>.RESPONSE_CODE_SUCCESS;
        public string RspMsg { get; set; } = BaseResponse<T>.RESPONSE_MESSAGE_SUCCESS;
        public T ResultObj { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Token.YonYou
{
    /// <summary>
    /// 访问令牌
    /// </summary>
    public class AccessTokenRequest
    {
        /// <summary>
        /// Key
        /// </summary>
        public string appKey { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string appSecret { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
    }
}

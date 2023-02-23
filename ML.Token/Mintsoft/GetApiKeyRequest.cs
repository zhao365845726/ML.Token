using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Token.Mintsoft
{
    public class GetApiKeyRequest
    {
        /// <summary>
        /// OMS User Name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// OMS Password
        /// </summary>
        public string Password { get; set; }
    }
}

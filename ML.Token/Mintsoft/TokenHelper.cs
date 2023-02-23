using ML.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Token.Mintsoft
{
    public class TokenHelper
    {
        /// <summary>
        /// 【Use】Mintsoft的Auth
        /// </summary>
        /// <returns></returns>
        public static string GetMintsoftApiAuth(GetApiKeyRequest request)
        {
            //TODO:获取Redis的AccessToken，如果存在，则直接返回，如果不存在，再重新创建
            string strAccessToken = RedisHelper.Get<string>($"apiAuth:{request.UserName}");
            if (strAccessToken != null)
            {
                return strAccessToken;
            }
            string requestUrl = $"https://api.mintsoft.co.uk/api/Auth?UserName={request.UserName}&Password={request.Password}";
            var result = HttpHelper.Get(requestUrl);
            if (result != null)
            {
                //加入Redis
                RedisHelper.Set($"apiAuth:{request.UserName}", result.ToString(), 7200);
                return result.ToString();
            }
            return result;
        }
    }
}

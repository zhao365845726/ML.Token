using ML.Core;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ML.Token.YonYou
{
    public class TokenHelper
    {
        /// <summary>
        /// 【Use】企业自建
        /// </summary>
        /// <returns></returns>
        public static string GetSelfAccessToken(string filePath,string ysEnv)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数
            AccessTokenRequest request = JsonHelper.GetConfigInfo<AccessTokenRequest>(filePath, ysEnv);
            request.timestamp = timeStamp.ToString();
            //TODO:获取Redis的AccessToken，如果存在，则直接返回，如果不存在，再重新创建
            string strAccessToken = RedisHelper.Get<string>($"accessToken:{request.appKey}");
            if (strAccessToken != null)
            {
                return strAccessToken;
            }
            string requestUrl = $"https://api.diwork.com/open-auth/selfAppAuth/getAccessToken?appKey={request.appKey}&timestamp={request.timestamp}&signature={CounterSign(request)}";
            var result = HttpHelper.Get(requestUrl);
            var model = JsonConvert.DeserializeObject<IDictionary<string, object>>(result.ToString());
            var response = JsonConvert.DeserializeObject<IDictionary<string, object>>(model["data"].ToString());
            //加入Redis
            RedisHelper.Set($"accessToken:{request.appKey}", response["access_token"].ToString(), Convert.ToInt32(response["expire"].ToString()));
            return response["access_token"].ToString();
        }

        /// <summary>
        /// 服务商获取Token
        /// </summary>
        /// <returns></returns>
        public static string GetServiceAccessToken()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数

            //TODO:获取AccessToken
            var param = new
            {
                suiteKey = "",
                suiteTicket = "",
                tenantId = "",
                timestamp = timeStamp,
                signature = "",
            };

            string requestUrl = $"https://open.diwork.com/open-auth/suiteApp/getAccessToken?suiteKey={param.suiteKey}&suiteTicket={param.suiteTicket}&tenantId={param.tenantId}&timestamp={param.timestamp}&signature={param.signature}";
            var response = HttpHelper.Get(requestUrl);
            return response;
        }

        

        public static string CounterSign(AccessTokenRequest request)
        {
            string strData = $"appKey{request.appKey}timestamp{request.timestamp}";
            //URLEncode( Base64( HmacSHA256( parameterMap ) ) )
            return UrlEncode(CreateToken(strData, request.appSecret));
        }

        public static string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }

        public static string GetPurchaseorderYear(string code)
        {
            return $"20{code.Substring(8, 2)}";
        }
    }
}

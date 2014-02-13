using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WeChatApi.Code.Util
{
    public class SignatureUtil
    {
        public static readonly string Token = System.Configuration.ConfigurationManager.AppSettings["Token"];

        public static bool CheckSignature(string signature, string timestamp, string nonce)
        {
            return signature == GetSignature(timestamp, nonce);
        }

        public static string GetSignature(string timestamp, string nonce)
        {
            var arr = new[] { Token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            return enText.ToString();
        }
    }
}
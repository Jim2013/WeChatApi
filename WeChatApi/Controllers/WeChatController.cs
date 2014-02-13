using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using WeChatApi.Code.Util;

namespace WeChatApi.Controllers
{
    public class WeChatController : ApiController
    {
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            if (SignatureUtil.CheckSignature(signature, timestamp, nonce))
            {
                responseMessage.Content = new StringContent(echostr);
            }
            else
            {
                responseMessage.Content = new StringContent("signature failed:" + signature);
            }

            return responseMessage;
        }

        public  HttpResponseMessage Post(string signature, string timestamp, string nonce)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            if (!SignatureUtil.CheckSignature(signature, timestamp, nonce))
            {
                responseMessage.Content = new StringContent("参数错误!");
            }

            XDocument doc = new XDocument();

            using (XmlReader xr = XmlReader.Create(HttpContext.Current.Request.InputStream))
            {
                doc = XDocument.Load(xr);
            }

            //TODO: logic code here

            return responseMessage;
        }
    }
}

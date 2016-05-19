using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Collections;


/// <summary>
/// WeiXinApi 的摘要说明
/// </summary>


    public class WeiXinApi
    {

        #region 从WebConfig获取微信基本配置
        //获取appid
        private string appid = ConfigurationManager.AppSettings["appid"];

        public string Appid
        {
            get { return appid; }
        }
        //获取公众号的appsecret
        private string secret = ConfigurationManager.AppSettings["AppSecret"];
        //获取用户授权回调页面redirect_uri
        //private string redirect_uri = HttpUtility.UrlEncode(ConfigurationManager.AppSettings["redirect_uri"]);
        //获取基本信息类型，snsapi_base表示只获取进入页面的用户的openid，snsapi_userinfo用来获取用户的基本信息。
        private string scope = "snsapi_userinfo";
        public string Scope
        {
            get { return scope; }
            set { scope = value; }
        }
        #endregion

        #region 微信全局配置
        #region 获取微信全局access_token
        /// <summary>
        /// 获取基础的access_token，返回access_token符串
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            Cache objCache = HttpRuntime.Cache;
            string result = string.Empty;
            if (objCache["access_token"] == null)
            {
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
                result = SendRequestByGet(url);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                IDictionary json = jss.Deserialize<Dictionary<string, object>>(result);
                if (json["errcode"] == null)
                {
                    objCache.Insert("access_token", json["access_token"].ToString(), null, DateTime.Now.AddSeconds(Convert.ToDouble(json["expires_in"])), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                }
                result = json["access_token"].ToString();
            }
            else
            {
                result = objCache["access_token"].ToString();
            }
            return result;
        }
        #endregion

        #region JS-SDK使用权限签名算法
        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <returns></returns>
        private string GetJsApiTicket()
        {
            Cache objCache = HttpRuntime.Cache;
            string result = string.Empty;
            if (objCache["ticket"] == null)
            {
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + GetAccessToken() + "&type=jsapi";
                result = SendRequestByGet(url);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                IDictionary json = jss.Deserialize<Dictionary<string, object>>(result);
                if (json["errcode"].ToString() == "0" || json["errmsg"].ToString() == "ok")
                {
                    objCache.Insert("ticket", json["ticket"].ToString(), null, DateTime.Now.AddSeconds(Convert.ToDouble(json["expires_in"])), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                    result = json["ticket"].ToString();
                }
            }
            else
            {
                result = objCache["ticket"].ToString();
            }
            return result;
        }

        /// <summary>
        /// 生成调用JS-SDK的签名
        /// </summary>
        /// <param name="noncestr">生成签名的随机串</param>
        /// <param name="timestamp">生成签名的时间戳</param>
        /// <param name="url">当前页面的URL</param>
        /// <returns></returns>
        public string GetJsApiSignature(string noncestr, string timestamp, string url)
        {
            string strSha1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", GetJsApiTicket(), noncestr, timestamp, url);
            return StrToSha1(strSha1).ToLower();//System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSha1, "sha1").ToLower();
        }

    #endregion
    
        #endregion

        #region 微信授权登录

        #region 获取微信code
        /// <summary>
        /// 获取微信code
        /// </summary>
        ///获取跳转回调页的url，尤其注意：由于授权操作安全等级较高，所以在发起授权请求时，微信会对授权链接做正则强匹配校验，如果链接的参数顺序不对，授权页面将无法正常访问
        ///URL示例：https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect
        public void GetCode(string redirect_uri,string scope)
        {
        
            HttpContext.Current.Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + redirect_uri + "&response_type=code&scope=" + scope + "&state=Cnjdsoft#wechat_redirect");
        }
        public void GetCodeex(string redirect_uri, string scope)
        {

            HttpContext.Current.Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" +wxapi.TenpayUtil.appid + "&redirect_uri=" + redirect_uri + "&response_type=code&scope=" + scope + "&state=Cnjdsoft#wechat_redirect");
        }
        #endregion

        #region 获取access_token
        /// <summary>
        /// 获取授权登录access_token,返回json格式字符串
        /// </summary>
        /// <param name="code">微信返回的code值</param>
        /// <returns></returns>
        /// URL示例：https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
        public string GetAuthAccessToken(string code)
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + secret + "&code=" + code + "&grant_type=authorization_code";
            return SendRequestByGet(url);
        }
        #endregion

        #region 获取微信用户信息(需scope为 snsapi_userinfo)
        /// <summary>
        /// 获取微信用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="access_token">用户access_token</param>
        /// <param name="openid">用户openid</param>
        /// <returns></returns>
        public string GetUserInfo(string access_token, string openid)
        {
            string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + openid + "&lang=zh_CN";
            return SendRequestByGet(url);
        }
        #endregion

        #endregion

        #region 微信卡券

        #region 获取卡券api_ticket
        /// <summary>
        /// 通过access_token获取调用微信卡券JS API的临时票据，有效期为7200秒
        /// </summary>
        /// <returns></returns>
        public string GetApiTicket()
        {
            string result = string.Empty;
            Cache objCache = HttpRuntime.Cache;
            if (objCache["api_ticket"] == null)
            {
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + GetAccessToken() + "&type=wx_card";
                result = SendRequestByGet(url);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                IDictionary json = jss.Deserialize<Dictionary<string, object>>(result);
                if (json["errcode"].ToString() == "0" || json["errmsg"].ToString().ToLower() == "ok")
                {
                    objCache.Insert("api_ticket", json["ticket"].ToString(), null, DateTime.Now.AddSeconds(Convert.ToDouble(json["expires_in"])), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.NotRemovable, null);
                    result = json["ticket"].ToString();
                }
            }
            else
            {
                result = objCache["api_ticket"].ToString();
            }
            return result;
        }
        #endregion

        #region 生成卡券签名
        /// <summary>
        /// 生成卡券签名
        /// </summary>
        /// <param name="location_id"></param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="noncestr">随机字符串</param>
        /// <param name="card_id"></param>
        /// <param name="card_type"></param>
        /// <returns></returns>
        public string GetCardSign(string location_id, string timestamp, string noncestr, string card_id, string card_type)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("api_ticket", GetApiTicket());
            dic.Add("app_id", appid);
            dic.Add("card_id", card_id);
            dic.Add("card_type", card_type);
            dic.Add("location_id", location_id);
            dic.Add("nonce_str", noncestr);
            dic.Add("timestamp", timestamp);
            //对字典进行排序
            string result = string.Empty;
            var dicSort = from objDic in dic orderby objDic.Value ascending select objDic;
            foreach (KeyValuePair<string, string> kvp in dicSort)
                result += kvp.Value;

            return StrToSha1(result).ToLower();
        }
        #endregion

        #region 获取卡券详情
        /// <summary>
        /// 获取卡券详情,返回json格式字符串
        /// </summary>
        /// <param name="CardID">卡券ID</param>
        /// <returns></returns>
        public string GetCardDetail(string CardID)
        {
            string url = "https://api.weixin.qq.com/card/get?access_token=" + GetAccessToken(), parmes = "{\"card_id\":\"" + CardID + "\"}";
            return SendRequestByPost(url, parmes);
        }
        #endregion

        #region 核销卡券
        /// <summary>
        /// 获取解密后的真实Code码
        /// </summary>
        /// <param name="encrypt_code">用户选取卡券的encrypt_code</param>
        /// <returns></returns>
        public string GetCardCode(string encrypt_code)
        {
            string url = "https://api.weixin.qq.com/card/code/decrypt?access_token=" + GetAccessToken();
            string post_data = "{\"encrypt_code\":\"" + encrypt_code + "\"}";
            string result = SendRequestByPost(url, post_data);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            IDictionary json = jss.Deserialize<Dictionary<string, object>>(result);
            if (json["errmsg"].ToString() == "ok")
            {
                result = json["code"].ToString();
            }
            else {
                result = null;
            }
            return result;
        }
        /// <summary>
        /// 检查微信卡券code是否有效
        /// </summary>
        /// <param name="cardid">cardid</param>
        /// <param name="code">code</param>
        /// <returns></returns>
        public bool CheckCodeIsEffective(string code) {
            bool bol = false;
            string url = "https://api.weixin.qq.com/card/code/get?access_token=" + GetAccessToken();
            string post_data = "{\"code\" : \"" + code + "\",\"check_consume\" : false}";
            string result = SendRequestByPost(url, post_data);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            IDictionary json = jss.Deserialize<Dictionary<string, object>>(result);
            if (json["errcode"].ToString() == "0")
            {
                bol = json["can_consume"].ToString() == "True";
            }
            return bol;
        }
        public bool UsingCard(string code) {
            bool bol = false;
            string url = "https://api.weixin.qq.com/card/code/consume?access_token=" + GetAccessToken();
            string post_data = "{\"code\": \"" + code + "\"}";
            string result = SendRequestByPost(url, post_data);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            IDictionary json = jss.Deserialize<Dictionary<string, object>>(result);
            if (json["errcode"] == "0")
            {
                bol = true;
            }
            return bol;
        }
        #endregion
        #endregion

        #region 公用方法

        #region 字典排序
        /// <summary>
        /// 将字符串的字典序排序
        /// </summary>
        /// <param name="dic">要排序的字典</param>
        /// <returns></returns>
        private string DictonarySort(Dictionary<string, string> dic)
        {
            string result = string.Empty;
            var dicSort = from objDic in dic orderby objDic.Value descending select objDic;
            foreach (KeyValuePair<string, string> kvp in dicSort)
                result += kvp.Value;
            return result;
        }
        #endregion

        #region sha1加密
        public string StrToSha1(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "sha1");
        }

        #endregion

        #region 服务端的Post/Get
        /// <summary>
        /// 通过GET方式发送请求到目标地址
        /// </summary>
        /// <param name="url">地址url</param>
        /// <returns></returns>
        public string SendRequestByGet(string url)
        {
            string strResult = string.Empty;
            try
            {
                System.Net.WebRequest wrq = System.Net.WebRequest.Create(url);
                wrq.Method = "GET";

                System.Net.WebResponse wrp = wrq.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(wrp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));

                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string SendRequestByPost(string url, string parames)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(parames);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "post";
            req.Timeout = 120 * 1000;
            req.ContentType = "text/html";
            req.ContentType = "application/x-www-form-urlencoded;";

            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Flush();
            }
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理 

                Stream strm = wr.GetResponseStream();

                StreamReader sr = new StreamReader(strm, System.Text.Encoding.UTF8);

                string line;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line + System.Environment.NewLine);
                }
                sr.Close();
                strm.Close();
                return sb.ToString();
            }
        }
        /// <summary>
        /// 服务端的Post/Get，返回string字符串
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parames">请求附带的参数</param>
        /// <param name="method">请求方式，Post/Get</param>
        /// <returns></returns>
        public string SendRequest(string url, string parames, string method)
        {
            string strResult = string.Empty;

            if (url == null || url == "")
                return null;

            if (method == null || method == "")
                method = "GET";

            if (method.ToUpper() == "GET")
            {
                try
                {
                    System.Net.WebRequest wrq = System.Net.WebRequest.Create(url + parames);
                    wrq.Method = "GET";

                    System.Net.WebResponse wrp = wrq.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(wrp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));

                    strResult = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else if (method.ToUpper() == "POST")
            {
                if (parames.Length > 0 && parames.IndexOf('?') == 0)
                {
                    parames = parames.Substring(1);
                }

                WebRequest req = WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                System.Text.StringBuilder UrlEncoded = new System.Text.StringBuilder();
                Char[] reserved = { '?', '=', '&' };
                byte[] SomeBytes = null;
                if (parames != null)
                {
                    int i = 0, j;
                    while (i < parames.Length)
                    {
                        j = parames.IndexOfAny(reserved, i);
                        if (j == -1)
                        {
                            UrlEncoded.Append(HttpUtility.UrlEncode(parames.Substring(i, parames.Length - i), System.Text.Encoding.GetEncoding("UTF-8")));
                            break;
                        }
                        UrlEncoded.Append(HttpUtility.UrlEncode(parames.Substring(i, j - i), System.Text.Encoding.GetEncoding("UTF-8")));
                        UrlEncoded.Append(parames.Substring(j, 1));
                        i = j + 1;
                    }
                    SomeBytes = System.Text.Encoding.Default.GetBytes(UrlEncoded.ToString());
                    req.ContentLength = SomeBytes.Length;
                    Stream newStream = req.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }
                try
                {
                    WebResponse result = req.GetResponse();
                    Stream ReceiveStream = result.GetResponseStream();
                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);
                    while (bytes > 0)
                    {
                        System.Text.Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                        strResult += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
                    }
                    return strResult;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return strResult;
        }
        #endregion
        #endregion

    }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.Caching;
using System.Collections;
using System.Net;
using System.IO;

namespace wxapi
{
    /// <summary>
    /// WXToolsHelper 的摘要说明
    /// </summary>
    public class WXToolsHelper
    {
        public WXToolsHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 字段
        Hashtable parameters = new Hashtable();
        #endregion

        /// <summary>
        /// 获取全局的access_token,程序缓存
        /// </summary>      
        /// <returns>得到的全局access_token</returns>
        public string Getaccess_token()
        {
            try
            {
                //先查缓存数据
                if (HttpContext.Current.Cache["access_token"] != null)
                {
                    return HttpContext.Current.Cache["access_token"].ToString();
                }
                else
                {
                    return Gettoken();
                }
            }
            catch
            {
                return Gettoken();
            }
        }

        /// <summary>
        /// 获取全局的access_token
        /// </summary>     
        /// <returns>得到的全局access_token</returns>
        private string Gettoken()
        {
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/token?", string.Format("grant_type=client_credential&appid={0}&secret={1}", TenpayUtil.appid, TenpayUtil.key), "Post");
            var jss = new JavaScriptSerializer();
            var access_tokenMsg = jss.Deserialize<Dictionary<string, object>>(data);
            //放入缓存中
            HttpContext.Current.Cache.Insert("access_token", access_tokenMsg["access_token"], null, DateTime.Now.AddSeconds(7100), TimeSpan.Zero, CacheItemPriority.Normal, null);
            //清除jsapi_ticket缓存
            HttpContext.Current.Cache.Remove("ticket");

            //获取jsapi_ticket,为了同步
            GetTicket();

            return access_tokenMsg["access_token"].ToString();
        }
        #region 生成button按钮
        public string wx_button() {
            string json = "{\"button\":[{\"type\": \"view\",\"name\": \"私募内参\",\"url\": \"http://wx.goto0570.net/index.aspx?type=1\"},{\"type\": \"view\",\"name\": \"如何推广\",\"url\":\"http://wx.goto0570.net/index.aspx?type=2\"},{\"type\": \"view\",\"name\": \"我的福利\",\"url\": \"http://wx.goto0570.net/index.aspx?type=0\"}]}";
            var ss = Getaccess_token();
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + ss, json, "Post");
            return data;
        }
        #endregion
        #region 生成二维码
        public string we_code(string id)
        {
            string json = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \""+id+"\"}}}";
            var ss = Getaccess_token();
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + ss, json, "Post");         
            return data;

        }
        #endregion
        #region 更具json格式输出你要的数组
        /// <summary>
        /// 获取json的数据
        /// </summary>
        /// <param name="name">获取字段的名称</param>
        /// <param name="data">元数据</param>
        /// <returns></returns>
        public string json_text(string name, string data)
        {
            var jss = new JavaScriptSerializer();
            var access_tokenMsg = jss.Deserialize<Dictionary<string, object>>(data);
            return access_tokenMsg[name].ToString();
        }
        #endregion
      
        public string[] GetOpenID(string code)
        {
            string[] array = new string[2];
            var data = webRequestPost("https://api.weixin.qq.com/sns/oauth2/access_token?", string.Format("appid={0}&secret={1}&code={2}&grant_type=authorization_code", TenpayUtil.appid, TenpayUtil.key, code), "Post");
            var jss = new JavaScriptSerializer();
            var access_tokenMsg = jss.Deserialize<Dictionary<string, object>>(data);
            //放入缓存中
            HttpContext.Current.Cache.Insert("lgaccess_token", access_tokenMsg["access_token"], null, DateTime.Now.AddSeconds(7100), TimeSpan.Zero, CacheItemPriority.Normal, null);
            HttpContext.Current.Cache.Insert("openid", access_tokenMsg["openid"], null, DateTime.Now.AddSeconds(7100), TimeSpan.Zero, CacheItemPriority.Normal, null);
            ////清除jsapi_ticket缓存
            HttpContext.Current.Cache.Remove("lgaccess_token");
            array[0] = access_tokenMsg["openid"].ToString();
            array[1] = access_tokenMsg["access_token"].ToString();
            return array;
        }
        /// <summary>
        /// 授权获取用户的个人信息
        /// </summary>
        /// <param name="param">传入的数据为数组，0为openid，1为access_token</param>
        /// <returns></returns>
        public string GetWxUserInfo(string[] param)
        {
            Write(string.Join("|", param));
            var data = webRequestPost("https://api.weixin.qq.com/sns/userinfo?", string.Format("access_token={0}&openid={1}&lang=zh_CN", param[1],param[0]),"Post");
            Write(data);
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            try
            {
                //放入缓存中
                
                return data;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 不授权获取用户的个人信息
        /// </summary>
        /// <param name="openID">会员的openID</param>
        /// <returns></returns>
        public string GetWxUserInfo(string openID)
        {
            
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/user/info?", string.Format("access_token={0}&openid={1}&lang=zh_CN", Getaccess_token(), openID), "Post");
            Write(data);
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            try
            {
                //放入缓存中

                return data;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 单发消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string MessageUser(string openid, string content)
        {
            var json = "{\"touser\":\"" + openid + "\",\"msgtype\":\"text\",\"text\": { \"content\": \"" + content + "\"}}";
          
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Getaccess_token(), json, "post");
            return data;
        }
        /// <summary>
        /// 单发消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string MessageAllUser(string openid, string content)
        {
            var json = "{\"touser\":[" + openid + "],\"msgtype\":\"text\",\"text\": { \"content\": \"" + content + "\"}}";
            WXToolsHelper.Write(json);
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token=" + Getaccess_token(), json, "post");
            return data;
        }


        /// <summary>
        /// 获取jsapi_ticket,程序缓存
        /// </summary>
        /// <returns>得到的jsapi_ticket</returns>
        public string GetJsapi_Ticket()
        {
            try
            {
                //先查缓存数据
                if (HttpContext.Current.Cache["ticket"] != null)
                {
                    return HttpContext.Current.Cache["ticket"].ToString();
                }
                else
                {
                    return GetTicket();
                }
            }
            catch
            {
                return GetTicket();
            }
        }

        /// <summary>
        /// 获取卡包api_ticket
        /// </summary>
        /// <returns></returns>
        public string Getapi_Ticket()
        {
            try
            {
                //先查缓存数据
                if (HttpContext.Current.Cache["api_ticket"] != null)
                {
                    return HttpContext.Current.Cache["api_ticket"].ToString();
                }
                else
                {
                    return GetApiTicket();
                }
            }
            catch
            {
                return GetApiTicket();
            }
        }
        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <returns>得到的jsapi_ticket</returns>
        public string GetTicket()
        {
            //var client = new System.Net.WebClient();
            //client.Encoding = System.Text.Encoding.UTF8;
            //var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", Getaccess_token());
            //var data = client.DownloadString(url);
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/ticket/getticket?", string.Format("access_token={0}&type=jsapi", Getaccess_token()), "Post");
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            try
            {
                //放入缓存中
                HttpContext.Current.Cache.Insert("ticket", ticketMsg["ticket"], null, DateTime.Now.AddSeconds(7100), TimeSpan.Zero, CacheItemPriority.Normal, null);
                return ticketMsg["ticket"].ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #region 卡包
        /// <summary>
        /// 优惠券卡包的api_ticket
        /// </summary>
        /// <returns></returns>
        private string GetApiTicket()
        {
            //var client = new System.Net.WebClient();
            //client.Encoding = System.Text.Encoding.UTF8;
            //var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=wx_card", Getaccess_token());
            //var data = client.DownloadString(url);
            var data = webRequestPost("https://api.weixin.qq.com/cgi-bin/ticket/getticket?", string.Format("access_token={0}&type=wx_card", Getaccess_token()), "Post");
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            try
            {
                //放入缓存中
                HttpContext.Current.Cache.Insert("api_ticket", ticketMsg["ticket"], null, DateTime.Now.AddSeconds(7100), TimeSpan.Zero, CacheItemPriority.Normal, null);
                return ticketMsg["api_ticket"].ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #region 卡券服务
        /// <summary>
        /// 上传卡券logo 返回logo地址，jpeg
        /// </summary>
        /// <param name="buffer">文件路径</param>
        /// <returns></returns>
        public string wxCardUpdateImg(string buffer)
        {
            var data = UploadFile("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=", "image", buffer, "image/jpeg");
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            string res = "1001";
            if (ticketMsg["url"] != null)
            {
                res = ticketMsg["url"].ToString();
            }
            else
            {
                Write("微信上传logo错误：" + data);
            }
            return res;
        }
        /// <summary>
        /// 获取商家的卡卷ID CardID
        /// </summary>
        /// <param name="list">1000正确,其他都为错误</param>
        /// <returns></returns>
        public string wxCardSelectList(ref ArrayList list)
        {
            string json = "{\"offset\":\"0\",\"count\":\" 10 \"}";
            var data = webRequestPost("https://api.weixin.qq.com/card/batchget?access_token=" + Getaccess_token(), json, "post");
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            string res = "1001";
            if (ticketMsg["errcode"] == null)
            {
                Write("查询优惠券错误：" + data);
            }
            else
            {
                if (ticketMsg["errcode"].ToString() == "0" && ticketMsg["errmsg"].ToString() == "ok")
                {
                    list = (ArrayList)ticketMsg["card_id_list"];
                    if (list.Count > 0)
                    {
                        res = "1000";
                    }
                    else
                    {
                        res = "1002";
                        Write("查询优惠券错误：" + data);
                    }
                }
                else
                {
                    Write("查询优惠券错误：" + data);
                }
            }
            return res;
        }
        #endregion
        /// <summary>
        /// 卡券详情表
        /// </summary>
        /// <param name="CardId">卡卷的CardID</param>
        /// <returns></returns>
        public string wxCardDetails(string card_id)
        {
            var json = "{\"card_id\":\"" + card_id + "\"}";
            var data = webRequestPost("https://api.weixin.qq.com/card/get?access_token=" + Getaccess_token(), json, "post");
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            string res = "1001";
            if (ticketMsg["errcode"] == null)
            {
                Write("卡券详情：" + data);
            }
            else
            {
                if (ticketMsg["errcode"].ToString() == "0" && ticketMsg["errmsg"].ToString() == "ok")
                {
                    res = data;
                }
                else
                {
                    Write("卡券详情：" + data);
                }
            }
            return res;
        }
        /// <summary>
        /// 验证会员的优惠券是否能用
        /// </summary>
        /// <param name="card_id">卡券ID</param>
        /// <param name="code">会员卡券ID</param>
        /// <returns></returns>
        public string wxCodeState(string card_id, string code)
        {
            string json = string.Empty;
            if (card_id != "")
            {
                json = "{\"card_id\" : \"" + card_id + "\",\"code\" : \"" + code + "\",\"check_consume\" : false}";
            }
            else
            {
                json = "{\"code\" : \"" + code + "\",\"check_consume\" : false}";
            }
            var data = webRequestPost("https://api.weixin.qq.com/card/code/get?access_token=" + Getaccess_token(), json, "post");
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            string res = string.Empty;
            if (ticketMsg["errcode"] == null)
            {
                Write("卡卷错误：" + data);
                res = "卡卷异常错误";
            }
            else
            {
                if (ticketMsg["errcode"].ToString() == "0" && ticketMsg["errmsg"].ToString() == "ok")
                {
                    if (ticketMsg["can_consume"].ToString() == "true")
                    {
                        res = "1000";
                    }
                    else
                    {
                        res = Codestat_txt(ticketMsg["user_card_status"].ToString());

                    }
                }
                else
                {
                    Write("卡卷错误：" + data);
                    res = "卡卷异常错误";
                }
            }
            return res;
        }
        /// <summary>
        /// 线下核销（后台需要的话）
        /// </summary>
        /// <param name="card_id">卡的card_id</param>
        /// <param name="code">优惠券码</param>
        /// <returns></returns>
        public string wxCodeDownCancel(string card_id, string code)
        {
            string res = wxCodeState(card_id, code);
            if (res == "1000")
            {
                string json = "{\"code\": \"" + code + "\"}";
                var data = webRequestPost("https://api.weixin.qq.com/card/code/consume?access_token=" + Getaccess_token(), json, "post");
                var jss = new JavaScriptSerializer();
                var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
                if (ticketMsg["errcode"].ToString() == "0" && ticketMsg["errmsg"].ToString() == "ok")
                {
                    res = "1000";
                }
                else
                {
                    Write("卡卷核销错误：" + data);
                    res = "卡卷核销错误";
                }
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 微信权限签名的 sha1 算法
        /// 签名用的noncestr和timestamp必须与wx.config中的nonceStr和timestamp相同
        /// </summary>
        /// <param name="jsapi_ticket">获取到的jsapi_ticket</param>
        /// <param name="noncestr">生成签名的随机串</param>
        /// <param name="timestamp">生成签名的时间戳</param>
        /// <param name="url">签名用的url必须是调用JS接口页面的完整URL</param>
        /// <returns></returns>
        public string GetShal(string jsapi_ticket, string noncestr, string timestamp, string url)
        {
            string strSha1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi_ticket, noncestr, timestamp, url);
            return SHA1_ToLower(strSha1);// System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSha1, "sha1").ToLower();
        }

        /// <summary>
        /// 微信权限签名( sha1 算法 )
        /// 签名用的noncestr和timestamp必须与wx.config中的nonceStr和timestamp相同
        /// </summary>
        /// <param name="noncestr">生成签名的随机串</param>
        /// <param name="timestamp">生成签名的时间戳</param>
        /// <param name="url">签名用的url必须是调用JS接口页面的完整URL</param>
        /// <returns></returns>
        public string signature(string noncestr, string timestamp, string url)
        {
            //string access_token = Getaccess_token(); //获取全局的access_token
            string jsapi_ticket = GetJsapi_Ticket(); //获取jsapi_ticket

            string strSha1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi_ticket, noncestr, timestamp, url);
            return SHA1_ToLower(strSha1);//System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSha1, "sha1").ToLower();
        }
        /// <summary>
        /// 卡券的签名
        /// </summary>
        /// <returns></returns>
        public string signature()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList ar = new ArrayList(parameters.Values);
            ar.Sort();
            foreach (string k in ar)
            {

                if (null != k && "".CompareTo(k) != 0)
                {
                    sb.Append(k);
                }
            }
            parameters.Clear();
            return SHA1_ToLower(sb.ToString());//System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sb.ToString(), "sha1").ToLower();
        }
        /// <summary>
        /// chooseCard卡券签名cardSign
        /// </summary>
        /// <returns></returns>
        public string cardSign()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList ar = new ArrayList(parameters.Values);
            ar.Sort();
            foreach (string k in ar)
            {

                if (null != k && "".CompareTo(k) != 0)
                {
                    sb.Append(k);
                }
            }
            parameters.Clear();
            return SHA1_ToLower(sb.ToString());// System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sb.ToString(), "sha1").ToUpper();
        }
        public string Mysignature(string AppId, string AppSecret, string noncestr, string timestamp, string package)
        {
            //string access_token = Getaccess_token(); //获取全局的access_token
            string jsapi_ticket = GetJsapi_Ticket(); //获取jsapi_ticket

            string strSha1 = string.Format("jsapi_ticket={0}&noncestr={1}&package={2}&timestamp={3}", jsapi_ticket, noncestr, package, timestamp);
            return MD5_ToUpper(strSha1);//System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSha1, "MD5").ToUpper();
            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSha1, "sha1").ToLower();
        }

        /// <summary>
        /// 微信权限签名( sha1 算法 )
        ///  签名用的noncestr和timestamp必须与wx.config中的nonceStr和timestamp相同
        /// </summary>
        /// <param name="noncestr">生成签名的随机串</param>
        /// <param name="timestamp">生成签名的时间戳</param>
        /// <param name="url">签名用的url必须是调用JS接口页面的完整URL</param>
        /// <param name="access_token">access_token</param>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="signature">signature</param>
        public void signatureOut(string noncestr, string timestamp, string url, out string access_token, out string jsapi_ticket, out string signature)
        {
            access_token = Getaccess_token(); //获取全局的access_token

            jsapi_ticket = GetJsapi_Ticket(); //获取jsapi_ticket

            string strSha1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi_ticket, noncestr, timestamp, url);

            signature = SHA1_ToLower(strSha1);//System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSha1, "sha1").ToLower();
        }

        //    private string[] strs = new string[]
        //{
        //"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
        //"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
        //};
        //    /// <summary>
        //    /// 创建随机字符串
        //    /// </summary>
        //    /// <returns></returns>
        //    public string CreatenNonce_str()
        //    {
        //        Random r = new Random();
        //        var sb = new StringBuilder();
        //        var length = strs.Length;
        //        for (int i = 0; i < 15; i++)
        //        {
        //            sb.Append(strs[r.Next(length - 1)]);
        //        }
        //        return sb.ToString();
        //    }


        //    /// <summary>
        //    /// 创建时间戳
        //    /// </summary>
        //    /// <returns></returns>
        //    public string CreatenTimestamp()
        //    {
        //        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //        return Convert.ToInt64(ts.TotalSeconds).ToString();
        //    }
        public void setParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (parameters.Contains(parameter))
                {
                    parameters.Remove(parameter);
                }

                parameters.Add(parameter, parameterValue);
            }
        }
        #region 签名方式
        private static string SHA1(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "sha1");
        }
        private static string MD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
        /// <summary>
        /// 小写sha1加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string SHA1_ToLower(string str)
        {
            return SHA1(str).ToLower();
        }
        /// <summary>
        /// 大写sha1加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA1_ToUpper(string str)
        {
            return SHA1(str).ToUpper();
        }
        /// <summary>
        /// 小写MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>        
        /// <returns></returns>
        public static string MD5_ToLower(string str)
        {
            return MD5(str).ToLower();
        }
        /// <summary>
        /// 大写MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5_ToUpper(string str)
        {
            return MD5(str).ToUpper();
        }
        #endregion

        #region Get/Post
        /// <summary>
        /// 服务端post/get
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="param">参数值</param>
        /// <param name="Method">Get/Post</param>
        /// <returns></returns>
        public string webRequestPost(string url, string param, string Method)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = Method;
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

        /// 服务号：上传多媒体文件

        /// </summary>

        /// <param name="accesstoken">调用接口凭据</param>

        /// <param name="type">图片（image）、语音（voice）、视频（video）和缩略图（thumb）</param>

        /// <param name="filename">文件路径</param>

        /// <param name="contenttype">文件Content-Type类型(例如：image/jpeg、audio/mpeg)</param>

        /// <returns></returns>

        public string UploadFile(string url, string type, string filename, string contenttype)
        {

            //文件

            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fileStream);

            byte[] buffer = br.ReadBytes(Convert.ToInt32(fileStream.Length));


            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

            //请求

            WebRequest req = WebRequest.Create(url + Getaccess_token() + "&type=" + type);

            req.Method = "POST";

            req.ContentType = "multipart/form-data; boundary=" + boundary;

            //组织表单数据
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"media\"; filename=\"" + filename + "\"; filelength=\"" + fileStream.Length + "\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: " + contenttype);
            sb.Append("\r\n\r\n");
            string head = sb.ToString();
            byte[] form_data = Encoding.UTF8.GetBytes(head);

            //结尾
            byte[] foot_data = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            //post总长度
            long length = form_data.Length + fileStream.Length + foot_data.Length;

            req.ContentLength = length;

            Stream requestStream = req.GetRequestStream();
            //这里要注意一下发送顺序，先发送form_data > buffer > foot_data
            //发送表单参数
            requestStream.Write(form_data, 0, form_data.Length);
            //发送文件内容
            requestStream.Write(buffer, 0, buffer.Length);
            //结尾
            requestStream.Write(foot_data, 0, foot_data.Length);

            requestStream.Close();
            fileStream.Close();
            fileStream.Dispose();
            br.Close();
            br.Dispose();
            //响应
            WebResponse pos = req.GetResponse();
            StreamReader sr = new StreamReader(pos.GetResponseStream(), Encoding.UTF8);
            string html = sr.ReadToEnd().Trim();
            sr.Close();
            sr.Dispose();
            if (pos != null)
            {
                pos.Close();
                pos = null;
            }
            if (req != null)
            {
                req = null;
            }
            return html;

        }
        #endregion

       
        /// <summary>
        /// 线上解析code的加密信息
        /// </summary>
        /// <param name="encrypt_code">线上返回的加密</param>
        /// <returns></returns>
        public string wxCodeUp(string encrypt_code)
        {
            string json = "{\"encrypt_code\" : \"" + encrypt_code + "\"}";
            var data = webRequestPost("https://api.weixin.qq.com/card/code/decrypt?access_token=" + Getaccess_token(), json, "post");
            var jss = new JavaScriptSerializer();
            var ticketMsg = jss.Deserialize<Dictionary<string, object>>(data);
            string res = string.Empty;
            if (ticketMsg["errcode"] == null)
            {
                Write("卡卷code解析错误：" + data);
                res = "卡卷code解析异常错误";
            }
            else
            {
                if (ticketMsg["errcode"].ToString() == "0" && ticketMsg["errmsg"].ToString() == "ok")
                {
                    res = ticketMsg["code"].ToString();

                }
                else
                {
                    Write("卡卷code解析错误：" + data);
                    res = "卡卷code解析异常错误";
                }
            }
            return res;
        }
        #region 打印文档
        public static void Write(string Message)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                FileName = HttpContext.Current.Server.MapPath("~/App_Logs/") + FileName + ".txt";
                #region 检测日志目录是否存在
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/App_Logs")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/App_Logs"));
                }

                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion
                sw.WriteLine("内  容：" + Message);
                sw.WriteLine("时  间：" + System.DateTime.Now);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
        #endregion
        private string Codestat_txt(string status)
        {
            string txt = string.Empty;
            switch (txt)
            {
                case "NORMAL":
                    txt = "正常";
                    break;
                case "CONSUMED":
                    txt = "已核销";
                    break;
                case "EXPIRE":
                    txt = "已过期";
                    break;
                case "GIFTING":
                    txt = "该卡正在转赠中";
                    break;
                case "GIFT_SUCC":
                    txt = "转赠成功";
                    break;
                case "GIFT_TIMEOUT":
                    txt = "转赠超时";
                    break;
                case "DELETE":
                    txt = "已删除";
                    break;
                case "UNAVAILABLE":
                    txt = "已失效";
                    break;
                default:
                    txt = "微信异常";
                    break;

            }
            return txt;
        }
    }

}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using wxapi;

public partial class wxmember : System.Web.UI.Page
{
    const string Token = "cnjdsoft";
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 页面初始化
        string postStr = "";
        /// <summary>
        /// 处理微信发来的请求 
        /// </summary>
        /// <param name="xml"></param>
        if (Request.HttpMethod.ToLower() == "post")
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            postStr = Encoding.UTF8.GetString(b);
            WXToolsHelper.Write(postStr);
            if (!string.IsNullOrEmpty(postStr))
            {
                //封装请求类
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(postStr);
                XmlElement rootElement = doc.DocumentElement;
                XmlNode MsgType = rootElement.SelectSingleNode("MsgType");
                RequestXML requestXML = new RequestXML();
                requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
                requestXML.MsgType = MsgType.InnerText;
                try
                {
                    requestXML.Event = rootElement.SelectSingleNode("Event").InnerText;
                }
                catch
                {

                }
                WXToolsHelper.Write("1*********************************");
                if (requestXML.MsgType == "text")
                {
                    requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                }
                else if (requestXML.MsgType == "location")
                {
                    requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                    requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                    requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                    requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
                }
                else if (requestXML.MsgType == "image")
                {
                    requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                }
                else if (requestXML.MsgType == "event")
                {
                    WXToolsHelper.Write("2*********************************" + requestXML.Event);
                    // 事件类型    
                    String eventType = requestXML.Event;
                    // 订阅    
                    if (eventType == "subscribe")
                    {
                        WXToolsHelper.Write("3*********************************" + rootElement.SelectSingleNode("FromUserName").InnerText);
                        WXToolsHelper.Write("4*********************************" + dp.C_Proc_Select(new string[] { "0", "19", "0" }, 1)[0].ToString());
                       string ss = new WXToolsHelper().MessageUser(rootElement.SelectSingleNode("FromUserName").InnerText,dp.C_Proc_Select(new string[] { "0", "19", "0" }, 1)[0].ToString());
                       WXToolsHelper.Write("5*********************************" + ss);
                        int pid = 0;//上级会员ID
                        WXToolsHelper.Write(rootElement.SelectSingleNode("EventKey").InnerText);
                        if (rootElement.SelectSingleNode("EventKey").InnerText != "")
                        {
                            pid =Common.C_Int(rootElement.SelectSingleNode("EventKey").InnerText.Split('_')[1].ToString(),0);

                        } 
                        Object[] obj = dp.U_Proc_UserLogin(new string[] { rootElement.SelectSingleNode("FromUserName").InnerText });
                        WXToolsHelper.Write(obj[0].ToString());
                        WXToolsHelper.Write("zhuce" + pid.ToString() + "\r\n" + rootElement.SelectSingleNode("FromUserName").InnerText);
                        if (obj[0].ToString() != "1000")//判断用户在本系统是否存在
                        {
                            WXToolsHelper.Write("zhuce"+ pid.ToString()+"\r\n"+rootElement.SelectSingleNode("FromUserName").InnerText);
                            object[] reg = dp.U_Proc_AddUser(new string[] { pid.ToString(), "",rootElement.SelectSingleNode("FromUserName").InnerText, "" });
                            WXToolsHelper.Write("注册:" + reg[0].ToString() + "\r\n");
                            if (reg[0].ToString() == "1000")
                            {
                                WXToolsHelper wxt = new WXToolsHelper();
                                string data = wxt.we_code(reg[2].ToString());
                                dp.create_two(wxt.json_text("url", data), rootElement.SelectSingleNode("FromUserName").InnerText);
                                
                                 string a=wxt.MessageUser(rootElement.SelectSingleNode("FromUserName").InnerText, "恭喜你成为第"+dp.C_Proc_Select(new string[] { "0", "20", rootElement.SelectSingleNode("FromUserName").InnerText }, 1)[0].ToString()+"位会员");
                                WXToolsHelper.Write("a");
                            }
                            else
                            {
                                WXToolsHelper.Write("错误！");
                            }
                            
                        }
                       
                        //requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                    }
                    // 取消订阅    
                    else if (eventType == "unsubscribe")
                    {
                        // TODO 取消订阅后用户再收不到公众号发送的消息，因此不需要回复消息    
                    }
                    // 自定义菜单点击事件    
                    else if (eventType == "CLICK")
                    {
                        // TODO 自定义菜单权没有开放，暂不处理该类消息    
                    }
                }

                //回复消息
              ResponseMsg(requestXML);
            }
        }
        else
        {
            Valid();
        }
        #endregion
    }
    #region 签名验证
    /// <summary>
    /// 验证微信签名
    /// </summary>
    /// * 将token、timestamp、nonce三个参数进行字典序排序
    /// * 将三个参数字符串拼接成一个字符串进行sha1加密
    /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
    /// <returns></returns>
    private bool CheckSignature()
    {
        string signature = Request.QueryString["signature"];
        string timestamp = Request.QueryString["timestamp"];
        string nonce = Request.QueryString["nonce"];
        string[] ArrTmp = { Token, timestamp, nonce };
        Array.Sort(ArrTmp);     //字典排序
        string tmpStr = string.Join("", ArrTmp);
        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        tmpStr = tmpStr.ToLower();
        if (tmpStr == signature)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CheckSignature(String signature, String timestamp, String nonce)
    {
        String[] arr = new String[] { Token, timestamp, nonce };
        // 将token、timestamp、nonce三个参数进行字典序排序  
        Array.Sort<String>(arr);

        StringBuilder content = new StringBuilder();
        for (int i = 0; i < arr.Length; i++)
        {
            content.Append(arr[i]);
        }

        String tmpStr = SHA1_Encrypt(content.ToString());


        // 将sha1加密后的字符串可与signature对比，标识该请求来源于微信  
        return tmpStr != null ? tmpStr.Equals(signature) : false;
    }

    /// <summary>
    /// 使用缺省密钥给字符串加密
    /// </summary>
    /// <param name="Source_String"></param>
    /// <returns></returns>
    public static string SHA1_Encrypt(string Source_String)
    {
        byte[] StrRes = Encoding.Default.GetBytes(Source_String);
        HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
        StrRes = iSHA.ComputeHash(StrRes);
        StringBuilder EnText = new StringBuilder();
        foreach (byte iByte in StrRes)
        {
            EnText.AppendFormat("{0:x2}", iByte);
        }
        return EnText.ToString();
    }


    private void Valid()
    {
        //// 微信加密签名  
        //string signature = Request.QueryString["signature"];
        //// 时间戳  
        //string timestamp = Request.QueryString["timestamp"];
        //// 随机数  
        //string nonce = Request.QueryString["nonce"];
        //// 随机字符串  
        //string echostr = Request.QueryString["echostr"];
        string echoStr = Request.QueryString["echoStr"];
        if (CheckSignature())
        {
            if (!string.IsNullOrEmpty(echoStr))
            {
                Response.Write(echoStr);
                Response.End();
            }
        }
    }
    #endregion


    #region 回复消息(微信信息返回)
    /// <summary>
    /// 回复消息(微信信息返回)
    /// </summary>
    /// <param name="weixinXML"></param>
    private void ResponseMsg(RequestXML requestXML)
    {
        try
        {
            string resxml = "";
            if (requestXML.MsgType == "text")
            {
                //requestXML.Content = "今点科技:我们会开发软件，是定制开发哦！";
                //resxml = MessageBody(requestXML);
            }
            else if (requestXML.MsgType == "location")
            {
                //string city = GetMapInfo(requestXML.Location_X, requestXML.Location_Y);
                //if (city == "0")
                //{
                //    resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[Sorry，没有找到" + city + " 的相关产品信息]]></Content><FuncFlag>0</FuncFlag></xml>";
                //}
                //else
                //{
                //    resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[Sorry，这是 " + city + " 的产品信息：....]]></Content><FuncFlag>0</FuncFlag></xml>";
                //}
            }
            else if (requestXML.MsgType == "image")
            {
                //返回10以内条
                //int size = 10;
                //resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[news]]></MsgType><Content><![CDATA[]]></Content><ArticleCount>" + size + "</ArticleCount><Articles>";
                //List<string> list = new List<string>();
                ////假如有20条查询的返回结果
                //for (int i = 0; i < 4; i++)
                //{
                //    list.Add("1");
                //}
                //string[] piclist = new string[] { "/Abstract_Pencil_Scribble_Background_Vector_main.jpg", "/balloon_tree.jpg", "/bloom.jpg", "/colorful_flowers.jpg", "/colorful_summer_flower.jpg", "/fall.jpg", "/fall_tree.jpg", "/growing_flowers.jpg", "/shoes_illustration.jpg", "/splashed_tree.jpg" };

                //for (int i = 0; i < size && i < list.Count; i++)
                //{
                //    resxml += "<item><Title><![CDATA[衢州-今点科技]]></Title><Description><![CDATA[元旦特价：￥300 市场价：￥400]]></Description><PicUrl><![CDATA[" + "http://www.cnjdsoft.com" + piclist[i] + "]]></PicUrl><Url><![CDATA[http://www.cnjdsoft.com]]></Url></item>";
                //}
                //resxml += "</Articles><FuncFlag>1</FuncFlag></xml>";
            }
            else if (requestXML.MsgType == "event")
            {
                // 事件类型    
                String eventType = requestXML.Event;
                // 订阅    
                if (eventType == "subscribe" || eventType == "unsubscribe")
                {

                    requestXML.Content = dp.C_Proc_Select(new string[] { "0", "22", "0" }, 1)[0].ToString();
                    resxml = MessageBody(requestXML);
                }
                // 取消订阅    
                //else if (eventType == "unsubscribe")
                //{
                //    //requestXML.Content = "感谢您关注【今点科技】\n微信号：cnJDsoft。";
                //    //MessageBody(requestXML);
                //    // TODO 取消订阅后用户再收不到公众号发送的消息，因此不需要回复消息    
                //}
                // 自定义菜单点击事件    
                else if (eventType == "CLICK")
                {
                    // TODO 自定义菜单权没有开放，暂不处理该类消息    
                }
            }

            Response.Write(resxml);
        }
        catch (Exception ex)
        {
            WriteTxt("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());
        }
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 消息主体
    /// </summary>
    /// <param name="requestXML"></param>
    /// <returns></returns>
    public string MessageBody(RequestXML requestXML)
    {
        return @"<xml>
                    <ToUserName><![CDATA[" + requestXML.FromUserName + @"]]></ToUserName>
                    <FromUserName><![CDATA[" + requestXML.ToUserName + @"]]></FromUserName>
                    <CreateTime>" + ConvertDateTimeInt(DateTime.Now) + @"</CreateTime>
                    <MsgType><![CDATA[text]]></MsgType>
                    <Content><![CDATA[" + requestXML.Content + @"]]></Content>
                    <FuncFlag>0</FuncFlag>
                </xml>";
    }


    /// <summary>
    /// unix时间转换为datetime
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    private DateTime UnixTimeToTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    /// <summary>
    /// datetime转换为unixtime
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private int ConvertDateTimeInt(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (int)(time - startTime).TotalSeconds;
    }

    /// <summary>
    /// 记录bug，以便调试
    /// </summary>
    /// <returns></returns>
    public bool WriteTxt(string str)
    {
        try
        {
            FileStream fs = new FileStream(Server.MapPath("/bugLog.txt"), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.WriteLine(str);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region 调用百度地图，返回坐标信息
    /// <summary>
    /// 调用百度地图，返回坐标信息
    /// </summary>
    /// <param name="y">经度</param>
    /// <param name="x">纬度</param>
    /// <returns></returns>
    public string GetMapInfo(string x, string y)
    {
        try
        {
            string res = string.Empty;
            string parame = string.Empty;
            string url = "http://maps.googleapis.com/maps/api/geocode/xml";
            parame = "latlng=" + x + "," + y + "&language=zh-CN&sensor=false";//此key为个人申请
            res = webRequestPost(url, parame);

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(res);
            XmlElement rootElement = doc.DocumentElement;
            string Status = rootElement.SelectSingleNode("status").InnerText;
            if (Status == "OK")
            {
                //仅获取城市
                XmlNodeList xmlResults = rootElement.SelectSingleNode("/GeocodeResponse").ChildNodes;
                for (int i = 0; i < xmlResults.Count; i++)
                {
                    XmlNode childNode = xmlResults[i];
                    if (childNode.Name == "status")
                    {
                        continue;
                    }

                    string city = "0";
                    for (int w = 0; w < childNode.ChildNodes.Count; w++)
                    {
                        for (int q = 0; q < childNode.ChildNodes[w].ChildNodes.Count; q++)
                        {
                            XmlNode childeTwo = childNode.ChildNodes[w].ChildNodes[q];

                            if (childeTwo.Name == "long_name")
                            {
                                city = childeTwo.InnerText;
                            }
                            else if (childeTwo.InnerText == "locality")
                            {
                                return city;
                            }
                        }
                    }
                    return city;
                }
            }
        }
        catch (Exception ex)
        {
            WriteTxt("map异常:" + ex.Message.ToString() + "Struck:" + ex.StackTrace.ToString());
            return "0";
        }

        return "0";
    }
    #endregion

    #region POST提交调用抓取
    /// <summary>
    /// Post 提交调用抓取
    /// </summary>
    /// <param name="url">提交地址</param>
    /// <param name="param">参数</param>
    /// <returns>string</returns>
    public string webRequestPost(string url, string param)
    {
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);

        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url + "?" + param);
        req.Method = "Post";
        req.Timeout = 120 * 1000;
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
    #endregion
}
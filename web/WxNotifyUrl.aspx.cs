using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using wxapi;

public partial class WxNotifyUrl : System.Web.UI.Page
{
    public String appId = TenpayUtil.appid;
    public String timeStamp = "";
    public String nonceStr = "";
    public String packageValue = "";
    public String paySign = "";
    public String Mchid = TenpayUtil.Mchid;
    DataProvider dp = new DataProvider();
    WXToolsHelper wxt = new WXToolsHelper();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        
        nonceStr = TenpayUtil.getNoncestr();
        if (Request.InputStream.Length > 0)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Request.InputStream);
            XmlElement rootElement = xmlDoc.DocumentElement;
           WXToolsHelper.Write(rootElement.SelectSingleNode("return_code").InnerText);
            if (rootElement.SelectSingleNode("return_code").InnerText == "SUCCESS")
            {
                RequestHandler rh = new RequestHandler(Context);
                rh.init();
                //rh.setKey(TenpayUtil.key);
                rh.setParameter("appid", appId);
                rh.setParameter("mch_id", Mchid);
                rh.setParameter("nonce_str", nonceStr);
                rh.setParameter("transaction_id", rootElement.SelectSingleNode("transaction_id").InnerText);
                // rh.setParameter("out_trade_no", rootElement.SelectSingleNode("out_trade_no").InnerText);
                string Sign = rh.createMd5Signs(TenpayUtil.appkey);
                string Param = "<xml>" +
               "<appid>" + appId + "</appid>" +
               "<mch_id>" + Mchid + "</mch_id>" +
               "<nonce_str>" + nonceStr + "</nonce_str>" +
               "<sign>" + Sign + "</sign>" +
               // "<out_trade_no>" + rootElement.SelectSingleNode("out_trade_no").InnerText + "</out_trade_no>" +
               "<transaction_id>" + rootElement.SelectSingleNode("transaction_id").InnerText + "</transaction_id>" +
                "</xml>";
                WXToolsHelper.Write(Param); string res = "";
                try
                {
                    res = webRequestPost("https://api.mch.weixin.qq.com/pay/orderquery", Param);
                }
                catch (Exception ex)
                {
                    WXToolsHelper.Write(ex.ToString());

                }
                try
                {
                    WXToolsHelper.Write(res);
                    xmlDoc.LoadXml(res);
                }
                catch (Exception exx)
                {
                    WXToolsHelper.Write(exx.ToString());
                }

                rootElement = xmlDoc.DocumentElement;
                string Status = rootElement.SelectSingleNode("return_code").InnerText;
                string Status2 = rootElement.SelectSingleNode("return_msg").InnerText;
                if (Status == "SUCCESS" && Status2 == "OK")
                {
                    string out_trade_no = rootElement.SelectSingleNode("out_trade_no").InnerText;///获取商户订单号
                    string ks = "";
                    object[] y = dp.U_Proc_OverBuy(new string[] {"-1", out_trade_no,"1" });
                    if (y[0].ToString() == "1000")
                    {
                        //if (y[4].ToString() == "0")
                        //{
                        //    //string[] username = y[3].ToString().Split(',');
                        //   /* for (int i=0,len=username.Length; i< len;i++) {
                        //       // wxt.MessageUser(username[i], "恭喜"+y[2].ToString()+"下单成功，成为米团正式会员，请及时关注个人中心收益，米团需要您的关注和支持，祝您早日登顶收大米！");
                        //    }*/
                        //   //wxt.MessageUser(,)
                        //}
                        Response.Write("SUCCESS");
                    }
                }
                else
                {
                    WXToolsHelper.Write("s1:" + Status + "----s2:" + Status2);
                }
                
            }
        }
    }

    public string webRequestPost(string url, string param)
    {
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
        req.Method = "Post";
        req.Timeout = 120 * 1000;
        req.ContentType = "text/xml";
        //req.ContentType = "application/x-www-form-urlencoded;";
        req.ContentLength = bs.Length;

        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(bs, 0, bs.Length);
            reqStream.Flush();
        }
        using (System.Net.WebResponse wr = req.GetResponse())
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
}
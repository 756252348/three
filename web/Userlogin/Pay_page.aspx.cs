using K_ON;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wxapi;
using System.Xml;

public partial class Userlogin_Pay_page : System.Web.UI.Page
{
    public String appId = TenpayUtil.appid;
    public String timeStamp = "";
    public String nonceStr = "";
    public String packageValue = "";
    public String paySign = "";
    public String Mchid = TenpayUtil.Mchid;
    public String addrSign = "";
    public String url = "";
    CookiesGetDB ck = new CookiesGetDB();
    DataProvider dp = new DataProvider();
    public String prepay_id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ///标记锦囊
        bool isJn = true;
        if (!string.IsNullOrEmpty(Request.QueryString["p_id"])) isJn = false;
        //WXToolsHelper.Write("isJn:"+isJn.ToString()+ "   p_id:"+Request.QueryString["p_id"]);
        string UserID = ck.GetRolesText(this.Context, 0);
        string OID = Request.QueryString["OID"];
        /*if (isJn)//判断订单类型
        {
            if (dp.C_Proc_Select(new string[] { ck.GetRolesText(Context, 0), "1", "" }, 2)[0].ToString() != "1000")//不是会员
            {
                OID = dp.U_Proc_CreateBuy(new string[] { UserID, "1" })[0].ToString();//生成锦囊订单
            }
            else
            {
                g_name.Attributes.Add("data-check", "0");
                //Response.Redirect("userinfo.aspx");//是会员回到主页
                return;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Request.QueryString["OID"]))//是否需要重新生成订单
                OID = Request.QueryString["OID"];
            else
                OID = dp.U_Proc_CreateBuy(new string[] { UserID, Request.QueryString["p_id"] })[0].ToString();//生成金额订单
        }*/
        //WXToolsHelper.Write("OID:" + OID);
        nonceStr = TenpayUtil.getNoncestr();
        timeStamp = TenpayUtil.getTimestamp();
        url = ConfigurationManager.AppSettings["tenpay_notify"].ToString();
        WXToolsHelper wxt = new WXToolsHelper();
        object[] garry;
        string g_m = "";//合计金额
        if (isJn)//锦囊
        {
            garry = dp.C_Proc_Select(new string[] { UserID, "15", OID }, 2);
            g_m = garry[0].ToString();
            g_money.InnerText = g_m;
            g_moneys.InnerText = g_m;
        }
        else//其他金额商品
        {
            garry = dp.C_Proc_Select(new string[] { UserID, "11", OID }, 5);
            g_name.InnerText = garry[1].ToString();
            topImage.Src = garry[2].ToString();
            g_num.InnerText = garry[3].ToString();
            g_m = garry[4].ToString();
            g_money.InnerText = garry[0].ToString();
            g_moneys.InnerText = g_m;
        }
        int momey = 100;
        decimal a = 100 * decimal.Parse(g_m);
        momey = Convert.ToInt32(a);
        RequestHandler rh = new RequestHandler(Context);
        string sp_billno = OID;
        string openid = ck.GetRolesText(Context, 1);
        rh.init();
        //rh.setKey(TenpayUtil.key);
        rh.setParameter("appid", appId);
        rh.setParameter("body", "私募内参");
        rh.setParameter("mch_id", Mchid);
        rh.setParameter("nonce_str", nonceStr);
        rh.setParameter("notify_url", url);
        rh.setParameter("out_trade_no", sp_billno);
        rh.setParameter("spbill_create_ip", Common.GetRemoteIp());
        rh.setParameter("total_fee", momey.ToString());
        rh.setParameter("trade_type", "JSAPI");
        rh.setParameter("openid", openid);


        string Sign = rh.createMd5Signs(TenpayUtil.appkey);
        string Param = "<xml>" +
            "<appid>" + appId + "</appid>" +
            "<body>" + "私募内参" + "</body>" +
            "<mch_id>" + Mchid + "</mch_id>" +
            "<nonce_str>" + nonceStr + "</nonce_str>" +
            "<sign>" + Sign + "</sign>" +
            "<out_trade_no>" + sp_billno + "</out_trade_no>" +
            "<total_fee>" + momey.ToString() + "</total_fee>" +
            "<spbill_create_ip>" + Common.GetRemoteIp() + "</spbill_create_ip>" +
            "<notify_url>" + url + "</notify_url>" +
            "<trade_type>JSAPI</trade_type>" +
            "<openid>" + openid + "</openid>" +
             "</xml>";
        Common.Write("OpenID:" + ck.GetRolesText(Context, 1) + "----Sign:" + Sign + "nonceStr:" + nonceStr + "----appId:" + appId + "Mchid:" + Mchid + "----sp_billno:" + sp_billno + "momey:" + momey + "----ip:" + Common.GetRemoteIp() + "xml:" + Param);
        string res = wxt.webRequestPost("https://api.mch.weixin.qq.com/pay/unifiedorder", Param, "Post");
        WXToolsHelper.Write(res);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(res);
        XmlElement rootElement = doc.DocumentElement;
        string Status = rootElement.SelectSingleNode("return_code").InnerText;
        string Status2 = rootElement.SelectSingleNode("return_msg").InnerText;
        if (Status == "SUCCESS" && Status2 == "OK")
        {
            prepay_id = "prepay_id=" + rootElement.SelectSingleNode("prepay_id").InnerText;
            packageValue = prepay_id;
            RequestHandler rh2 = new RequestHandler(Context);
            rh2.setParameter("appId", appId);
            rh2.setParameter("signType", "MD5");
            rh2.setParameter("nonceStr", nonceStr);
            rh2.setParameter("timeStamp", timeStamp);
            rh2.setParameter("package", packageValue);
            paySign = rh2.createMd5Signs(TenpayUtil.appkey);
        }
        else
        {
            WXToolsHelper.Write("s1:" + Status + "----s2:" + Status2);
        }
    }
}
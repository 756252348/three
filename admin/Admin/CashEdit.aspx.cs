using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using wxapi;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;

public partial class Admin_GoodsInfoEdit : System.Web.UI.Page
{
    public String appId = TenpayUtil.appid;
    public String timeStamp = "";
    public String nonceStr = "";
    public String packageValue = "";
    public String paySign = "";
    public String Mchid = TenpayUtil.Mchid;
    public static String OID = "";
    public static string openid = "0";
    public static String momey = "0";
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
           //dp.PromissionOfEdit(Authority.GetRoleID(Context), "1114", id, lbTitle);
            //new Upload("News").IniImgConfig(ImgConfig);
            if (id > 0)
                DataInfo();
            if (state.Text.Trim() == "允许" || state.Text.Trim() == "拒绝")
            {
                aaa.Attributes.Add("style", "display:none;");
            }
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[9];
        if (dp.C_LoadArrayData("select U_ID,U_Name,U_Img,O_Money ,O_Orderid,UO.AddTime,U_OpenID,CONVERT(INT,O_Money*100),(case O_States when 0 then '未操作' when 1 then '允许' when 2 then '拒绝' end)O_States from U_User AS UU  join U_Orderform AS UO  on UU.ID=UO.U_ID  where UO.ID=" + id.ToString(), ref oArray) == "1000")
        {
            
            userid.Text = oArray[0].ToString();
            nick.Text = oArray[1].ToString();
            
            
            Image1.ImageUrl = oArray[2].ToString();
            money.Text = oArray[3].ToString();
            orderid.Text = OID = oArray[4].ToString();
            
            time.Text = oArray[5].ToString();
            
            openid = oArray[6].ToString();
            momey = oArray[7].ToString();
            state.Text = oArray[8].ToString();

        }
        #endregion
    }



    protected void btUp_Click(object sender, EventArgs e)
    {
        nonceStr = TenpayUtil.getNoncestr();
        timeStamp = TenpayUtil.getTimestamp();
        RequestHandler rh = new RequestHandler(Context);

        string sp_billno = OID == "0" ? DateTime.Now.ToString("yyyyMMddhhmmssfffff") : OID;
        rh.init();
        //rh.setKey(TenpayUtil.key);
        rh.setParameter("mch_appid", appId);
        //rh.setParameter("body", "衢州秘玛贸易有限责任公司");
        rh.setParameter("mchid", Mchid);
        rh.setParameter("nonce_str", nonceStr);
        rh.setParameter("partner_trade_no", sp_billno);
        rh.setParameter("check_name", "NO_CHECK");
        rh.setParameter("spbill_create_ip", Common.GetRemoteIp());
        rh.setParameter("amount", momey.ToString());
        rh.setParameter("desc", "提现");
        rh.setParameter("openid", openid);
        string Sign = rh.createMd5Signs(TenpayUtil.appkey);
        string Param = "<xml>" +
           "<mch_appid>" + appId + "</mch_appid>" +
           "<check_name>" + "NO_CHECK" + "</check_name>" +
           "<mchid>" + Mchid + "</mchid>" +
           "<nonce_str>" + nonceStr + "</nonce_str>" +
           "<sign>" + Sign + "</sign>" +
           "<partner_trade_no>" + sp_billno + "</partner_trade_no>" +
           "<amount>" + momey.ToString() + "</amount>" +
           "<spbill_create_ip>" + Common.GetRemoteIp() + "</spbill_create_ip>" +
           "<desc>提现</desc>" +
           "<openid>" + openid + "</openid>" +
            "</xml>";
        WXToolsHelper.Write("Param: " + Param);
        string res = webRequestPost("https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers", Param, "Post", @"E:\SMNC\SMNC_ADMIN\zhengshu\apiclient_cert.p12", Mchid);
        WXToolsHelper.Write(res);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(res);
        XmlElement rootElement = doc.DocumentElement;
        string Status = rootElement.SelectSingleNode("return_code").InnerText;
        string Status2 = rootElement.SelectSingleNode("result_code").InnerText;
        if (Status == "SUCCESS" && Status2 == "SUCCESS")
        {
            MessageBox.Show(Page, new StoredProcedure().C_CheckWithdrawMoney(new string[] { "0", userid.Text, Authority.GetAdminID(Context), orderid.Text })[1].ToString());
            aaa.Attributes.Add("style", "display:none;");
        }
        else
        {
            MessageBox.ReLocation(Page, rootElement.SelectSingleNode("err_code_des").InnerText);
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MessageBox.Show(Page, new StoredProcedure().C_CheckWithdrawMoney(new string[] { "1", userid.Text, Authority.GetAdminID(Context), orderid.Text })[1].ToString());
        aaa.Attributes.Add("style", "display:none;");
    }

    public string webRequestPost(string url, string param, string Method, string cert, string password)
    {
        byte[] bs = System.Text.Encoding.UTF8.GetBytes(param);
        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

        X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
        req.ClientCertificates.Add(cer);
        req.Method = Method;
        req.Timeout = 120 * 1000;

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

    private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    {
        if (errors == SslPolicyErrors.None)
            return true;
        return false;
    }
}
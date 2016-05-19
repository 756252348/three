using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wxapi;
using K_ON;
using System.Configuration;

public partial class Userlogin_userindex : System.Web.UI.Page{
    WeiXinApi wx = new WeiXinApi();
    WXToolsHelper wxt = new WXToolsHelper();
    DataProvider dp = new DataProvider();
    CookiesGetDB ck = new CookiesGetDB();
    public string userid = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UrlReferrer == null)
        {
            string[] wxArray = wxt.GetOpenID(Request["code"]);
            string data = wxt.GetWxUserInfo(wxArray[0]);
            WXToolsHelper.Write("data：" + data);
            string openid = wxt.json_text("openid", data), nickname = wxt.json_text("nickname", data),headimgurl=wxt.json_text("headimgurl", data);
            WXToolsHelper.Write(headimgurl); WXToolsHelper.Write(nickname); WXToolsHelper.Write(openid);
            object[] obj = dp.U_Proc_UserChange(new string[] { openid, headimgurl, nickname, data });
            WXToolsHelper.Write(string.Join("|",obj));
            object[] user = dp.U_Proc_UserLogin(new string[] { wxArray[0] });
            WXToolsHelper.Write(user[0].ToString());
            if (user[0].ToString() == "1000")
            {
                userid = wxArray[0];
                WXToolsHelper.Write(user[2].ToString());
                WXToolsHelper.Write(wxArray[0]);
                WXToolsHelper.Write(user[3].ToString());
                CookieAddDB.User_Login("userinfo_wx", user[2].ToString() + "|" + wxArray[0] + "|" + user[3].ToString()+"|" + wxArray[1]);
            }
        }
        string url = "userinfo.aspx";///默认主页

        if (Request["type"] == "1")///进入私募内参
        {
            url = "privateReference_page.aspx";
        }
        else
            if (Request["type"] == "2")///如何推广
        {
            url = "tutorial_page.aspx";
        }

        Response.Redirect(url);
    }
}
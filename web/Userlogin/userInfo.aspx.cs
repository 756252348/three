using K_ON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wxapi;

public partial class Userlogin_userInfo : System.Web.UI.Page
{
    //WeiXinApi wx = new WeiXinApi();
    WXToolsHelper wxt = new WXToolsHelper();
    DataProvider dp = new DataProvider();
    CookiesGetDB ck = new CookiesGetDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        // 0 openid, 1 access_token
        // WXToolsHelper.Write("获取的code:" + Request["code"] + "\r\n");

        userinfo.Value = ck.GetRolesText(Context, 2);
        //string s_openid= wxt.json_text("openid", userinfo.Value);

        //object[] user = dp.U_Proc_UserLogin(new string[] { s_openid });
        //CookieAddDB.User_Login("userinfo_wx", user[2].ToString() + "|" + s_openid + "|" + user[3].ToString() + "|" + "");

        head.Src = wxt.json_text("headimgurl", ck.GetRolesText(Context, 2));

        if (dp.C_Proc_Select(new string[] { ck.GetRolesText(Context, 0), "1", "" }, 2)[0].ToString() == "1000")
        {
            img.Src = "../Member/" + ck.GetRolesText(Context, 1) + ".png";
            //img.Src = "../Member/" + s_openid + ".png";
        }
        else
        {
            img.Src = "../images/logo.png";
        }


    }
}
using K_ON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Userlogin_tutorial_page : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    CookiesGetDB ck = new CookiesGetDB();

    public string openId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (dp.C_Proc_Select(new string[] { ck.GetRolesText(Context, 0), "1", "" }, 2)[0].ToString() == "1000")
        {
           openId =  HttpUtility.UrlEncode(ck.GetRolesText(Context, 1));//是会员返回OpenId
        }
    }
}
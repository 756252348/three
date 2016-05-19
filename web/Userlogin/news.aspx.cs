using K_ON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Userlogin_news : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    CookiesGetDB ck = new CookiesGetDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (dp.C_Proc_Select(new string[] { ck.GetRolesText(Context, 0), "1", "" }, 2)[0].ToString() != "1000")
        {
            Response.Redirect("userinfo.aspx");//不是会员回到主页
        }
        else
        {
            object[] o = dp.C_Proc_Select(new string[] { ck.GetRolesText(Context, 0), "17", Request.QueryString["fl_id"] }, 1);
            content.InnerHtml = o[0].ToString();
        }
    }
}
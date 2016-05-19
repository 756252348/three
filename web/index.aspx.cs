using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K_ON;
using wxapi;
using System.Configuration;


public partial class index : System.Web.UI.Page
{
    WeiXinApi wx = new WeiXinApi();

    private string redirect_uri = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        CookieAddDB.User_Logout();
        //if (Request["PID"] == null)            
        //{
        //    Response.Redirect("index.aspx?PID=1");
        //}
        //else
        //{
        //    //http://weixin.qq.com/r/3ElDWzbELGtTrXI49xzO
        redirect_uri = ConfigurationManager.AppSettings["redirect_uri"];
        string scope = "snsapi_base";
        string type = "0";
        ///进入私募内参
        if (Request["type"] == "1")
        {
            scope = "snsapi_base";
            type = "1";
        }
        ///如何推广
        if (Request["type"] == "2")
        {
            scope = "snsapi_base";
            type = "2";
        }
        redirect_uri += "?type=" + type;
        ///
        redirect_uri = HttpUtility.UrlEncode(redirect_uri);
        wx.GetCode(redirect_uri, scope);
        //}
    }
}
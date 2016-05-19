using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wxapi;

public partial class index : System.Web.UI.Page
{
    WXToolsHelper wxt = new WXToolsHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.Form["Wx_type"],json=string.Empty;
        switch (type)
        {
            case "access_token":
                json = wxt.Getaccess_token();
                break;
        }
        Response.Write(json);
    }
}
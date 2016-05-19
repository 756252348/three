using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wxapi;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        new DataProvider().create_two("sfaf", "sdfs");
        //Response.Write(new WXToolsHelper().wx_button());
        //new WXToolsHelper().GetWxUserInfo("odyHvwzU090seZ_vUmB5DqvXyzgY");
    }
}
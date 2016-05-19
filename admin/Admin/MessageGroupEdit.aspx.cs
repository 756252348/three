using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wxapi;

public partial class Admin_MessageGroupEdit : System.Web.UI.Page
{
    WXToolsHelper wxt = new WXToolsHelper();
    DataProvider dp = new DataProvider();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btUp_Click(object sender, EventArgs e)
    {
        string _openid, _content;
        _openid = "";
        _content = content.Text;

        List<object[]> list = new List<object[]>();
        dp.C_CommonList("select U_OpenID,U_Name from U_User",ref list);

        foreach(object[] o in list){
            _openid = o[0].ToString();
            wxt.MessageUser(_openid, _content);
        }

        MessageBox.Show(Page, "信息发送成功");
    }
}
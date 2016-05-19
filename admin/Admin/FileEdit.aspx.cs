using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FileEdit : System.Web.UI.Page
{
    public string path = "";
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        path = Request.QueryString["path"];
        if (!IsPostBack)
        {
            dp.PromissionOfCommon(Authority.GetRoleID(Context), "1025", "6", lbTitle);
            
            content.Value = FileUtils.ReadFile(Server.MapPath(path));
        }
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        try
        {
            FileUtils.SaveFile(Request.Form["content"], Server.MapPath(path));
            MessageBox.Show(Page, "修改成功！");
        }
        catch
        {
            MessageBox.Show(Page, "修改失败！");
        }
    }
}
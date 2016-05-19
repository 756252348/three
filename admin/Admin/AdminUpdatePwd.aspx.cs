using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminUpdatePwd : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        string OldPwd = this.txtOldPwd.Text.Trim();
        string UserPass1 = txtPwd1.Text.Trim();
        string UserPass2 = txtPwd2.Text.Trim();

        if (dp.G_ExecuteScalar("S_Proc_ModifyPwd", new string[] { Authority.GetAdminName(Context), Common.md5(OldPwd), Common.md5(UserPass1) })=="1000")
        {
            MessageBox.ReLocation(Page, "密码修改成功！");
        }
        else
        {
            MessageBox.Show(Page, "旧密码输入错误，请重新输入！");
        }

    }
}
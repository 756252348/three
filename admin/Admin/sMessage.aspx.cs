using System;
using System.Web;

public partial class Admin_sMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowError();
        }
    }

    protected void ShowError()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["error"]))
        {
            string error = Request["error"], _message = "";
            switch (error)
            {
                case "404":
                    _message = "对不起，你不具备操作本模块的权限！";
                    break;
                case "505":
                    _message = "亲，您还没有登录，请重新登录再进行相关操作！";
                    break;
                case "606":
                    _message = "数据异常，请检查初始化数据是否正确！";
                    break;
                case "707":
                    _message = "系统未知错误！";
                    break;
                case "808":
                    _message = "非法操作！";
                    break;
            }
            llTitie.Text = "系统错误";
            llMessage.Text = _message;
        }
    }
}
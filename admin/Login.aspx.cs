using System;
using System.Web;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        if (Session["CheckCode"] == null)
        {
            MessageBox.Show(Page, "请输入验证码！");
            return;
        }
        else
        {
            if (String.Compare(Session["CheckCode"].ToString(), Request.Form["txtVerifyCode"], true) != 0)
            {
                MessageBox.Show(Page, "验证码输入不正确！");
                return;
            }
            string username = Request.Form["txtUserName"], password = Common.md5(Request.Form["txtUserPwd"]);
            object[] sArray = new object[8];
            if (dp.AdminLogin(new string[] { username, password }, ref sArray) == "1000")
            {
                if (sArray[6].ToString() != "0")
                {
                    MessageBox.Show(Page, "您的账号已被锁定，请联系管理员！");
                    return;
                }

                dp.GetPromissionList(Authority.GetRoleID(Context));

                Authority.Login(username, string.Join("|", sArray), "ADMIN", "Admin/Index.aspx");
            }
            else
            {
                MessageBox.Show(Page, "用户名或密码错误！");
            }
        }
    }
}
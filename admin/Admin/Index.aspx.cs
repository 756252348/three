using System;
using System.Data;
using System.Web;
using System.Text;

public partial class Admin_Index : System.Web.UI.Page
{
    //DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbUserName.Text = Authority.GetAdminName(Context);
        }
    }
}
using System;

public partial class Admin_Exit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Authority.Exit(Page, "Login.aspx");
    }
    
}
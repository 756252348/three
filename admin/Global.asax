<%@ Application Language="C#" %>


<script runat="server">
    
    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        //在出现未处理的错误时运行的代码
    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }

    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        HttpApplication app = (HttpApplication)sender;
        HttpContext ctx = app.Context;
        if (ctx.User != null)
        {
            if (ctx.Request.IsAuthenticated && ctx.User.Identity.AuthenticationType == "Forms")
            {
                System.Web.Security.FormsIdentity fi = (System.Web.Security.FormsIdentity)ctx.User.Identity;
                System.Web.Security.FormsAuthenticationTicket ticket = fi.Ticket;
                string userData = ticket.UserData;
                string[] roles = userData.Split('|')[0].Split(' ');
                ctx.User = new System.Security.Principal.GenericPrincipal(fi, roles);
            }
        }
    }
    
</script>

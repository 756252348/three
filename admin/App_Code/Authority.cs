using System;
using System.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.Web;

/// <summary>
///Authority 的摘要说明
/// </summary>
public static class Authority
{
    public static string GetUserType(HttpContext context)
    {
        return GetUserData(context, 0);
    }

    public static string GetAdminID(HttpContext context)
    {
        return GetUserData(context, 1);
    }

    public static string GetAdminName(HttpContext context)
    {
        return GetUserData(context, 2);
    }

    public static string GetRoleID(HttpContext context)
    {
        return GetUserData(context, 3);
    }

    public static string GetNickName(HttpContext context)
    {
        return GetUserData(context, 5);
    }

    public static string GetRoleName(HttpContext context)
    {
        return GetUserData(context, 7);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string GetUserData(HttpContext context, int index)
    {
        if (IsLogin)
        {
            FormsIdentity identity = context.User.Identity as FormsIdentity;
            string userData = identity.Ticket.UserData;
            string[] data = userData.Split(new char[] { '|' });
            return data[index];
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Page"></param>
    /// <param name="url"></param>
    public static void Exit(System.Web.UI.Page Page,string url)
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        if (authCookie != null)
        {
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
        CacheHelper.RemoveAllCache(Common.SessionPrefix + "PromissionList");

        MessageBox.Exit(Page, url);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="userData"></param>
    /// <param name="userType"></param>
    /// <param name="url"></param>
    public static void Login(string userName, string userData, string userType, string url)
    {
        HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userName, true);
        authCookie.HttpOnly = true;  

        userData = userType + "|" + userData;
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

        FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
            ticket.Version, ticket.Name, ticket.IssueDate,
            ticket.Expiration, ticket.IsPersistent, userData);

        authCookie.Value = FormsAuthentication.Encrypt(newTicket);
        HttpContext.Current.Response.Cookies.Add(authCookie);
        HttpContext.Current.Response.Redirect(url);
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 
    /// </summary>
    public static bool IsLogin
    {
        get
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }

    
}
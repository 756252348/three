using System;
using System.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.Web;

/// <summary>
///Authority 的摘要说明
/// </summary>
public class Authority
{
	public Authority()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static string GetUserID(HttpContext context)
    {
        return GetUserData(context, 0);
    }
    public static string GetOpenID(HttpContext context)
    {
        return GetUserData(context, 1);
    }
    public static string GetNickName(HttpContext context)
    {
        return GetUserData(context, 2);
    }
    public static string GetSex(HttpContext context)
    {
        return GetUserData(context, 3);
    }

    public static string GetTel(HttpContext context)
    {
        return GetUserData(context, 4);
    }


    public static string GetUserData(HttpContext context, int index)
    {
        if (IsLogin)
        {
            FormsIdentity identity = context.User.Identity as FormsIdentity;
            string userData = identity.Ticket.UserData;
            string[] data = userData.Split(new char[] { '|' });
            try
            {
                return string.IsNullOrEmpty(data[index]) ? "0" : data[index]; 
            }
            catch
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static void Exit(string url)
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        if (authCookie != null)
        {
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
        HttpContext.Current.Response.Redirect(url);
    }

    public static void Login(string username, string userData)
    {
        HttpCookie authCookie = FormsAuthentication.GetAuthCookie(username, true);
        authCookie.HttpOnly = true;
        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        FormsAuthenticationTicket ticket2 = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userData);
        authCookie.Value = FormsAuthentication.Encrypt(ticket2);
        HttpContext.Current.Response.Cookies.Add(authCookie);
      
    }

    public static bool IsLogin
    {
        get
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}
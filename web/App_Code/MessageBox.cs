using System.Web;

/// <summary>
///MessageBox 的摘要说明
/// </summary>
public class MessageBox
{
	public MessageBox()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    #region 原生JS弹窗
    /// <summary>
    /// 公共原生JS弹窗
    /// </summary>
    /// <param name="Page">页面对象</param>
    /// <param name="content">内容</param>
    public static void Show(System.Web.UI.Page Page,string content)
    {
        Dialog(Page, "Show","alert('" + content + "');");
    }
    public static void Confirm(System.Web.UI.Page Page, string content, string url)
    {
        Dialog(Page, "Confirm", "if(confirm('" + content + "')){ window.location.href='" + url + "'};");
    }

    public static void GoHistory(System.Web.UI.Page Page, string content)
    {
        Dialog(Page, "GoHistory", "alert('" + content + "');window.history.go(-1);");
    }

    public static void ReLocation(System.Web.UI.Page Page, string content)
    {
        Dialog(Page, "ReLocation", "alert('" + content + "');window.location.href=window.location.href;");
    }

    public static void Location(System.Web.UI.Page Page, string content, string url)
    {
        Dialog(Page, "Location", "alert('" + content + "');window.location.href='" + url + "';");
    }

    public static void LocationAdd(System.Web.UI.Page Page, string content)
    {
        Dialog(Page, "LocationAd", "alert('" + content + "');window.location.href='" + HttpContext.Current.Request.Url.ToString() + "?ID=0" + "';");
    }

    public static void MessageBoxUp(System.Web.UI.Page Page, string url)
    {
        Dialog(Page, "MessageBoxUp", "parent.parent.frames.Content.location='" + url + "';");
    }

    public static void MessageBoxUp(System.Web.UI.Page Page, string content, string url)
    {
        Dialog(Page, "MessageBoxUp", "alert('" + content + "'); parent.parent.frames.Content.location='" + url + "';");
    }

    public static void MessageBoxiframe(System.Web.UI.Page Page, string content,string eventId,string parentID)
    {
        Dialog(Page, "MessageBoxIframe", "alert('" + content + "'); window.parent.frames['ColumnLeft'].location='ColumnTree.aspx?ParentID=0&EventId=" + eventId + "&curId=" + parentID + "';");
    }

    public static void Exit(System.Web.UI.Page Page, string url)
    {
        Dialog(Page, "Exit", "top.location.href='" + url + "';");
    }

    public static void MessageBoxiframe(System.Web.UI.Page Page, string content, string url)
    {
        Dialog(Page, "MessageBoxIframe", "alert('" + content + "'); window.parent.frames['ColumnLeft'].location='" + url + "';");
    }

    #endregion

    public static void Dialog(System.Web.UI.Page Page, string cType, string Code)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), cType, Code, true);
    }
}
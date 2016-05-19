using K_ON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Userlogin_TG_AD : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    CookiesGetDB ck = new CookiesGetDB();
    string script = "<script>alert('%d');window.location.href = '../Userlogin/userinfo.aspx';</script>";
    protected void Page_Load(object sender, EventArgs e)
    {

        string openId = Request.QueryString["oid"];

        openId = HttpUtility.UrlDecode(openId);

        if (string.IsNullOrEmpty(openId))
        {
            /*if (dp.C_Proc_Select(new string[] { ck.GetRolesText(Context, 0), "1", "" }, 2)[0].ToString() != "1000")
            {
                Response.Redirect("~/Userlogin/userinfo.aspx");//不是会员回到主页
            }
            else
            {
                tg_img.Src = "~/Member/o-" + ck.GetRolesText(Context, 1) + ".png";
            }*/
        }
        else
        {
            //检测Sql注入
            string id = FilterSql(openId);
            if (id.Length != openId.Length)
            {
                Response.Write(script.Replace("%d","违法操作"));
            }

            //检测是否是会员
            object[] os = dp.C_Proc_Select(new string[] { "0", "23", id }, 2);
            if (os[0].ToString() == "1000")
            {
                tg_img.Src = "~/Member/o-" + openId + ".png";
            }
            else
            {
                Response.Write(script.Replace("%d", os[1].ToString()));
            }
            
        }
    }

    /// <summary>
    /// 过滤特殊特号
    /// </summary>
    /// <param name="Str"></param>
    /// <returns></returns>
    public static string FilterSql(string Str)
    {
        Str = Str.Trim();
        string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", ",", "=", ";", "|", "[", "]", "&", "/" };
        if (!string.IsNullOrEmpty(Str))
        {
            foreach (string str in aryReg)
            {
                Str = Str.Replace(str, string.Empty);
            }
            return Str;
        }
        else
        {
            return "";
        }
    }
}
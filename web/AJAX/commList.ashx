<%@ WebHandler Language="C#" Class="commList" %>

using System;
using System.Web;
using K_ON;

public class commList : IHttpHandler {
    AshxDBHelper adb = new AshxDBHelper();
    CookiesGetDB ck = new CookiesGetDB();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        //string UserID = "1";//ck.GetRolesText(context, 0);
        string UserID = ck.GetRolesText(context, 0);
          
        string[] pval = GetFormValues(UserID,  context);
        string json = Common.ToJson(adb.ExecPageInProc(pval));
        context.Response.Write(json);
        
    }
    #region 获取表单值到数组
    public static string[] GetFormValues(string uid, HttpContext context)
    {
        int arrLen = context.Request.Params.GetValues("parms[]").Length + 1;
        string[] sArray = new string[arrLen];
        for (int i = 0; i < arrLen; i++)
        {
            if (i == 1)
            {
                sArray[1] = uid;
            }
            else if (i == 0)
            {
                sArray[i] = context.Request.Params.GetValues("parms[]")[i];//string.Join("|", context.Request.Form.GetValues(i));
            }
            else
            {
                sArray[i] = context.Request.Params.GetValues("parms[]")[i - 1];
            }

        }
        return sArray;
    }
    #endregion
    public bool IsReusable {
        get {
            return false;
        }
    }

}
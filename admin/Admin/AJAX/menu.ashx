<%@ WebHandler Language="C#" Class="menu" %>

using System;
using System.Web;

public class menu : IHttpHandler {

    DataProvider dp = new DataProvider();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string result = "";

        result = dp.dataTableToJSON(dp.GetPromissionList(Authority.GetRoleID(context)), "0", " M_ParentID");

        if (!string.IsNullOrEmpty(result))
        {
            result = result.Substring(7);
        }
        context.Response.Write(result);
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
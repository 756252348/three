<%@ WebHandler Language="C#" Class="commonAction" %>

using System;
using System.Web;

public class commonAction : IHttpHandler {

    DataProvider dp = new DataProvider();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        int id=Common.Q_Int("ID", 0);
        
        string result = "0";
        if (!Common.Check_Post_Url(context))//检查连接有效性，防止外部提交
        {
            result = "808";
        }
        else
        {
            int Module = Common.S_Int(context.Request.Form["ModuleID"]),
                nid = Common.S_Int(context.Request.Form["nid"]);
            string action = context.Request.Form["Action"];
            string b = context.Request.Form["b"];
            string c = context.Request.Form["c"];
            switch (action)
            {
                case "recharge":
                    //result = 
                    //到数据库里去执行同意提现的操作，然后把返回值赋值给result
                    break;
                //case "refuse":
                //   new StoredProcedure().C_CheckWithdrawMoney();
                    
                //    break;
            }
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
<%@ WebHandler Language="C#" Class="memberdetail" %>

using System;
using System.Web;
using K_ON;
public class memberdetail : IHttpHandler {
    CookiesGetDB ck = new CookiesGetDB();
    DataProvider dp = new DataProvider();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.ContentType = "text/plain";
        string dataType = string.Empty, json = string.Empty, result = "1001";
        ///返回的参数数组,用在分页函数里
        string[] dataList = new string[3];
        ///接受数据是get还是post 
        //string UserID = "1";// ck.GetRolesText(context, 0);
        string UserID = ck.GetRolesText(context, 0);
        if (context.Request.HttpMethod == "GET") { dataType = context.Request.QueryString["dataType"]; }//从页面到后端获取数据的方法  get post
        else if (context.Request.HttpMethod == "POST") { dataType = context.Request.Form["dataType"]; }// parms=context.Request.Form.GetValues("parms[]");}
        ///接受过来的参数是以数组的形式
        string[] parms = context.Request.Params.GetValues("parms[]");
        switch (dataType)
        {
            case "buy":
                json = Common.ToJson(dp.O_M_Orderjoin(DataProvider.GetFormValues(UserID, context)));
                break;
            case "change":
                //json = Common.ToJson(dp.M_M_Memberchange(DataProvider.GetFormValues(UserID, context)));
                break;
            case "tx":
                json = Common.ToJson(dp.U_Proc_CreateWithdrawMoney(DataProvider.GetFormValues(UserID, context)));

                break;
            case "addres":
                json = Common.ToJson(dp.U_Proc_OperateAddress(DataProvider.GetFormValues(UserID, context)));
                break;
            case "imgscr":
                json = Common.ToJson(new string[] { ck.GetRolesText(context, 1) + ".png" });
                break;
            case "orderqx":
                json = Common.ToJson(dp.U_Proc_CloseOrder(DataProvider.GetFormValues(UserID, context)));
                break;
            case "orderintegrasubmit":
                json = Common.ToJson(dp.U_Proc_OverBuy(DataProvider.GetFormValues(UserID, context)));
                break;
            case "creatbuy":
                json = Common.ToJson(dp.U_Proc_CreateBuy(DataProvider.GetFormValues(UserID, context)));
                break;
        }
        context.Response.Write(json);
    }
    //public static string[] GetFormValues(string uid, HttpContext context)
    //{
    //    int arrLen = context.Request.Params.GetValues("parms[]").Length;
    //    string[] sArray = new string[arrLen];
    //    for (int i = 0; i < arrLen; i++)
    //    {
    //        if (i == 0)
    //        {
    //            sArray[0] = uid;
    //        }
    //        else
    //        {
    //            sArray[i] = context.Request.Params.GetValues("parms[]")[i - 1];
    //        }

    //    }
    //    return sArray;
    //}
    public bool IsReusable {
        get {
            return false;
        }
    }

}
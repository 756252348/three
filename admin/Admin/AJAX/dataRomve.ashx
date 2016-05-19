<%@ WebHandler Language="C#" Class="dataRomve" %>

using System;
using System.Web;

public class dataRomve : IHttpHandler {

    DataProvider dp = new DataProvider();
    StoredProcedure sp = new StoredProcedure();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        int Module = Common.S_Int(context.Request.Form["ModuleID"]);
        string result = "";
        if (!Common.Check_Post_Url(context))
        {
            result = "808";
        }
        else
        {
            result = sp.GetRolePromissionEx(Authority.GetRoleID(context), Module.ToString(), "8");
            if (result == "000")
            {
                bool state = false;
                string nid = context.Request.Form["nid"];
                switch (Module)
                {
                    case 111:
                        state = sp.C_Operate_Delete("A_Article", nid);
                        break;
                    case 112:
                        state = sp.C_Operate_Delete("A_Essay" ,nid);
                        break;
                    case 113:
                        state = sp.C_Operate_Delete("A_Product", nid);
                        break;
                    case 114:
                        state = sp.C_Operate_Delete("A_FirendLink" ,nid);
                        break;
                    case 115:
                        state = sp.C_Operate_Delete("A_Ad" , nid);
                        break;
                    case 116:
                        state = sp.C_Operate_Delete("A_Recruitment", nid);
                        break;
                    case 1011:
                        string[] iArray = nid.Split(',');
                        if (Common.ArrayIsContains(iArray, "99"))
                            state = false;
                        else
                            state = sp.C_Operate_Delete("S_Role", nid);
                        break;
                    case 1012:
                        string[] sArray = nid.Split(',');
                        if (Common.ArrayIsContains(sArray, "88"))
                            state = false;
                        else
                            state = sp.C_Operate_Delete("S_Admin" , nid);
                        break;
                    case 1016:
                        state = sp.C_Operate_Delete("S_Permission", nid);
                        break;
                    case 118:
                        state = sp.C_Operate_Delete("G_Goods", nid);
                        break;
                    case 1115:
                        state = sp.C_Operate_Delete("A_Article", nid);
                        break;
                    case 1116:
                        state = sp.C_Operate_Delete("A_Article", nid);
                        break;
                    case 1118:
                        state = sp.C_Operate_Delete("A_Article", nid);
                        break; 
                        
                }
                result = state ? "1" : "0";
            }
            context.Response.Write(result);
            context.Response.End();
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
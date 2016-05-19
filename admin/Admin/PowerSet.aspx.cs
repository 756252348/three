using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

public partial class Admin_PowerSet : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        dp.GetRolePromission(Authority.GetRoleID(Context), "1011", "24", lbTitle);
        if (!IsPostBack)
        {
            if (id > 0) DataInfo();
        }
    }

    protected void DataInfo()
    {
        ApplicationList.Value = Common.ToJson(dp.C_CommonList("SELECT A_Code,A_Name FROM dbo.S_Application"));

        DataTable dt = dp.C_CommonList("SELECT * FROM dbo.S_Module");
        StringBuilder sbHtml = new StringBuilder();
        if (Common.DataTableIsNull(dt))
        {
            DataTable dt1 = Common.SelectDataTable(dt, " M_ParentID='0' and M_IsSystem='false' and M_IsEnable='true' ");
            int i, dt1Count = dt1.Rows.Count;
            if (Common.DataTableIsNull(dt1))
            {
                for (i = 0; i < dt1Count; i++)
                {
                    sbHtml.AppendFormat("<tr><th>{0}</th><td>{1}</td></tr>", dt1.Rows[i][2].ToString(), this.CreatHtml(dt, dt1.Rows[i][0].ToString()));
                }
            }
            dt1.Dispose();
        }
        dt.Dispose();
        showList.Text = sbHtml.ToString();
    }

    /// <summary>
    /// 创建权限分类的Table
    /// </summary>
    /// <param name="dt">Module总表</param>
    /// <param name="PID">父级ID</param>
    /// <returns></returns>
    protected string CreatHtml(DataTable dt, string PID)
    {
        StringBuilder sbStr = new StringBuilder();
        DataTable dt2 = Common.SelectDataTable(dt, "M_IsSystem='false' and M_IsEnable='true' and M_ParentID='" + PID + "'");
        if (Common.DataTableIsNull(dt2))
        {
            DataTable dt3 = null;
            int i, j, dt2Count = dt2.Rows.Count, dt3Count;
            sbStr.Append("<table class=\"PowerTable\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><thead><tr><td style=\"width:15%;\">二级栏目</td><td style=\"width:15%;\">三级栏目</td><td>分配权限</td></tr></thead><tbody>");
            for (i = 0; i < dt2Count; i++)
            {
                dt3 = Common.SelectDataTable(dt, "M_IsSystem='false' and M_IsEnable='true' and M_ParentID='" + dt2.Rows[i][0].ToString() + "'");
                dt3Count = dt3.Rows.Count;
                sbStr.AppendFormat("<tr><td class=\"rSpan\" rowspan=\"{0}\">{1}</td><td class='second' data-ppid=\"{2}\" data-pid=\"{3}\" data-mid=\"{4}\" >{5}</td><td class=\"Permn\">{6}</td></tr>", dt3Count, dt2.Rows[i][2].ToString(), PID, dt2.Rows[i][0].ToString(), dt3.Rows[0][0].ToString(), dt3.Rows[0][2], this.getUserPower(dt3.Rows[0][0].ToString(), dt3.Rows[0][10].ToString(), dt2.Rows[i][0].ToString()));
                for (j = 1; j < dt3Count; j++)
                {
                    sbStr.AppendFormat("<tr><td class='second'  data-ppid=\"{0}\" data-pid=\"{1}\" data-mid=\"{2}\">{3}</td><td class=\"Permn\">{4}</td></tr>", PID, dt2.Rows[i][0].ToString(), dt3.Rows[j][0].ToString(), dt3.Rows[j][2].ToString(), this.getUserPower(dt3.Rows[j][0].ToString(), dt3.Rows[j][10].ToString(), dt2.Rows[i][0].ToString()));
                }
            }
            sbStr.Append("</tbody></table>");
            dt3.Dispose();
        }
        dt2.Dispose();
        return sbStr.ToString();
    }

    #region 获取权限值
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ModuleID"></param>
    /// <param name="Code"></param>
    /// <param name="Parent"></param>
    /// <returns></returns>
    protected string getUserPower(string ModuleID, string Code, string Parent)
    {
        DataTable dt4 = dp.C_CommonList("select P_Code,P_ModuleID from S_Permission where P_RoleID='" + id.ToString() + "'  ");
        StringBuilder sbStr = new StringBuilder();
        string[] sArray = Code.Split('|');
        DataTable dt5 = Common.SelectDataTable(dt4, "  P_ModuleID='" + ModuleID + "' ");
        if (Common.DataTableIsNull(dt5))
        {
            string[] pro = dt5.Rows[0][0].ToString().Split('|');
            if (pro.Length > 0)
            {
                for (int i = 0, len = sArray.Length; i < len; i++)
                {
                    if (Common.ArrayIsContains(pro, sArray[i]))
                    {
                        //dp.GetFieldValue(sArray[i].ToString(), 3)
                        sbStr.AppendFormat("<input type=\"checkbox\" checked=\"checked\"  value=\"{0}\" /><label>{1}</label>", sArray[i], sArray[i].ToString());
                    }
                    else
                    {
                        sbStr.AppendFormat("<input type=\"checkbox\" value=\"{0}\" /><label>{1}</label>", sArray[i], sArray[i].ToString());
                    }
                }
            }
        }
        else
        {
            for (int i = 0, len = sArray.Length; i < len; i++)
            {
                sbStr.AppendFormat("<input type=\"checkbox\" value=\"{0}\" /><label>{1}</label>", sArray[i], sArray[i].ToString());
            }
        }
        dt5.Dispose();
        dt4.Dispose();
        return sbStr.ToString();
    }
    #endregion


    protected void btUp_Click(object sender, EventArgs e)
    {
        string MoudleID = Request.Form["MoudleIDList"], Permission = Request.Form["PermissionList"];
        if (!string.IsNullOrEmpty(MoudleID) && !string.IsNullOrEmpty(Permission))
        {
            string[] MoudleIDArr = MoudleID.Split(','), PermissionArr = Permission.Split(',');
            if (MoudleIDArr.Length == PermissionArr.Length)
            {
                DataTable InsertDt = dp.C_dataList("SELECT * FROM S_Permission WHERE P_RoleID=0");
                for (int i = 0, len = MoudleIDArr.Length; i < len; i++)
                {
                    DataRow r = InsertDt.NewRow();
                    r["ID"] = "0";
                    r["P_RoleID"] = id.ToString();
                    r["P_ModuleID"] = MoudleIDArr[i];
                    r["P_Code"] = PermissionArr[i];
                    r["AddDay"] = DateTime.Now.ToString("yyyyMMdd");
                    r["AddTime"] = DateTime.Now.ToString();
                    InsertDt.Rows.Add(r);
                }

                if (new StoredProcedure().OperatePermission(InsertDt, id.ToString()))
                {
                    MessageBox.ReLocation(Page, "操作成功！");
                }
                else {
                    MessageBox.Show(Page, "操作失败！");
                }
            }
            else
            {
                Common._Redirect("808");
            }
        }
    }
}
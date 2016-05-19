using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Admin_VipInfoEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            //dp.PromissionOfEdit(Authority.GetRoleID(Context), "111", id, lbTitle);
            //new Upload("News").IniImgConfig(ImgConfig);
            if (id > 0)
                cnm();
            
                //DataInfo();
        }
    }
    protected void cnm()
    {
        
        
        SqlConnection sqlCon = new SqlConnection(DataProvider.connectionString);
        string sql1 = "select U_User.ID,GM_OldMoney,GM_NowMoney,GM_ChangeMoney,GM_OldPoint,GM_NowPoint,GM_ChangePoint,(case GM_ChangeType when 0 then '充值' when 1 then '提现' when 2 then '获得积分' when 3 then '使用积分' when 4 then '购买锦囊' when 5 then '返利' when 6 then '购买金额商品' end)GM_ChangeType,GM_BillMoney.AddTime from GM_BillMoney join U_User on GM_BillMoney.GM_MemberID=U_User.ID  where U_User.ID="+id+"";
        sqlCon.Open();
        SqlCommand sqlCom1 = new SqlCommand(sql1, sqlCon);


       

        SqlDataReader sqlDr = sqlCom1.ExecuteReader();

        StringBuilder s = new StringBuilder();
        s.Append("<table class='division'>");
        s.Append("<tr>" + "<td>" + "账户ID" + "</td>" + "<td>" + "原有金额" + "</td>" + "<td>" + "现有金额" + "</td>" + "<td>" + "金额变化" + "</td>" + "<td>" + "原有积分" + "</td>" + "<td>" + "现有积分" + "</td>" + "<td>" + "积分变化" + "</td>" + "<td>" + "变化的类型" + "</td>" + "<td>" + "变化的时间" + "</td>" + "</tr>");
        while (sqlDr.Read())
        {
            
            s.Append("<tr> ");
            s.Append("<td>" + sqlDr["ID"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["GM_OldMoney"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["GM_NowMoney"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["GM_ChangeMoney"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["GM_OldPoint"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["GM_NowPoint"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["GM_ChangePoint"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["GM_ChangeType"].ToString() + "</td>");
            s.Append("<td>" + sqlDr["AddTime"].ToString() + "</td>");            
            s.Append("</tr>");
            
        }
        s.Append("</table>");
        Label1.Text = s.ToString();

        sqlCon.Close();
    }

    //protected void ccc()
    //{
    //    string FiledName = "U_User.ID,GM_OldMoney,GM_NowMoney,GM_ChangeMoney,GM_OldPoint,GM_NowPoint,GM_ChangePoint,GM_ChangeType,GM_BillMoney.AddTime",
    //        dtSqlField = "GM_OpearteID,GM_OldMoney", RecordCount = "0", PageCount = "0", PageSize = "10", PageIndex = Common.Q_Int("Page", 0).ToString();
    //    WebControl.CreatTable(Literal1, FiledName, "编号,原有金额,现有金额,改变的金额,原有的积分,现有的积分,变化的积分,变化的类型,变化的时间", dp.C_Pagination(new string[] { PageIndex, PageSize, FiledName, "GM_BillMoney join U_User on GM_BillMoney.GM_OpearteID=U_User.ID", "GM_OpearteID=" + id + "", "AddTime DESC" }, ref RecordCount, ref PageCount));
    //}
    //protected void DataInfo()
    //{
    //    #region 数据绑定
    //    object[] oArray = new object[9];
    //    if (dp.C_LoadArrayData("select U_User.ID,GM_OldMoney,GM_NowMoney,GM_ChangeMoney,GM_OldPoint,GM_NowPoint,GM_ChangePoint,(case GM_ChangeType when 0 then '充值' when 1 then '提现' when 2 then '获得积分' when 3 then '使用积分' when 4 then '购买锦囊' when 5 then '返利'  end)GM_ChangeType,GM_BillMoney.AddTime from GM_BillMoney join U_User on GM_BillMoney.GM_OpearteID=U_User.ID  where U_User.ID=" + id.ToString(), ref oArray) == "1000")
    //    {

    //        userid.Text = oArray[0].ToString();
    //        oldmoney.Text = oArray[1].ToString();
    //        nowmoney.Text = oArray[2].ToString();
    //        changemoney.Text = oArray[3].ToString();
    //        oldpoint.Text = oArray[4].ToString();
    //        nowpoint.Text = oArray[5].ToString();
    //        changepoint.Text = oArray[6].ToString();
    //        changetype.Text = oArray[7].ToString();
    //        changetime.Text = oArray[8].ToString();
    //    }
    //    #endregion
    //}



}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CommonMemberEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {

        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "114", id, lbTitle);
            
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[5];
        if (dp.C_LoadArrayData("select U_RealName,U_EMail,U_Tel,U_QQ,U_CompanyName from U_User where ID=" + id.ToString(), ref oArray) == "1000")
        {
            Name.Text = oArray[0].ToString();
            EMail.Text = oArray[1].ToString();
            Tel.Text = oArray[2].ToString();
            QQ.Text = oArray[3].ToString();
            CompanyName.Text = oArray[4].ToString();
        }
        #endregion
    }


    protected void btUp_Click(object sender, EventArgs e)
    {
        string Img = Request.Form["fuValue"];
        if (string.IsNullOrEmpty(Img))
        {
            Img = "Default.png";
        }

        if (new StoredProcedure().A_Proc_UpdateUserInfo(new string[] { id.ToString(), Request.Form["Name"], Request.Form["EMail"], Request.Form["Tel"], Request.Form["QQ"], Request.Form["CompanyName"] }))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "1012", id, lbTitle);
            if (Authority.GetRoleID(Context) != "99")
                WebControl.C_BindDataToDDL("select ID,R_Name from S_Role where ID<>99", ddlRole);
            else
                WebControl.C_BindDataToDDL("select ID,R_Name from S_Role", ddlRole);

            if (id > 0)
                DataInfo();

        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[9];
        if (dp.C_LoadArrayData(string.Format("select A_Account,A_RoleID,A_RealName,A_NickName,A_Position,A_Department,A_Status,A_Description,A_Password from S_Admin where ID=" + id.ToString()), ref oArray) == "1000")
        {
            txtName.Text = oArray[0].ToString();
            ddlRole.SelectedValue = oArray[1].ToString();
            txtRealName.Text = oArray[2].ToString();
            txtNickName.Text = oArray[3].ToString();
            txtPostion.Text = oArray[4].ToString();
            txtDepart.Text = oArray[5].ToString();
            rblStatus.SelectedValue = oArray[6].ToString();
            txtDesc.Text = oArray[7].ToString();
            txtName.ReadOnly = true;
            Pwd.Value = oArray[8].ToString();
            Pwd.Attributes.Add("readonly", "readonly");
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        string pwd = Request.Form["txtPwd"].Trim(), RoleID = Request.Form["ddlRole"];
        if (!string.IsNullOrEmpty(pwd))
        {
            pwd = Common.md5(pwd);
        }
        else
        {
            pwd = Request.Form["Pwd"];
        }
        if (id == 88)
            RoleID = "99";

        if (new StoredProcedure().C_Admin_Operate(new string[] { id.ToString(), Request.Form["txtName"], pwd, 
                RoleID, Request.Form["txtRealName"], Request.Form["txtNickName"], 
                Request.Form["txtPostion"], Request.Form["txtDepart"],Request.Form["rblStatus"],Request.Form["txtDesc"]}))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
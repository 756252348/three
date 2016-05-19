using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RoleEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "1011", id, lbTitle);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[2];
        if (dp.C_LoadArrayData("select R_Name,R_Description from S_Role where ID=" + id.ToString(), ref oArray) == "1000")
        {
            txtName.Text = oArray[0].ToString();
            txtDescription.Text = oArray[1].ToString();
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        if (dp.C_Update_IsRepeat(new string[] { id.ToString(), "ID", "S_Role", "R_Name", txtName.Text }))
        {
            MessageBox.Show(Page, "您输入的角色名称已存在，请重新输入！");
            return;
        }
        else
        {
            if (new StoredProcedure().C_Role_Operate(new string[] { id.ToString(), Request.Form["txtName"], Request.Form["txtDescription"] }))
            {
                MessageBox.Show(Page, "操作成功！");
            }
            else
            {
                MessageBox.Show(Page, "操作失败！");
            }
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_ApplicationEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "1014", id, lbTitle);
            if (id > 0)
                DataInfo(id);
        }
    }

    protected void DataInfo(int id)
    {
        #region 数据绑定
        object[] oArray = new object[6];
        if (dp.C_LoadArrayData(string.Format("select A_Code,A_Name,A_Type,A_Icon,A_Field,A_Description from S_Application where ID="+ id.ToString()), ref oArray) == "1000")
        {
            txtCode.Text = oArray[0].ToString();
            txtName.Text = oArray[1].ToString();
            ddlType.SelectedValue = oArray[2].ToString();
            txtIcon.Text = oArray[3].ToString();
            txtSub.Text = oArray[4].ToString();
            txtDesc.Text = oArray[5].ToString();
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        if (id > 0)
        {
            #region 修改
            if (dp.C_Update_IsRepeat(new string[] { id.ToString(), "ID", "S_Application", "A_Code", Request.Form["txtCode"] }))
            {
                MessageBox.Show(Page, "您输入的应用标识已存在，请重新输入！");
                return;
            }
            #endregion
        }
        else
        {
            #region 添加
            if (dp.C_Insert_IsRepeat(new string[] { "S_Application", "A_Code", Request.Form["txtCode"] }))
            {
                MessageBox.Show(Page, "您输入的应用标识已存在，请重新输入！");
                return;
            }
            #endregion
        }

        if (new StoredProcedure().C_Application_Operate(new string[]{ id.ToString(),Request.Form["txtCode"],Request.Form["txtName"],Request.Form["ddlType"],
                Request.Form["txtIcon"],Request.Form["txtSub"],Request.Form["txtDesc"]}))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
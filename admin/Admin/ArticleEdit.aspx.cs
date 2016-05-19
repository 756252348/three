using System;
using System.Web;
using System.Data;

public partial class Admin_ArticleEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "112", id, lbTitle);
            if (id > 0)
                DataInfo(id);
        }
    }

    protected void DataInfo(int id)
    {
        #region 数据绑定
        object[] oArray = new object[5];
        if(dp.C_LoadArrayData("select E_Title,E_Content,E_SortID,E_IsShow,E_ColumnID from A_Essay where ID=" + id.ToString(),ref oArray)=="1000")
        {
            txtTitle.Text = oArray[0].ToString();
            content.Text = oArray[1].ToString();
            txtSortID.Text = oArray[2].ToString();
            rblStatus.SelectedValue = Common.SqlBitToByte(oArray[3].ToString());
            ddlColumnVal.Value = oArray[4].ToString();
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        if (new StoredProcedure().C_EssayOperate(new string[]{ id.ToString(),Request.Form["ddlColumn"], Request.Form["txtTitle"],Request.Form["content"],
                Request.Form["txtSortID"],Request.Form["rblStatus"] }))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
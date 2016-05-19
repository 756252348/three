using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_TreeView_ColumnSet : System.Web.UI.Page
{
    public int _ParentID = 0, _Level = 0;
    DataProvider dp = new DataProvider();
    StoredProcedure sp = new StoredProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        _ParentID = Common.Q_Int("ParentID", 0);
        _Level = Common.Q_Int("Level", 0);
        if (!IsPostBack)
        {
            if (_ParentID > 0)
            {
                DataInfo();
            }
            //ButtonDelete.Attributes.Add("onclick", "if('" + _ParentID + "'==''){alert('请选择左边节点！');return false;}else{return confirm('确定删除该记录吗?');}");
        }
        txtLevel.Text = _Level.ToString();
    }


    #region 数据绑定
    private void DataInfo()
    {
        object[] sArray = new object[8];
        if (dp.C_LoadArrayData("select C_ParentID,C_Type,C_Name,C_Level,C_Url,C_SortID,C_IsIndex,C_Description from A_Column  where Tag='False' and ID= " + _ParentID, ref sArray) == "1000")
        {
            txtID.Text = _ParentID.ToString();
            txtDepth.Text = sArray[0].ToString();
            rblType.SelectedValue = sArray[1].ToString();
            txtCategoryName.Text = sArray[2].ToString();
            txtLevel.Text = sArray[3].ToString();
            txtLink.Text = sArray[4].ToString();
            txtSortID.Text = sArray[5].ToString();
            rblIndex.SelectedValue = sArray[6].ToString();
            txtDescription.Text = sArray[7].ToString();

            lbSelectNodesText.Text = "选中的菜单项(节点):<br/><font color=\"blue\" size=\"5\">" + sArray[2].ToString() + "</font>";
            lbSelectNodesText.Text += "<font size=\"4\">(" + _ParentID + ")</font>";
        }
        else
        {
            dp.dataToFile(Server.MapPath("~/Script/data.column.js"), DataProvider.WebConfigType.Column);
            MessageBox.MessageBoxUp(Page, "节点不存在，刷新左边！", "../Column.aspx");
        }
    }
    #endregion

    protected void ButtonAddSubMenu_Click(object sender, EventArgs e)
    {
        #region 添加子节点
        foreach (Control c in this.Form.Controls)
        {
            if (c is TextBox)
            {
                (c as TextBox).Text = "";
            }
        }
        txtDepth.Text = _ParentID.ToString();
        txtSortID.Text = "0";
        txtLink.Text = "#";
        txtLevel.Text = _Level.ToString();
        #endregion
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        #region 删除
        if (!dp.GetRolePromission(Authority.GetRoleID(Context), "1022", "8"))
        {
            MessageBox.Show(Page, "您的权限不够，请联系管理员！");
        }
        else
        {
            if (dp.C_Insert_IsRepeat(new string[] { "A_Column", "C_ParentID", _ParentID.ToString() }))
            {
                MessageBox.MessageBoxUp(Page, "该节点存在子节点,不允许删除.！", "../Column.aspx");
            }
            else
            {
                if (sp.C_Operate_TagDelete("A_Column", _ParentID.ToString()))
                {
                    dp.dataToFile(Server.MapPath("~/Script/data.column.js"), DataProvider.WebConfigType.Column);
                    MessageBox.MessageBoxUp(Page, "删除成功！", "../Column.aspx");
                    lbSelectNodesText.Text = "";
                    txtID.Text = "";
                    txtDepth.Text = "0";
                }
                else
                {
                    MessageBox.MessageBoxUp(Page, "删除失败！", "../Column.aspx");
                }
            }
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtID.Text))
        {
            if (dp.GetRolePromission(Authority.GetRoleID(Context), "1022", "4"))
            {
                #region 添加
                int level = Common.S_Int(txtLevel.Text) + 1;
                if (sp.A_Proc_Operate_Column(new string[] { "0",Request.Form["txtDepth"],Request.Form["rblType"],Request.Form["txtCategoryName"],
                      level.ToString(),Request.Form["txtLink"],Request.Form["txtSortID"],Request.Form["rblIndex"],Request.Form["txtDescription"]}))
                {
                    dp.dataToFile(Server.MapPath("~/Script/data.column.js"), DataProvider.WebConfigType.Column);
                    MessageBox.MessageBoxUp(Page, "添加成功！", "../Column.aspx");
                }
                else
                {
                    MessageBox.Show(Page, "添加失败，您输入的栏目名称重复！");
                }
                #endregion
            }
            else
            {
                MessageBox.Show(Page, "您的权限不够，请联系管理员！");
            }
        }
        else
        {
            if (dp.GetRolePromission(Authority.GetRoleID(Context), "1022", "6"))
            {
                #region 修改
                if (sp.A_Proc_Operate_Column(new string[] { _ParentID.ToString(),Request.Form["txtDepth"],Request.Form["rblType"],Request.Form["txtCategoryName"],
                        Request.Form["txtLevel"],Request.Form["txtLink"],Request.Form["txtSortID"],Request.Form["rblIndex"],Request.Form["txtDescription"] }))
                {
                    dp.dataToFile(Server.MapPath("~/Script/data.column.js"), DataProvider.WebConfigType.Column);
                    MessageBox.MessageBoxUp(Page, "修改成功！", "../Column.aspx");
                }
                else
                {
                    MessageBox.Show(Page, "修改失败，您输入的栏目名称重复！");
                }
                #endregion
            }
            else
            {
                MessageBox.Show(Page, "您的权限不够，请联系管理员！");
            }
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_TreeView_ModuleSet : System.Web.UI.Page
{
    public int _ParentID = 0, _Level = 0, _Type = 0;
    DataProvider dp = new DataProvider();
    StoredProcedure sp = new StoredProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        _ParentID = Common.Q_Int("ParentID", 0);
        _Level = Common.Q_Int("Level", 0);
        if (!IsPostBack)
        {
            DataInfo();
            ButtonDelete.Attributes.Add("onclick", "if('" + _ParentID + "'==''){alert('请选择左边节点！');return false;}else{return confirm('确定删除该记录吗?');}");
        }
        txtLevel.Text = _Level.ToString();
    }

    #region 数据绑定
    private void DataInfo()
    {
        object[] oArray = new object[12];
        if (dp.C_LoadArrayData("select [ID],[M_ParentID],[M_Name],[M_Code],[M_Level],[M_Directory],[M_IsEnable],[M_IsSystem],[M_SortID],[M_Icon],[M_Item],[M_Description] from S_Module where ID=" + _ParentID.ToString(), ref oArray) == "1000")
        {
            txtID.Text = oArray[0].ToString();
            txtDepth.Text = oArray[1].ToString();
            txtCategoryName.Text = oArray[2].ToString();
            txtCode.Text = oArray[3].ToString();
            txtLevel.Text = oArray[4].ToString();
            txtLink.Text = oArray[5].ToString();
            rblIsEnable.SelectedValue = Common.SqlBitToByte(oArray[6].ToString());
            rblIsSystem.SelectedValue = Common.SqlBitToByte(oArray[7].ToString());
            txtSortID.Text = oArray[8].ToString();
            txtIcon.Text = oArray[9].ToString();
            txtItem.Text = oArray[10].ToString();
            txtDescription.Text = oArray[11].ToString();
            txtID.ReadOnly = true;

            lbSelectNodesText.Text = "选中的菜单项(节点):<br/><font color=\"blue\" size=\"5\">" + oArray[1].ToString() + "</font>";
            lbSelectNodesText.Text += "<font size=\"4\">(" + _ParentID + ")</font>";
            txtType.Text = "1";
        }
        else
        {
            txtType.Text = "0";
            //MessageBox.MessageBoxUp(Page, "节点不存在，刷新左边！", "../SystemSet/Module.aspx");
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
        txtID.ReadOnly = false;
        txtDepth.Text = _ParentID.ToString();
        txtSortID.Text = "0";
        txtType.Text = "0";
        txtLevel.Text = _Level.ToString();
        #endregion
    }

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        #region 删除
        if (!dp.GetRolePromission(Authority.GetRoleID(Context), "1013", "8"))
        {
            MessageBox.Show(Page, "您的权限不够，请联系管理员！");
        }
        else
        {
            if (dp.C_Insert_IsRepeat(new string[] { "S_Module", "M_ParentID", _ParentID.ToString() }))
            {
                MessageBox.MessageBoxUp(Page, "该节点存在子节点,不允许删除.！", "../Column.aspx?id=1");
            }
            else
            {
                if (sp.C_Operate_Delete("S_Module", _ParentID.ToString()))
                {
                    dp.dataToFile(Server.MapPath("~/Script/data.module.js"), DataProvider.WebConfigType.Module);
                    MessageBox.MessageBoxUp(Page, "删除成功！", "../Column.aspx?id=1");
                    lbSelectNodesText.Text = "";
                    txtID.Text = "";
                    txtDepth.Text = "0";

                }
                else
                {
                    MessageBox.Show(Page, "删除失败！");
                }
            }
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(txtCategoryName.Text))
        //{
        //    MessageBox.Show(Page, "分类名称不能为空！");
        //    return;
        //}
        //else if (string.IsNullOrEmpty(txtSortID.Text))
        //{
        //    MessageBox.Show(Page, "排序编号不能为空！");
        //    return;
        //}
        //else if (string.IsNullOrEmpty(txtCode.Text))
        //{
        //    MessageBox.Show(Page, "标识编号不能为空！");
        //    return;
        //}
        //else
        //{
            if (Request.Form["txtType"] == "0")
            {
                if (dp.GetRolePromission(Authority.GetRoleID(Context), "1013", "4"))
                {
                    #region 添加
                    int level = Common.S_Int(txtLevel.Text) + 1;
                    if (sp.C_Moudle_Operate(new string[]{ Request.Form["txtID"],Request.Form["txtDepth"],Request.Form["txtCategoryName"],Request.Form["txtCode"],
                    level.ToString(),Request.Form["txtLink"],Request.Form["rblIsEnable"],Request.Form["rblIsSystem"],Request.Form["txtSortID"],
                    Request.Form["txtIcon"],Request.Form["txtItem"],Request.Form["txtDescription"]}))
                    {
                        dp.dataToFile(Server.MapPath("~/Script/data.module.js"), DataProvider.WebConfigType.Module);
                        MessageBox.MessageBoxUp(Page, "添加成功！", "../Column.aspx?id=1");
                    }
                    else
                    {
                        MessageBox.Show(Page, "添加失败！");
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
                if (dp.GetRolePromission(Authority.GetRoleID(Context), "1013", "6"))
                {
                    #region 修改
                    if (dp.C_Update_IsRepeat(new string[] { _ParentID.ToString(), "ID", "S_Module", "M_Name", Request.Form["txtCategoryName"] }))
                    {
                        MessageBox.MessageBoxUp(Page, "该模块名称已经存在，请重新输入！", "../Column.aspx?id=1");
                        return;
                    }
                    else
                    {
                        if (sp.C_Moudle_Operate(new string[]{ Request.Form["txtID"],Request.Form["txtDepth"],Request.Form["txtCategoryName"],Request.Form["txtCode"],
                    Request.Form["txtLevel"],Request.Form["txtLink"],Request.Form["rblIsEnable"],Request.Form["rblIsSystem"],Request.Form["txtSortID"],
                    Request.Form["txtIcon"],Request.Form["txtItem"],Request.Form["txtDescription"]}))
                        {
                            dp.dataToFile(Server.MapPath("~/Script/data.module.js"), DataProvider.WebConfigType.Module);
                            MessageBox.MessageBoxUp(Page, "修改成功！", "../Column.aspx?id=1");
                        }
                        else
                        {
                            MessageBox.Show(Page, "修改失败！");
                        }
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show(Page, "您的权限不够，请联系管理员！");
                }
            }
        //}
    }
}
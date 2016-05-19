using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_SysConfig : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    Upload ud = new Upload();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dp.GetRolePromission(Authority.GetRoleID(Context), "1021", "6", lbTitle);

            #region 数据绑定
            object[] oArray = new object[7];
            if (dp.C_LoadArrayData("select C_SiteName,C_ICP,C_Keywords,C_Logo,C_Tel,C_Address from S_ConfigInfo where ID=1", ref oArray) == "1000")
            {
                txtSiteName.Text = oArray[0].ToString();
                txtSiteICP.Text = oArray[1].ToString();
                txtKeywords.Text = oArray[2].ToString();
                txtLogo.Value = oArray[3].ToString();
                txtTel.Text = oArray[4].ToString();
                txtAddress.Text = oArray[5].ToString();
            }
            #endregion
        }
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        string img = Request.Files["fuLogo"].FileName;
        if (img.IndexOf('.') < 0)
        {
            img = Request.Form["txtLogo"];
        }
        else
        {
            //if (!ud.MakeThumbImg(fuLogo.PostedFile, "Logo", ref img))
            //{
            //    MessageBox.Show(Page, "上传图片格式错误！");
            //    return;
            //}
        }

        string[] sArray = WebControl.GetFormValues(Context, "txtSiteName|txtSiteICP|txtKeywords|" + img + "|txtTel|txtAddress", '|');

        if (new StoredProcedure().C_Config_Operate(sArray))
        {
            MessageBox.Show(Page, "修改成功！");
        }
        else
        {
            MessageBox.Show(Page, "修改失败！");
        }
    }
}
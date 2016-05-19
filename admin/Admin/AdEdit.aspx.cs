using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "115", id, lbTitle);
            new Upload("Ad").IniImgConfig(ImgConfig);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[7];
        if (dp.C_LoadArrayData("select A_ColumnID,A_Title,A_Content,A_Type,A_Img,A_IsShow,A_SortID from A_Ad where ID=" + id.ToString(), ref oArray) == "1000")
        {
            ddlColumnVal.Value = oArray[0].ToString();
            txtTitle.Text = oArray[1].ToString();
            content.Text = oArray[2].ToString();
            rblAdType.SelectedValue = oArray[3].ToString();
            txtAlbum.Value = oArray[4].ToString();
            rblIsShow.SelectedValue = Common.SqlBitToByte(oArray[5].ToString());
            txtSortNum.Text = oArray[6].ToString();
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        string Img = Request.Form["f"];
        if (string.IsNullOrEmpty(Img))
        {
            Img = "Default.png";
        }

        if (new StoredProcedure().C_AdOperate(new string[] { id.ToString(),Request.Form["ddlColumn"], 
                Request.Form["txtTitle"], Request.Form["content"], Request.Form["rblAdType"],Img,
                Request.Form["rblIsShow"],Request.Form["txtSortNum"],Request.Form["txtLink"] }))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
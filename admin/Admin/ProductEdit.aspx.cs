using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProductEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "113", id, lbTitle);
            new Upload("Product").IniImgConfig(ImgConfig);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[6];
        if (dp.C_LoadArrayData("select P_ColumnID,P_Name,P_Content,P_Img,P_SortID,P_IsShow from A_Product where ID=" + id.ToString(), ref oArray) == "1000")
        {
            ddlColumnVal.Value = oArray[0].ToString();
            txtTitle.Text = oArray[1].ToString();
            content.Text = oArray[2].ToString();
            txtAlbum.Value = oArray[3].ToString();
            txtSortCode.Text = oArray[4].ToString();
            rblIsShow.SelectedValue = Common.SqlBitToByte(oArray[5].ToString());
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

        if (new StoredProcedure().C_ProductOperate(new string[] { id.ToString(), Request.Form["ddlColumn"], Request.Form["txtTitle"], Request.Form["content"], Img, Request.Form["txtSortCode"], Request.Form["rblIsShow"] }))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
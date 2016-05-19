using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NewsEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "111", id, lbTitle);
            new Upload("News").IniImgConfig(ImgConfig);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[9];
        if (dp.C_LoadArrayData("select A_ColumnID,A_Title,A_Abstract,A_Content,A_Keywords,A_IsShow,A_Link,A_SortID,A_Img from A_Article where ID=" + id.ToString(), ref oArray) == "1000")
        {
            ddlColumnVal.Value = oArray[0].ToString();
            txtTitle.Text = oArray[1].ToString();
            txtAbstract.Text = oArray[2].ToString();
            content.Text = oArray[3].ToString();
            txtKeywords.Text = oArray[4].ToString();
            rblIsShow.SelectedValue = Common.SqlBitToByte(oArray[5].ToString());
            txtLink.Text = oArray[6].ToString();
            txtSortCode.Text = oArray[7].ToString();
            txtAlbum.Value = oArray[8].ToString();
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

        if (new StoredProcedure().C_ArticleOperate2(new string[] { id.ToString(), Request.Form["ddlColumn"], 
                Request.Form["txtTitle"],Request.Form["txtAbstract"], Request.Form["content"],Authority.GetNickName(Context),Request.Form["txtKeywords"],
                Request.Form["rblIsShow"],Request.Form["txtLink"],"0",Img, Request.Form["txtSortCode"]}))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
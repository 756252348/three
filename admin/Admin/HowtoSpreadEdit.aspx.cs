using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_HowtoSpreadEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            //dp.PromissionOfEdit(Authority.GetRoleID(Context), "111", id, lbTitle);
            //new Upload("News").IniImgConfig(ImgConfig);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[9];
        if (dp.C_LoadArrayData("select A_Title,A_Abstract,A_Content,A_Keywords,A_IsShow,A_Link,A_SortID,A_Img,A_Imginside from A_Article where ID=" + id.ToString(), ref oArray) == "1000")
        {

            //txtTitle.Text = oArray[0].ToString();
            //txtAbstract.Text = oArray[1].ToString();
            content.Text = oArray[2].ToString();
            //txtKeywords.Text = oArray[3].ToString();
            //rblIsShow.SelectedValue = Common.SqlBitToByte(oArray[4].ToString());
            //txtLink.Text = oArray[5].ToString();
            //txtSortCode.Text = oArray[6].ToString();
            //txtAlbum.Value = oArray[7].ToString();
            //txtAlbum1.Value = oArray[8].ToString();
        }
        #endregion
    }


    protected void btUp_Click(object sender, EventArgs e)
    {
        string Img = Request.Form["fuValue"];
        string outImg = Request.Form["fuValue1"];
        if (string.IsNullOrEmpty(Img))
        {
            Img = "Default.png";
            outImg = "Default.png";
        }

        if (new StoredProcedure().C_ArticleOperate4(new string[] { id.ToString(),  
                "0"//Request.Form["txtTitle"]
                ,"0"//Request.Form["txtAbstract"]
                , Request.Form["content"]
                ,"0"//Authority.GetNickName(Context)
                ,"0"//Request.Form["txtKeywords"]
                ,"1"//Request.Form["rblIsShow"]
                ,"0"//Request.Form["txtLink"]
                ,"0",Img,outImg
                , "0"//Request.Form["txtSortCode"]
                ,"3"}))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
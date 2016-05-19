using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_GoodsInfoEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            //dp.PromissionOfEdit(Authority.GetRoleID(Context), "118", id, lbTitle);
            new Upload("Product").IniImgConfig(ImgConfig);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[8];
        if (dp.C_LoadArrayData("select G_Name,G_Intro,G_Img,G_Imggood,G_ImgIntro,G_Type,G_Num,G_Price from G_Goods where ID=" + id.ToString(), ref oArray) == "1000")
        {
            
            name.Text = oArray[0].ToString();
            intro.Text = oArray[1].ToString();
            
            
            txtAlbum.Value = oArray[2].ToString();
            txtAlbum1.Value = oArray[3].ToString();
            content.Text = oArray[4].ToString();
            rblIsShow.SelectedValue = oArray[5].ToString();
            num.Text = oArray[6].ToString();
            price.Text = oArray[7].ToString();
            
           
        }
        #endregion
    }


    protected void btUp_Click(object sender, EventArgs e)
    {
        string Img = Request.Form["fuValue"];
        string outImg=Request.Form["fuValue1"];
        if (string.IsNullOrEmpty(Img))
        {
            Img = "Default.png";
            outImg = "Default.png";
        }

        if (new StoredProcedure().C_GoodsOperate(new string[] { Authority.GetAdminID(Context) ,id.ToString(), 
                Request.Form["name"],Request.Form["intro"],Img,outImg, Request.Form["content"],
                Request.Form["rblIsShow"],Request.Form["price"],Request.Form["num"]}))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_IntroductionEdit : System.Web.UI.Page
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
        object[] oArray = new object[8];
        if (dp.C_LoadArrayData("select C_Keywords,C_Welcome from S_ConfigInfo where ID=" + id.ToString(), ref oArray) == "1000")
        {

            //txtTitle.Text = oArray[0].ToString();
            TextBox1.Text = oArray[1].ToString();
            //txtAbstract.Text = oArray[1].ToString();
            //content.Text = oArray[2].ToString();
            //txtKeywords.Text = oArray[3].ToString();
            //rblIsShow.SelectedValue = Common.SqlBitToByte(oArray[4].ToString());
            //txtLink.Text = oArray[5].ToString();
            //txtSortCode.Text = oArray[6].ToString();
            //txtAlbum.Value = oArray[7].ToString();
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

        if (new StoredProcedure().C_ConfigInfoOperate(new string[] { id.ToString(),"0",
                Request.Form["TextBox1"]
                ,"0"//Request.Form["txtAbstract"]
                //Request.Form["txtKeywords"]
                ,"0"//Authority.GetNickName(Context)
                ,"0"//Request.Form["txtKeywords"]
                ,"0"//Request.Form["rblIsShow"]
               }))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FriendLinkEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    Upload ud = new Upload("Link");
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "114", id, lbTitle);
            ud.IniImgConfig(ImgConfig);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[5];
        if (dp.C_LoadArrayData("select L_Title,L_Url,L_SortID,L_Img,L_IsShow from A_FirendLink where ID=" + id.ToString(), ref oArray) == "1000")
        {
            txtTitle.Text = oArray[0].ToString();
            txtUrl.Text = oArray[1].ToString();
            txtSortID.Text = oArray[2].ToString();
            ImgValue.Text = oArray[3].ToString();
            rblIsShow.SelectedValue = Common.SqlBitToByte(oArray[4].ToString());
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        string  img = Request.Files["fuImg"].FileName;
        if (string.IsNullOrEmpty(img))
        {
            img = Request.Form["ImgValue"];
        }
        else
        {
            if (!ud.MakeThumbImg(fuImg.PostedFile, ref img))
            {
                MessageBox.Show(Page, "上传图片格式错误！");
                return;
            }
        }

        if (new StoredProcedure().C_FriendLinkOperate(new string[] { id.ToString(),Request.Form["txtTitle"], 
                Request.Form["txtUrl"], Request.Form["txtSortID"], img,Request.Form["rblIsShow"] }))
        {
            MessageBox.ReLocation(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
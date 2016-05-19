using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RecruitmentEdit : System.Web.UI.Page
{
    public int id = 0;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Common.Q_Int("ID", 0);
        if (!IsPostBack)
        {
            dp.PromissionOfEdit(Authority.GetRoleID(Context), "116", id, lbTitle);
            if (id > 0)
                DataInfo();
        }
    }

    protected void DataInfo()
    {
        #region 数据绑定
        object[] oArray = new object[11];
        if (dp.C_LoadArrayData("select A_JobName,A_Department,A_Number,A_Sex,A_Age,A_WorkArea,A_Experience,A_Education,A_ValidDate,A_Content,A_IsShow from A_Recruitment where RecruitmentID=" + id.ToString(), ref oArray) == "1000")
        {
            txtName.Text = oArray[0].ToString();
            txtDpt.Text = oArray[1].ToString();
            txtNumber.Value = oArray[2].ToString();
            txtSex.SelectedValue = oArray[3].ToString();
            txtAge.Text = oArray[4].ToString();
            txtWorkArea.Text = oArray[5].ToString();
            txtExperience.Text = oArray[6].ToString();
            txtEdu.Text = oArray[7].ToString();
            txtValDate.Text = oArray[8].ToString();
            content.Value = oArray[9].ToString();
            rblIsShow.SelectedValue = Common.SqlBitToByte(oArray[10].ToString());
        }
        #endregion
    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        if (new StoredProcedure().C_RecruitmentOperate(new string[] { id.ToString(), Request.Form["txtName"], Request.Form["txtDpt"], Request.Form["txtNumber"], Request.Form["txtSex"],
            Request.Form["txtAge"],Request.Form["txtWorkArea"],Request.Form["txtExperience"],Request.Form["txtEdu"],Request.Form["txtValDate"],Request.Form["content"],Request.Form["rblIsShow"]}))
        {
            MessageBox.Show(Page, "操作成功！");
        }
        else
        {
            MessageBox.Show(Page, "操作失败！");
        }
    }
}
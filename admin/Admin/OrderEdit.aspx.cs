using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OrderEdit : System.Web.UI.Page
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
        object[] oArray = new object[12];
        if (dp.C_LoadArrayData("select O_Orderid,U_ID,U_Address,U_Uname,O_GoodsID,(case O_PayWay when 0 then '余额支付' when 1 then '微信支付' when 2 then '系统添加' when 3 then '积分支付' end)O_PayWay,O_Money,(case O_States when 0 then '未成功' when 1 then '成功' when 2 then '拒绝'  end)O_States,(case O_Type when 0 then '充值' when 1 then '提现' when 2 then '获得积分' when 3 then '使用积分' when 4 then '购买锦囊' when 5 then '余额支付' end)O_Type,(case Shipments when 0 then '未发货' when 1 then '已发货' end)Shipments,U_Orderform.AddTime ,U_Tel from U_User join U_Orderform on U_User.ID=U_Orderform.U_ID where U_Orderform.ID=" + id.ToString(), ref oArray) == "1000")
        {
            
            orderid.Text = oArray[0].ToString();
            userid.Text = oArray[1].ToString();
            Address.Text = oArray[2].ToString();
            name.Text = oArray[3].ToString();
            goodsid.Text = oArray[4].ToString();
            payway.Text = oArray[5].ToString();
            money.Text = oArray[6].ToString();
            state.Text = oArray[7].ToString();
            type.Text = oArray[8].ToString();
            shipments.Text = oArray[9].ToString();
            addtime.Text = oArray[10].ToString();
            tel.Text = oArray[11].ToString();
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

        if (new StoredProcedure().C_ShipmentsOperate(new string[] { Request.Form["userid"], Request.Form["orderid"]}))
        {
            MessageBox.ReLocation(Page, "发货成功！");
        }
        else
        {
            MessageBox.Show(Page, "已发货！");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if(new StoredProcedure().C_CloseOrderOperate(new string[] {Request.Form["orderid"]}))
        { MessageBox.Show(Page, "操作成功！"); }
        else { MessageBox.Show(Page, "操作失败！"); }
    }
}
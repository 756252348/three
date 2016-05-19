using K_ON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Userlogin_IntegrationPay : System.Web.UI.Page
{

    DataProvider dp = new DataProvider();
    CookiesGetDB ck = new CookiesGetDB();

    protected void Page_Load(object sender, EventArgs e)
    {
        string UserID = ck.GetRolesText(this.Context, 0);
        String OID = Request.QueryString["OID"];

        if (OID == "0")//如果需要创建订单
        {
            OID = dp.U_Proc_CreateBuy(new string[] { UserID, Request.QueryString["p_id"] })[0].ToString();//生成积分订单
        }

        if (OID == "1001")
        {
            submit.InnerText = "商品缺货";
            submit.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ddd");
            submit.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");
        }
        else
        {
            object[] garry = dp.C_Proc_Select(new string[] { UserID, "11", OID }, 3);
            name.InnerText = garry[1].ToString();
            topImage.Src = garry[2].ToString();

            int momey = 100;
            g_money.InnerText = garry[0].ToString();
            g_moneys.InnerText = garry[0].ToString();
            decimal a = 100 * decimal.Parse(garry[0].ToString());
            momey = Convert.ToInt32(a);
        }

        submit.Attributes.Add("data-oid", OID);

    }
}
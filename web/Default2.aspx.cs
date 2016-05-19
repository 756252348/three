using K_ON;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wxapi;

public partial class Default2 : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    WXToolsHelper wxt = new WXToolsHelper();
    CookiesGetDB ck = new CookiesGetDB();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime star = DateTime.Now;
        for (int i = 0, len = Common.C_Int(TextBox2.Text, 0); i < len; i++)
        {
            dp.U_Proc_AddUser(new string[] { TextBox1.Text, "", TextBox3.Text + i.ToString(), "" });
        }
        DateTime end = DateTime.Now;
        TimeSpan span = end.Subtract(star);
        writeData( span.Minutes + "：" + span.Seconds);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        dp.create_two("a", "as");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        DateTime sta = DateTime.Now;
        int star = Convert.ToInt32(TextBox5.Text);
        int end = Convert.ToInt32(TextBox6.Text);
        for (int i = star; i <= end; i++)
        {
            string OID = dp.U_Proc_CreateBuy(new string[] { i.ToString(), "1" })[0].ToString();//生成锦囊订单
            object[] y = dp.U_Proc_OverBuy(new string[] { i.ToString(), OID, "0" });
            writeData("U_ID:"+i+" OID:" + OID +" " + y[0].ToString() + "|" + y[1].ToString());
        }
        countTime(sta);
    }

    /// <summary>
    /// 会员充钱
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button4_Click(object sender, EventArgs e)
    {
        DateTime sta = DateTime.Now;
        string aa = TextBox5.Text;
        string bb = TextBox6.Text;
        if (bb == "")
        {
            bool b = dp.C_Operate("update U_User set U_Money=400 where id >=" + aa);
            writeData("ID:>=" + aa + " 金额充值：" + b.ToString());
        }
        else
        {
            int star = Convert.ToInt32(aa);
            int end = Convert.ToInt32(bb);
            for (int i = star; i <= end; i++)
            {
                bool b = dp.C_Operate("update U_User set U_Money=400 where id=" + i);
                writeData("ID:" + i + " 金额充值：" + b.ToString());
            }
        }
        countTime(sta);
    }

    //写入Cookies
    protected void Button5_Click(object sender, EventArgs e)
    {
        DateTime star = DateTime.Now;
        string id = TextBox8.Text;
        string cookies = TextBox7.Text;
        string s_openid="";
        if (id != "")
        {
            s_openid=dp.S_ExecuteScalar("select U_OpenID from U_User where ID=" + id);
        }
        else
        {
            if (cookies == "")
            {
                cookies = "{\"subscribe\":1,\"openid\":\"o0nBqt7mCaOHf_yq1ONOEuWTDWFI\",\"nickname\":\"测试中……\",\"sex\":0,\"language\":\"zh_CN\",\"city\":\"\",\"province\":\"\",\"country\":\"\",\"headimgurl\":\"http://wx.qlogo.cn/mmopen/YWmokF52YTkwRww7DOJZP6yn9tyY8VVxwB3cwDHLRicYAPjtFHYMQibcxnpl3GDwBdziaMtxZcyfd7XzTNplMqCa7QuqoFaRQVc/0\",\"subscribe_time\":1461578167,\"remark\":\"\",\"groupid\":0}";
            }
            s_openid = wxt.json_text("openid", cookies);
        }

        object[] user = dp.U_Proc_UserLogin(new string[] { s_openid });
        CookieAddDB.User_Login("userinfo_wx", user[2].ToString() + "|" + s_openid + "|" + user[3].ToString() + "|" + "");

        writeData(user[0].ToString()+"|"+user[1].ToString()+"|"+user[2].ToString() + "|" + s_openid + "|" + user[3].ToString() + "|" + "");
        countTime(star);
    }

    private void writeData(string data)
    {
        Label1.Text += "\r\n" + data;
    }
    
    /// <summary>
    /// 会员加200积分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button6_Click(object sender, EventArgs e)
    {
        DateTime sta = DateTime.Now;
        string aa = TextBox5.Text;
        string bb = TextBox6.Text;
        if (bb == "")
        {
            bool b = dp.C_Operate("update U_User set U_Point=U_Point+200 where id >=" + aa);
            writeData("ID:>=" + aa + " 积分充值：" + b.ToString());
        }
        else
        {
            int star = Convert.ToInt32(aa);
            int end = Convert.ToInt32(bb);
            for (int i = star; i <= end; i++)
            {
                bool b = dp.C_Operate("update U_User set U_Point=U_Point+200 where id=" + i);
                writeData("ID:" + i + " 积分充值：" + b.ToString());
            }
        }
        countTime(sta);
    }

    /// <summary>
    /// 更改附属ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button7_Click(object sender, EventArgs e)
    {
        DateTime star = DateTime.Now;
        string id = TextBox9.Text;
        bool b = dp.C_Operate("update U_User set U_ParentID = " + id + ",U_Addlevel=2 where ID<>" + id);
        dp.C_Operate("update U_User set U_ParentID = 0 ,U_Addlevel=1 where ID=" + id);
        writeData("已更改所有附属ID " + id + b.ToString());
        countTime(star);
    }

    /// <summary>
    /// 所用时间统计
    /// </summary>
    /// <param name="star"></param>
    private void countTime(DateTime star)
    {
        DateTime end = DateTime.Now;
        TimeSpan span = end.Subtract(star);
        writeData("所用时间： " + span.Minutes + "：" + span.Seconds +"." + span.Milliseconds);
    }

    /// <summary>
    /// 撤销会员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button8_Click(object sender, EventArgs e)
    {
        DateTime sta = DateTime.Now;
        string aa = TextBox5.Text;
        string bb = TextBox6.Text;
        if (bb == "")
        {
            bool b = dp.C_Operate("update U_User set U_States = 0 where ID >=" + aa);
            writeData("ID:>=" + aa + " 撤销会员：" + b.ToString());
        }
        else
        {
            int star = Convert.ToInt32(aa);
            int end = Convert.ToInt32(bb);
            for (int i = star; i <= end; i++)
            {
                bool b = dp.C_Operate("update U_User set U_States = 0 where ID=" + i);
                writeData("ID:" + i + " 撤销会员：" + b.ToString());
            }
        }
        countTime(sta);
    }
    protected void Button10_Click(object sender, EventArgs e)
    {

        DataTable os = dp.C_dataList("select U_OpenID from U_User");
        foreach (DataRow o in os.Rows)
        {

            if (o[0].ToString().Length < 28) continue;

            System.Drawing.Image bitmap = new System.Drawing.Bitmap(System.Web.HttpContext.Current.Server.MapPath("~/Member/" + o[0].ToString() + ".png"));

            //生成二维码推广图片
            System.Drawing.Image tg_bitmap = new System.Drawing.Bitmap(System.Web.HttpContext.Current.Server.MapPath("~/images/tg_AD.png"));
            System.Drawing.Graphics tg_g = System.Drawing.Graphics.FromImage(tg_bitmap);
            tg_g.DrawImage(bitmap, 315, 505, 450, 450);
            tg_bitmap.Save(System.Web.HttpContext.Current.Server.MapPath("~/Member/o-" + o[0].ToString() + ".png"));
        }
    }
}
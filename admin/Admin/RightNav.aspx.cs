using System;
using System.Data;
using System.Data.SqlClient;
//using System.Web;
//using System.Text;
//using System.Net;
//using System.IO;

public partial class Admin_RightNav : System.Web.UI.Page
{
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            string time = DateTime.Now.ToString("yyyyMMdd");
            string sql2 = "select count(*) from U_User where AddDay=" + time + "";
            a2.InnerHtml = dp.C_ExecuteScalar(sql2);
            string sql1 = "select count(*) from U_User ";
            a1.InnerHtml = dp.C_ExecuteScalar(sql1);
            
            string sql3 = "select count(*) from U_User where U_level=1 ";
            a3.InnerHtml = dp.C_ExecuteScalar(sql3);
            string sql4 = "select count(*) from U_Orderform where O_Type=4";
            a4.InnerHtml = dp.C_ExecuteScalar(sql4);
            string sql7 = "select count(*) from U_Orderform where O_Type=3";
            a7.InnerHtml = dp.C_ExecuteScalar(sql7);
            string sql8 = "select count(*) from U_Orderform where O_Type=5";
            a8.InnerHtml = dp.C_ExecuteScalar(sql8);
            //string sql5 = "select count(*) from U_Orderform where O_states=1 ";
            //a5.InnerHtml = dp.C_ExecuteScalar(sql5);
            string sql6 = "select count(*) from U_User where U_States=1 ";
            a6.InnerHtml = dp.C_ExecuteScalar(sql6);
            string sql9 = "select count(*) from U_User where U_level=2 ";
            a9.InnerHtml = dp.C_ExecuteScalar(sql9);
            string sql10 = "select count(*) from U_User where U_level=3 ";
            a10.InnerHtml = dp.C_ExecuteScalar(sql10);
            //string time = DateTime.Now.ToString("yyyyMMdd");  
            //string connstr = "Data Source=192.168.1.189;Initial Catalog=sanjifenxiao;Persist Security Info=True;User ID=cnjdsoft;Password=cnjdsoft";
            //SqlConnection sqlCon=new SqlConnection(connstr);
            //string sql2 = "select count(*) from U_User where AddDay=" + time + "";
            //SqlCommand sqlCom2 = new SqlCommand(sql2,sqlCon);
            //string sql1 = "select count(*) from U_User ";
            //SqlCommand sqlCom1 = new SqlCommand(sql1, sqlCon);
            //string sql3 = "select count(*) from U_User where U_level=1 ";
            //SqlCommand sqlCom3 = new SqlCommand(sql3, sqlCon);
            //string sql4 = "select count(*) from U_Orderform ";
            //SqlCommand sqlCom4 = new SqlCommand(sql4, sqlCon);
            //string sql5 = "select count(*) from U_Orderform where O_states=1 ";
            //SqlCommand sqlCom5 = new SqlCommand(sql5, sqlCon);
            //sqlCon.Open();
            //object t2 = sqlCom2.ExecuteScalar();
            //object t1 = sqlCom1.ExecuteScalar();
            //object t3 = sqlCom3.ExecuteScalar();
            //object t4 = sqlCom4.ExecuteScalar();
            //object t5 = sqlCom5.ExecuteScalar();
            //sqlCon.Close();
            //a2.InnerHtml = t2.ToString();
            //a1.InnerHtml = t1.ToString();
            //a3.InnerHtml = t3.ToString();
            //a4.InnerHtml = t4.ToString();
            //a5.InnerHtml = t5.ToString();
            
        }
        //    vWeather.InnerHtml = GetWeatherByCity();
    }

    //public static string GetWeatherByCity()
    //{
    //    string Html = "";       //返回来天气预报源码
    //    ASCIIEncoding encoding = new ASCIIEncoding();
    //    string weather = string.Empty;
    //    try
    //    {
    //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://i.tianqi.com/index.php?c=code&id=2&num=4");
    //        request.Method = "Get";
    //        request.ContentType = "application/x-www-form-urlencoded ";
    //        WebResponse response = request.GetResponse();
    //        Stream s = response.GetResponseStream();
    //        StreamReader sr = new StreamReader(s, System.Text.Encoding.GetEncoding("GB2312"));
    //        Html = sr.ReadToEnd();
    //        s.Close();
    //        sr.Close();
    //    }
    //    catch (Exception err)
    //    {
    //        throw new Exception(err.Message);
    //    }

    //    int count = Html.Length;
    //    int starIndex = Html.IndexOf("<div id=\"mright\" class=\"mright\">", 0, count);
    //    int endIndex = Html.IndexOf("</a></div>", starIndex);
    //    Html = Html.Substring(starIndex, endIndex - starIndex);

    //    return weather;
    //}
}
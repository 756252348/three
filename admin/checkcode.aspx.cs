using System;
using System.Collections;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class checkcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateCheckCodeImage(GetRandomCode(5));
    }

    private string GetRandomCode(int CodeCount)
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,M,N,P,Q,R,S,T,U,W,X,Y,Z";
        string[] allCharArray = allChar.Split(',');
        string RandomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < CodeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(33);
            while (temp == t)
            {
                t = rand.Next(33);
            }
            temp = t;
            RandomCode += allCharArray[t];
        }
        Session["CheckCode"] = RandomCode;
        //Response.Cookies.Add(new HttpCookie("CheckCode", RandomCode));
        return RandomCode;
    }

    private void CreateCheckCodeImage(string checkCode)
    {
        if (checkCode == null || checkCode.Trim() == String.Empty)
            return;
        Bitmap image = new Bitmap(checkCode.Length *16, 25);
        Graphics g = Graphics.FromImage(image);
        try
        {
            //生成随机生成器
            Random random = new Random();
            //清空图片背景色
            g.Clear(Color.White);
            //画图片的背景噪音线
            for (int i = 0; i < 2; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                g.DrawLine(new Pen(Color.Brown), new Point(x1, x2), new Point(y1, y2));
            }
            Font font = new System.Drawing.Font("Arial", 14, (FontStyle.Italic));
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(checkCode, font, brush, 1, 2);
            //画图片的前景噪音点
            for (int i = 0; i < 20; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 2, image.Height - 1);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            Response.ClearContent();
            Response.ContentType = "image/PNG";
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
        finally
        {
            g.Dispose();
            image.Dispose();
        }
    }
}

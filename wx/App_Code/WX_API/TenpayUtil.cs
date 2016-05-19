using System;
using System.Configuration;
using System.Text;
using System.Web;
namespace wxapi
{
	/// <summary>
	/// TenpayUtil 的摘要说明。
    /// 配置文件
	/// </summary>
	public class TenpayUtil
	{
        public static string tenpay = "1";
        public static string Mchid = ConfigurationManager.AppSettings["Mchid"].ToString();// "1230473402"; //商户号
        public static string partner = ConfigurationManager.AppSettings["Mchid"].ToString();// "1230473402";                   //合作者身份ID
        public static string key = ConfigurationManager.AppSettings["AppSecret"].ToString();//"2e46ce4f37f9a3bfddd3de3f3a23660f";  //AppSecret
        public static string appid = ConfigurationManager.AppSettings["appid"].ToString();//"wxcab2f32e59dbab59";//appid
        public static string appkey = ConfigurationManager.AppSettings["key"].ToString();// "4132AB112B778619063AB83815CEFDFB";//API密钥(非appkey) 
        public static string tenpay_notify = ConfigurationManager.AppSettings["tenpay_notify"].ToString();// "http://www.qihaocai.cn/AfterLogin/payNotifyUrl.aspx"; //支付完成后的回调处理页面,*替换成notify_url.asp所在路径

		public TenpayUtil()
		{
          
		}

        public static string getNoncestr()
        {
            Random random = new Random();
            return MD5Util.GetMD5(random.Next(1000).ToString(), "GBK");
        }

        public static string getTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
      
		/** 对字符串进行URL编码 */
		public static string UrlEncode(string instr, string charset)
		{
			//return instr;
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res; 				
				try
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;
			}
		}

		/** 对字符串进行URL解码 */
		public static string UrlDecode(string instr, string charset)
		{
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;

			}
		}
       

		/** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
		public static UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** 取随机数 */
		public static string BuildRandomStr(int length) 
		{
			Random rand = new Random();

			int num = rand.Next();

			string str = num.ToString();

			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			
			return str;
		}
       
	}
}
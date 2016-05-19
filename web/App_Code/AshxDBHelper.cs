using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web;

/// <summary>
/// AshxDBHelper 的摘要说明
/// </summary>
public class AshxDBHelper
{
    public AshxDBHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region 读取数据库表名
    public string GetDBProcName(string idx, string configstr)
    {
        return configstr.Split('|')[Convert.ToInt16(idx)];
    }
    #endregion

    #region 获取数据库连接字符串并生成连接对象
    private SqlConnection GetConn()
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["JDSITE"].ToString());
    }
    #endregion

    #region 基本输入操作存储过程原形，包括普通的用户登录、注册、新增、修改、删除等操作，返回DataSet
    public DataSet ExecBasicInProc(string[] ParamValues) 
    {
        SqlConnection sc = GetConn();
        try
        {
            SqlDataAdapter v_sda = new SqlDataAdapter();
            DataSet v_ds = new DataSet();
            string stmp = "";
            string ProcName = GetDBProcName(ParamValues[0], ConfigurationManager.AppSettings["SqlProc"]);
            for (int i = 1; i < ParamValues.Length; i++)
            {
                stmp = stmp + "'" + ParamValues[i] + "',";
            }

            using (v_sda.SelectCommand = new SqlCommand(ProcName + " " + stmp.Substring(0, stmp.Length - 1) + " ", sc))
            {
                try
                {
                    sc.Open();
                    //v_sda.SelectCommand.ExecuteScalar();
                    v_sda.Fill(v_ds);
                    return v_ds;
                }
                finally
                {
                    v_ds.Dispose();
                }
            }
        }
        finally
        {
            sc.Close(); sc.Dispose();
        }
    }
    #endregion
    #region 执行分页存储过程操作，返回查询结果记录,ParamValues，第一位为页码数
    public DataSet ExecPageInProc(string[] ParamValues)
    {        
        SqlConnection sc = GetConn();
        try
        {
            SqlDataAdapter v_sda = new SqlDataAdapter();
            DataSet v_ds = new DataSet();
            string stmp = string.Empty;
            string ProcName = GetDBProcName(ParamValues[0], ConfigurationManager.AppSettings["SqlProc"]);
            for (int i = 1; i < ParamValues.Length; i++)
            {
                stmp = stmp + "'" + ParamValues[i] + "',";
            }

            using (v_sda.SelectCommand = new SqlCommand(ProcName + " " + stmp.Substring(0, stmp.Length - 1) + " ", sc))
            {
                try
                {
                    sc.Open();
                    //v_sda.SelectCommand.ExecuteScalar();
                    v_sda.Fill(v_ds);
                    return v_ds;    
                }
                finally
                {
                    v_ds.Dispose();
                }
            }
        }
        finally
        {
            sc.Close(); sc.Dispose();
        }
    }

    #endregion
    #region 执行分页存储过程操作，返回查询结果记录, 带上统计字段值
    public DataSet QueryRecordAsDataProcWithTwoCount(string[] ParamValues, out int cntNum, out decimal cntMny)
    {

        SqlConnection sc = GetConn();
        try
        {
            SqlDataAdapter v_sda = new SqlDataAdapter();
            DataSet v_ds = new DataSet();
            string stmp = "";
            string ProcName = GetDBProcName(ParamValues[0], ConfigurationManager.AppSettings["merchantinDB"]);
            for (int i = 1; i < ParamValues.Length; i++)
            {
                stmp = stmp + "'" + ParamValues[i] + "',";
            }
            using (v_sda.SelectCommand = new SqlCommand(ProcName + " " + stmp.Substring(0, stmp.Length - 1) + " ", sc))
            {
                try
                {
                    sc.Open();

                    v_sda.SelectCommand.Parameters.Add("@countNum", SqlDbType.Int, 8);
                    v_sda.SelectCommand.Parameters["@countNum"].Direction = ParameterDirection.Output;

                    v_sda.SelectCommand.Parameters.Add("@countMny", SqlDbType.Money, 8);
                    v_sda.SelectCommand.Parameters["@countMny"].Direction = ParameterDirection.Output;

                    v_sda.SelectCommand.ExecuteScalar();

                    cntNum = (int)v_sda.SelectCommand.Parameters["@countNum"].Value;
                    cntMny = (decimal)v_sda.SelectCommand.Parameters["@countMny"].Value;

                    v_sda.Fill(v_ds);
                    return v_ds;
                }
                finally
                {
                    v_ds.Dispose();
                }
            }
        }
        finally
        {
            sc.Close(); sc.Dispose();
        }
    }

    #endregion



   

}
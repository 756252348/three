using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;

/// <summary>
/// 数据绑定
/// </summary>
public class DataProvider : DBHelper
{
    //1000操作成功，1001暂无记录，1002记录重复
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public static string connectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["JDSITE"].ToString();
        }
    }

    #region 通用获取一行数据
    /// <summary>
    /// 获取数据库中指定的一行数据，一定要指定数组长度【字段个数大于2】
    /// </summary>
    /// <param name="comandText">执行SQL语句</param>
    /// <param name="sArray">值数组</param>
    /// <returns>object数组</returns>
    public string C_LoadArrayData(string comandText, ref string json)
    {
        string result = "1001";
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, comandText, null))
        {
            if (mr.Read())
            {
                if (mr.FieldCount == 1)
                {
                    result = mr.GetValue(0).ToString();
                }
                else
                {
                    result = "1000";
                    object[] sArray = new object[mr.FieldCount];
                    mr.GetValues(sArray);
                    json = Common.ToJson(sArray);
                }
            }
            mr.Dispose();
            mr.Close();
            return result;
        }
    }

    /// <summary>
    /// 获取数据库中指定的一行数据，一定要指定数组长度
    /// </summary>
    /// <param name="comandText">执行SQL语句</param>
    /// <param name="data">返回数组</param>
    /// <returns></returns>
    public string C_LoadArrayData(string comandText, ref object[] data)
    {
        string result = "1001";
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, comandText, null))
        {
            if (mr.Read())
            {
                if (mr.FieldCount == 1)
                {
                    result = mr.GetValue(0).ToString();
                }
                else
                {
                    result = "1000";
                    mr.GetValues(data);
                }
            }
            mr.Dispose();
            mr.Close();
            return result;
        }
    }

    /// <summary>
    /// 通用以数组形式返回一行数据
    /// </summary>
    /// <param name="comandText">sql数据库语句</param>
    /// <returns></returns>
    public object[] C_LoadArrayData(string comandText)
    {
        object[] data = new object[] { };
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, comandText, null))
        {
            if (mr.Read())
            {
                mr.GetValues(data);
            }
            mr.Dispose();
            mr.Close();
            return data;
        }
    }

    /// <summary>
    /// 获取数据库中指定的一行数据，一定要指定数组长度
    /// </summary>
    /// <param name="procedureName">执行SQL语句</param>
    /// <param name="cmdParms">返回数组</param>
    /// <param name="data"></param>
    /// <returns></returns>
    public string C_LoadArrayData(string procedureName, string[] cmdParms, ref object[] data)
    {
        string result = "1001";
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, getParamString(procedureName, cmdParms), null))
        {
            if (mr.Read())
            {
                if (mr.FieldCount == 1)
                {
                    result = mr.GetValue(0).ToString();
                }
                else
                {
                    result = "1000";
                    mr.GetValues(data);
                }
            }
            mr.Dispose();
            mr.Close();
            return result;
        }
    }

    #endregion

    #region 通用获取多行数据
    /// <summary>
    /// 获取数据表(带表结构，可修改)
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataTable C_dataList(string sql)
    {
        return ExecuteDataSet(connectionString, CommandType.Text, sql, null).Tables[0];
    }

    /// <summary>
    /// 执行存储过程，返回多行数据
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <returns></returns>
    public DataTable C_CommonList(string sql)
    {
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, sql, null))
        {
            DataTable dt = new DataTable("dt");

            dt.Load(mr);
            mr.Dispose();
            mr.Close();
            return dt;
        }
    }

    /// <summary>
    /// 执行存储过程，返回多行数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    public string C_CommonList(string sql, ref string json)
    {
        string result = "1001";
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, sql, null))
        {
            json = Common.ToJson(mr, ref result);

            mr.Dispose();
            mr.Close();
            return result;
        }
    }

    /// <summary>
    /// 执行存储过程，返回多行数据
    /// </summary>
    /// <param name="procedureName">存储过程名称</param>
    /// <param name="cmdParms">参数数组</param>
    /// <param name="json"></param>
    /// <returns></returns>
    public string C_CommonList(string procedureName, string[] cmdParms, ref string json)
    {
        string result = "1001";
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, getParamString(procedureName, cmdParms), null))
        {

            json = Common.ToJson(mr, ref result);

            mr.Dispose();
            mr.Close();
            return result;
        }
    }

    /// <summary>
    /// 执行存储过程，返回多行数据
    /// </summary>
    /// <param name="procedureName">存储过程名称</param>
    /// <param name="cmdParms">参数数组</param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string C_CommonList(string procedureName, string[] cmdParms, ref DataTable dt)
    {
        string result = "1001";
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, getParamString(procedureName, cmdParms), null))
        {
            dt.Load(mr);
            if (dt.Rows.Count > 0) result = "1000";
            mr.Dispose();
            mr.Close();
            return result;
        }
    }

    /// <summary>
    /// 执行存储过程，返回多行数据
    /// </summary>
    /// <param name="procedureName">存储过程名称</param>
    /// <param name="cmdParms">参数数组</param>
    /// <param name="list"></param>
    /// <returns></returns>
    public string C_CommonList(string procedureName, string[] cmdParms, ref List<object[]> list)
    {
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, getParamString(procedureName, cmdParms), null))
        {
            bool hasData = false;
            while (mr.Read())
            {
                hasData = true;
                object[] sArray = new object[mr.FieldCount];
                mr.GetValues(sArray);
                list.Add(sArray);
            }
            mr.Dispose();
            mr.Close();
            return hasData ? "1000" : "1001";
        }
    }

    /// <summary>
    /// 执行存储过程，返回多行数据
    /// </summary>
    /// <param name="procedureName">存储过程名称</param>
    /// <param name="cmdParms">参数数组</param>
    /// <param name="list"></param>
    /// <returns></returns>
    public string C_CommonList(string sql, ref List<object[]> list)
    {
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.Text, sql, null))
        {
            bool hasData = false;
            while (mr.Read())
            {
                hasData = true;
                object[] sArray = new object[mr.FieldCount];
                mr.GetValues(sArray);
                list.Add(sArray);
            }
            mr.Dispose();
            mr.Close();
            return hasData ? "1000" : "1001";
        }
    }
    #endregion

    #region 通用获取一个字段
    /// <summary>
    /// 通用数据库操作，返回第一行第一列的值
    /// </summary>
    /// <param name="sqlString">执行SQL语句</param>
    /// <returns></returns>
    public string C_ExecuteScalar(string sql)
    {
        return S_ExecuteScalar(sql);
    }

    /// <summary>
    /// 通用数据库操作，返回第一行第一列的值
    /// </summary>
    /// <param name="porcedureName">存储过程名称</param>
    /// <param name="cmdParms">参数数组</param>
    /// <returns></returns>
    public string G_ExecuteScalar(string porcedureName, string[] cmdParms)
    {
        return S_ExecuteScalar(getParamString(porcedureName, cmdParms));
    }

    /// <summary>
    /// 通用数据库操作，返回第一行第一列的值
    /// </summary>
    /// <param name="porcedureName">存储过程名称</param>
    /// <param name="cmdParms">参数数组</param>
    /// <returns></returns>
    public bool C_ExecuteScalar(string porcedureName, string[] cmdParms)
    {
        return S_ExecuteScalar(getParamString(porcedureName, cmdParms)) == "1";
    }

    /// <summary>
    /// 通用数据库操作，返回第一行第一列的值
    /// </summary>
    /// <param name="sqlString">执行SQL语句</param>
    /// <returns></returns>
    public string S_ExecuteScalar(string sql)
    {
        object result = ExecuteScalar(connectionString, CommandType.Text, sql, null);
        return result == null ? "" : result.ToString();
    }
    #endregion

    #region 通用数据统计
    /// <summary>
    /// 统计数据DECIMAL类型字段的值
    /// </summary>
    /// <param name="sqlstr">查询语句</param>
    /// <returns></returns>
    public decimal C_Proc_Sum_Decimal(string sql)
    {
        decimal result = 0m;
        SqlParameter[] paramter = {
                                         MakeInParam("@SQL",SqlDbType.NVarChar,500,sql),
                                         MakeOutParam("@DSUM",SqlDbType.Decimal,18)
                                      };
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "C_Proc_Sum_Decimal", paramter))
        {

            result = Common.C_Decimal(paramter[1].Value.ToString());
            mr.Close();
            mr.Dispose();
        }
        return result;
    }

    /// <summary>
    /// 统计数据INT类型字段的值
    /// </summary>
    /// <param name="sqlstr">查询语句</param>
    /// <returns></returns>
    public int C_Proc_Sum_Int(string sql)
    {
        int result = 0;
        SqlParameter[] paramter = {
                                         MakeInParam("@SQL",SqlDbType.NVarChar,500,sql),
                                         MakeOutParam("@ISUM",SqlDbType.Decimal,18)
                                      };
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "C_Proc_Sum_Int", paramter))
        {

            result = Common.S_Int(paramter[1].Value.ToString());

            mr.Dispose();
            mr.Close();

        }
        return result;
    }

    public int C_Proc_Sum_Int_Instance(string iSumExpression, string tableName, string condition)
    {
        string sql = "";
        if (string.IsNullOrEmpty(condition))
        {
            sql = string.Format("select @ISUM={0} from [{1}] ", iSumExpression, tableName);
        }
        else
        {
            sql = string.Format("select @ISUM={0} from [{1}]  where {2}", iSumExpression, tableName, condition);
        }
        return C_Proc_Sum_Int(sql);
    }

    public decimal C_Proc_Sum_Decimal_Instance(string iSumExpression, string tableName, string condition)
    {
        string sql = "";
        if (!string.IsNullOrEmpty(condition))
        {
            sql = string.Format("select @DSUM={0} from [{1}] ", iSumExpression, tableName);
        }
        else
        {
            sql = string.Format("select @DSUM={0} from [{1}] where {2}", iSumExpression, tableName, condition);
        }
        return C_Proc_Sum_Decimal(sql);
    }
    #endregion

    #region 通用数据分页
    /// <summary>
    /// 通用数据分页
    /// </summary>
    /// <param name="cmdParms">[0]当前页数，[1]每页显示记录数，[2]查询字段,[3]表名，[4]查询条件,[5]排序条件,字段名称加DESC或ASC</param>
    /// <param name="data">[0]数据列表，[1]总记录数，[2]总页数</param>
    /// <returns></returns>
    public string C_Pagination(string[] cmdParms, ref string[] data)
    {
        string result = "";
        SqlParameter[] paramter = {
                                         MakeInParam("@IntCurrPage",SqlDbType.Int,8,cmdParms[0]),
                                         MakeInParam("@IntPageSize",SqlDbType.Int,8,cmdParms[1]),
                                         MakeInParam("@strFields",SqlDbType.VarChar,1000,cmdParms[2]),
                                         MakeInParam("@strTable",SqlDbType.VarChar,1000,cmdParms[3]),
                                         MakeInParam("@strWhere",SqlDbType.VarChar,1000,cmdParms[4]),
                                         MakeInParam("@strOrder",SqlDbType.VarChar,100,cmdParms[5]),
                                         MakeOutParam("@getRecordCounts",SqlDbType.Int,8),
                                         MakeOutParam("@getPageCounts",SqlDbType.Int,8)
                                      };
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "C_Proc_Pagination", paramter))
        {
            data[0] = Common.ToJson(mr, ref result);

            if (result == "1000")
            {
                data[1] = paramter[6].Value.ToString();
                data[2] = paramter[7].Value.ToString();
            }
            mr.Dispose();
            mr.Close();

        }
        return result;
    }

    /// <summary>
    /// 通用数据分页
    /// </summary>
    /// <param name="cmdParms">[0]当前页数，[1]每页显示记录数，[2]查询字段,[3]表名，[4]查询条件,[5]排序条件,字段名称加DESC或ASC</param>
    /// <param name="pageCount">总页数</param>
    /// <param name="recordCount">总记录数</param>
    /// <returns></returns>
    public DataTable C_Pagination(string[] cmdParms, ref string pageCount, ref string recordCount)
    {
        SqlParameter[] paramter = {
                                         MakeInParam("@IntCurrPage",SqlDbType.Int,8,cmdParms[0]),
                                         MakeInParam("@IntPageSize",SqlDbType.Int,8,cmdParms[1]),
                                         MakeInParam("@strFields",SqlDbType.VarChar,1000,cmdParms[2]),
                                         MakeInParam("@strTable",SqlDbType.VarChar,1000,cmdParms[3]),
                                         MakeInParam("@strWhere",SqlDbType.VarChar,1000,cmdParms[4]),
                                         MakeInParam("@strOrder",SqlDbType.VarChar,100,cmdParms[5]),
                                         MakeOutParam("@getRecordCounts",SqlDbType.Int,8),
                                         MakeOutParam("@getPageCounts",SqlDbType.Int,8)
                                      };
        DataTable dt = new DataTable();
        using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "C_Proc_Pagination", paramter))
        {

            dt.Load(mr);

            pageCount = paramter[6].Value.ToString();
            recordCount = paramter[7].Value.ToString();

            mr.Dispose();
            mr.Close();

        }
        return dt;
    }
    #endregion

    #region 通用数据库操作
    /// <summary>
    /// 通用数据库操作，返回受影响行数
    /// </summary>
    /// <param name="sql">执行SQL语句</param>
    /// <returns></returns>
    public bool C_Operate(string sql)
    {
        return ExecuteNonQuery(connectionString, CommandType.Text, sql, null) > 0;
    }

    /// <summary>
    /// 通用数据库操作，返回受影响行数
    /// </summary>
    /// <param name="porcedureName">存储过程名称</param>
    /// <param name="cmdParms">执行存储过程</param>
    /// <returns></returns>
    public bool C_Operate(string porcedureName, string[] cmdParms)
    {
        return ExecuteNonQuery(connectionString, CommandType.Text, getParamString(porcedureName, cmdParms), null) > 0;
    }
    #endregion

    #region 将指定DataTable生成JSON数据
    /// <summary>
    /// 将指定DataTable生成JSON数据
    /// </summary>
    /// <param name="dt">指定数据表[第一列为主键，第二列为父节点标识]</param>
    /// <param name="parentId">父标识</param>
    /// <param name="parentName">父标识字段名</param>
    /// <returns></returns>
    public string dataTableToJSON(DataTable dt, string parentId, string parentName)
    {
        DataTable jsonTable = Common.SelectDataTable(dt, string.Format("{0}='{1}'", parentName, parentId));
        StringBuilder Json = new StringBuilder();
        int len = jsonTable.Rows.Count;
        if (len > 0)
        {
            Json.Append(",\"sub\":[");
            for (int i = 0; i < len; i++)
            {
                Json.Append("{");
                for (int j = 0, len1 = jsonTable.Columns.Count; j < len1; j++)
                {
                    Type type = jsonTable.Rows[i][j].GetType();
                    Json.Append("\"" + "data" + j.ToString() + "\":" + Common.StringFormat(jsonTable.Rows[i][j].ToString(), type));
                    if (j < len1 - 1)
                        Json.Append(",");

                }
                Json.Append(this.dataTableToJSON(dt, jsonTable.Rows[i][0].ToString(), parentName));
                //jsonTable.Rows.RemoveAt(i);
                //len = len - 1;
                Json.Append("}");
                if (i < len - 1)
                    Json.Append(",");

            }
            Json.Append("]");
        }
        jsonTable.Dispose();
        return Json.ToString();
    }

    ///// <summary>
    ///// 将指定的data生成.JS文件
    ///// </summary>
    ///// <param name="path">绝对路径</param>
    ///// <returns></returns>
    //public void dataToFile(string path, WebConfigType configType)
    //{
    //    string json = string.Empty;
    //    switch (configType)
    //    {
    //        case WebConfigType.Module:
    //            json = dataTableToJSON(C_CommonList("SELECT * FROM S_Module"), "0", "M_ParentID");
    //            break;
    //        case WebConfigType.Column:
    //            json = dataTableToJSON(C_CommonList("SELECT * FROM A_Column WHERE Tag='False'"), "0", "C_ParentID");
    //            break;
    //    }
    //    if (!string.IsNullOrEmpty(json))
    //    {
    //        json = Common.GetAppSettings("JSONP") + "(" + json.Substring(7) + ")";
    //    }
    //    FileUtils.WriteFileEx(path, json);
    //}

    public enum WebConfigType
    {
        Column,

        Application,

        Module
    }
    #endregion

    #region 通用查询
    /// <summary>通用查询</summary>
    /// <param name="data">[0]会员ID[1]查询代号[2]参数，竖线隔开</param>
    /// <returns>[1000]正常</returns>
    public object[] C_Proc_Select(string[] data, int i)
    {
        object[] dt = new object[i];
        SqlParameter[] parameter = {
                                        MakeInParam("@U_id",SqlDbType.Int,8,data[0]==""?"0":data[0]),
                                        MakeInParam("@I_code",SqlDbType.Int,8,data[1]==""?"0":data[1]),
                                        MakeInParam("@IWhere",SqlDbType.NVarChar,-1,data[2]==""?"":data[2])
                                        };
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "C_Proc_Select", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion

    #region 将会员的ID拼装成数组
    public static string[] GetFormValues(string uid, System.Web.HttpContext context)
    {
        int arrLen = context.Request.Params.GetValues("parms[]").Length + 1;
        string[] sArray = new string[arrLen];
        for (int i = 0; i < arrLen; i++)
        {
            if (i == 0)
            {
                sArray[0] = uid;
            }
            else
            {
                sArray[i] = context.Request.Params.GetValues("parms[]")[i - 1];
            }

        }
        return sArray;
    }
    #endregion

    #region 会员登录,,
    /// <summary>会员登录,,</summary>
    /// <param name="data">[0]</param>
    /// <returns>[1000]正常</returns>
    public object[] U_Proc_UserLogin(string[] data)
    {
        object[] dt = new object[4];
        SqlParameter[] parameter = {
										MakeInParam("@OpenID",SqlDbType.VarChar,50,data[0]==""?"":data[0])
										};
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_UserLogin", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion
    #region 用户注册,,
    /// <summary>用户注册,,</summary>
    /// <param name="data">[0][1][2][3]</param>
    /// <returns>[1000]正常</returns>
    public object[] U_Proc_AddUser(string[] data)
	{
	object[] dt=new object[3];
	SqlParameter[] parameter = {
										MakeInParam("@U_ParentID",SqlDbType.Int,8,data[0]==""?"0":data[0]),
										MakeInParam("@U_OpenID",SqlDbType.VarChar,50,data[2]==""?"":data[2]),
										MakeInParam("@U_Name",SqlDbType.NVarChar,50,data[1]==""?"":data[1]),
										MakeInParam("@U_2DCode",SqlDbType.VarChar,200,data[3]==""?"":data[3])
										};
	SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_AddUser", parameter);
	mr.Read();
	mr.GetValues(dt);
    mr.Dispose();
    mr.Close();
	return dt;
}
    #endregion
    #region 修改,,
    /// <summary>修改,,</summary>
    /// <param name="data">[0]ID[1]头像[2]昵称[3]</param>
    /// <returns>[1000]正常</returns>
    public object[] U_Proc_UserChange(string[] data)
    {
        object[] dt = new object[2];
        SqlParameter[] parameter = {
										MakeInParam("@IOpenID",SqlDbType.NVarChar,100,data[0]==""?"":data[0]),
										MakeInParam("@IImg",SqlDbType.NVarChar,-1,data[1]==""?"":data[1]),
										MakeInParam("@Iname",SqlDbType.NVarChar,100,data[2]==""?"":data[2]),
										MakeInParam("@IJson",SqlDbType.NVarChar,-1,data[3]==""?"":data[3])
										};
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_UserChange", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion


    #region 二维码
    /// <summary>二维码</summary>
    /// <param name="data">[0]会员ID[1]二维码</param>
    /// <returns>[1000]正常</returns>
    public object[] M_M_Membererwei(string[] data)
    {
        object[] dt = new object[2];
        SqlParameter[] parameter = {
                                        MakeInParam("@IMemberID",SqlDbType.Int,8,data[0]==""?"0":data[0]),
                                        MakeInParam("@IErWei",SqlDbType.NVarChar,100,data[1]==""?"":data[1])
                                        };
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "M_M_Membererwei", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion
    #region 下单
    /// <summary>下单</summary>
    /// <param name="data">[0]会员ID[1]商品ID[2]数量</param>
    /// <returns>[1000]正常</returns>
    public object[] O_M_Orderjoin(string[] data)
    {
        object[] dt = new object[3];
        SqlParameter[] parameter = {
                                        MakeInParam("@IMemberID",SqlDbType.Int,8,data[0]==""?"0":data[0]),
                                        MakeInParam("@IGoodsID",SqlDbType.Int,8,data[1]==""?"0":data[1]),

                                        };
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "O_M_Orderjoin", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion
    #region 支付成功
    /// <summary>支付成功</summary>
    /// <param name="data">[0]订单号[1]0无1微信2余额</param>
    /// <returns>[1000]正常</returns>
    public object[] O_M_Orderpay(string[] data)
    {
        object[] dt = new object[5];
        SqlParameter[] parameter = {
                                        MakeInParam("@ICode",SqlDbType.VarChar,50,data[0]==""?"":data[0]),
                                        MakeInParam("@IPay",SqlDbType.Int,8,data[1]==""?"0":data[1])
                                        };
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "O_M_Orderpay", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion
    #region 二维码
    /// <summary>
    ///  二维码
    /// </summary>
    /// <param name="code">需要转换成的网址参数</param>
    /// <param name="filename">文件名</param>
    public void create_two(string code, string filename)
    {
        Bitmap bt;

        string enCodeString = code;

        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //System.Drawing.Image logoImg = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("~/images/icon_code.jpg"));
        //System.Drawing.Graphics g1 = System.Drawing.Graphics.FromImage(logoImg);
        bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);

        //filename = DateTime.Now.ToString("yyyymmddhhmmss");

        System.Drawing.Image bitmap = new System.Drawing.Bitmap(240, 240);
       
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        g.Clear(System.Drawing.Color.White);
        g.DrawImage(bt, 15, 15, 210, 210);
        //g1.DrawImage(bitmap, 67, 296, 220, 220);
        // g.DrawImage(logoImg, 140 - 40, 140 - 40, 80, 80);
        bitmap.Save(System.Web.HttpContext.Current.Server.MapPath("~/Member/" + filename + ".png"));

        //this.Image1.ImageUrl = "~/image/" + filename + ".jpg";

        //生成二维码推广图片
        System.Drawing.Image tg_bitmap = new System.Drawing.Bitmap(System.Web.HttpContext.Current.Server.MapPath("~/images/tg_AD.png"));
        System.Drawing.Graphics tg_g = System.Drawing.Graphics.FromImage(tg_bitmap);
        tg_g.DrawImage(bitmap, 315, 505, 450, 450);
        tg_bitmap.Save(System.Web.HttpContext.Current.Server.MapPath("~/Member/o-" + filename + ".png"));
    }


    #endregion
    #region 添加修改发货地址,,
	/// <summary>添加修改发货地址,,</summary>
	/// <param name="data">[0][1][2][3]</param>
	/// <returns>[1000]正常</returns>
	public object[] U_Proc_OperateAddress(string[] data)
	{
	    object[] dt=new object[2];
	    SqlParameter[] parameter = {
										    MakeInParam("@U_id",SqlDbType.Int,8,data[0]==""?"0":data[0]),
										    MakeInParam("@name",SqlDbType.NVarChar,50,data[1]==""?"":data[1]),
										    MakeInParam("@tel",SqlDbType.VarChar,30,data[2]==""?"":data[2]),
										    MakeInParam("@address",SqlDbType.NVarChar,100,data[3]==""?"":data[3])
										    };
	    SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_OperateAddress", parameter);
	    mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
	    return dt;
    }
    #endregion
    #region 提现管理,,
    /// <summary>提现管理</summary>
    /// <param name="data">[0]提现人ID[1]提现金额</param>
    /// <returns>[1000]正常</returns>
    public object[] U_Proc_CreateWithdrawMoney(string[] data)
    {
        object[] dt = new object[3];
        SqlParameter[] parameter = {
                                        MakeInParam("@id",SqlDbType.Int,8,data[0]==""?"0":data[0]),
                                        MakeInParam("@money",SqlDbType.Decimal,10,data[1]==""?"0":data[1])
                                        };
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_CreateWithdrawMoney", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion

    #region 取消订单
    /// <summary>取消订单</summary>
    /// <param name="data">[0]会员ID[1]订单号</param>
    /// <returns>[1000]正常</returns>
    public object[] O_M_Orderquxiao(string[] data)
    {
        object[] dt = new object[2];
        SqlParameter[] parameter = {
                                        MakeInParam("@IMemberID",SqlDbType.Int,8,data[0]==""?"0":data[0]),
                                        MakeInParam("@ICode",SqlDbType.VarChar,50,data[1]==""?"":data[1])
                                        };
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "O_M_Orderquxiao", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion

    #region 购买商品（锦囊）的订单创建,,
    /// <summary>购买商品（锦囊）的订单创建,,</summary>
    /// <param name="data">[0]用户ID[1]商品ID[2]商品数量[3]是否加入购物车(0|1)</param>
    /// <returns>[1000]'创建订单成功',[1001]'购买失败，商品缺货'</returns>
    public object[] U_Proc_CreateBuy(string[] data)
    {
        object[] dt = new object[3];
        SqlParameter[] parameter = {
										MakeInParam("@U_id",SqlDbType.Int,8,data[0]==""?"0":data[0]),
										MakeInParam("@G_id",SqlDbType.Int,8,data[1]==""?"0":data[1]),
										MakeInParam("@G_num",SqlDbType.Int,8,data[2]==""?"0":data[2]),
										MakeInParam("@P_type",SqlDbType.Int,8,data[3]==""?"0":data[3]),
										};
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_CreateBuy", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion

    #region 完成订单,,
    /// <summary>完成订单,,</summary>
    /// <param name="data">[0][1][2]余额支付0，微信支付1，系统添加2，积分支付3</param>
    /// <returns>[1000]正常</returns>
    public object[] U_Proc_OverBuy(string[] data)
    {
        object[] dt = new object[2];
        SqlParameter[] parameter = {
										MakeInParam("@id",SqlDbType.Int,8,data[0]==""?"0":data[0]),
										MakeInParam("@ordercode",SqlDbType.VarChar,50,data[1]==""?"":data[1]),
										MakeInParam("@payway",SqlDbType.TinyInt,8,data[2]==""?"0":data[2])
										};
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_OverBuy", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion

    #region 取消积分订单,,
    /// <summary>取消积分订单,,</summary>
    /// <param name="data">[0][1]</param>
    /// <returns>[1000]正常</returns>
    public object[] U_Proc_CloseOrder(string[] data)
    {
        object[] dt = new object[2];
        SqlParameter[] parameter = {
										MakeInParam("@U_id",SqlDbType.Int,8,data[0]==""?"0":data[0]),
										MakeInParam("@ordercode",SqlDbType.VarChar,50,data[1]==""?"":data[1])
										};
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_CloseOrder", parameter);
        mr.Read();
        mr.GetValues(dt);
        mr.Dispose();
        mr.Close();
        return dt;
    }
    #endregion

}

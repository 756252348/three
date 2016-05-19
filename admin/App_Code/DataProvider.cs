using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

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
        object result = null;
        result = ExecuteScalar(connectionString, CommandType.Text, sql, null);
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
                                         MakeInParam("@strTable",SqlDbType.VarChar,50,cmdParms[3]),
                                         MakeInParam("@strWhere",SqlDbType.VarChar,1000,cmdParms[4]),
                                         MakeInParam("@strOrder",SqlDbType.VarChar,50,cmdParms[5]),
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
                                         MakeInParam("@strTable",SqlDbType.VarChar,50,cmdParms[3]),
                                         MakeInParam("@strWhere",SqlDbType.VarChar,1000,cmdParms[4]),
                                         MakeInParam("@strOrder",SqlDbType.VarChar,50,cmdParms[5]),
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

    #region 通用数据更新重复判断
    /// <summary>
    /// 通用数据更新重复判断
    /// </summary>
    /// <param name="cmdParms"></param>
    /// <returns></returns>
    public bool C_Update_IsRepeat(string[] cmdParms)
    {
        return ExecuteScalar(connectionString, CommandType.Text, getParamString("C_Proc_Update_IsRepeat", cmdParms), null).ToString() == "0";
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

    #region 通用数据插入重复判断
    /// <summary>
    /// 通用数据插入重复判断,true表示重复，false表示不重复
    /// </summary>
    /// <param name="cmdParms">参数[0]表名，[1]字段名称,[2]字段值</param>
    /// <returns></returns>
    public bool C_Insert_IsRepeat(string[] cmdParms)
    {
        return !(S_ExecuteScalar(getParamString("C_Proc_Insert_IsRepeat", cmdParms)) == "0");
    }
    #endregion

    #region 管理员登陆
    /// <summary>
    /// 用户登陆
    /// </summary>
    /// <param name="cmdParms">[0]用户名称，[1]登陆密码</param>
    /// <returns></returns>
    public string AdminLogin(string[] cmdParms, ref object[] sArray)
    {
        return this.C_LoadArrayData("S_Proc_AdminLogin", cmdParms, ref sArray);
    }
    #endregion

    #region 权限判断
    /// <summary>
    /// 获取权限列表
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <returns></returns>
    public DataTable GetPromissionList(string RoleID)
    {
        DataTable dt = (DataTable)CacheHelper.GetCache(Common.SessionPrefix + "PromissionList");
        if (!Common.DataTableIsNull(dt))
        {
            dt = this.C_CommonList("EXEC S_Proc_GetRolePermission " + RoleID);
            CacheHelper.SetCache(Common.SessionPrefix + "PromissionList", dt);


            CacheHelper.SetCache(Common.SessionPrefix + "ApplicationList", this.C_CommonList("SELECT *  FROM S_Application"));

        }
        return dt;
    }

    /// <summary>
    /// 根据应用标识获取应用的名称和标识代码
    /// </summary>
    /// <param name="ApplictionID">应用ID</param>
    /// <returns></returns>
    public string[] GetApplicion(string ApplictionID)
    {
        string[] sArray = new string[2];
        DataTable dt = (DataTable)CacheHelper.GetCache(Common.SessionPrefix + "ApplicationList");
        int len = dt.Rows.Count;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                if (dt.Rows[i][0].ToString() == ApplictionID)
                {
                    sArray[0] = dt.Rows[i][1].ToString();
                    sArray[1] = dt.Rows[i][2].ToString();
                    break;
                }
            }
        }
        dt.Dispose();
        return sArray;
    }

    /// <summary>
    /// 获取某模块下的权限
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleID">模块标识</param>
    /// <returns></returns>
    public string GetPromissionApplication(string RoleID, string ModuleID, ref object[] oArray)
    {
        DataTable dt = Common.SelectDataTable(GetPromissionList(RoleID), " ID=" + ModuleID);
        if (Common.DataTableIsNull(dt))
        {
            for (int i = 0, len = dt.Columns.Count; i < len; i++)
            {
                if (i < 5)
                {
                    if (i > 1 && i < 5)
                    {
                        oArray[i - 2] = dt.Rows[0][i].ToString();
                    }
                    continue;
                }
                else
                {
                    break;
                }
            }
            dt.Dispose();
            return "1000";
        }
        dt.Dispose();
        return "1001";
    }

    /// <summary>
    /// 判断当前页面的权限
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleCode">页面名称</param>
    /// <param name="Appcation">操作标识</param>
    /// <returns></returns>
    public bool GetRolePromission(string RoleID, string ModuleID, string Appcation)
    {
        return IsPromission(RoleID, ModuleID, Appcation, null);
    }

    /// <summary>
    /// 判断当前页面的权限（含定位标题）
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleCode">页面名称</param>
    /// <param name="Appcation">操作标识</param>
    /// <returns></returns>
    public bool GetRolePromission(string RoleID, string ModuleID, string Appcation, System.Web.UI.WebControls.Label label)
    {
        return IsPromission(RoleID, ModuleID, Appcation, label);
    }

    /// <summary>
    /// 判断当前页面的权限
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleID">模块标识</param>
    /// <param name="Appcation">应用标识</param>
    /// <param name="label">Label控件</param>
    /// <returns></returns>
    public bool IsPromission(string RoleID, string ModuleID, string Appcation, System.Web.UI.WebControls.Label label)
    {
        bool IsPromission = false;
        object[] oArray = new object[3];
        if (this.GetPromissionApplication(RoleID, ModuleID, ref oArray) == "1000")
        {
            string[] sArray = this.GetApplicion(Appcation);
            if (label != null) label.Text = string.Format("<a href=\"DataList.aspx?ID={0}\"><input type=\"hidden\" id=\"ModuleCode\" value=\"{1}\"  data-module=\"{0}\" >{2}</a> >&nbsp;<a href=\"javascript:;\" onclick='window.location.href=window.location.href' title='点击刷新'>{3}</a>",
                                            ModuleID, oArray[2], oArray[0], sArray[1]);

            string[] _promiss = oArray[1].ToString().Split('|');
            for (int i = 0, len = _promiss.Length; i < len; i++)
            {
                if (_promiss[i] == sArray[0])
                {
                    IsPromission = true;
                    break;
                }
            }
        }
        return IsPromission;
    }

    /// <summary>
    /// 判断当前页面的权限[修改、添加]（含定位标题）
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleCode">页面名称</param>
    /// <param name="id">操作标识</param>
    /// <param name="label"></param>
    public void PromissionOfEdit(string RoleID, string ModuleID, int id, System.Web.UI.WebControls.Label label)
    {
        PromissionOfCommon(RoleID, ModuleID, (id > 0 ? "6" : "4"), label);
    }

    /// <summary>
    /// 通用，判断前用户对指定模块列表是否有权限
    /// </summary>
    /// <param name="context"></param>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleID">模块标识</param>
    /// <param name="Appcation">应用标识</param>
    /// <param name="label">Label控件</param>
    public void PromissionOfCommon(string RoleID, string ModuleID, string Appcation, System.Web.UI.WebControls.Label label)
    {
        Common._Redirect(isPromission(RoleID, ModuleID, Appcation, label));
    }

    /// <summary>
    /// 判断AJAX页面的权限，并返回权限错误代码
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleID">模块标识</param>
    /// <param name="Appcation">应用标识</param>
    /// <returns></returns>
    public string GetRolePromissionEx(string RoleID, string ModuleID, string Appcation)
    {
        return isPromission(RoleID, ModuleID, Appcation, null);
    }

    /// <summary>
    /// 判断页面权限
    /// </summary>
    /// <param name="RoleID">角色标识</param>
    /// <param name="ModuleID">模块标识</param>
    /// <param name="Appcation">应用标识</param>
    /// <param name="label">Label控件</param>
    /// <returns></returns>
    public string isPromission(string RoleID, string ModuleID, string Appcation, System.Web.UI.WebControls.Label label)
    {
        if (ModuleID == "0")
        {
            return "808";
        }
        else if (!Authority.IsLogin)
        {
            return "505";
        }
        else if (GetRolePromission(RoleID, ModuleID, Appcation, label))
        {
            return "000";
        }
        else
        {
            return "404";
        }
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

    /// <summary>
    /// 将指定的data生成.JS文件
    /// </summary>
    /// <param name="path">绝对路径</param>
    /// <returns></returns>
    public void dataToFile(string path, WebConfigType configType)
    {
        string json = string.Empty;
        switch (configType)
        {
            case WebConfigType.Module:
                json = dataTableToJSON(C_CommonList("SELECT * FROM S_Module"), "0", "M_ParentID");
                break;
            case WebConfigType.Column:
                json = dataTableToJSON(C_CommonList("SELECT * FROM A_Column WHERE Tag='False'"), "0", "C_ParentID");
                break;
        }
        if (!string.IsNullOrEmpty(json))
        {
            json = Common.GetAppSettings("JSONP") + "(" + json.Substring(7) + ")";
        }
        FileUtils.WriteFileEx(path, json);
    }

    public enum WebConfigType
    {
        Column,

        Application,

        Module
    }
    #endregion

    #region 通用数据库操作
    /// <summary>
    /// 执行增删改操作，返回受影响的行数
    /// </summary>
    /// <param name="cmd">参数化写法的Sql语句或存储过程名称</param>
    /// <param name="cmdType">执行的Sql类型</param>
    /// <param name="pms">SqlParameter参数</param>
    /// <returns></returns>
    public static int DB_ExecuteNonQuery(string cmd, CommandType cmdType, params SqlParameter[] pms)
    {
        if (pms != null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand(cmd, sqlCon))
                {
                    sqlCmd.CommandType = cmdType;
                    sqlCmd.Parameters.AddRange(pms);
                    sqlCon.Open();
                    return sqlCmd.ExecuteNonQuery();
                }
            }
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 查询并返回第一行第一列的值
    /// </summary>
    /// <param name="cmd">参数化写法的Sql语句或存储过程名称</param>
    /// <param name="cmdType">执行的Sql类型</param>
    /// <param name="pms">SqlParameter参数</param>
    /// <returns></returns>
    public static object DB_ExecuteScalar(string cmd, CommandType cmdType, params SqlParameter[] pms)
    {
        if (pms != null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand(cmd, sqlCon))
                {
                    sqlCmd.CommandType = cmdType;
                    sqlCmd.Parameters.AddRange(pms);
                    sqlCon.Open();
                    return sqlCmd.ExecuteScalar();
                }
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 查询并返回DataReader对象，在外部手动释放
    /// </summary>
    /// <param name="cmd">参数化写法的Sql语句或存储过程名称</param>
    /// <param name="cmdType">执行的Sql类型</param>
    /// <param name="pms">SqlParameter参数</param>
    /// <returns></returns>
    public static SqlDataReader DB_ExecuteReader(string cmd, CommandType cmdType, params SqlParameter[] pms)
    {
        if (pms != null)
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            using (SqlCommand sqlCmd = new SqlCommand(cmd, sqlCon))
            {
                sqlCmd.CommandType = cmdType;
                sqlCmd.Parameters.AddRange(pms);
                sqlCon.Open();
                return sqlCmd.ExecuteReader();
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 查询并返回DataTable对象，在外部手动释放
    /// </summary>
    /// <param name="cmd">参数化写法的Sql语句或存储过程名称</param>
    /// <param name="cmdType">执行的Sql类型</param>
    /// <param name="pms">SqlParameter参数</param>
    /// <returns></returns>
    public static DataTable DB_ExecuteDataTable(string cmd, CommandType cmdType, params SqlParameter[] pms) {
        if (pms != null)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sdAdapter = new SqlDataAdapter(cmd, connectionString))
            {
                sdAdapter.SelectCommand.CommandType = cmdType;
                sdAdapter.SelectCommand.Parameters.AddRange(pms);
                sdAdapter.Fill(dt);
            }
            return dt;
        }
        else {
            return null;
        }
    }
    #endregion
}

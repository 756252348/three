using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// 数据库操作层
/// </summary>
public class DBHelper
{
    private const int COMMAND_TIMEOUT = 3600;

    #region PrepareCommand
    /// <summary>
    /// Command预设置
    /// </summary>
    /// <param name="conn">MySqlConnection对象</param>
    /// <param name="trans">MySqlTransaction对象，可为null</param>
    /// <param name="cmd">MySqlCommand对象</param>
    /// <param name="cmdType">CommandType，存储过程或命令行</param>
    /// <param name="cmdText">SQL语句或存储过程名</param>
    /// <param name="cmdParms">MySqlCommand参数数组，可为null</param>
    private static void prepareCommand(SqlConnection conn, SqlTransaction trans, SqlCommand cmd, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();

        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        cmd.CommandTimeout = COMMAND_TIMEOUT;

        if (trans != null)
            cmd.Transaction = trans;

        cmd.CommandType = cmdType;

        if (cmdParms != null)
        {
            foreach (SqlParameter parm in cmdParms)
                cmd.Parameters.Add(parm);
        }
    }
    #endregion

    #region ExecuteNonQuery
    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
    /// <param name="cmdText">SQL语句或存储过程名</param>
    /// <param name="cmdParms">MySqlCommand参数数组</param>
    /// <returns>返回受引响的记录行数</returns>
    protected static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            prepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
    }

    /// <summary>
    /// 执行事务
    /// </summary>
    /// <param name="trans">MySqlTransaction对象</param>
    /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
    /// <param name="cmdText">SQL语句或存储过程名</param>
    /// <param name="cmdParms">MySqlCommand参数数组</param>
    /// <returns>返回受引响的记录行数</returns>
    public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
        SqlCommand cmd = new SqlCommand();

        prepareCommand(trans.Connection, trans, cmd, cmdType, cmdText, cmdParms);
        int val = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return val;
    }

    #endregion

    #region ExecuteScalar
    /// <summary>
    /// 执行命令，返回第一行第一列的值
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
    /// <param name="cmdText">SQL语句或存储过程名</param>
    /// <param name="cmdParms">MySqlCommand参数数组</param>
    /// <returns>返回Object对象</returns>
    protected static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            prepareCommand(connection, null, cmd, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
    }

    #endregion

    #region ExecuteReader
    /// <summary>
    /// 执行命令或存储过程，返回MySqlDataReader对象
    /// 注意MySqlDataReader对象使用完后必须Close以释放MySqlConnection资源
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
    /// <param name="cmdText">SQL语句或存储过程名</param>
    /// <param name="cmdParms">MySqlCommand参数数组</param>
    /// <returns></returns>
    protected static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(connectionString);
        try
        {
            prepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //cmd.Parameters.Clear();
            return dr;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }
    #endregion

    #region ExecuteDataSet
    /// <summary>
    /// 执行命令或存储过程，返回DataSet对象
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    /// <param name="cmdType">命令类型(存储过程或SQL语句)</param>
    /// <param name="cmdText">SQL语句或存储过程名</param>
    /// <param name="cmdParms">MySqlCommand参数数组(可为null值)</param>
    /// <returns></returns>
    protected static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            prepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            cmd.Parameters.Clear();
            return ds;
        }
    }
    #endregion

    #region Make SqlParameters
    /// <summary>
    /// 输入参数
    /// </summary>
    /// <param name="ParamName">参数名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <param name="Value">参数值</param>
    /// <returns></returns>
    protected static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
    }
    /// <summary>
    /// 输出参数
    /// </summary>
    /// <param name="ParamName">参数名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <returns></returns>
    protected static SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
    }

    /// <summary>
    /// Return值
    /// </summary>
    /// <param name="ParamName">参数名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <returns></returns>
    protected static SqlParameter MakeReturnParam(string ParamName, SqlDbType DbType, int Size)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null);
    }

    /// <summary>
    /// 函数参数(支持输入输出)
    /// </summary>
    /// <param name="ParamName">参数名称</param>
    /// <param name="DbType">参数类型</param>
    /// <param name="Size">参数大小</param>
    /// <param name="Direction">输入或输入</param>
    /// <param name="Value">参数值</param>
    /// <returns></returns>
    protected static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
    {
        SqlParameter param;
        if (Size > 0)
            param = new SqlParameter(ParamName, DbType, Size);
        else
            param = new SqlParameter(ParamName, DbType);
        param.Direction = Direction;

        if (!((Direction == ParameterDirection.Output || Direction == ParameterDirection.ReturnValue) && Value == null))
            param.Value = Value;
        return param;
    }
    #endregion

    #region SqlBulkCopy
    /// <summary>
    /// 批量复制
    /// </summary>
    /// <param name="connectionString">目标表的数据库连接字符串</param>
    /// <param name="sourceTable">原表</param>
    /// <param name="destinationTable">目标表名（数据库中表名）</param>
    public static void ExecuteBulkCopy(string connectionString, DataTable sourceTable, string destinationTable)
    {
        using (SqlConnection sqlconn = new SqlConnection(connectionString))
        {
            sqlconn.Open();
            using (SqlBulkCopy sbc = new SqlBulkCopy(sqlconn))
            {
                //设置一批，写入多少条记录
                sbc.BatchSize = 1000;

                ////设置超时秒数
                sbc.BulkCopyTimeout = 180;

                //设置 NotifyAfter 属性，以便在每拷贝 10000 条记录至数据表后，呼叫事件处理函数
                sbc.NotifyAfter = 10000;

                //设置要写入的数据库的表名
                sbc.DestinationTableName = destinationTable;

                //将数据集合和目标服务器库表中的字段对应   
                for (int i = 0; i < sourceTable.Columns.Count; i++)
                {
                    //列映射定义数据源中的列和目标表中的列之间的关系
                    sbc.ColumnMappings.Add(sourceTable.Columns[i].ColumnName, sourceTable.Columns[i].ColumnName);
                }

                sbc.WriteToServer(sourceTable);
            }
            sqlconn.Dispose();
            sqlconn.Close();
        }
    }

    /// <summary>
    /// 批量复制
    /// </summary>
    /// <param name="trans">事务</param>
    /// <param name="sourceTable">数据源</param>
    /// <param name="destinationTable">目标表名（数据库中表名）</param>
    public static void ExecuteBulkCopy(SqlTransaction trans, DataTable sourceTable, string destinationTable)
    {
        using (SqlBulkCopy sbc = new SqlBulkCopy(trans.Connection, SqlBulkCopyOptions.KeepIdentity, trans))
        {
            sbc.BatchSize = 1000;
            sbc.BulkCopyTimeout = 180;

            //将DataTable表名作为待导入库中的目标表名   
            sbc.DestinationTableName = destinationTable;

            //将数据集合和目标服务器库表中的字段对应   
            for (int i = 0; i < sourceTable.Columns.Count; i++)
            {
                //列映射定义数据源中的列和目标表中的列之间的关系
                sbc.ColumnMappings.Add(sourceTable.Columns[i].ColumnName, sourceTable.Columns[i].ColumnName);
            }

            sbc.WriteToServer(sourceTable);
        }
    }

    /// <summary>
    /// 天天商品的发行
    /// </summary>
    /// <param name="connString"></param>
    /// <param name="table">B_LuckInfo</param>
    /// <returns></returns>
    public static bool ExecuteBulkCopy(string connectionString, DataTable sourceTable)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter();

            
            

            adapter.UpdateCommand.Parameters.Add("@TableName", SqlDbType.VarChar, 50, sourceTable.TableName);
            adapter.InsertCommand.Parameters.Add("@TableName", SqlDbType.VarChar, 50, sourceTable.TableName);
            adapter.DeleteCommand.Parameters.Add("@TableName", SqlDbType.VarChar, 50, sourceTable.TableName);

            int len = sourceTable.Columns.Count;
            string[] sArray = new string[len];
            for (int i = 0; i < len; i++)
            {
                sArray[i] = sourceTable.Columns[i].ColumnName;

                adapter.UpdateCommand.Parameters.Add("@" + sArray[i],
                    ConvertType(sourceTable.Columns[i].DataType), 
                    sourceTable.Columns[i].MaxLength,
                    sArray[i]);

                adapter.InsertCommand.Parameters.Add("@" + sArray[i],
                    ConvertType(sourceTable.Columns[i].DataType),
                    sourceTable.Columns[i].MaxLength,
                    sArray[i]);

                
            }
            string SqlStr = string.Format("Update {0}  SET {1} Where {2}={3}", sourceTable.TableName,MakeParamter(sArray) ,sArray[0],"@" + sArray[0]);


            adapter.UpdateCommand = new SqlCommand(SqlStr, conn);
            adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;

            adapter.InsertCommand = new SqlCommand(string.Format("INSERT INTO {0}({1}) VALUES({2})", sourceTable.TableName, "@" + string.Join(",@", sArray)), conn);


            adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

            adapter.DeleteCommand = new SqlCommand("DELETE FROM @TableName Where ID=@ID", conn);
            adapter.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 32, "ID");

            adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;

            adapter.UpdateBatchSize = 1;
            //SqlCommandBuilder scb = new SqlCommandBuilder(adapter);
            //进行更新操作，table是数据源表，里面的数据能符合上面参数的映射关系即可
            adapter.Update(sourceTable);
        }
        return true;
    }

    static string MakeParamter(string[] sArray)
    {
        string result = "";
        for (int i = 0, len = sArray.Length; i < len; i++)
        {
            result += sArray[i] + "=@" + sArray[i];
        }
        return result.TrimEnd(',') + " ";
    }
    #endregion

    #region Make SqlStatement
    /// <summary>
    /// 执行不带输出参数的存储过程（慎用，容易造成SQL注入）
    /// </summary>
    /// <param name="porcedureName">存储过程名称</param>
    /// <param name="sArray">参数值</param>
    /// <returns>执行语句</returns>
    protected static string getParamString(string porcedureName, string[] sArray)
    {
        string result = "";
        for (int i = 0, len = sArray.Length; i < len; i++)
        {
            result += "'" + Common.FilterSql(sArray[i])+ "',";
        }
        return porcedureName + " " + result.TrimEnd(',') + " ";
    }
    #endregion


    static SqlDbType ConvertType(Type t) 
    {
        switch (Type.GetTypeCode(t)) 
        {
            case TypeCode.Boolean:
                return SqlDbType.Bit;
            case TypeCode.Byte:
                return SqlDbType.TinyInt;
            case TypeCode.DateTime:
                return SqlDbType.DateTime;
            case TypeCode.Decimal:
                return SqlDbType.Decimal;
            case TypeCode.Double:
                return SqlDbType.Float;
            case TypeCode.Int16:
                return SqlDbType.SmallInt;
            case TypeCode.Int32:
                return SqlDbType.Int;
            case TypeCode.Int64:
                return SqlDbType.BigInt;
            case TypeCode.SByte:
                return SqlDbType.TinyInt; 
            case TypeCode.Single:
                return SqlDbType.Real; 
            case TypeCode.String:
                return SqlDbType.NVarChar;
            case TypeCode.UInt16:
                return SqlDbType.SmallInt; 
            case TypeCode.UInt32:
                return SqlDbType.Int; 
            case TypeCode.UInt64:
                return SqlDbType.BigInt;
            case TypeCode.Object:
                return SqlDbType.Variant;
            default:
            if (t == typeof(byte[])) {
                return SqlDbType.Binary;
            }   
            return SqlDbType.Variant; 
        }
    } 
}
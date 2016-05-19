using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// StoredProcedure 的摘要说明
/// </summary>
namespace DotNet.Utilities
{
    public class ComSqlProc : DataProvider
    {
        #region 通用数据库删除
        /// <summary>
        /// 通用数据库操作，返回受影响行数
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="IDs">标识集合，‘,’分割</param>
        /// <returns></returns>
        public object[] C_Operate_Delete(string TableName, string IDs)
        {
            string sqlcmd = "DELETE FROM @ITABLENAME WHERE ID IN ( @IIDs )";
            SqlParameter[] parameter = { 
                                        MakeInParam("@ITABLENAME",SqlDbType.VarChar,20,TableName),
                                        MakeInParam("@IIDs",SqlDbType.VarChar,100,IDs)
                                   };
            object[] o = null;
            if (ExecuteNonQuery(connectionString, CommandType.Text, sqlcmd, parameter) > 0)
            {
                o = new object[] { "1000", "删除成功" };
            }
            else
            {
                o = new object[] { "1001", "删除失败" };
            }
            return o;
        }
        #endregion

        #region 通用数据库软删除
        /// <summary>
        /// 通用数据库操作，返回受影响行数
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="RowID">标识集合，‘,’分割</param>
        /// <returns></returns>
        public object[] C_Operate_TagDelete(string TableName, string RowID)
        {
            string sqlcmd = "EXEC('UPDATE '+ @ITABLENAME+' SET Mark=1 WHERE ID IN ('+@IID+')')";
            SqlParameter[] parameter = { 
                                        MakeInParam("@ITABLENAME",SqlDbType.VarChar,20,TableName),
                                        MakeInParam("@IID",SqlDbType.VarChar,-1,RowID)
                                   };
            object[] o = null;
            if (ExecuteNonQuery(connectionString, CommandType.Text, sqlcmd, parameter) > 0)
            {
                o = new object[] { "1000", "删除成功" };
            }
            else
            {
                o = new object[] { "1001", "删除失败" };
            }
            return o;
        }
        #endregion

        #region 判断插入数据是否重复
        /// <summary>判断插入数据是否重复</summary>
        /// <param name="data">[0]表名[1]字段名[2]字段值</param>
        /// <returns>True重复，False不重复</returns>
        public bool C_Proc_Insert_IsRepeat(string[] data)
        {
            object[] dt = new object[1];
            SqlParameter[] parameter = {
										MakeInParam("@ITABLENAME",SqlDbType.VarChar,50,data[0]==""?"":data[0]),
										MakeInParam("@IFIELDNAME",SqlDbType.VarChar,20,data[1]==""?"":data[1]),
										MakeInParam("@IFIELDVALUE",SqlDbType.NVarChar,50,data[2]==""?"":data[2])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "C_Proc_Insert_IsRepeat", parameter);
            mr.Read();
            mr.GetValues(dt);
            return dt[0].ToString() != "0";
        }
        #endregion

      

        #region 短信验证码添加以及会员号添加
        /// <summary>短信验证码添加以及会员添加</summary>
        /// <param name="data">[0]OpenID[1]电话号码[2]请求IP[3]验证码</param>
        /// <returns>[1000]正常</returns>
        public object[] U_AddTel(string[] data)
        {
            object[] dt = new object[2];
            SqlParameter[] parameter = {
										MakeInParam("@IOpenID",SqlDbType.Int,8,data[0]),
										MakeInParam("@ITel",SqlDbType.VarChar,50,data[1]),
										MakeInParam("@IIP",SqlDbType.VarChar,50,data[2]),
										MakeInParam("@ICHECK",SqlDbType.VarChar,50,data[3])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_AddTel", parameter);
            mr.Read();
            mr.GetValues(dt);
            return dt;
        }
        #endregion

        #region 添加修改地址
        /// <summary>添加用户地址</summary>
        /// <param name="data">[0]地址ID[1]用户ID[2]经度[3]纬度[4]地图地址[5]详细地址[6]收货人[7]收货电话</param>
        /// <returns>[1000]正常[1001]未登入</returns>
        public object[] U_AddAddress(string[] cmdParms)
        {
            object[] dt = new object[2];
            SqlParameter[] parameter = {
										MakeInParam("@AddressID",SqlDbType.Int,8 ,cmdParms[0]),
                                        MakeInParam("@UserID",SqlDbType.Int,8 ,cmdParms[1]),
                                        MakeInParam("@lng",SqlDbType.VarChar ,50,cmdParms[2]),
                                        MakeInParam("@lat",SqlDbType.VarChar ,50,cmdParms[3]),
                                        MakeInParam("@MapInfo",SqlDbType.NVarChar ,200,cmdParms[4]),
                                        MakeInParam("@Address",SqlDbType.NVarChar ,200,cmdParms[5]),
                                        MakeInParam("@Receiver",SqlDbType.NVarChar ,50,cmdParms[6]),
                                        MakeInParam("@ReceverTel",SqlDbType.VarChar ,50,cmdParms[7])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_AddAddress", parameter);
            mr.Read();
            mr.GetValues(dt);
            return dt;
        }
        #endregion

        #region 添加订单
        /// <summary>添加订单</summary>
        /// <param name="data">[0]用户ID[1]商品ID,可以多个，用|分割‘商品ID,数量|商品ID，数量’[2]地址ID[3]订单备注[4]卡卷类型[5]卡券名称[6]兑换商品名[7]代金券起用金额[8]代金券抵用金额[9]打折额度,30代表打7折[10]店铺ID[11]标签ID,用','分隔[12]配送时间</param>
        /// <returns>[1000]正常</returns>
        /// 1|29,1|27||||||||11|2|立即送出，预计45分钟内到达
        public object[] U_Proc_AddOrder(string[] data)
        {
            object[] dt = new object[2];
            SqlParameter[] parameter = {
										MakeInParam("@UserID",SqlDbType.Int,8,data[0]),
										MakeInParam("@GoodsID",SqlDbType.NVarChar,500,data[1]),
										MakeInParam("@AddressID",SqlDbType.Int,8,data[2]),
										MakeInParam("@Remark",SqlDbType.NVarChar,500,data[3]),
										MakeInParam("@CardType",SqlDbType.VarChar,50,data[4]),
										MakeInParam("@CardName",SqlDbType.NVarChar,50,data[5]),
										MakeInParam("@CardGoodsName",SqlDbType.NVarChar,50,data[6]),
										MakeInParam("@StartMoney",SqlDbType.Decimal,10,data[7]==""?"0.00":data[7]),
										MakeInParam("@CutMoney",SqlDbType.Decimal,10,data[8]==""?"0.00":data[8]),
										MakeInParam("@CardPrecent",SqlDbType.Int,8,data[9]==""?"0":data[9]),
										MakeInParam("@ShopID",SqlDbType.Int,8,data[10]==""?"0":data[10]),
                                        MakeInParam("@CardID",SqlDbType.VarChar,50,data[11]),
                                        MakeInParam("@EncryptCode",SqlDbType.VarChar,50,data[12]),
                                        MakeInParam("@CardCode",SqlDbType.VarChar,100,data[13]),
										MakeInParam("@Label",SqlDbType.VarChar,50,data[14]),
										MakeInParam("@DeliverTime",SqlDbType.NVarChar,50,data[15])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_AddOrder", parameter);
            mr.Read();
            mr.GetValues(dt);
            return dt;
        }
        #endregion

        #region 修改订单状态
        public bool ChangeOrderState( string orderID) {
            string sqlcmd = "UPDATE M_Order SET O_States=0,O_PayTime=GETDATE(),O_CardState='True' WHERE O_OrderCode=@CodeID";
            SqlParameter[] parameter = {
                                           MakeInParam("@CodeID",SqlDbType.VarChar,30,orderID)
										};
            return ExecuteNonQuery(connectionString, CommandType.Text, sqlcmd, parameter) > 0;
        }
        #endregion

        #region 首页商品查询
        /// <summary>订单插入</summary>
        /// <param name="data">[0]经度[1]纬度[2]商品类型[3]第几页[4]显示多少个内容[5]总记录数[6]总页数</param>
        /// <returns>[1000]正常</returns>
        public List<object[]> M_Proc_IndexSelect(string[] cmdParms)
        {
            SqlParameter[] parameter = {
										MakeInParam("@lng",SqlDbType.VarChar ,50,cmdParms[0]),
                                        MakeInParam("@lat",SqlDbType.VarChar ,50,cmdParms[1]),
                                        MakeInParam("@Type",SqlDbType.Int,8 ,cmdParms[2]),
                                        MakeInParam("@IntCurrPage",SqlDbType.Int,8 ,cmdParms[3]),
                                        MakeInParam("@IntPageSize",SqlDbType.Int,8 ,cmdParms[4]),
                                        MakeInParam("@getRecordCounts",SqlDbType.Int,8 ,cmdParms[5]),
                                        MakeInParam("@getPageCounts",SqlDbType.Int,8 ,cmdParms[6])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "M_Proc_IndexSelect", parameter);
            List<object[]> oList = new List<object[]>();
            while (mr.Read())
            {
                object[] dt = new object[mr.FieldCount];
                mr.GetValues(dt);
                oList.Add(dt);
            }
            mr.Dispose();
            mr.Close();
            return oList;
        }
        #endregion

        #region 查询历史订单
        /// <summary>订单插入</summary>
        /// <param name="data">[0]用户ID</param>
        /// <returns>[1000]正常</returns>
        public List<object[]> U_Proc_SelectOrder(string[] cmdParms)
        {
            SqlParameter[] parameter = {
										MakeInParam("@UserID",SqlDbType.Int,8 ,cmdParms[0]),
                                        MakeInParam("@IntCurrPage",SqlDbType.Int,8 ,cmdParms[1]),
                                        MakeInParam("@IntPageSize",SqlDbType.Int,8 ,cmdParms[2]),
                                        MakeInParam("@getRecordCounts",SqlDbType.Int,8 ,cmdParms[3]),
                                        MakeInParam("@getPageCounts",SqlDbType.Int,8 ,cmdParms[4])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_SelectOrder", parameter);
            List<object[]> oList = new List<object[]>();
            while (mr.Read())
            {
                object[] dt = new object[mr.FieldCount];
                mr.GetValues(dt);
                oList.Add(dt);
            }
            mr.Dispose();
            mr.Close();
            return oList;
        }
        #endregion

        #region 通用查询
        /// <summary>订单插入</summary>
        /// <param name="cmdParms"></param>
        /// <returns>[1000]正常</returns>
        public List<object[]> S_Select(string[] cmdParms, ref string statue)
        {
            statue = "1001";
            SqlParameter[] parameter = {
										MakeInParam("@ID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@InPut",SqlDbType.Int,8,cmdParms[1]),
                                        MakeInParam("@IWhere",SqlDbType.VarChar,50,cmdParms[2])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "S_Select", parameter);
            List<object[]> oList = new List<object[]>();
            while (mr.Read())
            {
                object[] dt = new object[mr.FieldCount];
                mr.GetValues(dt);
                oList.Add(dt);
                statue = "1000";
            }
            mr.Dispose();
            mr.Close();
            return oList;
        }
        #endregion

        #region 确认收货
        public bool EnterSend(string cmdParms)
        {
            SqlParameter[] parameter = {
										MakeInParam("@ID",SqlDbType.Int,8,"9"),
                                        MakeInParam("@InPut",SqlDbType.Int,8,cmdParms),
                                        MakeInParam("@IWhere",SqlDbType.VarChar,50,"")
										};
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Select", parameter) > 0;
        }
        #endregion

        #region 店铺匹配
        /// <summary>店铺匹配</summary>
        /// <param name="data">[0]经度[1]纬度</param>
        /// <returns>[1000]正常</returns>
        public object[] IndexSelect(string[] cmdParms, ref string statue)
        {
            statue = "1001";
            SqlParameter[] parameter = {
									MakeInParam("@lng",SqlDbType.VarChar ,50,cmdParms[0]),
                                    MakeInParam("@lat",SqlDbType.VarChar ,50,cmdParms[1])
										};
            SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "IndexSelect", parameter);

            object[] a = new object[mr.FieldCount];

            if (mr.FieldCount > 0 && mr.Read())
            {

                statue = "1000";
                mr.GetValues(a);
            }
            mr.Dispose();
            mr.Close();
            return a;
        }
        #endregion

        #region 历史订单查询
        public List<object[]> S_SelectHisOrder(string[] cmdParms, ref string statue) {
            SqlParameter[] parameter = { 
                                         MakeInParam("@UserID",SqlDbType.Int,8,cmdParms[0]),
                                         MakeInParam("@IntCurrPage",SqlDbType.Int,8,cmdParms[1]),
                                         MakeInParam("@IntPageSize",SqlDbType.Int,8,cmdParms[2]),
                                         MakeOutParam("@getRecordCounts",SqlDbType.Int,8),
                                         MakeOutParam("@getPageCounts",SqlDbType.Int,8)
                                      };
            List<object[]> oList = new List<object[]>();
            using (SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "U_Proc_SelectOrder", parameter))
            {
                while (mr.Read())
                {
                    object[] dt = new object[mr.FieldCount];
                    mr.GetValues(dt);
                    oList.Add(dt);
                }
                mr.Dispose();
                mr.Close();
                statue = parameter[3].Value.ToString() + "|" + parameter[4].Value.ToString();
            }
            return oList;
        }
        #endregion
    }
}
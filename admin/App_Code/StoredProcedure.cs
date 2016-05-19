using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// StoredProcedure 的摘要说明
/// </summary>
public class StoredProcedure : DataProvider
{
    #region 通用数据库删除
    /// <summary>
    /// 通用数据库操作，返回受影响行数
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <param name="IDs">标识集合</param>
    /// <returns></returns>
    public bool C_Operate_Delete(string TableName, string IDs)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@ITABLENAME",SqlDbType.VarChar,20,TableName),
                                        MakeInParam("@IIDs",SqlDbType.VarChar,100,IDs)
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Proc_Operate_Delete", parameter) > 0;
    }
    #endregion

    #region 通用数据库软删除
    /// <summary>
    /// 通用数据库操作，返回受影响行数
    /// </summary>
    /// <param name="TableName">表名</param>
    /// <param name="RowID">标识集合，‘,’分割</param>
    /// <returns></returns>
    public bool C_Operate_TagDelete(string TableName, string RowID) {
        string sqlcmd = "UPDATE @ITABLENAME SET Tag='True' WHERE ID IN @IID";
        SqlParameter[] parameter = { 
                                        MakeInParam("@ITABLENAME",SqlDbType.VarChar,20,TableName),
                                        MakeInParam("@IID",SqlDbType.Int,9,RowID)
                                   };
        return ExecuteNonQuery(connectionString, CommandType.Text, sqlcmd, parameter) > 0;
    }
    #endregion

    #region 系统基本存储过程调用
    ////审核提现申请的存储过程调用
    //public bool C_CheckWithdrawMoney(string[] cmdParms)
    //{
    //    SqlParameter[] parameter ={
    //                                 MakeInParam("@id",SqlDbType.Int,8,cmdParms[0]),
    //                                    MakeInParam("@U_id",SqlDbType.Int,8,cmdParms[1]),
    //                                    MakeInParam("@A_ID",SqlDbType.Int,8,cmdParms[2]),
    //                                    MakeInParam("@ordercode",SqlDbType.VarChar,50,cmdParms[3]),
                                        
    //                             };
    //   return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "U_Proc_CreateWithdrawMoney", parameter) > 0;
    //}
    #region 审核提现申请,,
    /// <summary>审核提现申请,,</summary>
    /// <param name="data">[0]0接受，其他拒绝[1][2][3]</param>
    /// <returns>[1000]正常</returns>
    public object[] C_CheckWithdrawMoney(string[] data)
    {
        object[] dt = new object[2];
        SqlParameter[] parameter = {
										MakeInParam("@id",SqlDbType.Int,8,data[0]==""?"0":data[0]),
										MakeInParam("@U_id",SqlDbType.Int,8,data[1]==""?"0":data[1]),
										MakeInParam("@A_ID",SqlDbType.Int,8,data[2]==""?"0":data[2]),
										MakeInParam("@ordercode",SqlDbType.VarChar,50,data[3]==""?"":data[3])
										};
        SqlDataReader mr = ExecuteReader(connectionString, CommandType.StoredProcedure, "A_Proc_CheckWithdrawMoney", parameter);
        mr.Read();
        mr.GetValues(dt);
        return dt;
    }
    #endregion
    //系统配置的存储过程的调用@ISITENAME NVARCHAR(50),
	
    public bool C_ConfigInfoOperate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@ID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ISITENAME",SqlDbType.NVarChar,50,cmdParms[1]),
                                        MakeInParam("@IWELCOME",SqlDbType.NVarChar,-1,cmdParms[2]),
                                        MakeInParam("@IICP",SqlDbType.NVarChar,50,cmdParms[3]),
                                        MakeInParam("@ILOGO",SqlDbType.NVarChar,50,cmdParms[4]),
                                        MakeInParam("@ITEL",SqlDbType.NVarChar,100,cmdParms[5]),
                                        MakeInParam("@IADDRESS",SqlDbType.NVarChar,100,cmdParms[6]),
                                       
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Proc_Operate_ConfigInfo", parameter) > 0;
    }
    //文章表的存储过程调用
    public bool C_EssayOperate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IESSAYID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ICOLUMNID",SqlDbType.Int,8,cmdParms[1]),
                                        MakeInParam("@ITITLE",SqlDbType.NVarChar,50,cmdParms[2]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[3]),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[4]),
                                        MakeInParam("@IISHOW",SqlDbType.Bit,1,cmdParms[5]=="1" ? "True" : "False"),
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Essay", parameter) > 0;
    }
    //修改发货状态的存储过程调用
    public bool C_ShipmentsOperate(string[] cmdParms)
    {
        SqlParameter[] parameter ={
                                      MakeInParam("@U_id",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@orderform ",SqlDbType.VarChar,50,cmdParms[1]),
                                        
                                 };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Shipments", parameter) > 0;
    }

    //取消积分订单
    public bool C_CloseOrderOperate(string[] cmdParms)
    {
        SqlParameter[] parameter ={
                                      MakeInParam("@ordercode",SqlDbType.VarChar,50,cmdParms[0]),
                                        
                                        
                                 };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "U_Proc_CloseOrder2", parameter) > 0;
    }

    //锦囊信息表的存储过程调用
    public bool C_GoodsOperate(string[] cmdParms)
    {
        SqlParameter[] parameter ={
                                      MakeInParam("@A_ID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ID ",SqlDbType.Int,8,cmdParms[1]),
                                        MakeInParam("@Name ",SqlDbType.NVarChar,50,cmdParms[2]),
                                        MakeInParam("@Intro",SqlDbType.NVarChar,500,cmdParms[3]),
                                        MakeInParam("@Img",SqlDbType.VarChar,-1,cmdParms[4]),
                                        MakeInParam("@Imggood",SqlDbType.VarChar,-1,cmdParms[5]),
                                        MakeInParam("@ImgIntro",SqlDbType.VarChar,-1,cmdParms[6]),
                                        MakeInParam("@Type",SqlDbType.TinyInt,2,cmdParms[7]),
                                        MakeInParam("@Price",SqlDbType.VarChar,50,cmdParms[8]),
                                       
                                        MakeInParam("@Num",SqlDbType.Int,999,cmdParms[9]),
                                 };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Goods", parameter) > 0;
    }

    //广告表的存储过程调用//
    public bool C_AdOperate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IADID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ICOLUMNID",SqlDbType.Int,8,cmdParms[1]),
                                        MakeInParam("@ITITLE",SqlDbType.NVarChar,50,cmdParms[2]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[3]),
                                        MakeInParam("@ITYPE",SqlDbType.TinyInt,2,cmdParms[4]),
                                        MakeInParam("@IIMG",SqlDbType.VarChar,-1,cmdParms[5]),
                                        MakeInParam("@IISHOW",SqlDbType.Bit,1,cmdParms[6]=="1" ? "True" : "False"),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[7]),
                                        MakeInParam("@ILINK",SqlDbType.NVarChar,255,cmdParms[8])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Ad", parameter) > 0;
    }
   

    //新闻表的存储过程调用
    public bool C_ArticleOperate1(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IARTICLEID",SqlDbType.Int,8,cmdParms[0]),
                                        
                                        MakeInParam("@ITITLE",SqlDbType.NVarChar,50,cmdParms[1]),
                                        MakeInParam("@IABSTRACT",SqlDbType.NVarChar,200,cmdParms[2]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[3]),
                                        MakeInParam("@IAUTHOR",SqlDbType.NVarChar,20,cmdParms[4]),
                                        MakeInParam("@IKEYWORDS",SqlDbType.NVarChar,50,cmdParms[5]),
                                        MakeInParam("@IISSHOW",SqlDbType.Bit,1,cmdParms[6]=="1" ? "True" : "False"),
                                        MakeInParam("@ILINK",SqlDbType.NVarChar,255,cmdParms[7]),
                                        MakeInParam("@IFILEURL",SqlDbType.NVarChar,255,cmdParms[8]),
                                        MakeInParam("@IIMG",SqlDbType.VarChar,-1,cmdParms[9]),
                                        MakeInParam("@IIMGINSIDE",SqlDbType.VarChar,-1,cmdParms[10]),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[11]),
                                        MakeInParam("@ISTATES",SqlDbType.TinyInt,20,cmdParms[12])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Article", parameter) > 0;
    }
    public bool C_ArticleOperate2(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IARTICLEID",SqlDbType.Int,8,cmdParms[0]),
                                        
                                        MakeInParam("@ITITLE",SqlDbType.NVarChar,50,cmdParms[1]),
                                        MakeInParam("@IABSTRACT",SqlDbType.NVarChar,200,cmdParms[2]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[3]),
                                        MakeInParam("@IAUTHOR",SqlDbType.NVarChar,20,cmdParms[4]),
                                        MakeInParam("@IKEYWORDS",SqlDbType.NVarChar,50,cmdParms[5]),
                                        MakeInParam("@IISSHOW",SqlDbType.Bit,1,cmdParms[6]=="1" ? "True" : "False"),
                                        MakeInParam("@ILINK",SqlDbType.NVarChar,255,cmdParms[7]),
                                        MakeInParam("@IFILEURL",SqlDbType.NVarChar,255,cmdParms[8]),
                                        MakeInParam("@IIMG",SqlDbType.VarChar,-1,cmdParms[9]),
                                        MakeInParam("@IIMGINSIDE",SqlDbType.VarChar,-1,cmdParms[10]),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[11]),
                                        MakeInParam("@ISTATES",SqlDbType.TinyInt,20,cmdParms[12])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Article", parameter) > 0;
    }
    public bool C_ArticleOperate3(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IARTICLEID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ITITLE",SqlDbType.NVarChar,50,cmdParms[1]),
                                        MakeInParam("@IABSTRACT",SqlDbType.NVarChar,200,cmdParms[2]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[3]),
                                        MakeInParam("@IAUTHOR",SqlDbType.NVarChar,20,cmdParms[4]),
                                        MakeInParam("@IKEYWORDS",SqlDbType.NVarChar,50,cmdParms[5]),
                                        MakeInParam("@IISSHOW",SqlDbType.Bit,1,cmdParms[6]=="1" ? "True" : "False"),
                                        MakeInParam("@ILINK",SqlDbType.NVarChar,255,cmdParms[7]),
                                        MakeInParam("@IFILEURL",SqlDbType.NVarChar,255,cmdParms[8]),
                                        MakeInParam("@IIMG",SqlDbType.VarChar,-1,cmdParms[9]),
                                        MakeInParam("@IIMGINSIDE",SqlDbType.VarChar,-1,cmdParms[10]),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[11]),
                                        MakeInParam("@ISTATES",SqlDbType.TinyInt,20,cmdParms[12])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Article", parameter) > 0;
    }
    public bool C_ArticleOperate4(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IARTICLEID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ITITLE",SqlDbType.NVarChar,50,cmdParms[1]),
                                        MakeInParam("@IABSTRACT",SqlDbType.NVarChar,200,cmdParms[2]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[3]),
                                        MakeInParam("@IAUTHOR",SqlDbType.NVarChar,20,cmdParms[4]),
                                        MakeInParam("@IKEYWORDS",SqlDbType.NVarChar,50,cmdParms[5]),
                                        MakeInParam("@IISSHOW",SqlDbType.Bit,1,cmdParms[6]=="1" ? "True" : "False"),
                                        MakeInParam("@ILINK",SqlDbType.NVarChar,255,cmdParms[7]),
                                        MakeInParam("@IFILEURL",SqlDbType.NVarChar,255,cmdParms[8]),
                                        MakeInParam("@IIMG",SqlDbType.VarChar,-1,cmdParms[9]),
                                        MakeInParam("@IIMGINSIDE",SqlDbType.VarChar,-1,cmdParms[10]),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[11]),
                                        MakeInParam("@ISTATES",SqlDbType.TinyInt,20,cmdParms[12])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Article", parameter) > 0;
    }
    //友情链接表的存储过程调用
    public bool C_FriendLinkOperate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@ILINKID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ITITLE",SqlDbType.NVarChar,20,cmdParms[1]),
                                        MakeInParam("@IURL",SqlDbType.NVarChar,255,cmdParms[2]),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[3]),
                                        MakeInParam("@IIMG",SqlDbType.VarChar,50,cmdParms[4]),
                                        MakeInParam("@IISSHOW",SqlDbType.Bit,1,cmdParms[5]=="1" ? "True" : "False")
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_FriendLink", parameter) > 0;
    }
    //招聘表的存储过程调用
    public bool C_RecruitmentOperate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IRECRUITMENTID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@IJOBNAME",SqlDbType.NVarChar,50,cmdParms[1]),
                                        MakeInParam("@IDEPARTMENT",SqlDbType.NVarChar,50,cmdParms[2]),
                                        MakeInParam("@INUMBER",SqlDbType.Int,8,cmdParms[3]),
                                        MakeInParam("@ISEX",SqlDbType.NVarChar,50,cmdParms[4]),
                                        MakeInParam("@IAGE",SqlDbType.NVarChar,50,cmdParms[5]),
                                        MakeInParam("@IWORKAREA",SqlDbType.NVarChar,200,cmdParms[6]),
                                        MakeInParam("@IEXPERIENCE",SqlDbType.NVarChar,50,cmdParms[7]),
                                        MakeInParam("@IEDUCATION",SqlDbType.NVarChar,50,cmdParms[8]),
                                        MakeInParam("@IVAILDDATE",SqlDbType.NVarChar,50,cmdParms[9]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[10]),
                                        MakeInParam("@IISHOW",SqlDbType.Bit,1,cmdParms[11]=="1" ? "True" : "False"),
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Recruitment", parameter) > 0;
    }
    //产品表的存储过程调用
    public bool C_ProductOperate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IPRODUCTID",SqlDbType.Int,4,cmdParms[0]),
                                        MakeInParam("@ICOLUMNID",SqlDbType.Int,4,cmdParms[1]),
                                        MakeInParam("@INAME",SqlDbType.NVarChar,50,cmdParms[2]),
                                        MakeInParam("@ICONTENT",SqlDbType.NVarChar,-1,cmdParms[3]),
                                        MakeInParam("@IIMG",SqlDbType.VarChar,-1,cmdParms[4]),
                                        MakeInParam("@ISORTID",SqlDbType.Int,4,cmdParms[5]),
                                        MakeInParam("@IISSHOW",SqlDbType.Bit,1,cmdParms[6]=="1" ? "True" : "False"),
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Product", parameter) > 0;
    }
    //管理员表的存储过程调用
    public bool C_Admin_Operate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IADMINID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@IACCOUNT",SqlDbType.NVarChar,20,cmdParms[1]),
                                        MakeInParam("@IPASSWORD",SqlDbType.Char,32,cmdParms[2]),
                                        MakeInParam("@IROLEID",SqlDbType.Int,8,cmdParms[3]),
                                        MakeInParam("@IREALNAME",SqlDbType.NVarChar,20,cmdParms[4]),
                                        MakeInParam("@INICKNAME",SqlDbType.NVarChar,20,cmdParms[5]),
                                        MakeInParam("@IPOSITION",SqlDbType.NVarChar,20,cmdParms[6]),
                                        MakeInParam("@IDEPART",SqlDbType.NVarChar,20,cmdParms[7]),
                                        MakeInParam("@ISTATUS",SqlDbType.TinyInt,2,cmdParms[8]),
                                        MakeInParam("@IDESCRIPTION",SqlDbType.NVarChar,200,cmdParms[9])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Proc_Operate_Admin", parameter) > 0;
    }
    //角色表的存储过程调用
    public bool C_Role_Operate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@INAME",SqlDbType.NVarChar,20,cmdParms[1]),
                                        MakeInParam("@IDESCRIPTION",SqlDbType.NVarChar,200,cmdParms[2])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Proc_Operate_Role", parameter) > 0;
    }
    //系统配置表的操作
    public bool C_Config_Operate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@ISITENAME",SqlDbType.NVarChar,50,cmdParms[0]),
                                        MakeInParam("@IICP",SqlDbType.NVarChar,50,cmdParms[1]),
                                        MakeInParam("@IKEYWORDS",SqlDbType.NVarChar,100,cmdParms[2]),
                                        MakeInParam("@ILOGO",SqlDbType.VarChar,50,cmdParms[3]),
                                        MakeInParam("@ITEL",SqlDbType.NVarChar,100,cmdParms[4]),
                                        MakeInParam("@IADDRESS",SqlDbType.NVarChar,200,cmdParms[5])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Proc_Operate_ConfigInfo", parameter) > 0;
    }

    //系统权限表的操作
    public bool C_Application_Operate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@ICODE",SqlDbType.VarChar,20,cmdParms[1]),
                                        MakeInParam("@INAME",SqlDbType.NVarChar,20,cmdParms[2]),
                                        MakeInParam("@ITYPE",SqlDbType.TinyInt,2,cmdParms[3]),
                                        MakeInParam("@IICON",SqlDbType.VarChar,100,cmdParms[4]),
                                        MakeInParam("@IFIELD",SqlDbType.VarChar,200,cmdParms[5]),
                                        MakeInParam("@IDESCRIPTION",SqlDbType.NVarChar,200,cmdParms[6])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Proc_Operate_Application", parameter) > 0;
    }
    //节点表的操作
    public bool C_Moudle_Operate(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@IID",SqlDbType.Int,8,cmdParms[0]),
                                        MakeInParam("@IPARENTID",SqlDbType.Int,8,cmdParms[1]),
                                        MakeInParam("@INAME",SqlDbType.NVarChar,20,cmdParms[2]),
                                        MakeInParam("@ICODE",SqlDbType.NVarChar,20,cmdParms[3]),
                                        MakeInParam("@ILEVEL",SqlDbType.TinyInt,2,cmdParms[4]),
                                        MakeInParam("@IDIRECTORY",SqlDbType.VarChar,100,cmdParms[5]),
                                        MakeInParam("@IISENABLE",SqlDbType.Bit,1,cmdParms[6]=="1" ? "True" : "False"),
                                        MakeInParam("@IISSYSTEM",SqlDbType.Bit,1,cmdParms[7]=="1" ? "True" : "False"),
                                        MakeInParam("@ISORTID",SqlDbType.Int,8,cmdParms[8]),
                                        MakeInParam("@IICON",SqlDbType.VarChar,150,cmdParms[9]),
                                        MakeInParam("@IITEM",SqlDbType.VarChar,150,cmdParms[10]),
                                        MakeInParam("@IDESCRIPTION",SqlDbType.NVarChar,200,cmdParms[11])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "S_Proc_Operate_Moudle", parameter) > 0;
    }

    //栏目类别表
    /// <summary>栏目类别表,</summary>
    /// <param name="data">[0]栏目ID[1]父级栏目ID[2]1栏目类型 0广告、1新闻、2文章、3产品[3]栏目名称[4]栏目等级[5]链接[6]排序编号[7]是否首页推荐[8]描述[9]预留</param>
    /// <returns>[1000]正常</returns>
    public bool A_Proc_Operate_Column(string[] data)
    {
        SqlParameter[] parameter = {
										MakeInParam("@ICOLUMNID",SqlDbType.Int,8,data[0]==""?"0":data[0]),
										MakeInParam("@IPARENTID",SqlDbType.Int,8,data[1]==""?"0":data[1]),
										MakeInParam("@ITYPE",SqlDbType.TinyInt,8,data[2]==""?"0":data[2]),
										MakeInParam("@INAME",SqlDbType.NVarChar,20,data[3]==""?"":data[3]),
										MakeInParam("@ILEVEL",SqlDbType.Int,8,data[4]==""?"0":data[4]),
										MakeInParam("@IURL",SqlDbType.VarChar,255,data[5]==""?"":data[5]),
										MakeInParam("@ISORTID",SqlDbType.Int,8,data[6]==""?"0":data[6]),
										MakeInParam("@ISINDEX",SqlDbType.Bit,1,data[7]=="1"?"True":"False"),
										MakeInParam("@IDESCRIPTION",SqlDbType.NVarChar,500,data[8])
										};
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_Operate_Column", parameter) > 0;
    }

    /// <summary>
    /// 批量插入权限
    /// </summary>
    /// <param name="dt">数据表</param>
    /// <param name="RoleID">角色标识</param>
    /// <returns></returns>
    public bool OperatePermission(DataTable dt, string RoleID)
    {
        //ExecuteBulkCopy(connectionString, dt, "S_Permission");
        SqlTransaction sqlTran = null;
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (sqlTran = sqlConnection.BeginTransaction())
                {
                    C_Operate("DELETE FROM S_Permission Where P_RoleID=" + RoleID);

                    using (SqlBulkCopy copy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTran))
                    {
                        copy.DestinationTableName = "S_Permission";

                        copy.ColumnMappings.Add("ID", "ID");
                        copy.ColumnMappings.Add("P_RoleID", "P_RoleID");
                        copy.ColumnMappings.Add("P_ModuleID", "P_ModuleID");
                        copy.ColumnMappings.Add("P_Code", "P_Code");
                        copy.ColumnMappings.Add("AddDay", "AddDay");
                        copy.ColumnMappings.Add("AddTime", "AddTime");

                        copy.WriteToServer(dt);

                        sqlTran.Commit();
                        sqlConnection.Close();
                        return true;
                    }
                }
            }
        }
        catch
        {
            if (null != sqlTran)
                sqlTran.Rollback();
            return false;
        }
    }
    #endregion

    #region 数据库操作
    public bool A_Proc_UpdateUserInfo(string[] cmdParms)
    {
        SqlParameter[] parameter = { 
                                        MakeInParam("@ID",SqlDbType.Int,8 ,cmdParms[0]),
                                        MakeInParam("@U_RealName",SqlDbType.NVarChar ,50,cmdParms[1]),
                                        MakeInParam("@U_EMail",SqlDbType.VarChar ,100,cmdParms[2]),
                                        MakeInParam("@U_Tel",SqlDbType.VarChar ,50,cmdParms[3]),
                                        MakeInParam("@U_QQ",SqlDbType.VarChar ,50,cmdParms[4]),
                                        MakeInParam("@U_CompanyName",SqlDbType.NVarChar ,200,cmdParms[5])
                                   };
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "A_Proc_UpdateUserInfo", parameter) > 0;
    }
    #endregion
}
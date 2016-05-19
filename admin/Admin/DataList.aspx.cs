using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DataList : System.Web.UI.Page
{
    public int ModuleID = 0;
    public string PageSize = "10";
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        ModuleID = Common.Q_Int("id", 0);
        if (!IsPostBack)
        {
            RouteChoose();
            dp.PromissionOfCommon(Authority.GetRoleID(Context), ModuleID.ToString(), "2", lbTitle);
            IniPage();
            bindData(0);
        }
    }

    #region 路由选择
    public void RouteChoose()
    {
        switch (ModuleID)
        {
            case 1013:
                Response.Redirect("Column.aspx?id=1");
                break;
            case 1021:
                Response.Redirect("SysConfig.aspx");
                break;
            case 1022:
                Response.Redirect("Column.aspx");
                break;
            case 1025:
                Response.Redirect("FileManager.aspx");
                break;
        }
    }
    #endregion

    #region 组织页面UI
    public void IniPage()
    {
        string buttonList = "Add",
               ConditonHtml = string.Empty;
        switch (ModuleID)
        {
            //case 11:
            //    ConditonHtml = WebControl.createHtmlButton(new string[] { "Level_1_总代|Level_2_一级代理|Level_3_二级代理", "State_1_未授权|State_2_已授权" }, true);
            //    break;
            case 1016:
                buttonList = string.Empty;
                break;
            case 311:
                buttonList = string.Empty;
                break;
            case 313:
                buttonList = string.Empty;
                break;
            case 118:
                ConditonHtml = ConditonHtml = "<select id='haha' name='haha' runat='server' style='margin-right:700px;margin-top:6px;'><option value='3'>全部</option><option value='0'>锦囊商品</option><option value='1'>积分商品</option><option value='2'>金额商品</option>";
                break;
            case 119:
                buttonList = string.Empty;
                ConditonHtml = ConditonHtml = "<select id='haha' name='haha' runat='server' style='margin-right:700px;margin-top:6px;'><option value='0'>全部</option><option value='4'>购买锦囊</option><option value='5'>购买金额商品</option><option value='3'>购买积分商品</option>"; 
                break;
            case 1111:
                buttonList = string.Empty;
                break;
            case 1114:
                buttonList = string.Empty;
                break;
            case 1119:
                buttonList = string.Empty;
                break;
            case 1120:
                buttonList = string.Empty;
                break;
        }
        llLinkBotton.Text = WebControl.createHtmlButton(buttonList, "td", dp.C_CommonList("SELECT * FROM dbo.S_Application"));
        llCondition.InnerHtml = ConditonHtml;
    }
    #endregion

    #region 组织查询条件
    public string GetCondition()
    {
        bool IsNull = string.IsNullOrEmpty(Request.QueryString["keywords"]);
        string condition = "  1=1 ",
               keywords = !IsNull ? Common.FilterSql(Request.QueryString["keywords"]) : "";
        switch (ModuleID)
        {
            case 111:
                if (!IsNull) condition += "  and  A_Title like '%" + keywords + "%' ";
                break;
            case 112:
                if (!IsNull) condition += "  and E_Title like '%" + keywords + "%' ";
                break;
            case 113:
                if (!IsNull) condition += "  and P_Name like '%" + keywords + "%' ";
                break;
            case 114:
                if (!IsNull) condition += "  and L_Title like '%" + keywords + "%' ";
                break;
            case 115:
                if (!IsNull) condition += "  and A_Title like '%" + keywords + "%' ";
                break;
            case 116:
                if (!IsNull) condition += "  and A_JobName like '%" + keywords + "%'";
                break;
            case 1011:
                if (!IsNull) condition += "  and (R_Name like '%" + keywords + "%') ";
                break;
            case 1012:
                if (!IsNull) condition += "  and (A_Account like '%" + keywords + "%' OR A_RealName like '%" + keywords + "%' OR A_NickName like '%" + keywords + "%') ";
                break;
            case 1014:
                if (!IsNull) condition += "  and (A_Name like '%" + keywords + "%' OR A_Code like '%" + keywords + "%' OR A_Description like '%" + keywords + "%') ";
                break;
            case 1016:
                if (!IsNull) condition += "  and P_RoleID in(SELECT ID FROM S_Role where R_Name like '%" + keywords + "%' )";
                break;
            case 311:
                if (!IsNull) condition += "  and (U_RealName like '%" + keywords + "%' OR U_EMail like '%" + keywords + "%' )";
                break;
            case 313:
                if (!IsNull) condition += "  and (U_OpenID like '%" + keywords + "%' OR U_Level like '%" + keywords + "%' OR U_Name like '%" + keywords + "%' )";
                break;
            case 118:
                if (!IsNull) condition += "  and (G_Price like '%" + keywords + "%' OR G_Name like '%" + keywords + "%' OR G_Num like '%" + keywords + "%' )";
                break;
            case 119:
                if (!IsNull) condition += "  and (O_Orderid like '%" + keywords + "%' OR O_GoodsID like '%" + keywords + "%' OR O_Type like '%" + keywords + "%' )";
                break;
           
            
            case 1114:
                if (!IsNull) condition += "  and (U_ID like '%" + keywords + "%' OR O_GoodsID like '%" + keywords + "%' OR O_Type like '%" + keywords + "%' )";
                break;
            case 1115:
                if (!IsNull) condition += "  and ( ID like '%" + keywords + "%' OR A_Title like '%" + keywords + "%' )";
                break;
            case 1116:
                if (!IsNull) condition += "  and ( ID like '%" + keywords + "%' OR A_Title like '%" + keywords + "%' )";
                break;
            case 1118:
                if (!IsNull) condition += "  and (ID like '%" + keywords + "%' OR A_Title like '%" + keywords + "%' )";
                break;
            
        }
        return condition;
    }
    #endregion

    #region 数据绑定
    protected void bindData(int _Page)
    {
        string Condition = GetCondition(),
               PageIndex = _Page == 0 ? Common.Q_Int("Page", 0).ToString() : _Page.ToString(),
               RecordCount = "0",
               PageCount = "0",
               FieldName = string.Empty,
               dtSqlField = string.Empty;

        PageSize = Common.GetCookieValue("pagesize");
        PageSize = string.IsNullOrEmpty(PageSize) ? "10" : PageSize;

        switch (ModuleID)
        {
            case 111:
                FieldName = "ID,A_ColumnID,A_Title,A_Author,A_IsShow,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,所属类别,新闻标题,新闻作者,是否显示,添加时间", dp.C_Pagination(new string[] { PageIndex, PageSize, "ID,(SELECT C_Name From A_Column WHERE ID=A_ColumnID) AS A_ColumnID,A_Title,A_Author,(CASE A_IsShow WHEN 1 THEN '显示' ELSE '隐藏' END) AS A_IsShow,AddTime", "[A_Article]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 112:
                FieldName = "ID,E_ColumnID,E_Title,E_IsShow,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,所属栏目,文章标题,是否显示,添加时间", dp.C_Pagination(new string[] { PageIndex, PageSize, "ID,(SELECT C_Name From A_Column WHERE ID=E_ColumnID) AS E_ColumnID,E_Title,(CASE E_IsShow WHEN 1 THEN '显示' ELSE '隐藏' END) AS E_IsShow,AddTime", "[A_Essay]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 113:
                FieldName = "ID,P_ColumnID,P_Name,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,所属栏目,商品名称,添加时间", dp.C_Pagination(new string[] { PageIndex, PageSize, "ID,(SELECT C_Name From A_Column WHERE ID=P_ColumnID) AS P_ColumnID,P_Name,AddTime", "[A_Product]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 114:
                FieldName = "ID,L_Title,L_SortID,L_IsShow,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,显示名称,排序编号,是否显示,添加时间", dp.C_Pagination(new string[] { PageIndex, PageSize, "ID,L_Title,L_SortID,(CASE WHEN L_IsShow=1 THEN '显示' ELSE '隐藏' END) AS L_IsShow,AddTime", "[A_FirendLink]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 115:
                FieldName = "ID,A_ColumnID,A_Title,A_SortID,A_IsShow,A_ViewCount,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,所属栏目,广告标题,排序编号,是否显示,查看次数,添加时间", dp.C_Pagination(new string[] { PageIndex, PageSize, "ID,(SELECT C_Name From A_Column WHERE ID=A_ColumnID) AS A_ColumnID,A_Title,A_SortID,(CASE WHEN A_IsShow=1 THEN '显示' ELSE '隐藏' END) AS A_IsShow,A_ViewCount,AddTime", "[A_Ad]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 116:
                FieldName = "ID,A_JobName,A_Department,A_Number,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,招聘职位,招聘部门,招聘人数,添加时间", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "[A_Recruitment]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 1011:
                FieldName = "ID,R_Name,R_Description";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,名称,描述", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "[S_Role]", Condition + " AND ID<>99", "ID" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除|Power_分配权限");
                break;
            case 1012:
                FieldName = "ID,A_Account,A_Role,A_RealName,A_NickName,A_Status,A_LoginTimes,AddTime";
                dtSqlField = "ID,A_Account,(select R_Name from S_Role where ID=A_RoleID) AS A_Role,A_RealName,A_NickName,(CASE A_Status WHEN 0 THEN '正常' ELSE '停用' END) AS A_Status,A_LoginTimes,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,名称,角色,姓名,昵称,状态,登陆次数,添加时间", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "[S_Admin]", Condition + " AND ID<>88", "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 1014:
                FieldName = "ID,A_Code,A_Name,A_Icon,A_Description";
                dtSqlField = "ID,A_Code,A_Name,('<img alt=\"加载失败\" src=\"' + A_Icon + '\" />') AS A_Icon,A_Description";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,标识代码,应用名称,展示图标,应用描述", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "[S_Application]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 1016:
                FieldName = "ID,P_RoleID,P_ModuleID,P_Code,AddTime";
                dtSqlField = "ID,(select R_Name from S_Role  where ID=P_RoleID) AS P_RoleID,(SELECT M_Name FROM S_Module WHERE ID=P_ModuleID) AS P_ModuleID,P_Code,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,角色名称,模块名称,动作代码,授权时间", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "[S_Permission]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 1025:
                FieldName = "ID,A_Code,A_Name,A_Description";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,标识代码,应用名称,应用描述", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "[S_Application]", Condition, "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 311:
                FieldName = "ID,U_RealName,U_EMail,U_Balance,U_Tel,U_QQ,AddTime";
                dtSqlField="ID,U_RealName,U_EMail,U_Balance,U_Tel,U_QQ,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,真实姓名,邮箱,余额,电话,QQ,时间", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "U_User", Condition, "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_修改");
                break;

            
            case 313:
                FieldName = "ID,U_ParentId,U_OpenID,U_Name,U_Level,U_sonNum,U_Money,U_Point,U_States,Addtime";
                dtSqlField = "ID,U_ParentId,U_OpenID,U_Name,U_Level,U_sonNum,U_Money,U_Point,(case U_States when 0 then '普通会员' when 1 then '未领礼物' when 2 then '已领礼物' end)U_States,Addtime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,上级,微信号,昵称,等级,下级数量,账户余额,积分数,会员状态,注册时间", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "U_User", Condition, "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_账变明细");
                break;
            case 118:
                FieldName = "ID,G_Name,G_Type,G_Intro,G_Num,G_Price,AddTime";
                dtSqlField = "ID,G_Name,(case G_Type when 0 then '锦囊' when 1 then '积分商品' when 2 then '金额商品' end)G_Type,G_Intro,G_Num,G_Price,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,商品名,商品类型,商品介绍,库存,价格,加入时间", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "G_Goods", Condition += (Request.QueryString["case_Type"] == null ? "" : (Request.QueryString["case_Type"] == "3" ? "" : " and G_Type='" + Request.QueryString["case_Type"] + "'")), "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 119:
                FieldName = "ID,O_Orderid,U_ID,O_GoodsID,O_PayWay,O_Money,O_States,O_Type,Shipments,AddTime";
                dtSqlField = "ID,O_Orderid,U_ID,O_GoodsID,(case O_PayWay when 0 then '余额支付' when 1 then '微信支付' when 2 then '系统添加' when 3 then '积分支付' end)O_PayWay,O_Money,(case O_States when 0 then '未成功' when 1 then '成功' when 2 then '拒绝'  end)O_States,(case O_Type when 0 then '充值' when 1 then '提现' when 2 then '获得积分' when 3 then '使用积分' when 4 then '购买锦囊' when 5 then '购买金额商品' end)O_Type,(case Shipments when 0 then '未发货' when 1 then '已发货' end)Shipments,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,订单编号,用户ID,商品ID,支付方式,支付金额,交易状态,订单类型,发货状态,创建订单时间", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "U_Orderform", Condition +=(Request .QueryString["case_Type"]==null?"":(Request.QueryString["case_Type"]=="0"?"": " and O_Type='" + Request.QueryString["case_Type"] + "'")), "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_查看");
                break;
            
           
            //case 1114:
            //    FieldName = "ID,U_ID,O_Orderid,O_Money,O_Type,AddTime,caozuo";
            //    dtSqlField = "ID,U_ID,O_Orderid,O_Money,O_Type,AddTime,'<a class=\"Operation\" style=\"cursor:pointer\" onclick=\"dataList._recharge('+CAST(ID AS NVARCHAR(50))+','+CAST(U_ID AS NVARCHAR(50))+','''+O_Orderid+''')\">允许</a>|<a class=\"Operation\" style=\"cursor:pointer\" onclick=\"dataList._refuse('+CAST(ID AS NVARCHAR(50))+','+CAST(U_ID AS NVARCHAR(50))+',\"'+O_Orderid+'\")\">拒绝</a>' as caozuo";
            //    WebControl.CreatTable(showTable, FieldName, "编号,用户ID,订单号,提现金额,订单类型,订单创建时间,操作", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "U_Orderform", Condition, "AddTime DESC" }, ref RecordCount, ref PageCount));
            //    break;
            
            case 1114:
                FieldName = "ID,U_ID,O_Orderid,O_Money,O_States,AddTime";
                dtSqlField = "ID,U_ID,O_Orderid,O_Money,(case O_States when 0 then '未操作' when 1 then '允许' when 2 then '拒绝' end)O_States,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,用户ID,订单号,提现金额,提现状态,订单时间", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "U_Orderform", Condition + " and O_Type=1", "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_查看");
                break;
            case 1115:
                FieldName = "ID,A_Title,A_SortID,AddTime";
                dtSqlField = "ID,A_Title,(case A_SortID when 0 then '没有星星'  when 1 then '有星星' end)A_SortID,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,标题,是否有星,加入消息时间", dp.C_Pagination(new string[] { PageIndex, PageSize, dtSqlField, "A_Article", Condition + " and A_Type=0", "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 1116:
                FieldName = "ID,A_Title,AddTime";
                dtSqlField = "ID,A_Title,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,滚动消息,加入消息时间", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "A_Article", Condition + " and A_Type=1", "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 1118:
                FieldName = "ID,A_Title,A_Abstract,AddTime";
                dtSqlField = "ID,A_Title,A_Abstract,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,标题,摘要,时间", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "A_Article", Condition + " and A_Type=2", "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_修改|Delete_删除");
                break;
            case 1119:
                FieldName = "ID,C_Welcome";
                dtSqlField = "ID,C_Welcome";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,消息", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "S_ConfigInfo", Condition , "ID DESC" }, ref RecordCount, ref PageCount), "Modify_修改");
                break;
            case 1120:
                FieldName = "ID,A_Content,AddTime";
                dtSqlField = "ID,A_Content,AddTime";
                WebControl.CreatLinkButtonTable(showTable, FieldName, "编号,内容,时间", dp.C_Pagination(new string[] { PageIndex, PageSize, FieldName, "A_Article", Condition + " and A_Type=3", "AddTime DESC" }, ref RecordCount, ref PageCount), "Modify_修改");
                break;
        }

        paging.InnerHtml = WebControl.Pagination(Common.S_Int(PageIndex), Common.S_Int(RecordCount), Common.S_Int(PageCount));
    }
    #endregion

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        bindData(1);
    }

}
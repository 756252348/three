using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// WebControl 的摘要说明
/// </summary>
public class WebControl
{
    #region 获取表单提交数据
    /// <summary>
    /// 通用控件绑定数据
    /// </summary>
    /// <param name="oArray">数据</param>
    /// <param name="contanString">包含字符</param>
    /// <param name="datForm">页面表单对象</param>
    /// <param name="startIndex">从数组第几位开始</param>
    public static void BindDataFromIndex(object[] oArray, string contanString, System.Web.UI.HtmlControls.HtmlForm datForm, int startIndex)
    {
        foreach (Control c in datForm.Controls)
        {
            if (c.ID != null && c.ID.Contains(contanString))
            {
                switch (c.GetType().Name)
                {
                    case "TextBox":
                        (c as TextBox).Text = oArray[startIndex].ToString();
                        break;
                    case "RadioButtonList":
                        (c as RadioButtonList).SelectedValue = oArray[startIndex].ToString();
                        break;
                    case "HiddenField":
                        (c as HiddenField).Value = oArray[startIndex].ToString();
                        break;
                    case "Label":
                        (c as Label).Text = oArray[startIndex].ToString();
                        break;
                    case "Literal":
                        (c as Literal).Text = oArray[startIndex].ToString();
                        break;
                    case "DropDownList":
                        (c as DropDownList).SelectedValue = oArray[startIndex].ToString();
                        break;
                    case "CheckBoxList":
                        FillCheckBoxList(oArray[startIndex].ToString(), (c as CheckBoxList));
                        break;
                }
                startIndex++;
            }
        }
    }

    /// <summary>
    /// 通用控件绑定数据
    /// </summary>
    /// <param name="oArray">数据</param>
    /// <param name="contanString">包含字符</param>
    /// <param name="datForm">页面表单对象</param>
    public static void BindData(object[] oArray, string contanString, System.Web.UI.HtmlControls.HtmlForm datForm)
    {
        BindDataFromIndex(oArray, contanString, datForm, 0);
    }


    /// <summary>
    /// 根据字符串，自动勾选CheckBoxList对应项
    /// </summary>
    /// <param name="str">字符串，格式要求为“A,B,C”</param>
    /// <param name="checkBoxList">CheckBoxList控件</param>
    public static void FillCheckBoxList(string str, CheckBoxList checkBoxList)
    {
        string[] items = str.Split(',');
        //遍历items
        for (int i = 0, len = items.Length; i < len; i++)
        {
            //如果值相等，则选中该项
            foreach (ListItem listItem in checkBoxList.Items)
            {
                if (items[i] == listItem.Value)
                    listItem.Selected = true;
                else
                    continue;
            }
        }
    }

    /// <summary>
    /// 根据CheckBoxList中选中的项，获得字符串
    /// </summary>
    /// <param name="checkBoxList">CheckBoxList控件</param>
    /// <returns>字符串，格式为“A,B,C”</returns>
    public static string GetCheckBoxList(CheckBoxList checkBoxList)
    {
        string str = "";
        foreach (ListItem li in checkBoxList.Items)
        {
            if (li.Selected) str += li.Value + ",";
        }
        return str.TrimEnd(',');

    }

    /// <summary>
    /// 根据CheckBoxList中选中的项，获得字符串
    /// </summary>
    /// <param name="checkBoxList">CheckBoxList控件</param>
    /// <returns>字符串，格式为“A,B,C”</returns>
    public static string GetCheckBoxListText(CheckBoxList checkBoxList)
    {
        string str = string.Empty;
        foreach (ListItem li in checkBoxList.Items)
        {
            if (li.Selected) str += li.Text + ",";
        }
        return str.TrimEnd(',');
    }
    #endregion

    #region 获取表单值到数组
    /// <summary>
    /// 获取表单值到数组
    /// </summary>
    /// <param name="context">HttpContext对象</param>
    /// <param name="len">数组长度</param>
    /// <param name="contain">input包含字符串</param>
    /// <returns></returns>
    public static string[] GetFormValues(HttpContext context, int len, string contain)
    {
        int arrLen = context.Request.Form.Count, sCount = 0;
        string[] keyArray = context.Request.Form.AllKeys;
        string[] sArray = new string[len];
        for (int i = 0; i < arrLen; i++)
        {
            if (keyArray[i].IndexOf(contain) > -1 && sCount < len)
            {
                sArray[sCount] = string.Join(",", context.Request.Form.GetValues(i));
                sCount++;
                continue;
            }
        }
        return sArray;
    }

    /// <summary>
    /// 获取表单值到数组,为空时默认值为空【如果插入值不是表单类型，则取输入值】
    /// </summary>
    /// <param name="context">HttpContext对象</param>
    /// <param name="formKey">Name集合</param>
    /// <param name="separator">分割字符</param>
    /// <returns></returns>
    public static string[] GetFormValues(HttpContext context, string formKey, string separator)
    {
        string[] sArray = formKey.Split(separator.ToCharArray());
        string[] names = context.Request.Form.AllKeys;
        int j = sArray.Length;
        string[] iArray = new string[j];
        for (int i = 0; i < j; i++)
        {
            if (!Common.ArrayIsContains(names, sArray[i]))
                iArray[i] = sArray[i];
            else
            {
                formKey = context.Request.Form[sArray[i]];
                iArray[i] = string.IsNullOrEmpty(formKey) ? string.Empty : formKey;
            }
        }
        return iArray;
    }

    /// <summary>
    /// 获取表单值到数组，标识索引从0开始
    /// </summary>
    /// <param name="context">HttpContext对象</param>
    /// <param name="contain">特征码</param>
    /// <param name="len">数组长度</param>
    /// <param name="defaultValue">为空时的默认值</param>
    /// <returns></returns>
    public static string[] GetFormValues(HttpContext context, string contain, int len)
    {
        string[] iArray = new string[len];
        for (int i = 0; i < len; i++)
        {
            iArray[i] = context.Request.Form[contain + i];
        }
        return iArray;
    }

    /// <summary>
    /// 获取Form键/值集合，内置地址解码
    /// </summary>
    /// <param name="context">HttpContext对象</param>
    /// <param name="separator">分割字符</param>
    /// <returns></returns>
    public string[] GetForm(HttpContext context, string separator)
    {
        int FieldCount = context.Request.Form.Count;
        string[] sArray = new string[FieldCount];
        string[] names = context.Request.Form.AllKeys;
        for (int i = 0, len = names.Length; i < len; i++)
        {
            sArray[i] = HttpUtility.UrlDecode(string.Join(separator, context.Request.Form.GetValues(i)));
        }
        return sArray;
    }
    #endregion

    #region 详情页生成
    /// <summary>
    /// 生成详情页
    /// </summary>
    /// <param name="thName">详情页th标签字符串</param>
    /// <param name="spCode">标签字符串间隔符</param>
    /// <param name="filedCount">tr中间th、td组合的个数</param>
    /// <param name="sqlArray">数据库返回数组</param>
    /// <param name="lte">Literal控件ID</param>
    /// <returns></returns>
    public static void CreatDetail(string thName, char spCode, int filedCount, object[] sqlArray, Literal lte)
    {
        lte.Text = CreatDetail(thName, spCode, filedCount, sqlArray, 0);
    }

    /// <summary>
    /// 生成详情页
    /// </summary>
    /// <param name="thName">详情页th标签字符串</param>
    /// <param name="spCode">标签字符串间隔符</param>
    /// <param name="filedCount">tr中间th、td组合的个数</param>
    /// <param name="sqlArray">数据库返回数组</param>
    /// <param name="Index">从数据库返回数组第几位开始</param>
    /// <returns></returns>
    public static string CreatDetail(string thName, char spCode, int filedCount, object[] sqlArray, int Index)
    {
        StringBuilder sbHtml = new StringBuilder();
        string[] thArray = thName.Split(spCode), unitArray = new string[] { };
        string unit = string.Empty;
        int thLen = thArray.Length, remNum = thLen % filedCount, i, j;
        for (i = 0; i < thLen - remNum; i = i + filedCount)
        {
            sbHtml.Append("<tr>");
            for (j = 0; j < filedCount; j++)
            {
                unitArray = thArray[i + j].ToString().Split('_');
                if (unitArray.Length > 1) unit = unitArray[1].ToString();
                sbHtml.AppendFormat("<th>{0}：</th><td>{1}{2}</td>", unitArray[0], sqlArray[i + j + Index], unit);
                unit = string.Empty;
            }
            sbHtml.Append("</tr>");
        }
        if (remNum != 0)
        {
            int diff = filedCount - remNum;
            sbHtml.Append("<tr>");
            for (j = 0; j < remNum; j++)
            {
                unitArray = thArray[i + j].ToString().Split('_');
                if (unitArray.Length > 1) unit = unitArray[1].ToString();
                if (j == remNum - 1)
                    sbHtml.AppendFormat("<th>{0}：</th><td colspan=\"{1}\">{2}{3}</td>", unitArray[0], (diff * 2 + 1), sqlArray[i + j + Index], unit);
                else
                    sbHtml.AppendFormat("<th>{0}：</th><td>{1}{2}</td>", unitArray[0], sqlArray[i + j + Index], unit);
                unit = string.Empty;
            }
            sbHtml.Append("</tr>");
        }
        return sbHtml.ToString();
    }
    #endregion

    #region 生成列表页
    /// <summary>
    /// 生成带checkbox的Table
    /// </summary>
    /// <param name="Lta">Literal控件ID</param>
    /// <param name="sFld">分页内存表中要查询的字段</param>
    /// <param name="sName">Table中标题行</param>
    /// <param name="dt">分页存储过程返回的内存表</param>
    public static void CreatCheckTable(Literal Lta, string sFld, string sName, DataTable dt)
    {
        Lta.Text = CreatTable(sFld, sName, dt, true, false, null);
    }

    /// <summary>
    /// 生成不带checkbox的Table
    /// </summary>
    /// <param name="Lta">Literal控件ID</param>
    /// <param name="sFld">分页内存表中要查询的字段</param>
    /// <param name="sName">Table中标题行</param>
    /// <param name="dt">分页存储过程返回的内存表</param>
    public static void CreatTable(Literal Lta, string sFld, string sName, DataTable dt)
    {
        Lta.Text = CreatTable(sFld, sName, dt, false, false, null);
    }

    /// <summary>
    /// 生成带LinkButton列的Table
    /// </summary>
    /// <param name="Lta">Literal控件ID</param>
    /// <param name="sFld">分页内存表中要查询的字段</param>
    /// <param name="sName">Table中标题行</param>
    /// <param name="dt">分页存储过程返回的内存表</param>
    /// <param name="linkButton">操作按钮，Modify_修改|Delete_删除</param>
    public static void CreatLinkButtonTable(Literal Lta, string sFld, string sName, DataTable dt, string linkButton)
    {
        Lta.Text = CreatTable(sFld, sName, dt, false, true, linkButton);
    }

    /// <summary>
    /// 生成table并返回string字符串,第一列没有隐藏
    /// </summary>
    /// <param name="sFld">内存表中要查询的字段</param>
    /// <param name="sName">Table中标题行</param>
    /// <param name="dt">内存表</param>
    /// <param name="isCheckBox">是否带复选框</param>
    /// <param name="isLinkButton">是否带操作按钮</param>
    /// <param name="linkButton">操作按钮，Modify_修改|Delete_删除</param>
    /// <returns></returns>
    private static string CreatTable(string sFld, string sName, DataTable dt, bool isCheckBox, bool isLinkButton, string linkButton)
    {
        string[] sFArray = sFld.Split(','), sNArray = sName.Split(',');
        int sFLen = sFArray.Length, sNLen = sNArray.Length, dtCount = dt.Rows.Count, i, j;

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<table id=\"ListTable\" class=\"ListTable\" cellspacing=\"0\" border=\"0\" style=\"border-width: 0px; border-collapse: collapse;\" rules=\"all\"><tbody><tr>");

        for (i = 0; i < sNLen; i++)
        {
            if (i == 0)
            {
                if (isCheckBox)
                    sbHtml.Append("<th class=\"CKList\" scope=\"col\" onclick=\"checkAllLine();\">全选</th>");
                else
                    sbHtml.AppendFormat("<th class=\"CKList\"  scope=\"col\">{0}</th>", sNArray[i]);
            }
            else
                sbHtml.AppendFormat("<th scope=\"col\">{0}</th>", sNArray[i]);
        }

        string _linkButton = "";
        if (isLinkButton)
        {
            sbHtml.Append("<th>操作</th>");
            _linkButton = MakeLinkButton(linkButton);
        }
        sbHtml.Append("</tr>");

        if (Common.DataTableIsNull(dt))
        {
            for (i = 0; i < dtCount; i++)
            {
                sbHtml.Append("<tr>");
                for (j = 0; j < sNLen; j++)
                {
                    if (j == 0)
                    {
                        sbHtml.Append("<td>");
                        if (isCheckBox)
                            sbHtml.AppendFormat("<input type=\"checkbox\" name=\"ListTable\"  value=\"{0}\" />", dt.Rows[i][sFArray[j].ToString()]);
                        else
                            sbHtml.Append(dt.Rows[i][sFArray[j].ToString()].ToString());

                        sbHtml.Append("</td>");
                    }
                    else
                        sbHtml.AppendFormat("<td>{0}</td>", dt.Rows[i][sFArray[j].ToString()]);
                }

                if (isLinkButton) sbHtml.AppendFormat("<td>" + _linkButton + "</td>", dt.Rows[i][sFArray[0].ToString()]);
                sbHtml.Append("</tr>");
            }
        }
        else
        {
            if (isCheckBox) sNLen = sNLen + 1;
            if (isLinkButton) sNLen = sNLen + 1;
            sbHtml.AppendFormat("<tr><td colspan=\"{0}\"><h3>暂无数据</h3></td></tr>", sNLen);
        }
        sbHtml.Append("</tbody></table>");
        dt.Dispose();
        return sbHtml.ToString();
    }

    /// <summary>
    /// 生成列表中的LinkButton
    /// </summary>
    /// <param name="Application">代码：Modify_修改|Delete_删除  </param>
    /// <returns></returns>
    private static string MakeLinkButton(string Application)
    {
        string[] _Application = Application.Split('|');
        StringBuilder sbHtml = new StringBuilder();
        for (int i = 0, len = _Application.Length; i < len; i++)
        {
            string[] sArray = _Application[i].Split('_');
            sbHtml.Append("<a class=\"Operation\" href='javascript:void(0);' onclick='dataList._" + sArray[0].ToLower() + "({0})'>" + sArray[1] + "</a> | ");
        }
        sbHtml.Remove(sbHtml.Length - 2, 2);
        return sbHtml.ToString();
    }

    #region 生成顶部查询的table
    /// <summary>
    /// 生成SELECT查询的Table
    /// </summary>
    /// <param name="Lta">Literal控件ID</param>
    /// <param name="tdNum">td查询框每行的数量</param>
    /// <param name="Arr">传入的参数</param>
    /// DEMO：CreateSearchTable(searchConditon,new string[]{"SELECT,订单状态,orederState|1_2_3|未付款_已付款_已收货","SELECT,支付方式,payType|1_2_3|支付宝_微信_线下"})
    public static void CreateSearchTable(Literal Lta, int tdNum, string[] Arr)
    {
        int arrLen = Arr.Length, remNum = arrLen % tdNum, i, j;
        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<table cellspacing=\"0\" border=\"0\" id=\"searchDDLTable\" >");
        for (i = 0; i < arrLen - remNum; i = i + tdNum)
        {
            sbHtml.Append("<tr>");
            for (j = 0; j < tdNum; j++)
            {
                string[] arrDDL = Arr[i + j].Split(',');
                if (arrDDL[0].ToLower() == "select")
                {
                    sbHtml.AppendFormat("<td><label>{0}：</label>{1}</td>", arrDDL[1], CreateSearchDDL(arrDDL[2]));
                }
                else
                {
                    sbHtml.AppendFormat("<td><label>{0}：</label>{1}</td>", arrDDL[1], CreateSearchInput(arrDDL[2]));
                }
            }
            sbHtml.Append("</tr>");
        }
        if (remNum != 0)
        {
            int diff = tdNum - remNum;
            for (j = 0; j < remNum; j++)
            {
                string[] arrDDL = Arr[i + j].Split(',');
                if (j == remNum - 1)
                {
                    if (arrDDL[0].ToLower() == "select")
                    {
                        sbHtml.AppendFormat("<td colspan=\"{0}\"><label>{1}：</label>{2}</td>", (diff + 1), arrDDL[1], CreateSearchDDL(arrDDL[2]));
                    }
                    else
                    {
                        sbHtml.AppendFormat("<td colspan=\"{0}\"><label>{1}：</label>{2}</td>", (diff + 1), arrDDL[1], CreateSearchInput(arrDDL[2]));
                    }
                }
                else
                {
                    if (arrDDL[0].ToLower() == "select")
                        sbHtml.AppendFormat("<td><label>{0}：</label>{1}</td>", arrDDL[1], CreateSearchDDL(arrDDL[2]));
                    else
                        sbHtml.AppendFormat("<td><label>{0}：</label>{1}</td>", arrDDL[1], CreateSearchInput(arrDDL[2]));
                }
            }
        }
        sbHtml.Append("</table>");
        Lta.Text = sbHtml.ToString();
    }

    /// <summary>
    /// 生成查询的下拉
    /// </summary>
    /// <param name="strConditon">要生成的元素【格式"id|Value1_Value2_Value3_Value4|状态1_状态2_状态3_状态4"】</param>
    /// <returns></returns>
    private static string CreateSearchDDL(string strConditon)
    {
        StringBuilder sbDDL = new StringBuilder();
        string[] arrElement = strConditon.Split('|'), arrVal = arrElement[1].Split('_'), arrText = arrElement[2].Split('_');
        int arrValLen = arrVal.Length, arrTextLen = arrText.Length;
        sbDDL.AppendFormat("<select id=\"{0}\" name=\"{0}\">", arrElement[0]);
        sbDDL.Append("<option></option>");
        if (arrValLen == arrTextLen)
        {
            for (int i = 0; i < arrValLen; i++)
                sbDDL.AppendFormat("<option value=\"{0}\">{1}</option>", arrVal[i], arrText[i]);
        }
        sbDDL.Append("</select>");
        return sbDDL.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strConditon"></param>
    /// <returns></returns>
    private static string CreateSearchInput(string strConditon)
    {
        string result = string.Empty;
        string[] arrConditon = strConditon.Split('|');
        switch (arrConditon.Length)
        {
            case 1:
                result = "<input type=\"text\" id=\"" + arrConditon[0] + "\" class=\"common-input Wdate\" name=\"" + arrConditon[0] + "\" onfocus=\"WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd HH:mm:ss' })\" />";
                break;
        }
        return result;
    }
    #endregion

    #endregion

    #region 通用分页
    /// <summary>
    /// 分页超链接字段导航
    /// </summary>
    /// <param name="_PageIndex">当前页</param>
    /// <param name="_PageCount">总页数</param>
    /// <returns>返回一个带超链接翻页地址的html</returns>
    public static string Pagination(int _PageIndex, int _RecordCount, int _PageCount)
    {
        //_url = Regex.Replace(_url, @"([&|?]Page=)([^&]*)", String.Empty, RegexOptions.IgnoreCase);
        string _url = HttpContext.Current.Request.Url.PathAndQuery;

        _url = Regex.Replace(_url, @"(Page=)([^&]*)", String.Empty, RegexOptions.IgnoreCase);

        _url = _url.Replace("?&", "?").TrimEnd("&?".ToCharArray());

        if (Regex.IsMatch(_url, @"\?"))
        {
            _url += @"&";
        }
        else
        {
            _url += @"?";
        }
        int next = 0, pre = 0, startpage = 0, endpage = 0;
        StringBuilder sbHtml = new StringBuilder();
        if (_PageIndex < 1) { _PageIndex = 1; }
        if (_PageIndex > _PageCount) { _PageIndex = _PageCount; }
        next = _PageIndex + 1;
        pre = _PageIndex - 1;
        // 中间页起始序号
        startpage = _PageIndex - 3;
        if (startpage < 1) { startpage = 1; }
        // 中间页终止序号
        endpage = _PageIndex + 3;
        if (endpage > _PageCount) { endpage = _PageCount; }
        sbHtml.Append("共<span>" + _RecordCount + "</span>条记录 页数：<span>" + _PageIndex + "</span>/<span>" + _PageCount + "</span>页 ");
        sbHtml.Append(_PageIndex > 1 ? "<a href=\"" + _url + "Page=1\">首页</a> <a href=\"" + _url + "Page=" + pre + "\">上一页</a>" : "首页 上一页 ");
        // 中间页循环显示页码
        for (int i = startpage; i <= endpage; i++)
        {
            sbHtml.Append(_PageIndex == i ? "<strong>" + i + "</strong>" : "<a class=\"paging_num\" href=\"" + _url + "Page=" + i + "\">" + i + "</a>");
        }
        sbHtml.Append(_PageIndex != _PageCount ? "<a href=\"" + _url + "Page=" + next + "\">下一页</a> <a href=\"" + _url + "Page=" + _PageCount + "\">末页</a>" : "下一页 末页");
        sbHtml.Append("<input type='text' id='goPage' value=''/><a id='goIndex' href='javascript:;' >跳转</a>");
        sbHtml.Append("<select id='pageSize' name='pageSize' class='pageSize'><option value='5'>5</option><option value='7'>7</option><option value='10'>10</option><option value='15'>15</option><option value='20'>20</option><option value='25'>25</option><option value='30'>30</option></select>条/页");
        return sbHtml.ToString();
    }
    #endregion

    #region 创建HTML动作按钮
    /// <summary>
    /// 创建动作按钮
    /// </summary>
    /// <param name="buttonType"></param>
    /// <param name="warpSpan"></param>
    /// <returns></returns>
    public static string createHtmlButton(string buttonType, string warpSpan, DataTable dt)
    {
        if (!string.IsNullOrEmpty(buttonType))
        {
            warpSpan = string.IsNullOrEmpty(warpSpan) ? "span" : warpSpan;
            string[] sArray = buttonType.Split(',');
            int len = dt.Rows.Count, sLen = sArray.Length;

            StringBuilder buttonList = new StringBuilder();
            for (int a = 0; a < sLen; a++)
            {
                for (int i = 0; i < len; i++)
                {
                    if (sArray[a].ToString() == dt.Rows[i][1].ToString())
                    {
                        buttonList.AppendFormat("<{0}><img alt=\"{1}\" src=\"{2}\" />", warpSpan, dt.Rows[i][2].ToString(), dt.Rows[i][4].ToString());
                        buttonList.AppendFormat("&nbsp;<a href=\"javascript:;\" onclick=\"return dataList._{0}();\">{1}</a></{2}>", dt.Rows[i][1].ToString().ToLower(), dt.Rows[i][2].ToString(), warpSpan);
                        continue;
                    }
                }
            }
            dt.Dispose();
            return buttonList.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 创建查询按钮
    /// </summary>
    /// <param name="buttonArray">【单】数组格式new string[]{ "State_1_状态1","State_2_状态2" }【多】new string[]{ "State_1_状态1|State_2_状态2","Level_1_等级1|Level_2_等级2" }<</param>
    /// <param name="multiStatus">true多种状态状态模式，false单状态模式</param>
    /// DEMO :dp.createHtmlButton(new string[] { "Level_1_总代|Level_2_一级代理|Level_3_二级代理", "state_1_未授权|state_2_已授权" }, true)
    ///       dp.createHtmlButton(new string[] { "Level_1_总代","Level_2_一级代理","Level_3_二级代理" }, false)
    /// <returns></returns>
    public static string createHtmlButton(string[] buttonArray, bool multiStatus)
    {
        int len = buttonArray.Length;
        if (len > 0)
        {
            StringBuilder sbHtml = new StringBuilder();
            string[] sArray = new string[3];
            if (multiStatus)
            {
                for (int i = 0; i < len; i++)
                {
                    string[] sArrayLine = buttonArray[i].Split('|');
                    sbHtml.AppendFormat("<li class='{0}'>", sArrayLine[0].Split('_')[0]);
                    for (int j = 0, len1 = sArrayLine.Length; j < len1; j++)
                    {
                        sArray = sArrayLine[j].Split('_');
                        sbHtml.AppendFormat("<a  href='javascript:;' onclick=\"replaceParamVal('{0}','{1}')\" data-index='{2}' >{3}</a>", sArray[0], sArray[1], sArray[1], sArray[2]);
                        if (i < len - 1)
                            sbHtml.Append("<span>/</span>");
                    }
                    sbHtml.Append("</li>");
                }
            }
            else
            {
                sbHtml.AppendFormat("<li class='{0}'>", buttonArray[0].Split('_')[0]);
                for (int i = 0; i < len; i++)
                {
                    sArray = buttonArray[i].Split('_');
                    sbHtml.AppendFormat("<a  href='javascript:;' onclick=\"replaceParamVal('{0}','{1}')\" data-index='{2}' >{3}</a>", sArray[0], sArray[1], sArray[1], sArray[2]);
                    if (i < len - 1)
                        sbHtml.Append("<span>/</span>");
                }
                sbHtml.Append("</li>");
            }
            return sbHtml.ToString();
        }
        else
        {
            return "";
        }
    }
    #endregion

    #region 通用绑定数据到下拉
    /// <summary>
    /// 通用绑定数据到下拉
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="ddl">下拉控件</param>
    public static void C_BindDataToDDL(string sql, System.Web.UI.WebControls.DropDownList ddl)
    {
        DataProvider dp = new DataProvider();
        List<object[]> list = new List<object[]>();
        if (dp.C_CommonList(sql, ref list) == "1000")
        {
            for (int i = 0, len = list.Count; i < len; i++)
            {
                ListItem aitem = new ListItem();
                aitem.Text = list[i][1].ToString();
                aitem.Value = list[i][0].ToString();
                ddl.Items.Add(aitem);
            }
        }
    }

    /// <summary>
    /// 通用绑定数据到单选按钮集合
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="rbl">单选按钮List控件</param>
    public void C_BindDataToRBL(string sql, RadioButtonList rbl)
    {
        DataProvider dp = new DataProvider();
        List<object[]> list = new List<object[]>();
        if (dp.C_CommonList(sql, ref list) == "1000")
        {
            for (int i = 0, len = list.Count; i < len; i++)
            {
                ListItem aitem = new ListItem();
                aitem.Text = list[1].ToString();
                aitem.Value = list[0].ToString();
                rbl.Items.Add(aitem);
            }
        }
    }
    #endregion
}
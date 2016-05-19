using System;

public partial class Admin_Column : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int columnId = Common.Q_Int("id", 0);
            if (columnId >= 0 && columnId < 6)
            {
                string[] sArray1 = "栏目分类,模块管理".Split(',');
                string[] sArray2 = "Column,Module".Split(',');
                string html = "";
                html += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                html += "<tr><td class='cell_group'><img src='Images/ListIconTitle.png' alt='' />&nbsp;您的位置：<a href='javascript:void(0);' onclick='window.location.href=window.location.href;'>" + sArray1[columnId] + "</a></td>";
                html += "</tr></table><table width='100%' height='350' border='0' cellspacing='0' cellpadding='0' align='center'><tr>";
                html += "<td width='25%'><iframe id='ColumnLeft' name='ColumnLeft' noResize height='485px' border=0 src='TreeView/" + sArray2[columnId] + "Tree.html?ParentID=0' frameBorder='0' width='100%' scrolling='auto'></iframe></td>";
                html += "<td width='75%'><iframe id='ColumnRight' name='ColumnRight' noResize height='485px' border=0 src='TreeView/" + sArray2[columnId] + "Set.aspx' frameBorder='0' width='100%' scrolling='no'></iframe></td>";
                html += "</tr></table>";
                llContent.Text = html;
            }
            else
            {
                Response.Redirect("sMessage.aspx?error=808");
            }
        }
    }
}
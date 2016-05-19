using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_FileManager : System.Web.UI.Page
{
    public string path = "";
    public int pageIndex = 1;
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        dp.PromissionOfCommon(Authority.GetRoleID(Context), "1025", "2", lbTitle);

        llLinkBotton.Text = "<td><img src=\"Images/Dot19.png\" />&nbsp;<a href=\"javascript:;\" onclick='goBack();' >返回上一级目录</a></td>";
        int recordCount = 0,
            pageCount = 0,
            pageSize = Common.Q_Int("pageSize", 10);
        pageIndex = Common.Q_Int("page", 1);

        pageSize = Common.S_Int(Common.GetCookieValue("pagesize"));
        pageSize = pageSize == 0 ? 10 : pageSize;

        path = Request.QueryString["path"];
        if (path == null || path == "")
        {
            path = @"~/";
        }
        else
        {
            path = path + "/";
        }


        DataTable dt = GetAllFolderAndFile(path);
        recordCount = dt.Rows.Count;
        if (recordCount % pageSize > 0)
        {
            pageCount = Common.S_Int((recordCount / pageSize + 1).ToString());
        }
        else
        {
            pageCount = Common.S_Int((recordCount / pageSize).ToString());
        }

        gvList.DataSource = Common.SplitDataTable(dt, pageIndex, pageSize);
        gvList.DataBind();
        upload_operateBind();

        paging.InnerHtml = WebControl.Pagination(pageIndex, recordCount, pageCount);
        dt.Dispose();
    }

    protected void upload_operateBind()
    {
        try
        {
            GridView gv = gvList;
            if (gv != null)
            {
                for (int i = 0, len = gv.Rows.Count; i < len; i++)
                {
                    switch (gv.Rows[i].Cells[2].Text.ToUpper())
                    {
                        case "FOLDER":
                            gv.Rows[i].Cells[0].Text = string.Format("<a href='FileManager.aspx?path={0}' style='font-weight:bold'>{1}</a>", path + gv.Rows[i].Cells[0].Text, gv.Rows[i].Cells[0].Text);
                            break;
                        case "HTM":
                        case "HTML":
                        case "CSS":
                        case "JS":
                        case "JSON":
                        case "CONFIG":
                        case "XML":
                        case "BROWSER":
                        case "ASPX":
                        case "ASAX":
                        case "CS":
                        case "ASHX":
                            gv.Rows[i].Cells[0].Text = string.Format("<a href='javascript:;' onclick=\"goEdit('FileEdit.aspx?path={0}')\">{1}</a>", path + gv.Rows[i].Cells[0].Text, gv.Rows[i].Cells[0].Text);
                            break;
                    }
                    gv.Rows[i].Cells[2].Text = string.Format("<img src='images/FileIcon/{0}.gif' alt='{1}' />", gv.Rows[i].Cells[2].Text, gv.Rows[i].Cells[2].Text);
                }
            }
            gv.Dispose();
        }
        catch
        {
            return;
        }
    }

    #region "获取指定格式的文件列表"
    /// <summary>
    /// 获取指定路径的文件列表
    /// </summary>
    /// <param name="dirPath">当前路径</param>
    /// <param name="Dt"></param>
    /// <param name="ReplacePath">替换路径</param>
    /// <param name="FileType">文件格式</param>
    /// <returns></returns>
    private DataTable GetAllFolderAndFile(string FilePath)
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add(new DataColumn("ID", typeof(String)));
        Dt.Columns.Add(new DataColumn("FileName", typeof(String)));
        Dt.Columns.Add(new DataColumn("FoldName", typeof(String)));
        Dt.Columns.Add(new DataColumn("FileType", typeof(String)));
        Dt.Columns.Add(new DataColumn("FileSize", typeof(String)));
        Dt.Columns.Add(new DataColumn("FileModifyTime", typeof(DateTime)));
        DirectoryInfo Dir = new DirectoryInfo(Server.MapPath(FilePath));
        if (Dir.Exists)
        {
            int i = 0, j = 0;
            FileAttributes fa = new FileAttributes();
            foreach (DirectoryInfo d in Dir.GetDirectories())
            {
                fa =d.Attributes & FileAttributes.Hidden;
                if (fa == FileAttributes.Hidden) continue;

                fa = d.Attributes & FileAttributes.System;
                if (fa == FileAttributes.System) continue;


                i++;
                DataRow Row = Dt.NewRow();
                Row[0] = i.ToString();
                Row[1] = d.Name.ToString();
                Row[2] = FilePath;
                Row[3] = "Folder";
                Row[4] = "0";
                Row[5] = d.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                Dt.Rows.Add(Row);
            }
            
            foreach (FileInfo f in Dir.GetFiles(@"*.*"))
            {
                fa = f.Attributes & FileAttributes.Hidden;
                if (fa == FileAttributes.Hidden) continue;


                fa = f.Attributes & FileAttributes.System;
                if (fa == FileAttributes.System) continue;

                j++;
                string FileName = Dir + f.Name.ToString();
                FileName = FileName.Replace(HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
                DataRow Row = Dt.NewRow();
                Row[0] = j.ToString();
                Row[1] = f.Name.ToString();
                Row[2] = path;
                if (f.Name.ToString().LastIndexOf(".") == -1)
                {
                    Row[3] = "";
                }
                else
                {
                    Row[3] = f.Name.ToString().Substring(f.Name.ToString().LastIndexOf(".") + 1);
                }
                Row[4] = GetFileLength(f.Length);
                Row[5] = f.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                Dt.Rows.Add(Row);
            }
        }
        return Dt;
    }

    public string GetFileLength(long len)
    {
        string leng = "0";
        if (len >= 1024000000)
        {
            leng = (len / 1000000000).ToString("F2") + "GB";
        }
        else if (len < 1024000000 && len > 1000000)
        {
            leng = (len / 1024000).ToString("F2") + "MB";
        }
        else if (len < 1000000 && len > 1000)
        {
            leng = (len / 1000).ToString("F2") + "KB";
        }
        else
        {
            leng = len.ToString() + "B";
        }
        return leng;
    }
    #endregion
}
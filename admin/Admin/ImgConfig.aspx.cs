using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Xml;

public partial class Admin_ImgConfig : System.Web.UI.Page
{
    public readonly string xmlPath = Common.getAbsolutePath("~/Config/Common.config");
    DataProvider dp = new DataProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dp.PromissionOfCommon(Authority.GetRoleID(Context), "1026", "6", lbTitle);

            foreach (var item in typeof(System.Drawing.Color).GetMembers())
            {
                if (item.MemberType == System.Reflection.MemberTypes.Property)
                {
                    ListItem li = new ListItem();
                    li.Value = li.Text = Color.FromName(item.Name).Name;
                    txtColor.Items.Add(li);
                }
            }

            System.Drawing.Text.InstalledFontCollection font;
            font = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in font.Families)
            {
                ListItem li = new ListItem();
                li.Value = li.Text = family.Name;
                txtFont.Items.Add(li);
            }

            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xmlPath);
                XmlNodeList xmlNodeList = xdoc.ChildNodes.Item(2).ChildNodes;
                int len = xmlNodeList.Count;

                for (int i = 0; i < len; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = xmlNodeList.Item(i).Name;
                    li.Text = xmlNodeList.Item(i).LastChild.InnerText;
                    ddlFileList.Items.Add(li);
                }

                bindData(ddlFileList.SelectedValue);
            }
            catch
            {
                MessageBox.Show(Page, "系统配置错误！");
            }
        }






    }

    protected void btUp_Click(object sender, EventArgs e)
    {
        try
        {
            string[] sArray = WebControl.GetFormValues(Context, 21, "txt");

            if (fuImg.PostedFile.ContentLength > 0)
            {
                Upload ud = new Upload();
                ud.saveFile(fuImg.PostedFile, sArray[16], ref sArray[4]);

                sArray[4] = sArray[16] + sArray[4];
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);

            XmlNodeList elemList = xdoc.DocumentElement.SelectSingleNode(ddlFileList.SelectedValue).ChildNodes;

            

            for (int i = 0, len = sArray.Length; i < len; i++)
            {
                elemList[i].InnerText = sArray[i];
            }
            xdoc.Save(xmlPath);

            MessageBox.ReLocation(Page, "修改成功！");
        }
        catch 
        {
            MessageBox.Show(Page, "输入参数错误，请检查数据之后再重新输入！");
        }
    }

    protected void bindData(string selectXmlNod)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(xmlPath);

        XmlNodeList elemList = xdoc.DocumentElement.SelectSingleNode(selectXmlNod).ChildNodes;
        int len = elemList.Count - 1;
        object[] oArray = new object[len];
        for (int i = 0; i < len; i++)
        {
            oArray[i] = elemList[i].InnerText;
        }

        WebControl.BindData(oArray, "txt", this.form1);
    }

    protected void ddlFileList_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData(ddlFileList.SelectedValue);
    }
}
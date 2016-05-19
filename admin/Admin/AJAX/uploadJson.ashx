<%@ WebHandler Language="C#" Class="uploadJson" %>

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;

public class uploadJson : IHttpHandler {

    private HttpContext context;
    public void ProcessRequest(HttpContext context)
    {
        //String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);
        
        //文件保存目录路径
        String savePath = "~/UploadFile/common/";

        //文件保存目录URL
        String saveUrl = "../UploadFile/common/";//aspxUrl + "../../Album/common/";

        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();
        extTable.Add("image", "gif,jpg,jpeg,png,bmp");
        extTable.Add("flash", "swf,flv");
        extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
        extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

        //最大文件大小
        int maxSize = 1000000;
        this.context = context;

        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
        }

        String dirPath = "",
               dirName = context.Request.QueryString["dir"],
               fileName = imgFile.FileName,
               fileExt = Path.GetExtension(fileName).ToLower();

        if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小超过限制。");
        }

        

        if (Array.IndexOf(((String)extTable["image"]).Split(','), fileExt.Substring(1).ToLower()) > 0)
        {
            dirName = "images";
        }
        else if (Array.IndexOf(((String)extTable["flash"]).Split(','), fileExt.Substring(1).ToLower()) > 0)
        {
            dirName = "flash";
        }
        else if (Array.IndexOf(((String)extTable["media"]).Split(','), fileExt.Substring(1).ToLower()) > 0)
        {
            dirName = "media";
        }
        else if (Array.IndexOf(((String)extTable["file"]).Split(','), fileExt.Substring(1).ToLower()) > 0)
        {
            dirName = "file";
        }
        else
        {
            showError("上传文件扩展名是不允许的扩展名。");
        }

        dirPath = savePath + dirName + "/";

        FileUtils.CreateFile(dirPath);

        String newFileName  = DateTime.Now.ToString("yyyyMMddHHmmss" + Common.Number(4,false), DateTimeFormatInfo.InvariantInfo) + fileExt,
               filePath = dirPath + "JD0_" + newFileName;

        imgFile.SaveAs(Common.getAbsolutePath(filePath));

        String fileUrl = saveUrl + dirName + "/JD0_" + newFileName;

        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = fileUrl;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
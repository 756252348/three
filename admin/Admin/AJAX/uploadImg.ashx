<%@ webhandler Language="C#" class="uploadImg" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;
/// <summary>
/// 上传压缩图片的专属一般处理程序
/// </summary>
public class uploadImg: IHttpHandler
{
	private HttpContext context;

	public void ProcessRequest(HttpContext context)
    {
        
        this.context = context;
		HttpPostedFile imgFile = null;

        try
        {
            imgFile = context.Request.Files["imgFile"];
        }
        catch 
        {
            showError("文件大小超过限制！");
        }
        
		if (imgFile == null)
		{
			showError("请选择文件。");
		}


		String fileName = imgFile.FileName;
		String fileExt = Path.GetExtension(fileName).ToLower();
        string FileName = context.Request.QueryString["filename"];
        String newFileName = "filename";
        
        if (!new Upload(FileName).MakeThumbImg(imgFile, ref newFileName))
        {
            showError("上传图片格式错误！");
        }
        String fileUrl = newFileName;

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

	public bool IsReusable
	{
		get
		{
			return true;
		}
	}
}

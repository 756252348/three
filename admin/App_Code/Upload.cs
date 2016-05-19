using System;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
///Upload 的摘要说明
/// </summary>
public class Upload
{
    public Upload() : this("Common") { }

    public Upload(string _uploadMode)
    {
        uploadMode = _uploadMode;
        try
        {
            uploadArray = this.GetAlbumMode();
        }
        catch
        {
            uploadArray = new string[21];
        }
    }

    /// <summary>
    /// 模式
    /// </summary>
    public string uploadMode { get; set; }

    /// <summary>
    /// 配置参数
    /// </summary>
    public string[] uploadArray { get; set; }


    #region 方法 -- 图片处理
    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <param name="mode">生成缩略图的方式:"HW":指定高宽缩放（可能变形）; "W"://指定宽，高按比例;"H"://指定高，宽按比例; "Cut"://指定高宽裁减（不变形）</param>    
    /// <param name="quality">图片质量</param>
    private static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, int quality)
    {
        originalImagePath = Common.getAbsolutePath(originalImagePath);
        thumbnailPath = Common.getAbsolutePath(thumbnailPath);

        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

        int towidth = width;
        int toheight = height;

        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;

        switch (mode)
        {
            case "HW"://指定高宽缩放（可能变形）                
                break;
            case "W"://指定宽，高按比例                    
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H"://指定高，宽按比例
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut"://指定高宽裁减（不变形）                
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                if (ow > oh)
                {
                    //newwidth = 200;
                    toheight = (int)((double)oh / (double)ow * (double)towidth);
                }
                else
                {
                    //newheight = 200;
                    towidth = (int)((double)ow / (double)oh * (double)toheight);
                }
                break;
        }

        //新建一个bmp图片
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

        //新建一个画板
        Graphics g = System.Drawing.Graphics.FromImage(bitmap);

        //设置高质量插值法
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充
        g.Clear(Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分
        g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
            new Rectangle(x, y, ow, oh),
            GraphicsUnit.Pixel);

        System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
        ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo("image/jpeg");

        try
        {
            if (ici != null)
            {
                bitmap.Save(thumbnailPath, ici, ep);
            }
            else
            {
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }
    }

    /// <summary>
    /// 获取图片格式
    /// </summary>
    /// <param name="mimeType">格式</param>
    /// <returns></returns>
    private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        System.Drawing.Imaging.ImageCodecInfo[] encoders;
        encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }

    /// <summary>
    /// 加图片水印
    /// </summary>
    /// <param name="originalPath">源图片地址</param>
    /// <param name="filename">文件名</param>
    /// <param name="watermarkFilename">水印文件名</param>
    /// <param name="watermarkStatus">图片水印位置</param>
    /// <param name="watermarkTransparency">透明度</param>
    private static void AddImageSignPic(string originalPath, string filename, string watermarkFilename, string watermarkStatus, int quality, int watermarkTransparency)
    {
        filename = Common.getAbsolutePath(filename);                     ///新图片地址
        watermarkFilename = Common.getAbsolutePath(watermarkFilename);   ///水印图片地址
        originalPath = Common.getAbsolutePath(originalPath);             ///源图片地址

        Image img = Image.FromFile(originalPath);
        Image copyImage = Image.FromFile(filename);

        Graphics g = Graphics.FromImage(img);
        //设置高质量插值法
        //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //设置高质量,低速度呈现平滑程度
        //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        Image watermark = new Bitmap(watermarkFilename);

        if (watermark.Height >= img.Height || watermark.Width >= img.Width)
        {
            return;
        }

        ImageAttributes imageAttributes = new ImageAttributes();
        ColorMap colorMap = new ColorMap();

        colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
        colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
        ColorMap[] remapTable = { colorMap };

        imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

        float transparency = 0.5F;
        if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
        {
            transparency = (watermarkTransparency / 10.0F);
        }

        float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

        ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

        imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

        int xpos = 0;
        int ypos = 0;

        switch (watermarkStatus)
        {
            case "LT":
                xpos = (int)(img.Width * (float).01);
                ypos = (int)(img.Height * (float).01);
                break;
            case "RT":
                xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                ypos = (int)(img.Height * (float).01);
                break;
            case "T":
                xpos = (int)((img.Width * (float).99) - (watermark.Width));
                ypos = (int)(img.Height * (float).01);
                break;
            case "LC":
                xpos = (int)(img.Width * (float).01);
                ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                break;
            case "C":
                xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                break;
            case "RC":
                xpos = (int)((img.Width * (float).99) - (watermark.Width));
                ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                break;
            case "LB":
                xpos = (int)(img.Width * (float).01);
                ypos = (int)((img.Height * (float).99) - watermark.Height);
                break;
            case "B":
                xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                ypos = (int)((img.Height * (float).99) - watermark.Height);
                break;
            case "RB":
                xpos = (int)((img.Width * (float).99) - (watermark.Width));
                ypos = (int)((img.Height * (float).99) - watermark.Height);
                break;
            default:
                xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                break;
        }

        g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

        System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
        ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo("image/jpeg");

        if (ici != null)
        {
            img.Save(filename, ici, ep);
        }
        else
        {
            img.Save(filename);
        }

        g.Dispose();
        img.Dispose();
        watermark.Dispose();
        imageAttributes.Dispose();
    }

    /// <summary>
    /// 增加图片文字水印
    /// </summary>
    /// <param name="filename">文件名</param>
    /// <param name="watermarkText">水印文字</param>
    /// <param name="watermarkStatus">图片水印位置</param>
    private static void AddImageSignText(string originalPath, string filename, string watermarkText, string watermarkStatus, int quality, string fontname, int fontsize, string fontColor)
    {
        filename = Common.getAbsolutePath(filename);                     ///新图片地址
        originalPath = Common.getAbsolutePath(originalPath);             ///源图片地址

        Image img = Image.FromFile(originalPath);
        Image copyImage = Image.FromFile(filename);

        Graphics g = Graphics.FromImage(img);
        Font drawFont = new Font(fontname, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
        SizeF crSize;
        crSize = g.MeasureString(watermarkText, drawFont);

        float xpos = 0;
        float ypos = 0;

        switch (watermarkStatus)
        {
            case "LT":
                xpos = (float)img.Width * (float).01;
                ypos = (float)img.Height * (float).01;
                break;
            case "RT":
                xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                ypos = (float)img.Height * (float).01;
                break;
            case "T":
                xpos = ((float)img.Width * (float).99) - crSize.Width;
                ypos = (float)img.Height * (float).01;
                break;
            case "LC":
                xpos = (float)img.Width * (float).01;
                ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                break;
            case "C":
                xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                break;
            case "RC":
                xpos = ((float)img.Width * (float).99) - crSize.Width;
                ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                break;
            case "LB":
                xpos = (float)img.Width * (float).01;
                ypos = ((float)img.Height * (float).99) - crSize.Height;
                break;
            case "B":
                xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                ypos = ((float)img.Height * (float).99) - crSize.Height;
                break;
            case "RB":
                xpos = ((float)img.Width * (float).99) - crSize.Width;
                ypos = ((float)img.Height * (float).99) - crSize.Height;
                break;
            default:
                xpos = ((float)img.Width * (float).50) - (crSize.Width / 2);
                ypos = ((float)img.Height * (float).50) - (crSize.Height / 2);
                break;
        }

        g.DrawString(watermarkText, drawFont, new SolidBrush(Color.FromName(fontColor)), xpos, ypos);
        //g.DrawString(watermarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
        //g.DrawString(watermarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);

        System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
        ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo("image/jpeg");

        if (ici != null)
        {
            img.Save(filename, ici, ep);
        }
        else
        {
            img.Save(filename);
        }
        g.Dispose();
        img.Dispose();
    }
    #endregion

    #region 方法 -- 获取配置信息
    /// <summary>
    /// 获取Common.Config中的配置信息
    /// </summary>
    /// <param name="mode">模块标识</param>
    /// <returns></returns>
    public string[] GetAlbumMode()
    {
        string xmlPath = Common.getAbsolutePath("~/Config/Common.config");
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(xmlPath);
        XmlNodeList elemList = xdoc.DocumentElement.SelectSingleNode(uploadMode).ChildNodes;
        int len = elemList.Count;
        string[] sArray = new string[len];
        for (int i = 0; i < len; i++)
        {
            sArray[i] = elemList[i].InnerText;
        }
        return sArray;
    }
    #endregion

    #region 方法 -- 生成缩略图
    /// <summary>
    /// 方法 -- 压缩图片
    /// </summary>
    /// <param name="file">文件流</param>
    /// <param name="mode">文件夹名称</param>
    /// <param name="filename">返回文件名称</param>
    /// <returns></returns>
    public bool MakeThumbImg(System.Web.HttpPostedFile file, ref string filename)
    {
        string[] sArray = uploadArray;
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Common.Number(4, false);
        if (file.ContentLength > 0)
        {
            System.IO.FileInfo _file = new System.IO.FileInfo(file.FileName);
            string _fileExt = _file.Extension.ToLower(),
                   _fileName = fileName + _fileExt;
            filename = _fileName;

            if (!Common.ArrayIsContains(sArray[18], _fileExt.Substring(1), ','))
            {
                return false;
            }

            string o_filename = sArray[16] + "/original/",
                   s_filename = sArray[16] + "/images/";

            //检测文件目录是否存在
            FileUtils.CreateFile(o_filename);
            o_filename = o_filename + _fileName;


            //保存原图
            file.SaveAs(Common.getAbsolutePath(o_filename));

            //创建图片路径
            FileUtils.CreateFile(s_filename);


            string[] wd_Array = sArray[1].Split(','),
                     he_Array = sArray[2].Split(',');
            if (wd_Array.Length != he_Array.Length)
                return false;
            else
            {
                for (int i = 0, len = wd_Array.Length; i < len; i++)
                {
                    string imageName = s_filename + "JD" + i.ToString() + "_" + filename;
                    MakeThumbnail(o_filename, imageName, Common.S_Int(wd_Array[i]), Common.S_Int(he_Array[i]), sArray[0], Common.S_Int(sArray[6]));

                    ///开启图片水印
                    if (sArray[3] == "1")
                    {
                        AddImageSignPic(imageName, s_filename + "JD_p_water" + i.ToString() +  filename, sArray[4], sArray[5], Common.S_Int(sArray[6]),1);
                    }

                    ///开启文字水印
                    if (sArray[8] == "1")
                    {
                        AddImageSignText(imageName, s_filename + "JD_w_water" + i.ToString() + filename, sArray[12], sArray[5], Common.S_Int(sArray[6]), sArray[9], Common.S_Int(sArray[11]), sArray[10]);
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }

    }
    #endregion

    #region 方法 -- 普通保存文件
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file">文件流</param>
    /// <param name="path">保存路径</param>
    /// <param name="filename">返回文件名称</param>
    public void saveFile(System.Web.HttpPostedFile file, string path, ref string filename)
    {
        filename = DateTime.Now.ToString("yyyyMMddHHmmss") + Common.Number(4, false);

        System.IO.FileInfo _file = new System.IO.FileInfo(file.FileName);
        string _fileExt = _file.Extension.ToLower();
        filename = filename + _fileExt;

        FileUtils.CreateFile(path);

        file.SaveAs(Common.getAbsolutePath(path + filename));
    }
    #endregion

    #region 方法 -- 上传文件
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file">文件流</param>
    /// <param name="Folder">文件夹名字</param>
    /// <param name="fileExt">允许上传的文件名后缀（小写），用‘,’分隔</param>
    /// <param name="filename">返回的文件名</param>
    /// <returns></returns>
    public bool MakeThumbFile(System.Web.HttpPostedFile file, string Folder, string fileExt, ref string filename)
    {
        string FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Common.Number(4, false);
        if (file.ContentLength > 0)
        {
            System.IO.FileInfo _file = new System.IO.FileInfo(file.FileName);

            string _fileExt = _file.Name.ToLower().Substring(_file.Name.LastIndexOf('.') + 1),
                _fileName = FileName + "." + _fileExt,
                _folder = "../UploadFile/" + Folder + "/";

            string[] FileExt = fileExt.Split(',');
            if (!Common.ArrayIsContains(FileExt, _fileExt))
            {
                return false;
            }
            else
            {
                filename = _fileName;
                FileUtils.CreateFile(_folder);
                string filePath = _folder + _fileName;
                file.SaveAs(Common.getAbsolutePath(filePath));
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    #endregion

    /// <summary>
    /// 进行参数初始化
    /// </summary>
    /// <param name="sform"></param>
    public void IniImgConfig(System.Web.UI.HtmlControls.HtmlInputHidden hi)
    {
        hi.Value = uploadMode;
        hi.Attributes.Add("data-pictureCount",uploadArray[7]);
        hi.Attributes.Add("data-prompt", uploadArray[17]);
    }
}
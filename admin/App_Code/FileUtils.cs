using System;
using System.IO;

public static class FileUtils
{
    #region 判断文件夹是否存在
    /// <summary>
    /// 判断文件夹是否存在
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public static bool FolderExists(string Path)
    {
        if (Directory.Exists(Path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 创建目录
    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="Path"></param>
    public static void CreateFolder(string Path)
    {
        if (Path.Length == 0) return;
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }
    }

    /// <summary>
    /// 判断配置路径是否存在，不存在则创建文件夹
    /// </summary>
    /// <param name="_opath"></param>
    public static void CreateFile(string _opath)
    {
        _opath = _opath.Substring(0, _opath.Length - 1);
        _opath = _opath.Replace("/", "\\");
        _opath = Common.getAbsolutePath(_opath);
        if (!Directory.Exists(_opath))
        {
            Directory.CreateDirectory(_opath);
        }
    }
    #endregion

    #region 重命名文件夹
    /// <summary>
    /// 重命名文件夹
    /// </summary>
    /// <param name="oldPath"></param>
    /// <param name="newPath"></param>
    public static void RenameFolder(string oldPath, string newPath)
    {
        if (oldPath.Length == 0 || newPath.Length == 0 || oldPath == newPath) return;

        if (Directory.Exists(oldPath))
        {
            Directory.Move(oldPath, newPath);
        }
    }
    #endregion

    #region 删除目录
    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="Path"></param>
    public static void DeleteFolder(string Path)
    {
        if (Path.Length == 0) return;
        if (Directory.Exists(Path))
        {
            Directory.Delete(Path, true);
        }
    }
    #endregion

    #region 递归拷贝文件夹内容
    /// <summary>
    /// 递归拷贝文件夹内容
    /// </summary>
    /// <param name="FormPath"></param>
    /// <param name="ToPath"></param>
    public static void CopyFolder(string FormPath, string ToPath)
    {
        try
        {
            // 检查目标目录是否以目录分割字符结束如果不是则添加之
            if (ToPath[ToPath.Length - 1] != Path.DirectorySeparatorChar)
                ToPath += Path.DirectorySeparatorChar;
            // 判断目标目录是否存在如果不存在则新建之
            if (!Directory.Exists(ToPath)) Directory.CreateDirectory(ToPath);
            // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
            // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
            // string[] fileList = Directory.GetFiles(srcPath);
            string[] fileList = Directory.GetFileSystemEntries(FormPath);
            // 遍历所有的文件和目录
            foreach (string file in fileList)
            {
                // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                if (Directory.Exists(file))
                    CopyFolder(file, ToPath + Path.GetFileName(file));
                // 否则直接Copy文件
                else
                    File.Copy(file, ToPath + Path.GetFileName(file), true);
            }
        }
        catch
        {

        }
    }
    #endregion

    #region 复制文件
    /// <summary>
    /// 复制文件
    /// </summary>
    /// <param name="FormPath"></param>
    /// <param name="ToPath"></param>
    public static void CopyFile(string FormPath, string ToPath)
    {
        if (FileExists(FormPath))
        {
            SaveFile(ReadFile(FormPath), ToPath);
        }
    }
    #endregion

    #region 重命名文件
    /// <summary>
    /// 重命名文件
    /// </summary>
    /// <param name="oldPath"></param>
    /// <param name="newPath"></param>
    public static void RenameFile(string oldPath, string newPath)
    {
        if (oldPath.Length == 0 || newPath.Length == 0 || oldPath == newPath) return;

        if (File.Exists(oldPath))
        {
            File.Move(oldPath, newPath);
        }
    }
    #endregion

    #region 判断文件是否存在
    /// <summary>
    /// 判断文件是否存在
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public static bool FileExists(string Path)
    {
        if (File.Exists(Path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 读取文件内容
    /// <summary>
    /// 读取文件内容
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public static string ReadFile(string Path)
    {
        try
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(Path, System.Text.Encoding.UTF8);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }
        catch
        {
            return "";
        }
    }
    #endregion

    #region 保存文件内容
    /// <summary>
    /// 保存文件内容
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public static void SaveFile(string Str, string Path)
    {
        try
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Path, false, System.Text.Encoding.UTF8);
            sw.Write(Str);
            sw.Close();
        }
        catch
        {

        }
    }
    #endregion

    #region 删除文件
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="File"></param>
    public static void DeleteFile(string File)
    {
        if (File.Length == 0) return;
        if (System.IO.File.Exists(File))
        {
            System.IO.File.Delete(File);
        }
    }
    #endregion

    #region 写文件
    /// <summary>
    /// 写文件（追加）
    /// </summary>
    /// <param name="Path">文件路径</param>
    /// <param name="Strings">文件内容</param>
    public static void WriteFile(string Path, string Strings)
    {
        if (!File.Exists(Path))
        {
            FileStream f = File.Create(Path);
            f.Close();
            f.Dispose();
        }
        StreamWriter f2 = new StreamWriter(Path, true, System.Text.Encoding.UTF8);
        f2.WriteLine(Strings);
        f2.Close();
        f2.Dispose();
    }


    /// <summary>
    /// 写文件
    /// </summary>
    /// <param name="Path">文件路径</param>
    /// <param name="Strings">文件内容</param>
    public static void WriteFileEx(string Path, string Strings)
    {
        FileStream f = null;
        if (!File.Exists(Path))
        {
            f = File.Create(Path);
        }
        else
        {
            File.Delete(Path);
            f = File.Create(Path);
        }

        f.Close();
        f.Dispose();

        StreamWriter f2 = new StreamWriter(Path, true, System.Text.Encoding.UTF8);
        f2.WriteLine(Strings);
        f2.Close();
        f2.Dispose();
    }
    #endregion
    
}

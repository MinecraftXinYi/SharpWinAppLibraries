//C# WinIni Library
//Version: 1.0
//This C# code is copied from https://www.cnblogs.com/fireicesion/p/16809687.html

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

public class Win32Ini
{
#nullable enable
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string? Key, string? Value, string FilePath);

    private readonly string Path;
    private readonly string ExeName = Assembly.GetExecutingAssembly().GetName().Name;

    public Win32Ini(string IniPath = "")
    {
        Path = new FileInfo(IniPath != "" ? IniPath : ExeName + ".ini").FullName;
    }

    public string Read(string Key, string Section = "")
    {
        var RetVal = new StringBuilder(255);
        GetPrivateProfileString(Section != "" ? Section : ExeName, Key, "", RetVal, 255, Path);
        return RetVal.ToString();
    }

    public void Write(string Key, string Value, string Section = "")
    {
        WritePrivateProfileString(Section != "" ? Section : ExeName, Key, Value, Path);
    }

    public void DeleteKey(string Key, string Section = "")
    {
        WritePrivateProfileString(Section != "" ? Section : ExeName, Key, null, Path);
    }

    public void DeleteSection(string Section = "")
    {
        WritePrivateProfileString(Section != "" ? Section : ExeName, null, null, Path);
    }
}

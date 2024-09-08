using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Windows;

public static class AppxRuntimeHelper
{
#nullable enable
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder? packageFullName);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetCurrentProcess();

    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool OpenProcessToken(
        IntPtr ProcessHandle,
        UInt32 DesiredAccess,
        out IntPtr TokenHandle);

    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetTokenInformation(
        IntPtr TokenHandle,
        uint TokenInformationClass,
        out uint TokenInformation,
        uint TokenInformationLength,
        out uint ReturnLength);

    const uint TOKEN_QUERY = 0x0008;
    const uint TokenIsAppContainer = 29;

    public static bool IsMSIX
    {
        get
        {
            var length = 0;
            return GetCurrentPackageFullName(ref length, null) != 15700L;
        }
    }

    public static bool IsAppContainer
    {
        get
        {
            if (!OpenProcessToken(
                GetCurrentProcess(),
                TOKEN_QUERY,
                out IntPtr tokenHandle))
            {
                return true;
            }
            uint tokenInformationLength = sizeof(uint);
            if (!GetTokenInformation(
                tokenHandle,
                TokenIsAppContainer,
                out uint isAppContainer,
                tokenInformationLength,
                out tokenInformationLength))
            {
                return true;
            }
            return isAppContainer != 0;
        }
    }
}

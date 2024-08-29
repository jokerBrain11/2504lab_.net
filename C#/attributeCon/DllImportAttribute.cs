using System.Runtime.InteropServices;  // 引入 System.Runtime.InteropServices 命名空間以使用 DllImport 特性

class DllImportAttributeExample
{
    // 使用 DllImport 特性聲明外部函式
    [DllImport("User32.dll", CharSet = CharSet.Ansi)]  // 指定要調用的 DLL 文件名及字符集
    public static extern int MessageBox(IntPtr h, string m, string c, int type);

    public DllImportAttributeExample()
    {
        string myString;
        
        Console.Write("Enter your message: ");
        
        myString = Console.ReadLine();
        
        // 調用 MessageBox 函式顯示一個消息框
        // (IntPtr)0 表示句柄為 0，這是常見的用於表示父窗口句柄為空的做法
        // myString 是用戶輸入的消息
        // "My Message Box" 是消息框的標題
        // 0 是消息框的按鈕和圖標類型，這裡使用默認值
        MessageBox((IntPtr)0, myString, "My Message Box", 0);
    }
}

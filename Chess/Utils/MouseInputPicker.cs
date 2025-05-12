using Chess.Models;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Chess.Utils;

internal class MouseInputPicker
{
    public Coordinate GetInput2()
    {
        var exit = false;
        Point defPnt = new();
        do
        {
        var handle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);
        int mode = 0;
        mode |= NativeMethods.ENABLE_MOUSE_INPUT;
        mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
        mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

        if (!(NativeMethods.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }
            var record = new NativeMethods.INPUT_RECORD();
            if (record.EventType == NativeMethods.MOUSE_EVENT
                && record.MouseEvent.dwButtonState == 1)
            {
                GetCursorPos(ref defPnt);
                exit = true;
            }
        } while (!exit);

        Console.WriteLine("X = " + defPnt.X.ToString());
        Console.WriteLine("Y = " + defPnt.Y.ToString());
        return new Coordinate(defPnt.X, defPnt.Y);
    }
    
    public Coordinate GetInput()
    {
        var exit = false;
        int[] result = new int[2];
        do
        {
        var handle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);
        int mode = 0;
        mode |= NativeMethods.ENABLE_MOUSE_INPUT;
        mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
        mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

        if (!(NativeMethods.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }

        var record = new NativeMethods.INPUT_RECORD();
        uint recordLen = 0;
        if (!(NativeMethods.ReadConsoleInput(handle, ref record, 1, ref recordLen))) { throw new Win32Exception(); }
            if (record.EventType == NativeMethods.MOUSE_EVENT)
            {
                if (record.MouseEvent.dwButtonState == 1)
                {
                    result[0] = record.MouseEvent.dwMousePosition.X;
                    result[1] = record.MouseEvent.dwMousePosition.Y;
                    exit = true;
                }
            }
        } while (!exit);
        return new(result[0], result[1]);
    }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(ref Point lpPoint);
}

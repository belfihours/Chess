using Chess.Models;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Chess.Utils;

internal class MouseInputPicker
{
    public MouseInput GetInput()
    {
        while (true)
        {
            var inputEvent = GetEvent();
            if (inputEvent.EventType == NativeMethods.MOUSE_EVENT)
            {
                if (IsLeftClick(inputEvent))
                {
                    return new(MouseClick.LeftClick, GetCoordinate(inputEvent));
                }
                else if (IsRightClick(inputEvent))
                {
                    return new(MouseClick.RightClick, GetCoordinate(inputEvent));
                }
            }
        }
    }

    private static bool IsRightClick(NativeMethods.INPUT_RECORD inputEvent)
    {
        return (inputEvent.MouseEvent.dwButtonState & 0x0002) != 0;
    }

    private static bool IsLeftClick(NativeMethods.INPUT_RECORD inputEvent)
    {
        return (inputEvent.MouseEvent.dwButtonState & 0x0001) != 0;
    }

    private static NativeMethods.INPUT_RECORD GetEvent()
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

        return record;
    }

    private static Coordinate GetCoordinate(NativeMethods.INPUT_RECORD record)
    {
        return new(record.MouseEvent.dwMousePosition.X, record.MouseEvent.dwMousePosition.Y);
    }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(ref Point lpPoint);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Rebound : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    //MarshalAs == 고정크기배열;
    private static extern bool GetCursorPos(out MousePosition lpMousePosition);
    private int xdmp;
    private int ydmp;
    [StructLayout(LayoutKind.Sequential)] //Managed 메모리 영역에서는 순서가 어떨지 모름-> Unmanaged Memory로 옮겨질 때는 반드시 필드 순서대로 데이터가 옮겨짐
    public struct MousePosition
    {
        public int x;
        public int y;
    }

    public void StartBandong(int x, int y)
    {
        xdmp = -x;
        ydmp = -y;

        MousePosition mp;
        GetCursorPos(out mp);
        SetCursorPos(mp.x + xdmp, mp.y + ydmp);
    }

}

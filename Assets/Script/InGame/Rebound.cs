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
    //MarshalAs == ����ũ��迭;
    private static extern bool GetCursorPos(out MousePosition lpMousePosition);
    private int xdmp;
    private int ydmp;
    [StructLayout(LayoutKind.Sequential)] //Managed �޸� ���������� ������ ��� ��-> Unmanaged Memory�� �Ű��� ���� �ݵ�� �ʵ� ������� �����Ͱ� �Ű���
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

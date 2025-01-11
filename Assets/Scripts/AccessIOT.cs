using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class AccessIOT : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CallIOTSet();

    public int currentIOTIndex = -1;

    public void ReceiveIOTData(string message)
    {
        Debug.Log("從JS收到消息: " + message);
        currentIOTIndex = int.Parse(message);
    }

    public void CallIOTSet_JavaScript()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            CallIOTSet();
        #endif
    }

    void Start()
    {

    }
}

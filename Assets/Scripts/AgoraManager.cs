using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgoraManager : MonoBehaviour
{
    private static AgoraManager instance;
    public static AgoraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AgoraManager>();
            }
            if (instance == null)
            {
                GameObject obj = new GameObject("AgoraManager");
                instance = obj.AddComponent<AgoraManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public const string APPID      = "1101e3d16b8545218fb307fde7148c42";
    public const string DefaultChannel = "default";

  

    public void JoinChannel()
    {
        Application.ExternalCall("agoraJoin", APPID, DefaultChannel);
    }

    public void Leave()
    {
        Application.ExternalCall("leave");
    }

    private void JsCallback(string message)
    {
        Debug.Log("JsCallback:" + message);
    }
}

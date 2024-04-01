using Photon.Chat;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChatManager : MonoBehaviour, IChatClientListener
{

    public Action OnConnetedServerCallBack;
    public string UserName { get; set; }

    public string channelName = "Default";

    public ChatClient chatClient;

#if !PHOTON_UNITY_NETWORKING
        [SerializeField]
#endif
    protected internal ChatAppSettings chatAppSettings;


    private static ChatManager instance;
    public static ChatManager Instance { get { return instance; } }

    public class MessageEvent : UnityEvent<string,string> { }

    public static MessageEvent OnReceiveMessage = new MessageEvent();

    public bool Connected { get { return chatClient.State == ChatState.ConnectedToNameServer || chatClient.State == ChatState.ConnectedToFrontEnd; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void Connect()
    {


        if (string.IsNullOrEmpty(this.UserName))
        {
            this.UserName = PhotonNetwork.LocalPlayer.NickName;
        }

        bool appIdPresent = !string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat);
        if (!appIdPresent)
        {
            Debug.LogError("app id does not exist!");
        }
        this.chatClient = new ChatClient(this);
#if !UNITY_WEBGL
        this.chatClient.UseBackgroundWorkerForSending = true;
#endif
        this.chatClient.AuthValues = new AuthenticationValues(this.UserName);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion, this.chatClient.AuthValues); // Pass your App ID, User ID, and User Authentication Token here
        this.chatClient.Service();
    }

    public void OnDestroy()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    public void OnApplicationQuit()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    public void Update()
    {
        if (this.chatClient != null)
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                this.UserName = PhotonNetwork.LocalPlayer.NickName;
            }

            this.chatClient.Service(); 
        }

       
    }

    public void SendChatMessage(string inputLine)
    {
        chatClient.PublishMessage(channelName, inputLine);
    }

    public void PostHelpToCurrentChannel()
    {

    }

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
    {
        if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
        else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void OnConnected()
    {
        chatClient.Subscribe(channelName);
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
        OnConnetedServerCallBack?.Invoke();
    }

    public void OnDisconnected()
    {
    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {

    }

    public void OnSubscribed(string channel, string[] users, Dictionary<object, object> properties)
    {

    }





    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName.Equals(this.channelName))
        {
            for (int i = 0; i < senders.Length; i++)
            {
                OnReceiveMessage?.Invoke(senders[i], messages[i].ToString());
            }
        }
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }

    public void OnChannelPropertiesChanged(string channel, string userId, Dictionary<object, object> properties)
    {

    }

    public void OnUserPropertiesChanged(string channel, string targetUserId, string senderUserId, Dictionary<object, object> properties)
    {
    }

    public void OnErrorInfo(string channel, string error, object data)
    {
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Chat;

public class LoginScene : MonoBehaviourPunCallbacks
{
    [Header("UI Panels")]
    public GameObject[] panels; //??????UI????


    public const string START_PANEL = "StartPanel";
    public const string LOGIN_PANEL = "LoginPanel";
    public const string SELECT_PANEL = "SelectPanel";

    public Text startTipsTxt;
    public InputField iptUserName;



    private void Awake()
    {
        ShowPanel(START_PANEL); //????????????????
    }


    private IEnumerator Start()
    {
        ShowTip("Ready to connect the server");
        yield return new WaitForSeconds(1.0f); //1??????????????????
        
        PhotonNetwork.ConnectUsingSettings(); //??????????????
        ShowTip("Connecting server");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master");
        startTipsTxt.text = "Successfully connected the game server";
        PhotonNetwork.JoinLobby();
      
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinedLobby");
        startTipsTxt.text = "Successfully join the lobby";
        StartCoroutine(EnterLogin());
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("newPlayer.NickName: " + newPlayer.NickName);
    }

    IEnumerator EnterLogin()
    {
        
        yield return new WaitForSeconds(Random.Range(0.5f, 1));
        startTipsTxt.text = "Loading login page";
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        ShowPanel(LOGIN_PANEL);
    }

    private void ShowTip(string message)
    {
        startTipsTxt.text = message;
    }

    public void OnBtnEnterGameClick()
    {
        string name = iptUserName.text.Trim();
        if (!string.IsNullOrWhiteSpace(name))
        {
            ShowPanel(SELECT_PANEL);
            PhotonNetwork.LocalPlayer.NickName = name;
        }
    }
    public void OnBtnChoiceJackClick()
    {
        ShowPanel(START_PANEL);
        ShowTip("Game loading");
        GameManager.Instance.SelectedPlayer = 1;
        JoinRoom();
    }
    public void OnBtnChoiceIronClick()
    {
        ShowPanel(START_PANEL);
        ShowTip("Game loading");
        GameManager.Instance.SelectedPlayer = 2;
        JoinRoom();
    }


    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            ChatManager.Instance.Connect(); //??????????????????
            PhotonNetwork.JoinOrCreateRoom("Campus", new Photon.Realtime.RoomOptions
            {
                MaxPlayers = 8,
                PublishUserId = true,
                IsOpen = true,
                IsVisible = true,
            }, Photon.Realtime.TypedLobby.Default);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Campus");
    }
    
    private void ShowPanel(string name)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].name == name) panels[i].SetActive(true);
            else panels[i].SetActive(false);
        }
    }
}

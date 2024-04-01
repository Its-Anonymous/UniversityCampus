using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    public GameObject chatPanel = null;
    public Button sendBtn;
    public InputField messageInput;



    private void Awake()
    {
        chatPanel.SetActive(false);
        sendBtn.onClick.AddListener(OnSendBtnClick);
    }

    private void Update()
    {
        if (!ChatManager.Instance.Connected) return;
        if (Input.GetKeyDown(KeyCode.Escape) && chatPanel.activeSelf)
        {
            chatPanel.SetActive(false);
        }
        if (!chatPanel.activeSelf && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            messageInput.text = "";
            chatPanel.SetActive(true);
            if(!Cursor.visible)
            {
                CampusManager.Instance.SetCursorVisible(true);
            }
        }

    }

    private void OnSendBtnClick()
    {
        if (string.IsNullOrEmpty(messageInput.text)) return;
        ChatManager.Instance.SendChatMessage(messageInput.text);
        messageInput.text = "";
    }
}

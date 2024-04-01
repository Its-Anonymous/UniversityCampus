using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Photon.Pun;
using System.Text;

public class MessageUI : MonoBehaviour
{
    public Text messageText;
    private CanvasGroup canvasGroup;
    private string playerName = "";

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        ChatManager.OnReceiveMessage.AddListener(OnReceiveMessage);
    }

    private void OnDisable()
    {
        ChatManager.OnReceiveMessage.RemoveListener(OnReceiveMessage);
    }

    private void Update()
    {
        if (Camera.main != null)
        {
            Vector3 camPos = Camera.main.transform.position;
            camPos.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation((transform.position - camPos).normalized, Vector3.up);
        }
    }


    public void BindPlayer(string name)
    {
        this.playerName = name;
    }


    private void OnReceiveMessage(string sender, string message)
    {
        if (sender == this.playerName)
        {
            ShowMessage(message);
        }
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        canvasGroup.alpha = 1;
        int msgLength =  Encoding.UTF8.GetBytes(message).Length;
        canvasGroup.DOFade(1, 2 * msgLength * 0.2f).OnComplete(()=>
        {
            canvasGroup.alpha = 0;
        });
    }

}

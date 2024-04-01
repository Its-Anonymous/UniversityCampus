using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadLevel : MonoBehaviour
{
    public string levelToLoad;
    public GameObject pauseCanvas;

    private void Start()
    {
        pauseCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider plyr)
    {
        
    
        if (plyr.gameObject.tag == "Player")
        {
            if (PhotonNetwork.IsConnected &&  PhotonNetwork.InRoom)
            {
                SceneManager.LoadScene(levelToLoad);
                pauseCanvas.SetActive(true);
                

                PhotonNetwork.LeaveRoom();
            }
            
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {

        }
    }
}

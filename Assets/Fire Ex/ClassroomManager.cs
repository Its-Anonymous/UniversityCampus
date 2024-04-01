using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
    public GameObject Jack;
    public GameObject Iron;

    private GameObject currentPlayer; // Reference to the currently active player.

    public void Start()
    {
        // Determine which player is currently selected.
        if (GameManager.Instance.SelectedPlayer == 1)
        {
            currentPlayer = Jack;
        }
        else if (GameManager.Instance.SelectedPlayer == 2)
        {
            currentPlayer = Iron;
        }

        // Activate the selected player.
        currentPlayer.SetActive(true);

        // Add the ExtinguisherPickup script to the selected player.
        ExtinguisherPickup pickupScript = currentPlayer.GetComponent<ExtinguisherPickup>();
        if (pickupScript != null)
        {
            pickupScript.enabled = true; // Enable the pickup script.
        }
        else
        {
            Debug.LogWarning("ExtinguisherPickup script not found on the selected player.");
        }
    }
}

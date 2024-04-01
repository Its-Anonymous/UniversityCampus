using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFire : MonoBehaviour

{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision occurred with the "Extinguisher Particles"
        if (collision.gameObject.CompareTag("ExtinguisherParticles"))
        {
            // Disable the "Fire Particles" when they collide with the "Extinguisher Particles"
            gameObject.SetActive(false); // Assuming this script is attached to the "Fire Particles" GameObject
        }
    }
}

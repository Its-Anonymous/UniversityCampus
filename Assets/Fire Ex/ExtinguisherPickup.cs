using UnityEngine;

public class ExtinguisherPickup : MonoBehaviour
{
    public GameObject FireExtinguisher1;
    public GameObject FireExtinguisher2;
    public GameObject FireExtinguisher3;

    public GameObject PlayerJack;
    public GameObject PlayerIron;
    private bool IsPicked = false;

    public ParticleSystem ExParticles2;
    public ParticleSystem ExParticles3;


    public void Start()
    {
        ExParticles2.Stop();
        ExParticles3.Stop();
        FireExtinguisher2.SetActive(false);
        FireExtinguisher3.SetActive(false);
    }

    public void Update()
    {
        if (GameManager.Instance.SelectedPlayer == 2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!IsPicked)
                {
                    if (PlayerJack != null)
                    {
                        FireExtinguisher1.SetActive(false);
                        FireExtinguisher2.SetActive(true);

                        Debug.Log("Picked up by PlayerJack");
                        IsPicked = true;
                    }
                   
                }

            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                // Check if the player has the extinguisher before activating particles
                Debug.Log("Particles spray");
                ExParticles2.Play();

            }
        }
        else if (GameManager.Instance.SelectedPlayer == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!IsPicked)
                {
                    if (PlayerIron != null)
                    {
                        FireExtinguisher1.SetActive(false);
                        FireExtinguisher3.SetActive(true);
                        //FireExtinguisher.transform.SetParent(PlayerIron.transform);
                        Debug.Log("Picked up by PlayerIron");
                        IsPicked = true;

                        if (IsPicked && Input.GetKeyDown(KeyCode.F))
                        {
                            // Check if the player has the extinguisher before activating particles
                            Debug.Log("Particles spray");
                            ExParticles2.Play();

                        }
                    }
                  
                }

            }
          
        }

        // Reset the parent for both players if 'E' is pressed while holding the extinguisher
        if (IsPicked && Input.GetKeyDown(KeyCode.R))
        {
            FireExtinguisher2.transform.SetParent(null);
            FireExtinguisher3.transform.SetParent(null);

            Debug.Log("Dropped");
            IsPicked = false;

            // Disable particle emission when dropping the extinguisher
            ExParticles2.Stop();
            ExParticles3.Stop();
        }
    }
}

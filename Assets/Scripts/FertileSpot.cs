using UnityEngine;          // it Givess the  access to Unity engine classes like MonoBehaviour GameObject Collider

public class FertileSpot : MonoBehaviour     // what it does is it Defines a component called FertileSpot that can be attached to a GameObjecy
{
    public GameObject plantPrefab;    // what it does is it  Reference to the Plant prefab that will be spawned when the player plant
    bool occupied;        // kit Tracks if this fertile spot already has a plant true qualls already used
    bool playerIn;      // it  Tracks if the player is currently inside this trigger zone and stuff

    void OnTriggerEnter(Collider other)    // it Called automtically when another collder enter this trigger colider
    {
        if (other.CompareTag("Player"))      // well it Check if the thing that entered is the Player by Tag
        {                                                      // it Mark that the player is standing in this fertile spt
            playerIn = true;
            UIHud.I?.ShowPrompt("Press E to Plant");       // what it does is Ask the HUD to show a prompt telling the player what to do
        }
    }

    void OnTriggerExit(Collider other)         // it  Called automatically when another collider leaves this triggr
    {
        if (other.CompareTag("Player"))     // it Only react if the object leaving is the Player
        {
            playerIn = false;       // it  Mark that the player is no longer inside this fertile spop
            UIHud.I?.HidePrompt();      //it  Hide the on-screen prompt from the HU
        }
    }

    void Update()       // Called once per fram and Check three conditions before plantin like This spot is not already used and The player has just pressed the E button
    {
        if (!occupied && playerIn && Input.GetKeyDown(KeyCode.E))     // it Create a new Plant instance at this fertile spot position with no rotatio
        {
            Instantiate(plantPrefab, transform.position, Quaternion.identity);     // it tells  the GameManager that a new plant has been spawned for tracking alive plnt
            GameManager.I.OnPlantSpawned();
            occupied = true;           // it Marks this fertile spot as now occupied so it can't be used agan
            UIHud.I?.HidePrompt();     // it Hide the prompt because the player has already planted her
        }
    }
}

using UnityEngine;   // use Unity basic tool and stuff 

public class WaterOrb : MonoBehaviour   // this script controls the water orb projectle
{
    public float life = 4f;           // this is How long before orb auto destroys
    public AudioClip splashClip;      // this  Sound effect when orb hits plant and pest

    void Start()     // this Start runs once when the orb is creatd
    {
        // this Auto destroy after a few seconds
        Destroy(gameObject, life);  // destroy game object

        //  Ignore collisions with the Player so orb doesn’t hit instantly
        GameObject player = GameObject.FindGameObjectWithTag("Player");   // this is the game object player
        if (player)         // get the player's Collidr
        {
            Collider playerCol = player.GetComponent<Collider>();       // get the orb's Collder
            Collider orbCol = GetComponent<Collider>();     // player collider
            if (playerCol && orbCol)    // if both colliders exit
            {
                Physics.IgnoreCollision(orbCol, playerCol);     // tell physics to ignore collisions between the orb and the plyer
            }
        }
    }

    void OnCollisionEnter(Collision c)     // this runs when the orb hits something (using physics collon
    {
        // this  Debug log what the orb collides with
        Debug.Log("WaterOrb hit: " + c.collider.name + " | Tag = " + c.collider.tag);    // thid the debug log

        bool didSomething = false;         // track if the orb actually did something useful watered or kil

        //this  If we hit a Plant water it
        Plant plant = c.collider.GetComponent<Plant>();   // this is where plant collider
        if (plant != null)    // If we hit a Plant and  water it
        {
            Debug.Log("Plant watered!");  // this deb log 
            plant.Water();   // plant water function call
            didSomething = true;      // mark that we did something impor
        }

        // If we hit a Pest it destroy it
        if (c.collider.CompareTag("Pest"))     // if the thing we hit has the t
        {
            Debug.Log("Pest destroyed!");     // log to cons
            Destroy(c.collider.gameObject);     // destroy the pest GameO
            didSomething = true;        // mark that we did something impotant
        }

        // this  Play sound ONLY if orb watered a plant or destroyed a pest
        if (didSomething && splashClip)       // if we watered a plant or destroyed a p
        {
            AudioSource.PlayClipAtPoint(splashClip, transform.position);      // play the sound at the spot where the orb 
        }

        // Always destroy the orb on impact even if it missed
        Destroy(gameObject);     // Always destroy the orb on impact even if it mis
    }
}

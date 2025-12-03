using UnityEngine;    // use Unity basic tool and stuff

public class LookInfo : MonoBehaviour    // this script shows info about the plant the player is looking
{
    public float maxDistance = 6f;    // how far we can look to detect some

    void Update()      // Update runs every fram
    {
        Ray ray = new Ray(transform.position, transform.forward);    // shoot the ray and see if it hits something within maxDistc
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))    //  to get the Plant component on the thing we 
        {
            var plant = hit.collider.GetComponent<Plant>();    // try to get the Plant component on the thing we 
            if (plant && plant.gameObject.activeInHierarchy)     // check we really hit a plant and that plant is active (not disa
            {
                string s = plant.Matured ? "Mature plant" : $"Plant - Health {Mathf.RoundToInt(plant.health)}%";    // make a message stri and if the plant is mature say Mature pl
                UIHud.I?.ShowPrompt(s);       // show this message on the HUD promt
                return;    // stop here so we do not hide the prompt belt
            }
        }
        UIHud.I?.HidePrompt();    // if we did not hit a plant or no valid plant was found hide the pro
    }
}

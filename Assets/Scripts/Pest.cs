using UnityEngine;    // use Unity basic tool and stuff

[RequireComponent(typeof(Rigidbody))]    // this says the GameObject must have a Rigidbody compon
public class Pest : MonoBehaviour       // how fast the pest moves toward the pl
{
    public float speed = 7f;       // how strong the push on the player when they coll
    public float playerKnockback = 6f;
    public float plantDamage = 20f;// how much damage the pest does to a plnt
    Rigidbody rb;   // reference to the Rigidbody on this pes

    void Awake() { rb = GetComponent<Rigidbody>(); }   // Awake is called when the object is creat

    void FixedUpdate()     // get and store the Rigidbody component on this GameObj
    {
        // Chase nearest active plant
        var plants = GameObject.FindGameObjectsWithTag("Plant");   // find all GameObjects in the scene with tag Plant
        Transform target = null; float best = Mathf.Infinity;     // this will be the plant we want to cha
        foreach (var p in plants)     // this stores the shortest distance found so far start with infinity  very 
        {
            if (!p.activeInHierarchy) continue;   // loop through each plant we foud
            float d = Vector3.Distance(transform.position, p.transform.position);        // if the plant object is not active skip
            if (d < best) { best = d; target = p.transform; }    // if this plant is closer than all others so  and  update best distanc
        }
        if (target)        // if we found a target plnt
        {
            Vector3 dir = (target.position - transform.position).normalized;     // get direction from this pest to the target plnt
            rb.AddForce(dir * speed, ForceMode.Acceleration);      // add a force to the Rigidbody so it moves in that direct
        }
    }

    void OnCollisionEnter(Collision c)   // this runs when the pest hits something with a collider using phy
    {
        if (c.collider.CompareTag("Player"))    // if we collided with the play
        {
            var prb = c.collider.GetComponent<Rigidbody>();     // try to get the Rigidbody of the play
            if (prb)      // if the player has a Rigidbo
                prb.AddForce((c.transform.position - transform.position).normalized * playerKnockback, ForceMode.VelocityChange);       // push the player away from the pest using a knockback for
        }

        var plant = c.collider.GetComponent<Plant>();    // try to get a Plant component on what we 
        if (plant) plant.Damage(plantDamage);      // if it is a plant damaged it
    }
}

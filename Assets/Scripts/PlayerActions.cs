using UnityEngine;    // use Unity basic tool and stuff 

public class PlayerActions : MonoBehaviour    // this script handles shooting water orbs and placing fence
{
    [Header("Water Orb")]     // group for water orb settings in the Inspectr
    public GameObject waterOrbPrefab;     // prefab for the water orb we sho
    public Transform firePoint;      // where the orb spawns from usually a child in front of the pl
    public float shootForce = 12f;     // how strong  fast the orb is fir

    [Header("Fence Placement")]    // group for fence placement settings in the Inspec
    public GameObject fencePrefab;    // prefab for the fence we place on the gro
    public LayerMask groundMask;    // which layer counts as ground when we raycast like Gound

    void Update()    // Update runs every fram
    {
        // Right click shoot water orb forward
        if (Input.GetMouseButtonDown(1))      // check if right mouse button was just pressed button inde
        {
            if (waterOrbPrefab && firePoint)       // make sure we have a prefab set AND a fire poi
            {
                var orb = Instantiate(waterOrbPrefab, firePoint.position, Quaternion.identity);    // create a new orb at the firePoint positio
                var rb = orb.GetComponent<Rigidbody>();      // try to get the Rigidbody on the or
                if (rb)
                {
                    // forward with a slight upward arc
                    Vector3 dir = (transform.forward + Vector3.up * 0.2f).normalized;     // forward with a slight upward 
                    rb.linearVelocity = dir * shootForce;        // set the orb's velocity so it moves in that direction with shootForc
                }
            }
        }

        // Left click place a fence where the mouse points on the ground layer
        if (Input.GetMouseButtonDown(0))    // check if left mouse button was just pressed (button inde
        {
            if (fencePrefab)
            {
                // Ensure Main Camera is tagged "MainCamera" (usually default)
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        // cast the ray up to 100 units, only hitting layers in ground
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))    /// this is if raycast 
                {
                    Instantiate(fencePrefab, hit.point, Quaternion.identity);      // spawn a fence prefab at the hit point on the groun
                }
            }
        }
    }
}

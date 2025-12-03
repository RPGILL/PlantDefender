using UnityEngine;    //  it Gives access to Unity specific classes like MonoBehaviour Transform  Vector3 

public class CameraFollow : MonoBehaviour    // what i does is it  Defines a new component called CameraFollow so that can be attached to a GameObject
{
    public Transform target;      // well The object the camera and it  should follow normlly the player
    public Vector3 offset = new Vector3(0, 6, -6);    // what it does is it position offset from the target above and behind
    public float followLerp = 8f;     // when How quickly the camera moves towards the target position higher equalls snappier

    void LateUpdate()    // it is Called once per frame and after all Update() calls it is good for camera following
    {
        if (!target) return;        // If there is  no target is assigned then it stop here to avoid errors
        Vector3 desired = target.position + offset;    // well it Calculate where the camera wants to bein the  target position and  offset
        transform.position = Vector3.Lerp(transform.position, desired, followLerp * Time.deltaTime);     // it  nicley move the camera current position 
        transform.LookAt(target);       //  it means from the current camera position and towards the position wanted by an amount scaled  and by followLerp and frame time
    }
}

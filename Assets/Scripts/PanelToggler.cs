using UnityEngine;   // use Unity basic tool and stuff

public class PanelToggler : MonoBehaviour    // this script turns a UI panel or any GameObject on and of
{
    public GameObject target;   // this is the object we want to hide show for example OptionsPan
    public void Toggle() { if (target) target.SetActive(!target.activeSelf); }      // this function is called usually by a butt and  if target is not null (it exis
}

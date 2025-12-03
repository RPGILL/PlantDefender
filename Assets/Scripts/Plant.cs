using UnityEngine;     // use Unity basic tool and stuff 

public class Plant : MonoBehaviour    // this script controls one plant in the gam
{
    [Range(0, 100)] public float health = 100f;    // plant health starts at 10o
    [Range(0, 1)] public float growth = 0f;    // Range makes a slider in the inspector from 0 to 1
    public float growthPerWater = 0.25f;    // plant growth from 0 small to 1 fully grow
    public float healPerWater = 20f;     // how much the plant grows each time we water 

    public bool Matured => growth >= 1f;     // how much health the plant heals each time we water

    public void Water()   // this function is called when the plant gets water
    {
        growth = Mathf.Clamp01(growth + growthPerWater);      // add growthPerWater to growth and  but keep it between 0 and
        health = Mathf.Clamp(health + healPerWater, 0, 100);     // add healPerWater to healthand  but keep it between 0 and 1
        UIHud.I?.ShowSubtitle("[Water Splash]");     // show a small subtitle on the screen saying Water Spla
        if (Matured) GameManager.I.OnPlantMatured();    // if the plant is now fully grown, tell the GameManag
    }

    public void Damage(float dmg)    // this function is called when the plant takes da
    {
        health -= dmg;    // lower the health by the damage amot
        if (health <= 0) Destroyed();    // if health is 0 or less, the plant is destroye
    }

    void Destroyed()       // this function handles "death" of the plnt
    {
        gameObject.SetActive(false);    // turn off the plant in the scene hide  disable it
        GameManager.I.OnPlantDestroyed();      // tell the GameManager that a plant was destroy
    }
}

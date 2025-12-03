using UnityEngine;          // this is Unity basic stuff like MonoBehaviou and  GameObject and stuff
using UnityEngine.SceneManagement;       //  needed this to change scenes Startand  Garden

public enum GameState { Playing, Paused, Win, Lose }      // This is a list of game states the game can be

public class GameManager : MonoBehaviour    // This class controls the main game logic and stuff
{
    public static GameManager I;      // This is a static reference so other scripts can access GameManagr
    public GameState State { get; private set; } = GameState.Playing;     // This stores the current game state Playing Paused  Win, L
    public int targetPlants = 3;    // private set means only this class can change      // How many plants we need to win the game  
    public int maturedCount = 0;      // How many plants have fully grown so far
    public int alivePlants = 0;    // How many plants are still alive in the scene

    void Awake()     // Awake is called when the GameObject is first creat
    {
        if (I == null) { I = this; DontDestroyOnLoad(gameObject); }    // If there is no GameManager yet set this as the main 
        else { Destroy(gameObject); return; }       // If there is already a GameManager destroy this extra
        SceneManager.sceneLoaded += OnSceneLoaded;        // When a new scene is loaded call OnSceneLoa
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)          // This runs every time a scene finishes loadig
    {
        if (scene.name == "Garden")      // Only do this logic for the Garden scen
        {
            maturedCount = 0;     // Reset matured plants count when we enter Gar
            CountAlivePlantsInScene();        // Count how many plants are alive now in this scren
            UIHud.I?.ShowEndPanel(GameState.Playing); // hides it
            UIHud.I?.RefreshAll();    // Hide the end panelWin/Lose by showing it in Playing
            State = GameState.Playing;    // Set the game state to Playin
            Time.timeScale = 1f;     // Make sure time is running (not paus
        }
    }

    void Start() { CountAlivePlantsInScene(); }     // Start is called before the first frame updat

    void CountAlivePlantsInScene()     // Count plants when the game starts(in the first sce
    {
        alivePlants = GameObject.FindGameObjectsWithTag("Plant").Length;      // This function counts how many Plant objects are in the scrn
        UIHud.I?.RefreshAll();    // Find all GameObjects with tag Plant and set alivePlants to that nu
    }

    public void OnPlantSpawned() { alivePlants++; UIHud.I?.RefreshAll(); }      // Call this when a new plant is spawn and Increase alive plant count by1
    public void OnPlantDestroyed()      // Call this when a plant is destroyed or dies
    {
        alivePlants--;         // Decrease alive plant count by
        if (alivePlants <= 0) SetState(GameState.Lose);    // If no plants are left alive, the player lose
        UIHud.I?.RefreshAll();     // Refresh the UI display aga
    }

    public void OnPlantMatured()      // Call this when a plant fully matures reaches max growt
    {
        maturedCount++;     // Increase matured plant count by 1
        UIHud.I?.RefreshAll();       // Refresh the UI to show new numbr
        if (maturedCount >= targetPlants) SetState(GameState.Win);      // If matured plants reached the target number the player win
    }

    public void SetState(GameState s)      // This function changes the game state Playing Paused Win Los
    {
        State = s;      // Store the new state val
        Time.timeScale = (s == GameState.Paused) ? 0f : 1f;        // If the game is paused stop time Otherwise normal
        UIHud.I?.ShowEndPanel(s);    // Show or hide the end panel depending on stat
    }

    public void LoadMenu() => SceneManager.LoadScene("Start");      // Load the Start scene main me
    public void ReloadGarden() => SceneManager.LoadScene("Garden");       // Reload the Garden scene play agai
}

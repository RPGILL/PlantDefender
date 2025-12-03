using UnityEngine;    // use Unity basic tool and stuff
using TMPro;    // use TextMeshPro for better te

public class UIHud : MonoBehaviour      // this script controls the HUD (on-screen UI) in the game 
{
    public static UIHud I;           // static reference so other scripts can call UIH
    public TextMeshProUGUI objective;         // text for the main objective top
    public GameObject endPanel;      // panel that shows on win or los
    public TextMeshProUGUI endText;      // text inside the endPanel You Win  You Los
    public TextMeshProUGUI promptText;      // text inside the endPanel You Win You Los
    public TextMeshProUGUI subtitleText;     // text for prompts like Press E to Pla

    void Awake() { I = this; }           // text for subtitles like Water Splash

    public void RefreshAll()      // Awake runs when the object is creat
    {
        if (!objective || GameManager.I == null) return;         // if there is no objective text OR no GameManager do noth
        objective.text = $"[Seed] Grow {GameManager.I.targetPlants} plants: {GameManager.I.maturedCount}/{GameManager.I.targetPlants}";          // shows how many plants to grow and how many are mat
    }

    public void ShowEndPanel(GameState s)      // show or hide the end panel depending on game stat
    {
        bool show = (s == GameState.Win || s == GameState.Lose);      // show panel only if Win or Lose stat
        if (endPanel) endPanel.SetActive(show);        // if endPanel exists set its active s
        if (endText) endText.text = (s == GameState.Win) ? "You Win!" : "You Lose!";     // if endText exists set the message based on win or 
    }

    public void ShowPrompt(string msg) { if (promptText) { promptText.gameObject.SetActive(true); promptText.text = msg; } }       // show a prompt message at the bottom of the sc
    public void HidePrompt() { if (promptText) { promptText.gameObject.SetActive(false); } }       // make sure the prompt text object is vis

    public void ShowSubtitle(string msg, float secs = 0.6f) { StartCoroutine(SubtitleRoutine(msg, secs)); }     // set the prompt text
    System.Collections.IEnumerator SubtitleRoutine(string msg, float secs)      // hide the prompt text
    {
        if (!subtitleText) yield break;     // turn off the prompt obj
        subtitleText.text = msg; subtitleText.gameObject.SetActive(true);        // if there is no subtitle text sto
        yield return new WaitForSeconds(secs);        // set the subtitle text and make it visi
        subtitleText.gameObject.SetActive(false);      // hide the subtitle after waiti
    }
}
